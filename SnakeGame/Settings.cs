using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeGame
{
    public enum Directions
    {

        // this is an enum class called Directions
        // we are using enum because its easier to classify the directions 
        // for this game 

        Left,
        Right,
        Up,
        Down

    };

    public class Settings
    {
        public static int Width { get; set; }
        public static int Height { get; set; }
        public static int Speed { get; set; }
        public static int Score { get; set; }
        public static int Points { get; set; }
        public static bool GameOver { get; set; }
    
        public static Directions direction { get; set; }

        public Settings()
        {
            // these are the default settings
            Width = 16;
            Height = 16;
            Speed = 16;
            Score = 0;
            Points = 100;
            GameOver = false;
            direction = Directions.Down; // the default direction will be down
        }

    }
}
