using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using MonoGame.Extended;
using MonoGame.Extended.ViewportAdapters;

namespace flappy_bird
{
	/// <summary>
	/// This is the main type for your game.
	/// </summary>
	public class Game1 : Game
	{
		GraphicsDeviceManager graphics;
		SpriteBatch spriteBatch;
        Background background;
        Base based;
        Camera2D camera;
        UI ui;
        SpriteFont font;
        Random random;
        public List<Pipe> pipe;
        Bird bird;
        int Ini_X;
        int Ini_Y;
        int Gen_Pipe_Timer=119;
        const int Gen_Timeup=120;

        //Data
        int Score = 0;
        int bird_pox_X;
        int bird_pox_Y;
        int nextpipe_X;
        int nextpipe_Y;


		public Game1()
		{
			graphics = new GraphicsDeviceManager (this);
		}

		protected override void Initialize ()
        {
            var viewportAdapter = new BoxingViewportAdapter(Window, GraphicsDevice, 1280, 768);
			base.Initialize ();
            camera = new Camera2D(viewportAdapter);
            camera.ZoomIn(2.5f);
            camera.Position = new Vector2(-455.0f, -260.0f);
            Gen_Pipe_Timer = 119;
            Score = 0;
		}

		protected override void LoadContent ()
		{
            // Create a new SpriteBatch, which can be used to draw textures.

			spriteBatch = new SpriteBatch (GraphicsDevice);
            background = new Background(Content.Load<Texture2D>("Content/background/background"),new Vector2(0.0f, 0),new Vector2(0.2f, 0),10);
            based = new Base(Content.Load<Texture2D>("Content/object/base"), new Vector2(0.0f, background.Texture1.Height-background.Texture1.Height/5), new Vector2(1.0f, 0), 10);
            Ini_X = Content.Load<Texture2D>("Content/background/background").Width / 2;
            Ini_Y = Content.Load<Texture2D>("Content/background/background").Height / 2;
            bird = new Bird(Content.Load<Texture2D>("Content/chara/yellow"), new Vector2(Ini_X, Ini_Y), new Vector2(0, 1.8f), new Vector2(0, -1f), 1, 3);
            ui = new UI(Content.Load<Texture2D>("Content/user_interface/title"), Content.Load<Texture2D>("Content/user_interface/board"), new Vector2(0, 0), new Vector2(0, 0),GraphicsDevice.Viewport.Bounds.Width,GraphicsDevice.Viewport.Bounds.Height);
            font = Content.Load<SpriteFont>("Content/font/arial");
            random = new Random();
            pipe = new List<Pipe>();
		}

		protected override void Update (GameTime gameTime)
		{
            Gen_Pipe_Timer++;
            if (Gen_Pipe_Timer % Gen_Timeup == 0)
            {
                pipe.Add(GenerateNewPipe());
                Gen_Pipe_Timer = 0;
            }

            /*
            for (int i = 0; i < total; i++)
            {
                pipe.Add(GenerateNewPipe());
            }*/

            for (int total_pipe = 0; total_pipe < pipe.Count; total_pipe++)
            {
                pipe[total_pipe].Update();
                if (pipe[total_pipe].TTL <= 0)
                {
                    pipe.RemoveAt(total_pipe);
                    total_pipe--;
                }

            }

            for (int total_pipe = 0; total_pipe < pipe.Count; total_pipe++)
            {
                if (pipe[total_pipe].Position_REWARD.X >= -10)
                {
                    nextpipe_X = (int)pipe[total_pipe].Position_REWARD.X - (int)bird.Position.X - 2;
                    nextpipe_Y = (int)pipe[total_pipe].Position_DOWN.Y - (int)bird.Position.Y - 4;
                    break;
                }
            }

            bird_pox_X = (int)bird.Position.X + bird.Texture.Width / 6 - 2;
            bird_pox_Y = (int)bird.Position.Y + bird.Texture.Height / 2 - 2;


            bird.Update();
            based.Update();
            background.Update();
			base.Update (gameTime);

            CheckBallCollision();

            var keyboardState = Keyboard.GetState();
            if (keyboardState.IsKeyDown(Keys.R))
            {
                base. Initialize(); 
            }
		}

        public Pipe GenerateNewPipe()
        {
            Texture2D texture_up = Content.Load<Texture2D>("Content/object/up_pipe");
            Texture2D texture_down = Content.Load<Texture2D>("Content/object/down_pipe");
            Texture2D texture_reward = Content.Load<Texture2D>("Content/object/reward");

            Vector2 position = new Vector2(Content.Load<Texture2D>("Content/background/background").Width*2, (float)(-20-random.Next(120)));

            Vector2 velocity = new Vector2(1.0f, 0f);
            int ttl = 500;

            return new Pipe(texture_up, texture_down, texture_reward, position, velocity, ttl);
        }

        public void CheckBallCollision()
        {
            if (bird.Collider.Intersects(based.Collider1))
            {
                base. Initialize(); 
                Gen_Pipe_Timer = 119;
            }

            if (bird.Collider.Intersects(based.Collider2))
            {
                base.Initialize();
                Gen_Pipe_Timer = 119;
            }

            if (bird.Collider.Intersects(based.Collider3))
            {
                base.Initialize();
                Gen_Pipe_Timer = 119;
            }

            for (int index = 0; index < pipe.Count; index++)
            {
                if (bird.Collider.Intersects(pipe[index].Collider_REWARD))
                {
                    pipe[index].Position_REWARD = new Vector2(-10, -10);
                    Score=index+1;
                }

                if (bird.Collider.Intersects(pipe[index].Collider_UP)||bird.Collider.Intersects(pipe[index].Collider_DOWN))
                {
                    base.Initialize();
                    Gen_Pipe_Timer = 119;
                    Score = 0;
                }
            }
        }

		protected override void Draw (GameTime gameTime)
		{

            GraphicsDevice.Clear(Color.CornflowerBlue);
            var transformMatrix = camera.GetViewMatrix();
            spriteBatch.Begin(transformMatrix: transformMatrix);
            background.Draw(spriteBatch);
            based.Draw(spriteBatch);
            for (int index = 0; index < pipe.Count; index++)
            {
                pipe[index].Draw(spriteBatch);
            }
            bird.Draw(spriteBatch, bird.Position);


            Texture2D SimpleTexture = new Texture2D(GraphicsDevice, 1, 1, false, SurfaceFormat.Color);

            Int32[] pixel = { 0xFFFFFF }; // White. 0xFF is Red, 0xFF0000 is Blue
            SimpleTexture.SetData<Int32>(pixel, 0, SimpleTexture.Width * SimpleTexture.Height);

            // Paint a 100x1 line starting at 20, 50
            this.spriteBatch.Draw(SimpleTexture, new Rectangle((int)bird_pox_X, (int)bird_pox_Y, (int)nextpipe_X, 1), Color.DarkRed);
            this.spriteBatch.Draw(SimpleTexture, new Rectangle((int)bird_pox_X, (int)bird_pox_Y, 1, (int)nextpipe_Y), Color.DarkRed);
            //this.spriteBatch.Draw(SimpleTexture, new Rectangle((int)bird_pox_X, (int)bird_pox_Y, 1, (int)nextpipe_Y), Color.DarkBlue);

            spriteBatch.End();

            spriteBatch.Begin();
            ui.Draw(spriteBatch);
            spriteBatch.DrawString(font, "dist_X:" + nextpipe_X + "\ndist_Y:" + nextpipe_Y, new Vector2(ui.Board_Position.X+20, ui.Title_Position.Y+100), Color.SaddleBrown);
            spriteBatch.End();
			base.Draw (gameTime);
		}
	}
}
