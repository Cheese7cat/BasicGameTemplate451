using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GameSystemServices;

namespace BasicGameTemplate451
{
    public partial class GameScreen : UserControl
    {
        //player1 button control keys - DO NOT CHANGE
        Boolean leftArrowDown, downArrowDown, rightArrowDown, upArrowDown, bDown, nDown, mDown, spaceDown;


        //player2 button control keys - DO NOT CHANGE
        Boolean aDown, sDown, dDown, wDown, cDown, vDown, xDown, zDown;

        private void GameScreen_Load(object sender, EventArgs e)
        {

        }

        //TODO create your global game variables here
        int heroX, heroY, heroSize, heroSpeed, playerShotX, playerShotY, playerShotSize, playerShotSpeed, defaultShotSpeed, heroRec, playerShotRec;
        int monX, monY, monSize, monSpeed, monRec, score;
        SolidBrush heroBrush = new SolidBrush(Color.Black);
        SolidBrush shotBrush = new SolidBrush(Color.White);

        public GameScreen()
        {
            InitializeComponent();
            InitializeGameValues();
        }

        public void InitializeGameValues()
        {
            //TODO - setup all your initial game values here. Use this method
            // each time you restart your game to reset all values.
            heroX = 100;
            heroY = 283;
            heroSize = 20;
            heroSpeed = 5;

            monX = 20;
            monY = 10;
            monSize = 30;
            monSpeed = 5;

            score = 1;

            

            playerShotX = 110;
            playerShotY = 280;
            playerShotSize = 5;
            playerShotSpeed = 0;

           // defaultShotSpeed = 5;

        }

        private void GameScreen_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            // opens a pause screen is escape is pressed. Depending on what is pressed
            // on pause screen the program will either continue or exit to main menu
            if (e.KeyCode == Keys.Escape && gameTimer.Enabled)
            {
                gameTimer.Enabled = false;
                rightArrowDown = leftArrowDown = upArrowDown = downArrowDown = false;

                DialogResult result = PauseForm.Show();

                if (result == DialogResult.Cancel)
                {
                    gameTimer.Enabled = true;
                }
                else if (result == DialogResult.Abort)
                {
                    MainForm.ChangeScreen(this, "MenuScreen");
                }
            }

            //TODO - basic player 1 key down bools set below. Add remainging key down
            // required for player 1 or player 2 here.

            //player 1 button presses
            switch (e.KeyCode)
            {
                case Keys.Left:
                    leftArrowDown = true;
                    break;
                case Keys.Right:
                    rightArrowDown = true;
                    break;
                case Keys.Up:
                    upArrowDown = true;
                    break;
                case Keys.Space:
                    spaceDown = true;
                    playerShotX = heroX;
                    playerShotY = heroY;
                    playerShotSpeed = 5;
                    break;
            }
        }

        private void GameScreen_KeyUp(object sender, KeyEventArgs e)
        {
            //TODO - basic player 1 key up bools set below. Add remainging key up
            // required for player 1 or player 2 here.

            //player 1 button releases
            switch (e.KeyCode)
            {
                case Keys.Left:
                    leftArrowDown = false;
                    break;
                case Keys.Right:
                    rightArrowDown = false;
                    break;
                case Keys.Space:
                    spaceDown = true;
                    break;

            }
        }

        /// <summary>
        /// This is the Game Engine and repeats on each interval of the timer. For example
        /// if the interval is set to 16 then it will run each 16ms or approx. 50 times
        /// per second
        /// </summary>
        private void gameTimer_Tick(object sender, EventArgs e)
        {
            //TODO move main character 
            //if (leftArrowDown == true && playerShotY < this.Height - playerShotSpeed && heroY < this.Height - heroSpeed)
            if (leftArrowDown == true && heroX > 0)
            {
                heroX = heroX - heroSpeed;
                //playerShotX = playerShotX - playerShotSpeed;
            }
            //if (rightArrowDown == true && playerShotX < this.Height - playerShotSpeed && heroX < this.Height - heroSpeed)
            if (rightArrowDown == true && heroX < this.Width - heroSize)
            {
                heroX = heroX + heroSpeed;
                // playerShotX = playerShotX + playerShotSpeed;
            }

            if (spaceDown == true && playerShotSpeed == 0)
            {
                playerShotSpeed = defaultShotSpeed;
            }

            if (playerShotSpeed > 0)
            {
                playerShotY = playerShotY - playerShotSpeed;

            }

            if (playerShotY < 10)
            {
                spaceDown = false;

                playerShotSpeed = 0;
            }


            if(monX == 20 && monY == 10)
            {
                monX =this.Width - monSpeed;
            }


            Rectangle playerShotRec = new Rectangle(playerShotX, playerShotY, playerShotSize, playerShotSpeed);
            Rectangle heroRec = new Rectangle(heroX, heroY, heroSize, heroSpeed);
            Rectangle monRec = new Rectangle(monX,monY,monSize,monSpeed);

            //TODO move npc characters
            //TODO move npc characters
             if (playerShotRec.IntersectsWith(monRec))
            {
                scoreOutput.Text = score++ + "";
                   
                


            }

            //TODO collisions checks



            //calls the GameScreen_Paint method to draw the screen.
            Refresh();
        }


        //Everything that is to be drawn on the screen should be done here
        private void GameScreen_Paint(object sender, PaintEventArgs e)
        {
            //draw rectangle to screen
            e.Graphics.FillRectangle(heroBrush, heroX, heroY, heroSize, heroSize);
            e.Graphics.FillRectangle(shotBrush, playerShotX, playerShotY, playerShotSize, playerShotSize);
            e.Graphics.FillRectangle(shotBrush, monX,monY,monSize, monSize);
        }
    }

}
