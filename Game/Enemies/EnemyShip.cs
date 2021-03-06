﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Drawing;
using Engine;
using Engine.Utils;

namespace Game
{
    public class EnemyShip : GameObject
    {
        private static Random rnd = new Random();
        
        private EnemyBehavior behavior;

        Image result;
        public EnemyShip(int shipIndex, EnemyBehavior behavior)
        {
            this.behavior = behavior;
            
            Visible = false;
            result = Spritesheet.Load(shipIndex);
        }

        public PlayerShip Player
        {
            get
            {
                return AllObjects
                    .Select(obj => obj as PlayerShip)
                    .FirstOrDefault(obj => obj != null);
            }
        }

        public EnemyBehavior Behavior
        {
            get { return behavior; }
        }

        public override void Update(float deltaTime)
        {
            behavior.Update(this, deltaTime);
            Visible = true;
        }

        public void Explode()
        {
            if (rnd.NextDouble() > 0.95)
            {
                PowerUp pup = new PowerUp();
                pup.Center = Center;
                Root.AddChild(pup);
            }
            Explosion.Burst(Parent, Center);
            Delete();
        }

        public override void DrawOn(Graphics graphics)
        {
            graphics.DrawImage(LoadImage(), Bounds);
        }

        private Image LoadImage()
        {
            Extent = new SizeF(result.Size.Width / 2, result.Size.Height / 2);
            return result;
        }
    }
}
