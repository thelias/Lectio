using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LectioTranscoder.Interfaces;

namespace LectioTranscoder
{
    public class Bitmap : IBitmap
    {
        public System.Drawing.Bitmap ToBitmap(byte[] arrBytes)
        {
            var ms = new System.IO.MemoryStream(arrBytes);
            var returnImage = System.Drawing.Image.FromStream(ms);
            var bitmap = new System.Drawing.Bitmap(returnImage);

            return bitmap;
        }

        public System.Drawing.Bitmap ReducedBitmap(System.Drawing.Bitmap original, int reducedWidth, int reducedHeight)
        {
            var reduced = new System.Drawing.Bitmap(reducedWidth, reducedHeight);
            using (var dc = Graphics.FromImage(reduced))
            {
                // you might want to change properties like
                dc.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                dc.DrawImage(original, new Rectangle(0, 0, reducedWidth, reducedHeight), new Rectangle(0, 0, original.Width, original.Height), GraphicsUnit.Pixel);
            }

            return reduced;
        }
    }
}
