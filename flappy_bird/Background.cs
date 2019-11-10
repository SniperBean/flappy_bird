using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace flappy_bird
{
    public class Background
    {
        public Texture2D Texture1 { get; set; }
        public Texture2D Texture2 { get; set; }
        public Texture2D Texture3 { get; set; }
        public Vector2 Position1 { get; set; }
        public Vector2 Position2 { get; set; }
        public Vector2 Position3 { get; set; }
        public Vector2 GenPosition { get; set; }
        public Vector2 Velocity { get; set; }
        public int TTL { get; set; }

        public Background(Texture2D texture, Vector2 position, Vector2 velocity, int ttl)
        {
            Texture1 = texture;
            Texture2 = texture;
            Texture3 = texture;
            Position1 = position;
            position = new Vector2(Texture1.Width, position.Y);
            Position2 = position;
            position = new Vector2(Texture1.Width*2, position.Y);
            Position3 = position;
            Velocity = velocity;
            TTL = ttl;
            GenPosition = new Vector2(Texture1.Width * 2-0.5f, position.Y);;
        }

        public void Update()
        {
            TTL++;
            Position1 -= Velocity;
            Position2 -= Velocity;
            Position3 -= Velocity;

            if (Position1.X <= 0 - (Texture1.Width))
            { Position1 = GenPosition; }

            if (Position2.X <= 0 - (Texture2.Width))
            { Position2 = GenPosition; }

            if (Position3.X <= 0 - (Texture3.Width))
            { Position3 = GenPosition; }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            Rectangle rectangle1 = new Rectangle(0, 0, Texture1.Width, Texture1.Height);
            Rectangle rectangle2 = new Rectangle(0, 0, Texture2.Width, Texture2.Height);
            Rectangle rectangle3 = new Rectangle(0, 0, Texture2.Width, Texture3.Height);
            spriteBatch.Draw(Texture1, Position1, rectangle1, Color.White);
            spriteBatch.Draw(Texture2, Position2, rectangle2, Color.White);
            spriteBatch.Draw(Texture3, Position3, rectangle3, Color.White);
        }

    }

}
