using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ticTacToe
{
    public partial class mainForm : Form
    {
        private int [,] buttonValues;   // 1 for an X, 0 for an O and -1 for empty
        private string [,] textButtonValues;
        public mainForm()
        {
            InitializeComponent();
            buttonValues = new int[3,3] { { -1, -1, -1 },
                                         { - 1, - 1, - 1 },
                                            {-1, - 1 ,- 1 } };
            textButtonValues = new string[3, 3] { { " ", " ", " " },
                                                { " ", " ", " " },
                                                { " ", " ", " " }};
        }

        private void button_Click(object sender, EventArgs e)
        {

            // Get the sender as a Button.
            Button myButton = sender as Button;
            

            switch (myButton.Name)
            {
                case "firstLeftButton":
                    textButtonValues[0, 0] = "X";
                    buttonValues[0, 0] = 1;
                    break;
                case "firstMiddleButton":
                    textButtonValues[0, 1] = "X";
                    buttonValues[0, 1] = 1;
                    break;

                case "firstRightButton":
                    textButtonValues[0, 2] = "X";
                    buttonValues[0, 2] = 1;
                    break;

                case "secondLeftButton":
                    textButtonValues[1, 0] = "X";
                    buttonValues[1, 0] = 1;
                    break;
                case "secondMiddleButton":
                    textButtonValues[1, 1] = "X";
                    buttonValues[1, 1] = 1;
                    break;

                case "secondRightButton":
                    textButtonValues[1, 2] = "X";
                    buttonValues[1, 2] = 1;
                    break;

                case "thirdLeftButton":
                    textButtonValues[2, 0] = "X";
                    buttonValues[2, 0] = 1;
                    break;
                case "thirdMiddleButton":
                    textButtonValues[2, 1] = "X";
                    buttonValues[2, 1] = 1;
                    break;

                case "thirdRightButton":
                    textButtonValues[2, 2] = "X";
                    buttonValues[2, 2] = 1;
                    break;
            }
           

            updateAllButtons();
            disableAllButtons();
            playerWins();
        }

        private void updateAllButtons()
        {
            firstLeftButton.Text = textButtonValues[0,0]; 
            firstMiddleButton.Text = textButtonValues[0,1];
            firstRightButton.Text = textButtonValues[0,2];
            secondLeftButton.Text = textButtonValues[1,0];
            secondMiddleButton.Text = textButtonValues[1,1];
            secondRightButton.Text = textButtonValues[1,2];
            thirdLeftButton.Text = textButtonValues[2,0];
            thirdMiddleButton.Text = textButtonValues[2,1];
            thirdRightButton.Text = textButtonValues[2,2];
        }

        private void disableAllButtons()
        {
            firstLeftButton.Enabled = false;
            firstMiddleButton.Enabled = false;
            firstRightButton.Enabled = false;
            secondLeftButton.Enabled = false;
            secondMiddleButton.Enabled = false;
            secondRightButton.Enabled = false;
            thirdLeftButton.Enabled = false;
            thirdMiddleButton.Enabled = false;
            thirdRightButton.Enabled = false;


        }

        private void enableAllButtons()
        {
            firstLeftButton.Enabled = true;
            firstMiddleButton.Enabled = true;
            firstRightButton.Enabled = true;
            secondLeftButton.Enabled = true;
            secondMiddleButton.Enabled = true;
            secondRightButton.Enabled = true;
            thirdLeftButton.Enabled = true;
            thirdMiddleButton.Enabled = true;
            thirdRightButton.Enabled = true;


        }

        private void aiButton_Click(object sender, EventArgs e)
        {
            takeAIturn();
            enableAllButtons();
            aIWins();
        }


        private void takeAIturn()
        {

            int bestScore = 0;
            int moveScore;
            int bestRow = -1;
            int bestCol = -1;

            for(int i = 0; i < buttonValues.GetLength(0); i++)
            {
                for(int z=0;  z < buttonValues.GetLength(1); z++)
                {
                    if (buttonValues[i,z] == -1)
                    {
                        moveScore = turnScore(i, z);

                        if(moveScore > bestScore)
                        {
                            bestScore = moveScore;
                            bestRow = i;
                            bestCol = z;
                        }
                    }
                }
            }

            if(bestRow != -1 && bestCol != -1)
            {
                aIMove(bestRow, bestCol);
            }
        }

        private int turnScore(int row, int col)
        {
            int score = 0;

            buttonValues[row, col] = 0;
            if (gameWon(0))
            {
                score += 100;
            }
            buttonValues[row, col] = -1;

            buttonValues[row, col] = 1;
            if (gameWon(1))
            {
                score += 90;
            }
            buttonValues[row, col] = -1;

            if (row == 1 && col == 1)
            {
                score += 50;
            }

            if((row == 0 && col == 0) || (row == 0 && col == 2) || (row == 2 && col == 0) || (row == 2 && col == 2))
            {
                score += 30;
            }

            if((row == 0 && col == 1) || (row == 1 && col == 0) || (row == 2 && col == 1) || (row ==1 && col == 2))
            {
                score += 10;
            }

            if (buttonValues[row, col] == -1)
            {
                score += 5;
            }

            return score;
        }

        private void aIMove(int row, int col)
        {
            buttonValues[row, col] = 0;
            textButtonValues[row, col] = "O";
            updateAllButtons();
            disableAllButtons();
        }

        private void aIWins()
        {
            if (gameWon(0))
            {
                aiButton.Text = "AI Wins";
                disableAllButtons() ;
                aiButton.Enabled = false;
                aiButton.BackColor = Color.Red;
            }
        }

        private void playerWins()
        {
            if (gameWon(1))
            {
                aiButton.Text = "Player Wins";
                disableAllButtons();
                aiButton.Enabled = false;
                aiButton.BackColor = Color.Blue;
            }
        }

        private bool gameWon(int player)
        {
            if (buttonValues[0,0]==player && buttonValues[0,1]==player && buttonValues[0,2]==player ||
                buttonValues[1, 0] == player && buttonValues[1, 1] == player && buttonValues[1, 2] == player ||
                buttonValues[2, 0] == player && buttonValues[2, 1] == player && buttonValues[2, 2] == player || 
                buttonValues[0, 0] == player && buttonValues[1, 0] == player && buttonValues[2, 0] == player ||
                buttonValues[0, 1] == player && buttonValues[1, 1] == player && buttonValues[2, 1] == player ||
                buttonValues[0, 2] == player && buttonValues[1, 2] == player && buttonValues[2, 2] == player ||
                buttonValues[0, 2] == player && buttonValues[1, 1] == player && buttonValues[2, 0] == player ||
                buttonValues[0, 0] == player && buttonValues[1, 1] == player && buttonValues[2, 2] == player)
            {
                return true;
            }

            return false;
        }
    }

    


}
