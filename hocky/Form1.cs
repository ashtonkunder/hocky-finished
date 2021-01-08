using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System;
using System.Windows.Forms;
using System.Media;

namespace PongExample
{
    public partial class Form1 : Form
    {
        int paddle1X = 5;
        int paddle1Y = 60;
        int player1Score = 0;

        int paddle2X =  350;
        int paddle2Y = 60;
        int player2Score = 0;

        int paddleWidth = 30;
        int paddleHeight = 30;
        int paddleSpeed = 4;

        int ballX = 200;
        int ballY = 60;
        int ballXSpeed = 2;
        int ballYSpeed = -2;
        int ballWidth = 15;
        int ballHeight = 15;

        int leftWalltopX = 1;
        int leftWalltopY = 1;
        int leftWalltopWidth = 10;
        int leftWalltopHight = 59;

        int leftWallbottomX = 1;
        int leftWallbottomY = 96;
        int leftWallbottomWidth = 10;
        int leftWallbottomHight = 76;


        int rightWalltopX = 381;
        int rightWalltopY = 1;
        int rightWalltopWidth = 10;
        int rightWalltopHight = 59;

        int rightWallbottomX = 384;
        int rightWallbottomy = 96;
        int rightWallbottomWidth = 10;
        int rightWallbottomHight = 76;

        bool wDown = false;
        bool sDown = false;
        bool aDown = false;
        bool dDown = false;
        bool upArrowDown = false;
        bool downArrowDown = false;
        bool leftArrowDown = false;
        bool rightArrowDown = false;

        SolidBrush RedBrush = new SolidBrush(Color.Red);
        SolidBrush whiteBrush = new SolidBrush(Color.Black);
        Font screenFont = new Font("Consolas", 12);
        

        public Form1()
        {
           
            InitializeComponent();
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.W:
                    wDown = true;
                    break;
                case Keys.S:
                    sDown = true;
                    break;
                case Keys.A:
                    aDown = true;
                    break;
                case Keys.D:
                    dDown = true;
                    break;
                case Keys.Up:
                    upArrowDown = true;
                    break;
                case Keys.Down:
                    downArrowDown = true;
                    break;
                case Keys.Left:
                    leftArrowDown = true;
                    break;
                case Keys.Right:
                    rightArrowDown = true;
                    break;
            }

        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.W:
                    wDown = false;
                    break;
                case Keys.S:
                    sDown = false;
                    break;
                case Keys.A:
                    aDown = false;
                    break;
                case Keys.D:
                    dDown = false;
                    break;
                case Keys.Up:
                    upArrowDown = false;
                    break;
                case Keys.Down:
                    downArrowDown = false;
                    break;
                case Keys.Left:
                    leftArrowDown = false;
                    break;
                case Keys.Right:
                    rightArrowDown = false;
                    break;
            }

        }

        private void gameTimer_Tick(object sender, EventArgs e)
        {
            SoundPlayer player = new SoundPlayer(hocky.Properties.Resources.goal);
            //move ball
            ballX += ballXSpeed;
            ballY += ballYSpeed;

            //move player 1
            if (wDown == true && paddle1Y > 0)
            {
                paddle1Y -= paddleSpeed;
            }

            if (sDown == true && paddle1Y < this.Height - paddleHeight)
            {
                paddle1Y += paddleSpeed;
            }
            if (aDown == true && paddle1Y > 0)
            {
                paddle1X -= paddleSpeed;
            }
            if (dDown == true && paddle1Y > 0)
            {
                paddle1X += paddleSpeed;
            }

            //move player 2
            if (upArrowDown == true && paddle2Y > 0)
            {
                paddle2Y -= paddleSpeed;
            }

            if (downArrowDown == true && paddle2Y < this.Height - paddleHeight)
            {
                paddle2Y += paddleSpeed;
            }
            if (leftArrowDown == true && paddle2Y < this.Height - paddleHeight)
            {
                paddle2X -= paddleSpeed;
            }
            if (rightArrowDown == true && paddle2Y < this.Height - paddleHeight)
            {
                paddle2X += paddleSpeed;
            }

            //top and bottom wall collision
            if (ballY < 0 || ballY > this.Height - ballHeight)
            {
                ballYSpeed *= -1;  // or: ballYSpeed = -ballYSpeed;
            }
            // if (ballX > this.Width - ballHeight)
            // {
            // ballXSpeed *= -1;  // or: ballYSpeed = -ballYSpeed;
            // }


            //create Rectangles of objects on screen to be used for collision detection
            Rectangle player1Rec = new Rectangle(paddle1X, paddle1Y, paddleWidth, paddleHeight);
            Rectangle player2Rec = new Rectangle(paddle2X, paddle2Y, paddleWidth, paddleHeight);
            Rectangle ballRec = new Rectangle(ballX, ballY, ballWidth, ballHeight);
            Rectangle lefttopWall = new Rectangle(leftWalltopX, leftWalltopY, leftWalltopWidth, leftWalltopHight);
            Rectangle leftbottomWall = new Rectangle(leftWallbottomX, leftWallbottomY, leftWallbottomWidth, leftWallbottomHight);
            Rectangle righttopWall = new Rectangle(rightWalltopX, rightWalltopY, rightWalltopWidth, rightWalltopHight);
            Rectangle rightbottomWall = new Rectangle(rightWallbottomX, rightWallbottomy, rightWallbottomWidth, rightWallbottomHight);
            //check if ball hits either paddle. If it does change the direction
            //and place the ball in front of the paddle hit
            if (player1Rec.IntersectsWith(ballRec))
            {
                ballXSpeed = 2;
                ballX = paddle1X + paddleWidth + paddleHeight + 1;
            }
            else if (player2Rec.IntersectsWith(ballRec))
            {
                ballXSpeed = -2;
                ballX = paddle2X - ballWidth - paddleHeight - 1;
            }
            if (lefttopWall.IntersectsWith(ballRec))
            {
                ballXSpeed = 2;

            }
            if (leftbottomWall.IntersectsWith(ballRec))
            {
                ballXSpeed = 2;

            }
            if (righttopWall.IntersectsWith(ballRec))
            {
                ballXSpeed = -2;

            }
            if (rightbottomWall.IntersectsWith(ballRec))
            {
                ballXSpeed = -2;

            }

            if (ballX < 0)
            {
                player2Score++;
                ballX = 200;
                ballY = 60;

                paddle1Y = 50;
                paddle2Y = 50;


                player.Play();
            }
            if (ballX > 396)
            {
                player1Score++;
                ballX = 200;
                ballY = 60;
                player.Play();
                paddle1Y = 50;
                paddle2Y = 50;
            }



            if (player1Score == 3 || player2Score == 3)
            {
                gameTimer.Enabled = false;
            }


            Refresh();

        }
        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.FillEllipse(whiteBrush, ballX, ballY, ballWidth, ballHeight);

            e.Graphics.FillEllipse(RedBrush, paddle1X, paddle1Y, paddleWidth, paddleHeight);
            e.Graphics.FillEllipse(RedBrush, paddle2X, paddle2Y, paddleWidth, paddleHeight);


            e.Graphics.DrawString($"{player1Score}", screenFont, whiteBrush, 280, 10);
            e.Graphics.DrawString($"{player2Score}", screenFont, whiteBrush, 310, 10);

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void Form1_Load_1(object sender, EventArgs e)
        {

        }
    }
}
