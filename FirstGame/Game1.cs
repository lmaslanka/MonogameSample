using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace FirstGame
{
    using System.Collections.Generic;
    using System.Threading;
    using System.Xml;

    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        VertexPositionTexture[] floorVerts;
        BasicEffect effect;
        Texture2D checkerboardTexture;
        Camera camera;

        List<Tile> tileList;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            this.IsMouseVisible = true;
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            const int repetitions = 1;
            int count = -1;
            const int width = 100; // divisible by 2
            const int height = 100; // divisible by 2
            floorVerts = new VertexPositionTexture[6*width*height];
            tileList = new List<Tile>();
            for (int w = -1 * (width / 2); w < width / 2; w++)
            {
                for (int h = -1 * (height / 2); h < height / 2; h++)
                {
                    var tile = new Tile();

                    tile.vertex1.Position = new Vector3(0 + w, 0 + h, 0);
                    tile.vertex1.TextureCoordinate = new Vector2(0, 0);
                    tile.vertex1Count = ++count;

                    tile.vertex2.Position = new Vector3(0 + w, 1 + h, 0);
                    tile.vertex2.TextureCoordinate = new Vector2(0, repetitions);
                    tile.vertex2Count = ++count;

                    tile.vertex3.Position = new Vector3(1 + w, 0 + h, 0);
                    tile.vertex3.TextureCoordinate = new Vector2(repetitions, 0);
                    tile.vertex3Count = ++count;

                    tile.vertex4.Position = tile.vertex2.Position;
                    tile.vertex4.TextureCoordinate = tile.vertex2.TextureCoordinate;
                    tile.vertex4Count = ++count;

                    tile.vertex5.Position = new Vector3(1 + w, 1 + h, 0);
                    tile.vertex5.TextureCoordinate = new Vector2(repetitions, repetitions);
                    tile.vertex5Count = ++count;

                    tile.vertex6.Position = tile.vertex3.Position;
                    tile.vertex6.TextureCoordinate = tile.vertex3.TextureCoordinate;
                    tile.vertex6Count = ++count;
                    
                    tileList.Add(tile);

                    //floorVerts[count + 0].Position = new Vector3(0+w, 0+h, 0);
                    //floorVerts[count + 1].Position = new Vector3(0+w, 1+h, 0);
                    //floorVerts[count + 2].Position = new Vector3(1+w, 0+h, 0);

                    //floorVerts[count + 3].Position = floorVerts[count + 1].Position;
                    //floorVerts[count + 4].Position = new Vector3(1+w, 1+h, 0);
                    //floorVerts[count + 5].Position = floorVerts[count + 2].Position;

                    //int repetitions = 1;

                    //floorVerts[count + 0].TextureCoordinate = new Vector2(0, 0);
                    //floorVerts[count + 1].TextureCoordinate = new Vector2(0, repetitions);
                    //floorVerts[count + 2].TextureCoordinate = new Vector2(repetitions, 0);

                    //floorVerts[count + 3].TextureCoordinate = floorVerts[count + 1].TextureCoordinate;
                    //floorVerts[count + 4].TextureCoordinate = new Vector2(repetitions, repetitions);
                    //floorVerts[count + 5].TextureCoordinate = floorVerts[count + 2].TextureCoordinate;
                }
            }

            foreach (var tile in tileList)
            {
                this.floorVerts[tile.vertex1Count] = tile.vertex1;
                this.floorVerts[tile.vertex2Count] = tile.vertex2;
                this.floorVerts[tile.vertex3Count] = tile.vertex3;
                this.floorVerts[tile.vertex4Count] = tile.vertex4;
                this.floorVerts[tile.vertex5Count] = tile.vertex5;
                this.floorVerts[tile.vertex6Count] = tile.vertex6;
            }

            // TODO: Add your initialization logic here
            //floorVerts = new VertexPositionTexture[6];

            //floorVerts[0].Position = new Vector3(0, 0, 0);
            //floorVerts[1].Position = new Vector3(0, 1, 0);
            //floorVerts[2].Position = new Vector3(1, 0, 0);

            //floorVerts[3].Position = floorVerts[1].Position;
            //floorVerts[4].Position = new Vector3(1, 1, 0);
            //floorVerts[5].Position = floorVerts[2].Position;

            //int repetitions = 1;

            //floorVerts[0].TextureCoordinate = new Vector2(0, 0);
            //floorVerts[1].TextureCoordinate = new Vector2(0, repetitions);
            //floorVerts[2].TextureCoordinate = new Vector2(repetitions, 0);

            //floorVerts[3].TextureCoordinate = floorVerts[1].TextureCoordinate;
            //floorVerts[4].TextureCoordinate = new Vector2(repetitions, repetitions);
            //floorVerts[5].TextureCoordinate = floorVerts[2].TextureCoordinate;

            effect = new BasicEffect(graphics.GraphicsDevice);

            camera = new Camera(graphics.GraphicsDevice);

            base.Initialize();
        }

        void DrawGround()
        {
            effect.View = camera.ViewMatrix;
            effect.Projection = camera.ProjectionMatrix;

            effect.TextureEnabled = true;
            effect.Texture = checkerboardTexture;

            foreach (var pass in effect.CurrentTechnique.Passes)
            {
                pass.Apply();
                graphics.GraphicsDevice.DrawUserPrimitives(PrimitiveType.TriangleList, floorVerts, 0, this.tileList.Count * 2);
            }
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            using (var stream = TitleContainer.OpenStream("Content/GrassGreenTexture0001.jpg"))
            {
                checkerboardTexture = Texture2D.FromStream(this.GraphicsDevice, stream);
            }
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            camera.Update(gameTime);

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            DrawGround();

            base.Draw(gameTime);
        }
    }
}
