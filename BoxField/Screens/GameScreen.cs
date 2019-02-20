using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BoxField
{
    public partial class GameScreen : UserControl
    {
        //player1 button control keys
        Boolean leftArrowDown, rightArrowDown;

        //used to draw boxes on screen
        SolidBrush boxBrush = new SolidBrush(Color.White);

        List<Box> boxesLeft = new List<Box>();
        List<Box> boxesRight = new List<Box>();
        int timer = 0;

        public GameScreen()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Set initial game values here
        /// </summary>
        public void OnStart()
        {
            boxesLeft.Add(new Box(30, 30, 30));
            boxesRight.Add(new Box(130, 30, 30));
        }

        private void GameScreen_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            //player 1 button presses
            switch (e.KeyCode)
            {
                case Keys.Left:
                    leftArrowDown = true;
                    break;
                case Keys.Right:
                    rightArrowDown = true;
                    break;           
            }
        }

        private void GameScreen_KeyUp(object sender, KeyEventArgs e)
        {
            //player 1 button releases
            switch (e.KeyCode)
            {
                case Keys.Left:
                    leftArrowDown = false;
                    break;
                case Keys.Right:
                    rightArrowDown = false;
                    break;
            }
        }

        private void gameLoop_Tick(object sender, EventArgs e)
        {
            timer++;
            //TODO - update location of all boxes (drop down screen)            
            foreach (Box bL in boxesLeft) { bL.y = bL.y + 5; }
            foreach (Box bR in boxesRight) { bR.y = bR.y + 5; }
            //TODO - remove box if it has gone of screen
            if (boxesLeft[0].y == this.Height) { boxesLeft.Remove(boxesLeft[0]); }
            //TODO - add new box if it is time
            if (timer == 10)
            {
                OnStart();
                timer = 0;
            }
            Refresh();
        }

        private void GameScreen_Paint(object sender, PaintEventArgs e)
        {
            //TODO - draw boxes to screen
            foreach (Box b in boxesLeft) { e.Graphics.FillRectangle(boxBrush, b.x, b.y, b.size, b.size); }
            foreach (Box b in boxesRight) { e.Graphics.FillRectangle(boxBrush, b.x, b.y, b.size, b.size); }
        }
    }
}
