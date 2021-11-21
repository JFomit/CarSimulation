using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using MonoGame;
using System.Collections.Generic;
using System.Text;

namespace CarSimulation
{
    class Agent : Sprite
    {
        /// <summary>
        /// reference to all collidable obstacles on the map
        /// </summary>
        public Sprite[] Map { get; private set; }

        public Agent(Texture2D texture, Vector2 position, float rotation, Sprite[] map) : base(texture, position, rotation)
        {
            Map = map;
        }
        /// <summary>
        /// Moves this agent along with it's rotation
        /// </summary>
        /// <param name="distance">distance to trave. If negative, will move backwards</param>
        public void Move(float distance)
        {
            float NewX = Position.X + distance * MathF.Cos(RotationRadians);
            float NewY = Position.Y + distance * MathF.Sin(RotationRadians);

            Position = new Vector2(NewX, NewY);
        }
        /// <summary>
        /// Rotates this agent by a given angle
        /// </summary>
        /// <param name="angle">angle measured in degrees</param>
        public void Rotate(float angle)
        {
            Rotation += angle;
        }
        /// <summary>
        /// Does raycasting in a given angle
        /// </summary>
        /// <param name="angle">angle to raycast on. Measured in degrees</param>
        /// <param name="maxSteps">limit of a calculation steps.</param>
        /// <param name="obstacles">array of possible obstacles</param>
        /// <returns>distance between starting position and first obstacle met; if the no obstacle was found - returns <c>maxSteps</c> value</returns>
        public float Raycast(float angle, int maxSteps, Sprite[] obstacles)
        {
            float angleRadians = MathHelper.ToRadians(angle);
            Vector2 ray = Position;
            Vector2 rayIncrement = new Vector2(MathF.Cos(angleRadians), MathF.Sin(angleRadians));

            for (int i = 0; i < maxSteps; i++)
            {
                ray += rayIncrement;

                foreach (var item in obstacles)
                {
                    if (item.Collider.Contains(ray))
                    {
                        return Vector2.Distance(Position, ray);
                    }
                }
            }
            return maxSteps;
        }
    }
}
