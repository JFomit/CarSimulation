using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame;
using System;
using System.Collections.Generic;
using System.Linq;
using TextCopy;

namespace CarSimulation
{
    public class Game1 : Game
    {
        /// <summary>
        /// Counts iterations
        /// </summary>
        private long counter = 0;
        /// <summary>
        /// Counts tick from the last PAUSE button press
        /// </summary>
        private long tickCounter = 0;
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private List<Car> cars;
        private Sprite[] collidable;
        private SpriteFont font;

        private string debugString;
        private string objectsInfo;
        private bool pause = false;
        private Vector2 scroll = Vector2.Zero;
        private const float moveSpeed = 10f;

        private Texture2D carTexture;
        private Texture2D wallTexture;

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
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            carTexture = Content.Load<Texture2D>(@"car");
            wallTexture = Content.Load<Texture2D>(@"stolb");
            font = Content.Load<SpriteFont>(@"File");

            StartSim();
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            
            KeyboardState keyboardState = Keyboard.GetState();
            Vector2 mousePos = Mouse.GetState(Window).Position.ToVector2();

            if (tickCounter >= 30)
            {
                if (keyboardState.IsKeyDown(Keys.Space))
                {
                    tickCounter = 0;
                    pause = !pause;
                }
            }
            tickCounter++;

            #region Main loop
            Car currentCar = null;
            objectsInfo = string.Empty;
            List<Car> deleted = new List<Car>();

            foreach (var key in keyboardState.GetPressedKeys())
            {
                switch (key)
                {
                    case Keys.W:
                    case Keys.Up: scroll.Y += moveSpeed; break;
                    case Keys.S:
                    case Keys.Down: scroll.Y -= moveSpeed; break;
                    case Keys.A:
                    case Keys.Left: /*scroll.X += moveSpeed;*/ break;
                    case Keys.D:
                    case Keys.Right: /*scroll.X -= moveSpeed;*/ break;
                    default:
                        break;
                }
            }

            if (!pause)
            {
                scroll.X -= 0.5f;
                foreach (var item in cars)
                {
                    if (item.Collide(collidable) || item.Position.X < MathF.Abs(scroll.X + 32))
                    {
                        deleted.Add(item);
                    }
                    item.Think();
                }
                counter++;
            }
            for (int i = 0; i < deleted.Count; i++)
            {
                Car item = deleted[i];
                cars.Remove(item);
            }

            int objectsCount = 0;
            foreach (var item in cars)
            {
                if (objectsCount < 3)
                {
                    objectsInfo += $"\nPosition = {item.Position};";
                    objectsCount++;
                }
                if (Vector2.Distance(mousePos, item.Position + scroll) <= 30f)
                {
                    currentCar = item;
                }
            }

            debugString = $"counter = {counter}";
            if (currentCar != null)
            {
                debugString += $"\npointer = {currentCar.Genome.Pointer}";
                string data = string.Join(",", currentCar.Genome.Commands.Select(t => t.OpCode).ToArray());
                debugString += $"\n{data}";
                if (keyboardState.IsKeyDown(Keys.Enter))
                {
                    ClipboardService.SetText($"{{{data}}}");
                }
                color = currentCar.Collide(collidable) ? Color.Turquoise : Color.CornflowerBlue;
            }
            else
            {
                color = Color.CornflowerBlue;
            }
            #endregion

            if (cars.Count <= 5)
            {
                RestartSim();
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(color);
            _spriteBatch.Begin();
            #region drawing debug lines
            foreach (var car in cars)
            {
                Vector4 distances = new Vector4(
                   car.Raycast(car.Rotation + 0, 150, collidable),
                   car.Raycast(car.Rotation + 90, 150, collidable),
                   car.Raycast(car.Rotation + 180, 150, collidable),
                   car.Raycast(car.Rotation + 270, 150, collidable));

                car.Redraw(_spriteBatch, Color.White, scroll);
                _spriteBatch.DrawLine(car.Position + scroll, distances.X, car.RotationRadians, Color.Red);
                _spriteBatch.DrawLine(car.Position + scroll, distances.Y, car.RotationRadians + MathF.PI / 2, Color.Red);
                _spriteBatch.DrawLine(car.Position + scroll , distances.Z, car.RotationRadians + MathF.PI, Color.Red);
                _spriteBatch.DrawLine(car.Position + scroll, distances.W, car.RotationRadians + 3 * MathF.PI / 2, Color.Red);
            }
            #endregion
            foreach (var item in collidable)
            {
                item.Redraw(_spriteBatch, Color.White, scroll);
            }

            string scrollString = $"scroll = ({scroll.X},{scroll.Y})";

            _spriteBatch.DrawString(font, debugString, Vector2.One, Color.Black);
            _spriteBatch.DrawString(font, scrollString, new Vector2(Window.ClientBounds.Width - font.MeasureString(scrollString).X, 1), Color.Black);
            _spriteBatch.DrawString(font, objectsInfo, new Vector2(0, Window.ClientBounds.Height - font.MeasureString(objectsInfo).Y), Color.Black, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
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

        private void StartSim()
        {
            Random rnd = GenerateObstaclesAndReturnGenRnd();
            // cool {0,6,1,7,5,1,0,1,2,5}
            cars = new List<Car>(20);
            for (int i = 0; i < cars.Capacity; i++)
            {
                int[] comms = new int[10];

                for (int j = 0; j < comms.Length; j++)
                {
                    comms[j] = rnd.Next(0, 8);
                }

                InitionalGenomeBuilder initionalGenomeBuilder = new InitionalGenomeBuilder(comms);
                GenomeCreator genomeCreator = new GenomeCreator(initionalGenomeBuilder);

                cars.Add(new Car(carTexture, CreateRandomPos(rnd), 0f, collidable, genomeCreator.CreateGenome()));
            }
        }

        private Random GenerateObstaclesAndReturnGenRnd()
        {
            scroll = Vector2.Zero;
            counter = 0;
            Random rnd = new Random();

            collidable = new Sprite[20];
            for (int i = 0; i < collidable.Length; i++)
            {
                collidable[i] = new Sprite(wallTexture, CreateRandomPos(rnd), 0f);
            }

            return rnd;
        }

        private void RestartSim()
        {
            Random rnd = GenerateObstaclesAndReturnGenRnd();

            List<Car> genomeDonors = new List<Car>();
            genomeDonors.AddRange(cars);
            cars.Clear();
            while (cars.Count < 20)
            {
                Car donor = genomeDonors[rnd.Next(0, genomeDonors.Count)];
                IBuilder builder = new GeneticAlgorithGenomeBuilder(donor.Genome, 1f, 8, rnd);
                GenomeCreator genomeCreator = new GenomeCreator(builder);
                cars.Add(new Car(carTexture, CreateRandomPos(rnd), 0f, collidable, genomeCreator.CreateGenome()));
            }
        }
    }
}
