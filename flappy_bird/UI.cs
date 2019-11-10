using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace flappy_bird
{
    public class UI
    {
        public Texture2D Title { get; set; }
        public Texture2D Board { get; set; }
        public Vector2 Title_Position { get; set; }
        public Vector2 Board_Position { get; set; }
        public float Win_Width { get; set; }
        public float Win_Height { get; set; }

        public UI(Texture2D title, Texture2D board, Vector2 title_position, Vector2 board_position, float win_width, float win_height)
        {
            Title = title;
            Board = board;
            title_position.X = win_width / 1.5f+25;
            title_position.Y += 20;
            board_position.X = win_width / 1.5f;
            Title_Position = title_position;
            Board_Position = board_position;
            Win_Width = win_width;
            Win_Height = win_height;
        }

        public void Update()
        {
            
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            Rectangle rectangle_bo = new Rectangle(0, 0, Board.Width, Board.Height);
            Rectangle rectangle_ti = new Rectangle(0, 0, Title.Width, Title.Height);
            //spriteBatch.Draw(Board, Board_Position, rectangle_bo, Color.White);
            spriteBatch.Draw(Board,Board_Position,null,Color.White,0,new Vector2(0, 0),new Vector2(1.85f, 1.875f),SpriteEffects.None,0);
            spriteBatch.Draw(Title, Title_Position, null, Color.White, 0, new Vector2(0, 0), new Vector2(2.5f, 2.5f), SpriteEffects.None, 0);
            //spriteBatch.Draw(Board, Board_Position, Color.White);
            //spriteBatch.Draw(Title, Title_Position, Color.White);
        }
    }
}
