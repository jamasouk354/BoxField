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
        Pen outLine = new Pen(Color.Black, 4);

        List<Box> boxesLeft = new List<Box>();
        List<Box> boxesRight = new List<Box>();
        int timer = 0;
        Random randGen = new Random();
        
        public GameScreen()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Set initial game values here
        /// </summary>
        public void OnStart()
        {
            boxesLeft.Add(new Box(30, 30, 30, randGen.Next(1, 255), randGen.Next(1, 255), randGen.Next(1, 255)));
            boxesRight.Add(new Box(130, 30, 30, randGen.Next(1, 255), randGen.Next(1, 255), randGen.Next(1, 255)));
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
            foreach (Box bL in boxesLeft)
            {
                bL.y = bL.y + 5;
                if (boxesRight[0].y == this.Height-50) { boxesRight.Remove(boxesRight[0]); }
            }
            foreach (Box bR in boxesRight)
            {
                bR.y = bR.y + 5;
                if (boxesLeft[0].y == this.Height-50) { boxesLeft.Remove(boxesLeft[0]); }
            }
            if (timer == 10)
            {
                OnStart();
                timer = 0;
            }
            Refresh();
        }

        private void GameScreen_Paint(object sender, PaintEventArgs e)
        {
            #region Creates Boxes(left&right)w/ random Colors
            for (int i = 0; i < boxesLeft.Count; i++)
            {
                boxBrush = new SolidBrush(Color.FromArgb(255, boxesLeft[i].red, boxesLeft[i].green, boxesLeft[i].blue));
                e.Graphics.DrawRectangle(outLine, boxesLeft[i].x, boxesLeft[i].y, boxesLeft[i].size, boxesLeft[i].size);
                e.Graphics.FillRectangle(boxBrush, boxesLeft[i].x, boxesLeft[i].y, boxesLeft[i].size, boxesLeft[i].size);
            }
            for (int i = 0; i < boxesRight.Count; i++)
            {
                boxBrush = new SolidBrush(Color.FromArgb(255, boxesRight[i].red, boxesRight[i].green, boxesRight[i].blue));
                e.Graphics.DrawRectangle(outLine, boxesRight[i].x, boxesRight[i].y, boxesRight[i].size, boxesRight[i].size);
                e.Graphics.FillRectangle(boxBrush, boxesRight[i].x, boxesRight[i].y, boxesRight[i].size, boxesRight[i].size);
            }
            #endregion
        }
    }
}
