using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace SnakeGame
    public partial class SnakeForm : Form
    {
        private List<Circle> Snake = new List<Circle>();
        private Circle food = new Circle();


        public SnakeForm()
        {
            InitializeComponent();

            // set settings to default
            new Settings();
            
            gameTimer.Interval = 1000 / Settings.Speed; // changing the game time to settings speed
            gameTimer.Tick += UpdateScreen;
            gameTimer.Start();

            //start a new game
            StartGame();
        }

        private void StartGame()
        {
            lblGameOver.Visible = false;

            // set settings to default
            new Settings();

            // create new player object
            Snake.Clear();
            Circle head = new Circle { X = 10, Y = 5 };
            Snake.Add(head);

            lblScore.Text = Settings.Score.ToString();
            GenerateFood();

        }

        //place a random foodobject somewhere in the game
        private void GenerateFood()
        {
            int maxXPos = pbCanvas.Size.Width / Settings.Width;
            int maxYPos = pbCanvas.Size.Height / Settings.Height;

            Random random = new Random();
            food = new Circle { X = random.Next(0, maxXPos), Y = random.Next(0, maxYPos) };
        }

        private void UpdateScreen(object sender, EventArgs e)
        {
            // check for Game over
            if(Settings.GameOver)
            {
                // check if Enter is pressed
                if (Input.KeyPressed(Keys.Enter))
                {
                    StartGame();
                }
            }
            else
            {
                if (Input.KeyPressed(Keys.Right) && Settings.direction != Directions.Left)
                    Settings.direction = Directions.Right;
                else if (Input.KeyPressed(Keys.Left) && Settings.direction != Directions.Right)
                    Settings.direction = Directions.Left;
                else if (Input.KeyPressed(Keys.Up) && Settings.direction != Directions.Down)
                    Settings.direction = Directions.Up;
                else if (Input.KeyPressed(Keys.Down) && Settings.direction != Directions.Up)
                    Settings.direction = Directions.Down;

                MovePlayer();
            }
            pbCanvas.Invalidate();
        }

        private void pbCanvas_Paint(object sender, PaintEventArgs e)
        {
            Graphics canvas = e.Graphics;
            
            if(!Settings.GameOver)
            {
                // set colour of snake
                Brush snakeColour;

                //draw snake
                for(int i = 0; i< Snake.Count; i++)
                {
                    if (i == 0)
                        snakeColour = Brushes.Black; //draw head
                    else
                        snakeColour = Brushes.Green; //rest of the body

                    canvas.FillEllipse(snakeColour,
                        new Rectangle(Snake[i].X * Settings.Width,
                                        Snake[i].Y * Settings.Height,
                                        Settings.Width, Settings.Height));

                    canvas.FillEllipse(Brushes.Magenta,
                        new Rectangle(  food.X * Settings.Width,
                                        food.Y * Settings.Height, 
                                        Settings.Width, Settings.Height));
                }
            }
            else
            {
                string gameOver = "Game over \nYour final score is: " + Settings.Score + "\nPress ENTER to try again";
                lblGameOver.Text = gameOver;
                lblGameOver.Visible = true;

            }
        }

        private void MovePlayer()
        {
            for (int i = Snake.Count -1; i >= 0; i--)
            {
                if(i == 0)
                {
                    switch (Settings.direction)
                    {
                        case Directions.Right:
                        Snake[i].X++;
                            break;

                        case Directions.Left:
                        Snake[i].X--;
                            break;

                        case Directions.Down:
                            Snake[i].Y++;
                            break;

                        case Directions.Up:
                        Snake[i].Y--;
                            break;

                        
                    }
                    int maxXPos = pbCanvas.Size.Width / Settings.Width;
                    int maxYPos = pbCanvas.Size.Height / Settings.Height;

                    // detect collision with game border
                    if (Snake[i].X < 0 || Snake[i].Y <= 0
                        || Snake[i].X >= maxXPos || Snake[i].Y >= maxYPos)
                    {
                        Die();
                    }

                    // detect collision with body
                    for (int j = 1; j < Snake.Count; j++)
                    {
                        if (Snake[i].X == Snake[j].X && Snake[i].Y == Snake[j].Y)
                        {
                            Die();
                        }
                    }


                    // detect collision with food piece
                    if (Snake[0].X == food.X && Snake[0].Y == food.Y)
                    {
                        Eat();
                        GenerateFood();
                    }

                }
                else
                {
                    Snake[i].X = Snake[i - 1].X;
                    Snake[i].Y = Snake[i - 1].Y;
                }
            }
        }

        private void SnakeForm_KeyUp(object sender, KeyEventArgs e)
        {
            Input.ChangeState(e.KeyCode, true);
        }
        private void SnakeForm_KeyDown(object sender, KeyEventArgs e)
        {
            Input.ChangeState(e.KeyCode, false);
        }



        private void Eat()
        {
            Circle circle = new Circle
            {
                X = Snake[Snake.Count - 1].X,
                Y = Snake[Snake.Count - 1].Y

            };
            Snake.Add(circle);

            Settings.Score += Settings.Points;
            lblScore.Text = Settings.Score.ToString();

            GenerateFood();
        }

        private void Die()
        {
            Settings.GameOver = true;
        }


    }
}
