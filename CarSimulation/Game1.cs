using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame;
using System;

namespace CarSimulation
{
    public class Game1 : Game
    {
        private int counter = 0;
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private Car car;
        private Sprite[] collidable;
        private SpriteFont font;

        Color color = Color.CornflowerBlue;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        protected override void LoadContent()
        {
            Random rnd = new Random();

            _spriteBatch = new SpriteBatch(GraphicsDevice);
            Texture2D textureCar = Content.Load<Texture2D>(@"car");
            Texture2D textureWall = Content.Load<Texture2D>(@"stolb");
            font = Content.Load<SpriteFont>(@"File");

            int[] comms = new int[10];

            for (int i = 0; i < comms.Length; i++)
            {
                comms[i] = rnd.Next(0, 8);
            }

            InitionalGenomeBuilder initionalGenomeBuilder = new InitionalGenomeBuilder(comms);
            GenomeCreator genomeCreator = new GenomeCreator(initionalGenomeBuilder);

            collidable = new Sprite[20];
            for (int i = 0; i < collidable.Length; i++)
            {
                collidable[i] = new Sprite(textureWall, CreateRandomPos(rnd), 0f);
            }

            car = new Car(textureCar, new Vector2(100f, 100f), 0f, collidable, genomeCreator.CreateGenome());
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            
            KeyboardState keyboardState = Keyboard.GetState();
            if (!keyboardState.IsKeyDown(Keys.Space))
            {
                car.Think();
            }

            color = car.Collide(collidable) ? Color.Turquoise : Color.CornflowerBlue;

            base.Update(gameTime);
            counter++;
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(color);

            Vector4 distances = new Vector4(
                car.Raycast(car.Rotation + 0,   150, collidable),
                car.Raycast(car.Rotation + 90,  150, collidable),
                car.Raycast(car.Rotation + 180, 150, collidable),
                car.Raycast(car.Rotation + 270, 150, collidable));

            _spriteBatch.Begin();
            car.Redraw(_spriteBatch, Color.White);
            #region drawing debug lines
            _spriteBatch.DrawLine(car.Position, distances.X, car.RotationRadians, Color.Red);
            _spriteBatch.DrawLine(car.Position, distances.Y, car.RotationRadians + MathF.PI / 2, Color.Red);
            _spriteBatch.DrawLine(car.Position, distances.Z, car.RotationRadians + MathF.PI, Color.Red);
            _spriteBatch.DrawLine(car.Position, distances.W, car.RotationRadians + 3 * MathF.PI / 2, Color.Red);
            #endregion
            foreach (var item in collidable)
            {
                item.Redraw(_spriteBatch, Color.White);
            }
            _spriteBatch.DrawString(font, $"counter = {counter}\npointer = {car.Genome.Pointer}", Vector2.One, Color.Black);
            _spriteBatch.End();

            base.Draw(gameTime);
        }
        /// <summary>
        /// Creates random pos vectors
        /// </summary>
        /// <param name="rnd"><c>Random</c> class instance</param>
        /// <returns><c>Vector2</c> with random componets: [0-500)</returns>
        private Vector2 CreateRandomPos(Random rnd)
        {
            return new Vector2(rnd.Next(0, 500), rnd.Next(0, 500));
        }
    }
}
