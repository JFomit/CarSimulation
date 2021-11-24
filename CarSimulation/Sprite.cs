using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame;
using System;

namespace CarSimulation
{
    class Sprite
    {
        public Texture2D Texture { get; set; }
        public Vector2 Position { get; set; }
        private Vector2 Origin { get => new Vector2(Texture.Width / 2, Texture.Height / 2); }
        /// <summary>
        /// Measured in degrees
        /// </summary>
        public float Rotation { get; set; }
        /// <summary>
        /// Measured in radians
        /// </summary>
        public float RotationRadians { get => MathHelper.ToRadians(Rotation); }
        public Rectangle Collider 
        { 
            get
            {
                return new Rectangle(
                    x: (int)Position.X - Texture.Width / 2,
                    y: (int)Position.Y - Texture.Height / 2,
                    width: Texture.Width,
                    height: Texture.Height);
            } 
        }

        public Sprite(Texture2D texture, Vector2 position, float rotation)
        {
            Rotation = rotation;
            Texture = texture ?? throw new ArgumentNullException(nameof(texture));
            Position = position;
        }

        /// <summary>
        /// Redraws this sprite in a current SpriteBatch
        /// </summary>
        /// <param name="spriteBatch">current <c>SpriteBatch</c> to draw the sprite in</param>
        public void Redraw(SpriteBatch spriteBatch)
        {
            Redraw(spriteBatch, Color.White);
        }
        /// <summary>
        /// Redraws this sprite in a current SpriteBatch
        /// </summary>
        /// <param name="spriteBatch">current <c>SpriteBatch</c> to draw the sprite in</param>
        /// <param name="color">a color mask</param>
        public void Redraw(SpriteBatch spriteBatch, Color color)
        {
            Redraw(spriteBatch, color, Vector2.Zero);
        }
        /// <summary>
        /// Redraws this sprite in a current SpriteBatch
        /// </summary>
        /// <param name="spriteBatch">current <c>SpriteBatch</c> to draw the sprite in</param>
        /// <param name="color">a color mask</param>
        public void Redraw(SpriteBatch spriteBatch, Color color, Vector2 scroll)
        {
            Rectangle collider = Collider;
            collider.Location = (collider.Location.ToVector2() + scroll).ToPoint();

            spriteBatch.Draw(
                texture: Texture,
                position: Position + scroll,
                sourceRectangle: null,
                color: color,
                rotation: RotationRadians,
                origin: Origin,
                scale: 1f,
                effects: SpriteEffects.None,
                layerDepth: 0);
            //drawing collider of the sprite
            spriteBatch.DrawRectangle(collider, Color.Blue);
        }
        /// <summary>
        /// Checks weather or not this sprite collides with other
        /// </summary>
        /// <param name="other">other Sprite</param>
        /// <returns><c>true</c> if Sprites are colliding; otherwise <c>false</c></returns>
        public bool Collide(Sprite other)
        {
            return Collider.Intersects(other.Collider);
        }
        /// <summary>
        /// Checks weather or not this sprite collides with any other object
        /// </summary>
        /// <param name="others">Sprites to check collisions with</param>
        /// <returns><c>true</c> if collides with any of the <c>cothers</c> sprite; otherwise <c>false</c></returns>
        public bool Collide(Sprite[] others)
        {
            foreach (var item in others)
            {
                if (Collider.Intersects(item.Collider))
                {
                    return true;
                }
            }

            return false;
        }
    }
}
