using Engine;
using Engine.Extensions;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    public class SpaceNoise : GameObject
    {
        private Bitmap image;
        float speed;
        float scale;
        bool flipX;
        bool flipY;
        Point[] posiciones;
        Image Imagen;
        Image Imagen2;

        public SpaceNoise(Image image, float speed, float scale, bool flipX, bool flipY, GameObject padre)
        {
            posiciones = new Point[2];
            Parent = padre;
            this.speed = speed;
            this.scale = scale;
            this.flipX = flipX;
            this.flipY = flipY;
            PrepararImagen(image, scale, flipX, flipY);

            Bitmap PreImage = new Bitmap((Parent.Right * 2).RoundedToInt(), Parent.Bottom.RoundedToInt());
            Ensamblador(PreImage);
            Divisor(PreImage);
            posiciones[0] = new Point(0, 0);
            posiciones[1] = new Point(PreImage.Size.Width / 2, 0);
        }

        private void Divisor(Bitmap preImage)
        {
            Image temp = new Bitmap(preImage.Size.Width / 2, preImage.Size.Height);
            Graphics graphics = Graphics.FromImage(temp);
            graphics.DrawImage(preImage,0,0,new Rectangle(0,0,temp.Size.Width, temp.Size.Height), GraphicsUnit.Pixel);
            Imagen = temp;
            graphics.DrawImage(preImage, preImage.Size.Width / 2, 0, new Rectangle(0, 0, temp.Size.Width, temp.Size.Height), GraphicsUnit.Pixel);
            Imagen2 = temp;
        }

        private void PrepararImagen(Image image, float scale, bool flipX, bool flipY)
        {
            Extent = new SizeF(image.Width * scale, image.Height * scale);
            this.image = new Bitmap(image, new Size(Width.RoundedToInt(), Height.RoundedToInt()));
            if (flipX) { this.image.RotateFlip(RotateFlipType.RotateNoneFlipX); }
            if (flipY) { this.image.RotateFlip(RotateFlipType.RotateNoneFlipY); }
        }
        private void Ensamblador(Bitmap PreImage)
        {
            for (int x = 0; x < PreImage.Size.Width; x++)
            {
                int miX = x % this.image.Size.Width;
                for (int y = 0; y < PreImage.Size.Height; y++)
                {
                    PreImage.SetPixel(x, y, this.image.GetPixel(miX, y % this.image.Size.Height));
                }
            }
        }

        public override void Update(float deltaTime)
        {
            MoveLeft(deltaTime);
            KeepInsideScreen();
        }

        private void MoveLeft(float deltaTime)
        {
            for (int i = 0; i < posiciones.Length; i++)
            {
                posiciones[i].X -= (speed * deltaTime).RoundedToInt();
            }
        }

        private void KeepInsideScreen()
        {
            for (int i = 0; i < posiciones.Length; i++)
            {
                if (posiciones[i].X < image.Height * -1)
                {
                    posiciones[i].X = 0;
                }
            }
        }

        public override void DrawOn(Graphics graphics)
        {
            graphics.DrawImage(Imagen, posiciones[0]);
            graphics.DrawImage(Imagen2, posiciones[1]);
        }
    }
}
