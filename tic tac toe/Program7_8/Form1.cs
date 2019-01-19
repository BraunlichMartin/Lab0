using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Program7_8
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            ResetSquares();
        }

        const string USER_SYMBOL = "X";
        const string COMPUTER_SYMBOL = "O";
        const string EMPTY = "";
        const int SIZE = 5;

        // constants for the 2 diagonals
        const int TOP_LEFT_TO_BOTTOM_RIGHT = 1;
        const int TOP_RIGHT_TO_BOTTOM_LEFT = 2;

        // constants for IsWinner
        const int NONE = -1;
        const int ROW = 1;
        const int COLUMN = 2;
        const int DIAGONAL = 3;

        // This method takes a row and column as parameters and 
        // returns a reference to a label on the form in that position
        private Label GetSquare(int row, int column)
        {
            int labelNumber = row * SIZE + column + 1;
            return (Label)(this.Controls["label" + labelNumber.ToString()]);
        }

        // This method does the "reverse" process of GetSquare
        // It takes a label on the form as it's parameter and
        // returns the row and column of that square as output parameters
        private void GetRowAndColumn(Label l, out int row, out int column)
        {
            int position = int.Parse(l.Name.Substring(5));
            row = (position - 1) / SIZE;
            column = (position - 1) % SIZE;
        }

        // This method takes a row (in the range of 0 - 2) and returns true if 
        // the row on the form contains 3 Xs or 3 Os.
        // Use it as a model for writing IsColumnWinner

        /*
                private bool IsRowWinner(int row)
                {
                    if (GetSquare(row, 0).Text == GetSquare(row, 1).Text
                            && GetSquare(row, 1).Text == GetSquare(row, 2).Text
                            && GetSquare(row, 0).Text != EMPTY)
                    {
                        return true;
                    }
                    else
                        return false;
                }
         */

                 //* This is a better version of IsRowWinner.  Can you tell me what it does?
                private bool IsRowWinner(int row)
                {
                    Label square = GetSquare(row, 0);
                    string symbol = square.Text;
                    for (int col = 1; col < SIZE; col++)
                    {
                        square = GetSquare(row, col);
                        if (symbol == EMPTY || square.Text != symbol)
                            return false;
                    }
                    return true;
                }


        // TODO:  Write these 3
        /*private bool IsColumnWinner(int col)
        {
            if (GetSquare(0, col).Text == GetSquare(1, col).Text
                    && GetSquare(1, col).Text == GetSquare(2, col).Text
                    && GetSquare(0, col).Text != EMPTY)
            {
                return true;
            }
            else
                return false;
        }
        */

        private bool IsColumnWinner(int col)
        {
            Label square = GetSquare(col, 0);
            string symbol = square.Text;
            for (int row = 1; row < SIZE; row++)
            {
                square = GetSquare(row, col);
                if (symbol == EMPTY || square.Text != symbol)
                    return false;
            }
            return true;
        }
        /*
        private bool IsDiagonal1Winner()
        {
            if (GetSquare(0,0).Text == GetSquare(1,1).Text 
                && GetSquare(1,1).Text == GetSquare(2,2).Text
                && GetSquare(1,1).Text != EMPTY)
            {
                return true;
            }
            else
                return false;
        }
        */
        private bool IsDiagonal1Winner()
        {
            int row = 0;
            Label square = GetSquare(row, 0);
            string symbol = square.Text;
            for (row = 0; row < SIZE; row++)
            {
                square = GetSquare(row, row);
                if (symbol == EMPTY || square.Text != symbol)
                    return false;
                
            }
            return true;
        }

        /*
        private bool IsDiagonal2Winner()
        {
            if (GetSquare(0, 2).Text == GetSquare(1, 1).Text
                && GetSquare(1, 1).Text == GetSquare(2, 0).Text
                && GetSquare(1, 1).Text != EMPTY)
            {
                return true;
            }
            else
                return false;
        }
        */
        private bool IsDiagonal2Winner()
        {
            int row = 0; 
            int col = SIZE-1;
            Label square = GetSquare(row, col);
            string symbol = square.Text;
            for (row = 0; row < SIZE; row++)
            {
                square = GetSquare(row, col);
                if (symbol == EMPTY || square.Text != symbol)
                    return false;
                col--;
            }
            return true;
        }


        // This method determines if any row, column or diagonal on the board is a winner.
        // It returns true or false and the output parameters will contain appropriate values
        // when the method returns true.  See constant definitions at top of form.
        private bool IsWinner(out int whichDimension, out int whichOne)
        {
            // rows
            for (int row = 0; row < SIZE; row++)
            {
                if (IsRowWinner(row))
                {
                    whichDimension = ROW;
                    whichOne = row;
                    return true;
                }
            }
            // columns
            for (int column = 0; column < SIZE; column++)
            {
                if (IsColumnWinner(column))
                {
                    whichDimension = COLUMN;
                    whichOne = column;
                    return true;
                }
            }
            // diagonals
            if (IsDiagonal1Winner())
            {
                whichDimension = DIAGONAL;
                whichOne = TOP_LEFT_TO_BOTTOM_RIGHT;
                return true;
            }
            if (IsDiagonal2Winner())
            {
                whichDimension = DIAGONAL;
                whichOne = TOP_RIGHT_TO_BOTTOM_LEFT;
                return true;
            }
            whichDimension = NONE;
            whichOne = NONE;
            return false;
        }

        // I wrote this method to show you how to call IsWinner
        private bool IsTie()
        {
            int winningDimension, winningValue;
            return (IsFull() && !IsWinner(out winningDimension, out winningValue));
        }

        // TODO:  Write this one
        private bool IsFull()
        {
            for (int row = 0; row < SIZE; row++)
            {
                for (int col = 0; col < SIZE; col++)
                {
                    Label square = GetSquare(row, col);
                    if (square.Text == EMPTY)
                        return false;
                }
            }
            return true;
        }

        // Inside the click event handler you have a reference to the label that was clicked
        // Use this method (and pass that label as a parameter) to disable just that one square
        private void DisableSquare(Label square)
        {
            square.Click -= new System.EventHandler(this.label_Click);
        }

        // Setting the enabled property changes the look and feel of the cell.
        // Instead, this code removes the event handler from each square.
        // Use it when someone wins or the board is full to prevent clicking a square.
        private void DisableAllSquares()
        {
            for (int row = 0; row < SIZE; row++)
            {
                for (int col = 0; col < SIZE; col++)
                {
                    Label square = GetSquare(row, col);
                    DisableSquare(square);
                }
            }
        }

        // You'll need this method to allow the user to start a new game
        private void EnableAllSquares()
        {
            for (int row = 0; row < SIZE; row++)
            {
                for (int col = 0; col < SIZE; col++)
                {
                    Label square = GetSquare(row, col);
                    square.Click += new System.EventHandler(this.label_Click);
                }
            }
        }

        // This method "highlights" a column by making the font color for the cells in the column red
        // It takes one parameter, the column and does not return a value
        private void HighlightColumn(int col)
        {
            for (int row = 0; row < SIZE; row++)
            {
                Label square = GetSquare(row, col);
                square.ForeColor = Color.Red;
            }
        }

        // TODO:  Write these 2

        private void HighlightRow(int row)
        {
            for (int col = 0; col < SIZE; col++)
            {
                Label square = GetSquare(row, col);
                square.ForeColor = Color.Red;
            }
        }

        private void HighlightDiagonal1()
        {
            for (int row = 0, col = 0; row < SIZE; row++, col++)
            {
                Label l = GetSquare(row, col);
                l.ForeColor = Color.Red;
            }
        }

        private void HighlightDiagonal2()
        {
            for (int row = 0, col = SIZE-1; row < SIZE; row++, col--)
            {
                Label l = GetSquare(row, col);
                l.ForeColor = Color.Red;
            }
        }

        // I needed this method to highlighting the diagonals work like rows and columns in the next method
        private void HighlightDiagonal(int whichDiagonal)
        {
            if (whichDiagonal == TOP_LEFT_TO_BOTTOM_RIGHT)
                HighlightDiagonal1();
            else
                HighlightDiagonal2();

        }

        // TODO:  finish this
        private void HighlightWinner(string player, int winningDimension, int winningValue)
        {
            switch (winningDimension)
            {
                case ROW:
                    HighlightRow(winningValue);
                    resultLabel.Text = (player + " wins!");
                    break;
                case COLUMN:
                    HighlightColumn(winningValue);
                    resultLabel.Text = (player + " wins!");
                    break;
                case DIAGONAL:
                    HighlightDiagonal(winningValue);
                    resultLabel.Text = (player + " wins!");
                    break;
            }
        }

        // TODO:  Finish this
        // The method should set all of the squares to empty and set the font color to black
        private void ResetSquares()
        {
            DisableAllSquares();
            EnableAllSquares();
            for (int col = 0; col < SIZE; col++)
            {
                for (int row = 0; row < SIZE; row++)
                {
                    Label square = GetSquare(row, col);
                    square.ForeColor = Color.Black;
                    square.Text = EMPTY;
                }
            }
            

            resultLabel.Text = "";
        }

        // TODO:  Finish this
        /*label square;
         * do
         * {
         *      row=random number between 0 and 2
         *      col=random number between 0 and 2
         *      square = GetSquare(row, col);
         * }
         * while square is not empty
         * put an O in the square
         * disable the square
         * 
         * Notes: Randomness-look at rock paper scissors
         * Random generator = new Random(); // before do
         * int row = generator.Next(0, SIZE)
         * 
         * Notes: loop condition
         * while(square.text!=EMPTY);
         * label square = GetSquare(row, col);
        */
        private void MakeComputerMove()
        {
            Random numberGenerator = new Random();
            Label square;
            do
            {
                int col = numberGenerator.Next(0, SIZE);
                int row = numberGenerator.Next(0, SIZE);
                square = GetSquare(row, col);
            }
            while (square.Text != EMPTY);
            square.Text = COMPUTER_SYMBOL;
            DisableSquare(square);
            /*
            int winningDimension = NONE;
            int winningValue = NONE;

            Random random = new Random();
            int randomNumber = random.Next(0, 9);



            label clickedLabel = randomNumber;
            clickedLabel.Text = COMPUTER_SYMBOL;
            DisableSquare(clickedLabel);
            if (IsWinner(out winningDimension, out winningValue))
            {
                DisableAllSquares();
                HighlightWinner("The computer", winningDimension, winningValue);
            }
            */
        }
        
        /*
         *put an "X" in the label
         * disable that label
         * If there is a winner then 
         *      disable all squares
         *      Highlight the winning row or column or diagonal and display a message
         * else-if the board is not full
         *      Computer makes a move
         *      if there is a winner then
         *          disable all the squares
         *          highlight the winner
         *      end if
         * else
         *      Display a message that says "It's a tie"
         * end if
        */

        private void label_Click(object sender, EventArgs e)
        {
            int winningDimension = NONE;
            int winningValue = NONE;

            Label clickedLabel = (Label)sender;
            clickedLabel.Text = USER_SYMBOL;
            DisableSquare(clickedLabel);
            if (IsWinner(out winningDimension, out winningValue))
            {
                DisableAllSquares();
                HighlightWinner("The User", winningDimension, winningValue);
            }
            else if (!IsFull())
            {
                MakeComputerMove();
                if (IsWinner(out winningDimension, out winningValue))
                {
                    DisableAllSquares();
                    HighlightWinner("The Computer", winningDimension, winningValue);
                }
            }
            else
                resultLabel.Text = "It's a tie";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ResetSquares();
            
        }
    }
}
