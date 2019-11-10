using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace flappy_bird
{
    public class Pipe
    {
        public Texture2D Texture_UP { get; set; }
        public Vector2 Position_UP { get; set; }

        public Texture2D Texture_DOWN { get; set; }
        public Vector2 Position_DOWN { get; set; }

        public Texture2D Texture_REWARD { get; set; }
        public Vector2 Position_REWARD { get; set; }

        public Vector2 Velocity { get; set; }

        public Rectangle Collider_UP
        {
            get
            {
                return new Rectangle(
                    (int)Position_UP.X,
                    (int)Position_UP.Y ,
                    Texture_UP.Width ,
                    Texture_UP.Height );
            }
        }

        public Rectangle Collider_DOWN
        {
            get
            {
                return new Rectangle(
                    (int)Position_DOWN.X,
                    (int)Position_DOWN.Y ,
                    Texture_DOWN.Width ,
                    Texture_DOWN.Height );
            }
        }

        public Rectangle Collider_REWARD
        {
            get
            {
                return new Rectangle(
                    (int)Position_REWARD.X,
                    (int)Position_REWARD.Y,
                    Texture_REWARD.Width,
                    Texture_REWARD.Height);
            }
        }

        public int TTL { get; set; }
        public int Block_Dist= 50;

        public Pipe(Texture2D texture_up, Texture2D texture_down, Texture2D texture_reward, Vector2 position, Vector2 velocity, int ttl)
        {
            Texture_UP = texture_up;
            Position_UP = position;

            Texture_DOWN = texture_down;
            Position_DOWN = new Vector2(position.X,position.Y+texture_up.Height+Block_Dist);

            Texture_REWARD = texture_reward;
            Position_REWARD = new Vector2(position.X + texture_reward.Width, position.Y + texture_up.Height + Block_Dist/2-texture_reward.Height/2);

            Velocity = velocity;
            TTL = ttl;
        }

        public void Update()
        {
            TTL--;

                Position_UP -= Velocity;
                Position_DOWN -= Velocity;
                Position_REWARD -= Velocity;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            
                Rectangle sourceRectangle_UP = new Rectangle(0, 0, Texture_UP.Width, Texture_UP.Height);
                Rectangle sourceRectangle_DOWN = new Rectangle(0, 0, Texture_DOWN.Width, Texture_DOWN.Height);
                Rectangle sourceRectangle_REWARD = new Rectangle(0, 0, Texture_REWARD.Width, Texture_REWARD.Height);

                spriteBatch.Draw(Texture_UP, Position_UP, sourceRectangle_UP, Color.White);
                spriteBatch.Draw(Texture_DOWN, Position_DOWN, sourceRectangle_DOWN, Color.White);
                spriteBatch.Draw(Texture_REWARD, Position_REWARD, sourceRectangle_REWARD, Color.White);

        }
    }
}
