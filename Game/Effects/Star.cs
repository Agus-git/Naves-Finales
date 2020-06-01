using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Drawing;
using Engine;

using Engine.Extensions;

namespace Game
{
    public class Star : GameObject
    {
        private Image img;
        private Random rnd = new Random();
        float speed;

        public Star(Image img)
        {
            this.speed = rnd.Next(300);
            int size = (0.16 * speed).FloorToInt().Max(1);
            this.img = new Bitmap(img, new Size(size, size));

            Extent = this.img.Size;
            Visible = false;
        }

        public override void Update(float deltaTime)
        {
            X -= speed * deltaTime;
            Visible = true;
        }

        public override void DrawOn(Graphics graphics)
        {
            graphics.DrawImage(img, Position);
        }
    }
}
