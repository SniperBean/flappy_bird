using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace flappy_bird
{
    public class Bird
    {
        public Texture2D Texture { get; set; }
        public Vector2 Position { get; set; }
        public Vector2 Gravity { get; set; }
        public Vector2 JumpHeight { get; set; }
        public int Rows { get; set; }
        public int Columns { get; set; }
        private int currentFrame;
        private int totalFrames;
        private float angle = -0.5f;
        private Vector2 origin = new Vector2(0,0);
        public int frame=0;
        public int Jump_Time=0;
        KeyboardState oldState;

        public Rectangle Collider
        {
            get
            {
                return new Rectangle(
                    (int)Position.X-Texture.Width/2+Texture.Width/3+2,
                    (int)Position.Y-Texture.Height/2+2,
                    Texture.Width/3-4,
                    Texture.Height-4);
            }
        }

        public Bird(Texture2D texture, Vector2 position, Vector2 gravity, Vector2 jump, int rows, int columns)
        {
            Texture = texture;
            Position = position;
            Gravity = gravity;
            JumpHeight = jump;
            Rows = rows;
            Columns = columns;
            currentFrame = 0;
            totalFrames = Rows * Columns;
            origin.X = texture.Width / (2*3);
            origin.Y = texture.Height / 2;
        }

        public void Update()
        {
            if (Jump_Time <= 0)
            { Jump_Time = 0; }
            else
            { Jump_Time--; }

            Position += Gravity+(JumpHeight*Jump_Time);


            if(angle<=1.2f)
            angle+=0.05f;

            frame++;
            if(frame%15 == 0)
                currentFrame++;
            if (currentFrame == totalFrames)
            {
                currentFrame = 0;
                frame = 0;
            }
            /*
            if(keyboardState.IsKeyDown(Keys.Space)&& keyboardState.IsKeyUp(Keys.Space))
            {
                Jump();
            }*/

            KeyboardState newState = Keyboard.GetState();

            // Is the SPACE key down?
            if (newState.IsKeyDown(Keys.Space))
            {
                // If not down last update, key has just been pressed.
                if (!oldState.IsKeyDown(Keys.Space))
                {
                    Jump();
                }
            }
            else if (oldState.IsKeyDown(Keys.Space))
            {
                // Key was down last update, but not down now, so
                // it has just been released.
            }

            // Update saved state.
            oldState = newState;
        }

        public void Jump()
        {
            if (Position.Y > 20.0f)
            {
                //Position += JumpHeight;
                Jump_Time = 10;
                angle = -0.5f;
            }
        }


        public void Draw(SpriteBatch spriteBatch, Vector2 location)
        {
            int width = Texture.Width / Columns;
            int height = Texture.Height / Rows;
            int row = (int)((float)currentFrame / (float)Columns);
            int column = currentFrame % Columns;
            Rectangle sourceRectangle = new Rectangle(width * column, height * row, width, height);
            Rectangle destinationRectangle = new Rectangle((int)location.X, (int)location.Y, width, height);

            spriteBatch.Draw(Texture, destinationRectangle, sourceRectangle, Color.White, angle, origin, SpriteEffects.None,0f);

        }
    }
}
