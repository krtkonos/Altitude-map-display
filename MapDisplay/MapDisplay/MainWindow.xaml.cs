using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MapDisplay
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		private Heightmap _heightmap;
		private float crossSize = 5f;
		private float dotSize = 10f;
		private float zoom = 10f;
		private enum CircleState
		{
			Center,
			Edge
		}

		private Point? _circleCenter;
		private CircleState _circleState;
		private Ellipse _orangeDot;


		public MainWindow()
		{
			InitializeComponent();
			string heightmapFilePath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "heightmap_ASCII", "heightmap_ASCII");
			_heightmap = Heightmap.LoadFromFile(heightmapFilePath);

			WriteableBitmap heightmapBitmap = _heightmap.GenerateBitmap();
			_heightmap.SaveBitmapToFile(heightmapBitmap, "output.png");
			HeightmapImage.Source = heightmapBitmap;

			HeightmapCanvas.Width = heightmapBitmap.PixelWidth;
			HeightmapCanvas.Height = heightmapBitmap.PixelHeight;
		}
		private void HeightmapImage_MouseMove(object sender, MouseEventArgs e)
		{
			Point position = e.GetPosition(HeightmapImage);

			int x = (int)(position.X / HeightmapImage.ActualWidth * _heightmap.Ncols);
			int y = (int)(position.Y / HeightmapImage.ActualHeight * _heightmap.Nrows);

			if (x >= 0 && x < _heightmap.Ncols && y >= 0 && y < _heightmap.Nrows)
			{
				double height = _heightmap.Heights[y][x];
				CoordinateLabel.Content = $"X: {x}, Y: {y}, Height: {height}";
			}
		}

		private void ScrollViewer_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
		{
			ScrollViewer scrollViewer = (ScrollViewer)sender;
			double zoomFactor = e.Delta > 0 ? 1.1 : 1 / 1.1;
			double currentZoom = scrollViewer.LayoutTransform.Value.M11;

			if (currentZoom * zoomFactor < 0.1)
			{
				zoomFactor = 0.1 / currentZoom;
			}
			else if (currentZoom * zoomFactor > zoom)
			{
				zoomFactor = zoom / currentZoom;
			}

			scrollViewer.LayoutTransform = new ScaleTransform(currentZoom * zoomFactor, currentZoom * zoomFactor);
			e.Handled = true;
		}
		private void HeightmapImage_MouseDown(object sender, MouseButtonEventArgs e)
		{
			Point clickPosition = e.GetPosition(HeightmapImage);

			if (_circleState == CircleState.Center)
			{
				ClearCanvasElements(); // Clear old elements before drawing the new red cross

				_circleCenter = clickPosition;
				_circleState = CircleState.Edge;

				DrawCross(clickPosition, Brushes.Red);
				OutputTextBlock.Text = "";

				int x = (int)(clickPosition.X / HeightmapImage.ActualWidth * _heightmap.Ncols);
				int y = (int)((1 - clickPosition.Y / HeightmapImage.ActualHeight) * _heightmap.Nrows);

				RedCrossCoordinates.Text = $"Red cross Coordinates: X: {x}, Y: {y}";
			}
			else
			{
				double radius = Math.Sqrt(Math.Pow(clickPosition.X - _circleCenter.Value.X, 2) + Math.Pow(clickPosition.Y - _circleCenter.Value.Y, 2));
				_circleState = CircleState.Center;

				DrawCross(clickPosition, Brushes.Blue);
				DrawCircle(_circleCenter.Value, radius);
			}
		}


		private void DrawCross(Point position, SolidColorBrush color)
		{
			Line line1 = new Line { X1 = position.X - crossSize, Y1 = position.Y - crossSize, 
				X2 = position.X + crossSize, Y2 = position.Y + crossSize, Stroke = color, StrokeThickness = 1 };
			Line line2 = new Line { X1 = position.X - crossSize, Y1 = position.Y + crossSize, 
				X2 = position.X + crossSize, Y2 = position.Y - crossSize, Stroke = color, StrokeThickness = 1 };

			HeightmapCanvas.Children.Add(line1);
			HeightmapCanvas.Children.Add(line2);
		}

		private void DrawCircle(Point center, double radius)
		{
			Ellipse circle = new Ellipse { Width = radius * 2, Height = radius * 2, Stroke = Brushes.Green, StrokeThickness = 1 };

			Canvas.SetLeft(circle, center.X - radius);
			Canvas.SetTop(circle, center.Y - radius);

			HeightmapCanvas.Children.Add(circle);
			List<PointHeightData> pointsWithinCircle = GetPointsWithinCircle(center, radius);
			var sortedPoints = pointsWithinCircle.OrderByDescending(p => p.Height).ToList();
			var top10Highest = sortedPoints.Take(10).ToList();
			var top10Lowest = sortedPoints.Skip(sortedPoints.Count - 10).Reverse().ToList();

			Top10HighestDataGrid.ItemsSource = top10Highest;
			Top10LowestDataGrid.ItemsSource = top10Lowest;
		}
		private List<PointHeightData> GetPointsWithinCircle(Point center, double radius)
		{
			List<PointHeightData> pointsWithinCircle = new List<PointHeightData>();

			int centerX = (int)(center.X / HeightmapImage.ActualWidth * _heightmap.Ncols);
			int centerY = (int)(center.Y / HeightmapImage.ActualHeight * _heightmap.Nrows); // Change this line
			int maxRadius = (int)Math.Ceiling(radius / HeightmapImage.ActualWidth * _heightmap.Ncols);

			for (int i = -maxRadius; i <= maxRadius; i++)
			{
				for (int j = -maxRadius; j <= maxRadius; j++)
				{
					int x = centerX + i;
					int y = centerY + j;

					if (x >= 0 && x < _heightmap.Ncols && y >= 0 && y < _heightmap.Nrows)
					{
						double distance = Math.Sqrt(Math.Pow(x - centerX, 2) + Math.Pow(y - centerY, 2));
						if (distance <= maxRadius)
						{
							pointsWithinCircle.Add(new PointHeightData { Point = $"X: {x}, Y: {y}", Height = _heightmap.Heights[y][x] });
						}
					}
				}
			}

			return pointsWithinCircle;
		}

		private void ClearCanvasElements()
		{
			List<UIElement> elementsToRemove = new List<UIElement>();

			foreach (UIElement element in HeightmapCanvas.Children)
			{
				if (element is Line || element is Ellipse)
				{
					elementsToRemove.Add(element);
				}
			}

			foreach (UIElement element in elementsToRemove)
			{
				HeightmapCanvas.Children.Remove(element);
			}
		}
		private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			if (e.AddedItems.Count > 0)
			{
				PointHeightData selectedItem = e.AddedItems[0] as PointHeightData;
				OutputTextBlock.Text = "Selected point coordinates: " + selectedItem.Point;
				string[] coordinates = selectedItem.Point.Split(new char[] { ' ', ',' }, StringSplitOptions.RemoveEmptyEntries);
				int x = int.Parse(coordinates[1]);
				int y = int.Parse(coordinates[3]);

				Point canvasPoint = new Point(
					x * HeightmapImage.RenderSize.Width / _heightmap.Ncols,
					y * HeightmapImage.RenderSize.Height / _heightmap.Nrows); // Change this line

				DrawDot(canvasPoint, Brushes.Orange);
			}
		}

		private void DrawDot(Point position, SolidColorBrush color)
		{
			if (_orangeDot != null)
			{
				HeightmapCanvas.Children.Remove(_orangeDot);
			}

			_orangeDot = new Ellipse { Width = dotSize, Height = dotSize, Fill = color };

			Canvas.SetLeft(_orangeDot, position.X - _orangeDot.Width / 2);
			Canvas.SetTop(_orangeDot, position.Y - _orangeDot.Height / 2);

			HeightmapCanvas.Children.Add(_orangeDot);
		}
	}
}
