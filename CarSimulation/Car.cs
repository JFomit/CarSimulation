using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarSimulation
{
    class Car : Agent
    {
        public Genome Genome { get; private set; }
        public Car(Texture2D texture, Vector2 position, float rotation, Sprite[] map, Genome genome) : base(texture, position, rotation, map)
        {
            Genome = genome;
        }

        public void Think()
        {
            Genome.Next(this);
        }
    }
}
