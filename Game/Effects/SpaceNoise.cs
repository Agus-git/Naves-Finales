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
        private Image image;
        float speed;
        float scale;
        bool flipX;
        bool flipY;
        Image Imagen;

        public SpaceNoise(Image image, float speed, float scale, bool flipX, bool flipY)
        {
            this.speed = speed;
            this.scale = scale;
            this.flipX = flipX;
            this.flipY = flipY;
            this.image = image;

            Extent = new SizeF(image.Width * scale, image.Height * scale);

            Imagen = new Bitmap(image, new Size(Width.RoundedToInt(), Height.RoundedToInt()));
            if (flipX) { Imagen.RotateFlip(RotateFlipType.RotateNoneFlipX); }
            if (flipY) { Imagen.RotateFlip(RotateFlipType.RotateNoneFlipY); }
        }

        public override void Update(float deltaTime)
        {
            MoveLeft(deltaTime);
            KeepInsideScreen();
        }

        private void MoveLeft(float deltaTime)
        {
            X -= speed * deltaTime;
        }

        private void KeepInsideScreen()
        {
            X = X.Mod(Parent.Width);
            Y = Y.Mod(Parent.Height);
        }

        public override void DrawOn(Graphics graphics)
        {
            int w = Width.RoundedToInt();
            int h = Height.RoundedToInt();
            int x = Position.X.RoundedToInt();
            int y = Position.Y.RoundedToInt();
            while (x >= Parent.Left) { x -= w; }
            while (y >= Parent.Top) { y -= h; }

            for (int x1 = x; x1 <= Parent.Right; x1 += w)
            {
                for (int y1 = y; y1 <= Parent.Bottom; y1 += h)
                {
                    graphics.DrawImage(Imagen, x1, y1);
                }
            }
        }
    }
}
