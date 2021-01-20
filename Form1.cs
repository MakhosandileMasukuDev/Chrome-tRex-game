using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TrayNotify;

namespace tRex02
{
    public partial class Form1 : Form
    {
        bool jumping = false;
        int jumpSpeed;
        int force = 12;
        int score = 0;
        int obstacleSpeed = 10;
        Random rand = new Random();
        int position;
        bool isGameOver = false;
      
        public Form1()
        {
            InitializeComponent();

            GameRest();
        }

        private void MainGameTimerEvent(object sender, EventArgs e)


        {
            tRex.Top += jumpSpeed;

            textScore.Text = "Score : " + score;

            if (jumping == true && force < 0)
            {
                jumping = false;
            }

            if (jumping == true)
            {
                jumpSpeed = -12;
                force -= 1;
            }
            else
            {
                jumpSpeed = 12;
            }

            if (tRex.Top > 209 && jumping == false)
            {
                force = 12;
                tRex.Top = 210;
                jumpSpeed = 0;
            }

            int bac = 2;
            int scCount = 0;

            foreach (Control x in this.Controls)
            {
                if (x is PictureBox && (string)x.Tag == "obstacle")
                {
                    x.Left -= obstacleSpeed;

                    if (x.Left < -100)
                    {
                        x.Left = this.ClientSize.Width + rand.Next(300, 600) + (x.Width * 20);
                        score++;
                    }
                    if (tRex.Bounds.IntersectsWith(x.Bounds)) 
                    {
                        gameTimer.Stop();
                        tRex.Image = Properties.Resources.dead;
                        textScore.Text += "  Press R to restart game";
                        isGameOver = true;
                    }

                             if (score%10==0)
                    {

                        BackColor = BackColor == Color.Red ? Color.White : Color.Red;

                    }
                 
                }


            }

            if (score > 5)
            {
                obstacleSpeed = 9;
            }
        }



        private void keyisdown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Up && jumping == false)
            {
                jumping = true;
            }
        }

        private void keyisup(object sender, KeyEventArgs e)
        {
            if (jumping == true)
            {
                jumping = false;
            }

            if (e.KeyCode != Keys.R && isGameOver ==true)

            {
                textScore.Text = "Error, Press R to Restart";
            }
            if (e.KeyCode == Keys.R && isGameOver == true)
            {
                GameRest();
            }
        }

        private void GameRest()
        {
            force = 12;
            jumpSpeed = 0;
            jumping = false;
            score = 0;
            obstacleSpeed = 10;
            textScore.Text = "Score : " + score;
            tRex.Image = Properties.Resources.running;
            isGameOver = false;
            tRex.Top = 210 ;

            foreach(Control x in this.Controls)
            {
                if (x is PictureBox && (string)x.Tag == "obstacle")
                {
                    position = this.ClientSize.Width + rand.Next(800, 1400) + (x.Width * 30);


                        x.Left = position;
                        }
            }

            gameTimer.Start();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
