using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using System.Windows.Media;
using System.Windows;

namespace MapDisplay
{
	internal class Heightmap
	{
		public int Ncols { get; set; }
		public int Nrows { get; set; }
		public double Xllcorner { get; set; }
		public double Yllcorner { get; set; }
		public double Cellsize { get; set; }
		public double[][] Heights { get; set; }

		public Heightmap(int ncols, int nrows, double xllcorner, double yllcorner, double cellsize)
		{
			Ncols = ncols;
			Nrows = nrows;
			Xllcorner = xllcorner;
			Yllcorner = yllcorner;
			Cellsize = cellsize;
			Heights = new double[nrows][];
			for (int i = 0; i < nrows; i++)
			{
				Heights[i] = new double[ncols];
			}
		}
		public static Heightmap LoadFromFile(string filePath)
		{
			using (StreamReader reader = new StreamReader(filePath))
			{
				int ncols = int.Parse(reader.ReadLine().Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).Last());
				int nrows = int.Parse(reader.ReadLine().Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).Last());
				double xllcorner = double.Parse(reader.ReadLine().Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).Last(), CultureInfo.InvariantCulture);
				double yllcorner = double.Parse(reader.ReadLine().Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).Last(), CultureInfo.InvariantCulture);
				double cellsize = double.Parse(reader.ReadLine().Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).Last(), CultureInfo.InvariantCulture);

				Heightmap heightmap = new Heightmap(ncols, nrows, xllcorner, yllcorner, cellsize);

				for (int row = 0; row < nrows; row++)
				{
					string[] values = reader.ReadLine().Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
					for (int col = 0; col < ncols; col++)
					{
						heightmap.Heights[row][col] = double.Parse(values[col], CultureInfo.InvariantCulture);
					}
				}

				return heightmap;
			}
		}

		public WriteableBitmap GenerateBitmap()
		{
			double min = Heights.Min(row => row.Min());
			double max = Heights.Max(row => row.Max());

			WriteableBitmap bitmap = new WriteableBitmap(Ncols, Nrows, 96, 96, PixelFormats.Gray8, null);

			for (int row = 0; row < Nrows; row++)
			{
				for (int col = 0; col < Ncols; col++)
				{
					double normalizedHeight = (Heights[row][col] - min) / (max - min);
					byte grayValue = (byte)(normalizedHeight * 255);
					bitmap.SetPixel(col, row, grayValue);
				}
			}

			return bitmap;
		}
		public void SaveBitmapToFile(WriteableBitmap bitmap, string filePath)
		{
			using (FileStream stream = new FileStream(filePath, FileMode.Create))
			{
				PngBitmapEncoder encoder = new PngBitmapEncoder();
				encoder.Frames.Add(BitmapFrame.Create(bitmap));
				encoder.Save(stream);
			}
		}


	}
}
