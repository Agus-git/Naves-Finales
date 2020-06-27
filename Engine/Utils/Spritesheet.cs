using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Drawing;

namespace Engine.Utils
{
    public class Spritesheet
    {
        public static Image Load(int indice)
        {
            if (Images == null)
                Images = CutPieces();
            return Images[indice];
        }
        
        private static Image[] Images;

        private static Image[] CutPieces()
        {
            Image Imagen = new Bitmap(@"Resources\shipsheetparts.png");
            List<Image> pieces = new List<Image>();
            int tamaño = Imagen.Width / 200;
            for (int j = 0; j < tamaño; j++)
            {
                for (int i = 0; i < tamaño; i++)
                {
                    Image temp = new Bitmap(200, 200);
                    Graphics graphics = Graphics.FromImage(temp);
                    graphics.DrawImage(Imagen,
                        new Rectangle(0, 0, 200, 200),
                        new Rectangle(i * 200, j * 200, 200, 200),
                        GraphicsUnit.Pixel);
                    graphics.Dispose();

                    temp.RotateFlip(RotateFlipType.Rotate270FlipNone);
                    pieces.Add(temp);
                }
            }
            return pieces.ToArray();
        }
    }
}
