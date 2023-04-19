using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;

namespace MapDisplay
{
	public static class WriteableBitmapExtensions
	{
		public static void SetPixel(this WriteableBitmap bitmap, int x, int y, byte value)
		{
			var rect = new Int32Rect(x, y, 1, 1);
			var pixelData = new byte[] { value };
			bitmap.WritePixels(rect, pixelData, 1, 0);
		}
	}

}
