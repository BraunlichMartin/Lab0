using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
// there is a timer used in this program -MWB 1/16/2019
using System.Timers;

namespace Memory
{
    public partial class boardForm : Form
    {
        public boardForm()
        {
            InitializeComponent();
        }

        #region Instance Variables
        const int NOT_PICKED_YET = -1;

        int firstCardNumber = NOT_PICKED_YET;
        int secondCardNumber = NOT_PICKED_YET;
        int matches = 0;
        #endregion

        #region Methods

        // This method finds a picture box on the board based on it's number (1 - 20)
        // It takes an integer as it's parameter and returns the picture box controls
        // that's name contains that number
        private PictureBox GetCard(int i)
        {
            PictureBox card = (PictureBox)this.Controls["card" + i];
            return card;
        }

        // This method gets the filename for the image displayed in a picture box given it's number
        // It takes an integer as it's parameter and returns a string containing the 
        // filename for the image in the corresponding picture box
        private string GetCardFilename(int i)
        {
            return GetCard(i).Tag.ToString();
        }

        // This method changes the filename for a given picture box
        // It takes an integer and a string that represents a filename as it's parameters
        // It doesn't return a value but stores the filename for the image to be displayed
        // in the picture box.  It doesn't actually display the new image
        private void SetCardFilename(int i, string filename)
        {
            GetCard(i).Tag = filename;
        }

        // These 2 methods get the value (and suit) of the card in a given picturebox
        // Both methods take an integer as the parameter and return a string
        private string GetCardValue(int index)
        {
            return GetCardFilename(index).Substring(4, 1);
        }

        private string GetCardSuit(int index)
        {
            return GetCardFilename(index).Substring(5, 1);
        }

        // TODO:  students should write this one
        //added if else statement, before it just returned "true"-MWB_2019-1-14
        private bool IsMatch(int index1, int index2)
        {
            if (GetCardValue(index1) == GetCardValue(index2))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        // This method fills each picture box with a filename
        
        
        private void FillCardFilenames()
        {
            string[] values = { "a", "2", "j", "q", "k" };
            string[] suits = { "c", "d", "h", "s" };
            int i = 1;

            for (int suit = 0; suit <= 3; suit++)
            {
                for (int value = 0; value <= 4; value++)
                {
                    
                    SetCardFilename(i, "card" + values[value] + suits[suit] + ".jpg");
                    i++;
                }
            }
        }

        // TODO:  students should write this one
        //created a random number generator called rnd and applied it to the integers 1 through 4 to represent the card values, then created another instance inorder to represent suits 1 through 3
        //-MWB_2019-1-14
        
        private void ShuffleCards()
        {
            /*
            

            for each card the method GetCardFilename will get the cards name and store it in the variable 'card1'
            then a random number from one to twenty (inclusive) will be generated and assigned to 'value'
            the method GetCardFilename will then be applied to this 'value' and assigned to the string 'card2'
            then SetCardFilename is used to assign 'card2' to 'i'
            then SetCardFilename is used to assign 'card1' to 'value'
            overall this randomly assigns each card a new place in the ordering-MWB 1/18/2019
            */
            Random rnd = new Random();
            for (int i = 1; i <= 20; i++)
            {
                string card1 = GetCardFilename(i);
                int value = rnd.Next(1, 21);
                string card2 = GetCardFilename(value);
                SetCardFilename(i, card2);
                SetCardFilename(value, card1);
            }
        }

        // This method loads (shows) an image in a picture box.  Assumes that filenames
        // have been filled in an earlier call to FillCardFilenames
        private void LoadCard(int i)
        {
            PictureBox card = GetCard(i);
            card.Image = Image.FromFile(System.Environment.CurrentDirectory + "\\Cards\\" + GetCardFilename(i));
        }

        // This method loads the image for the back of a card in a picture box
        private void LoadCardBack(int i)
        {
            PictureBox card = GetCard(i);
            card.Image = Image.FromFile(System.Environment.CurrentDirectory + "\\Cards\\black_back.jpg");
        }

        // TODO:  students should write all of these
        // shows (loads) the backs of all of the cards
        //-MWB 1-16-2019
        private void LoadAllCardBacks()
        {
            for(int i=1; i <= 20; i++)
            {
                LoadCardBack(i);
            }
        }

        //temporary function to make sure that all the cards are randomized and loaded in the event handler "boardformload "-MWB 1/16/2019
        private void LoadAllCardFronts()
        {
            for (int i = 1; i <= 20; i++)
            {
                LoadCard(i);
            }
        }

        // Hides a picture box
        private void HideCard(int i)
        {
            PictureBox card = GetCard(i);
            card.Visible = false;
        }

        //-MWB 1/16/2019
        private void HideAllCards()
        {
            for (int i = 1; i <= 20; i++)
            {
                HideCard(i);
            }
        }

        // shows a picture box
        //-MWB 1/16/2019
        private void ShowCard(int i)
        {
            PictureBox card = GetCard(i);
            card.Image = Image.FromFile(System.Environment.CurrentDirectory + "\\Cards\\" + GetCardFilename(i) + ".jpg");
        }

        //a small method I wrote inorder to check if the cards were sufficiently randomized.-MWB 1/16/2019
        private void ShowAllCards()
        {
            for (int i = 1; i <= 20; i++)
            {
                ShowCard(i);
            }
        }

        // disables a picture box
        private void DisableCard(int i)
        {
            PictureBox card = GetCard(i);
            card.Enabled = false;
        }

        private void DisableAllCards()
        {
            for (int i = 1; i <= 20; i++)
            {
                DisableCard(i);
            }
        }

        private void EnableCard(int i)
        {
            PictureBox card = GetCard(i);
            card.Enabled = true;
        }

        private void EnableAllCards()
        {
            for (int i = 1; i <= 20; i++)
            {
                EnableCard(i);
            }
        }
    
        private void EnableAllVisibleCards()
        {
            for (int i = 1; i <= 20; i++)
            {
                PictureBox card = GetCard(i);
                if (card.Visible == true)
                {
                    card.Enabled = true;
                }

            }
        }

        #endregion

        #region EventHandlers
        private void boardForm_Load(object sender, EventArgs e)
        {
            /* 
             * Fill the picture boxes with filenames
             * Shuffle the cards
             * Load all of the card backs - 
             *      While you're testing you might want to load all of card faces
             *      to make sure that the cards are loaded successfully and that
             *      they're shuffled.  If you get all 2s, something is wrong.
             * 
            */
            
            FillCardFilenames();
            ShuffleCards();
            //LoadAllCardFronts();      //A call to a small method I wrote inorder to check if the cards were sufficiently randomized.  -MWB 1/18/2019
            LoadAllCardBacks();
            

        }

        private void card_Click(object sender, EventArgs e)
        {
            PictureBox card = (PictureBox)sender;
            int cardNumber = int.Parse(card.Name.Substring(4));
            

            /* 
             * if the first card isn't picked yet
             *      save the first card index
             *      load the card
             *      disable the card
             *  else (the user just picked the second card)
             *      save the second card index
             *      load the card
             *      disable all of the cards
             *      start the flip timer
             *  end if
            */
            if (firstCardNumber == NOT_PICKED_YET)
            {
                firstCardNumber = cardNumber;
                LoadCard(cardNumber);
                DisableCard(cardNumber);
                
            }
            else
            {
                
                secondCardNumber = cardNumber;
                LoadCard(cardNumber);
                DisableAllCards();
                //MessageBox.Show("card click else statement");
                //flipTimer = new System.Threading.Timer(1000);
                //flipTimer_Tick(sender, e
                flipTimer.Enabled = true;
            }
        }

        private void flipTimer_Tick(object sender, EventArgs e)
        {
            /*
             * stop the flip timer
             * if the first card and second card are a match
             *      increment the number of matches
             *      hide the first card
             *      hide the second card
             *      reset the first card number
             *      reset the second card number
             *      if the number of matches is 10
             *          show a message box
             *      else
             *          enable all of the cards left on the board
             *      end if
             * else
             *      flip the first card back over
             *      flip the second card back over
             *      reset the first card number
             *      reset the second card number
             *      enable all of the cards left on the board
             * end if
             * The flip timer is an object on the boardForm.cs-MWB 1/18/2019
             */
            flipTimer.Enabled = false;
            if (IsMatch(firstCardNumber, secondCardNumber) == true)
            {
                matches += 1;
                HideCard(firstCardNumber);
                HideCard(secondCardNumber);
                firstCardNumber = NOT_PICKED_YET;
                secondCardNumber = NOT_PICKED_YET;
                if (matches == 10)
                {
                    MessageBox.Show("number of matches is 10");
                }
                else
                {
                    EnableAllVisibleCards();
                }
            }
            else
            {
                LoadCardBack(firstCardNumber);
                LoadCardBack(secondCardNumber);
                firstCardNumber = NOT_PICKED_YET;
                secondCardNumber = NOT_PICKED_YET;
                EnableAllVisibleCards();
            }
        }
        #endregion
    }
}
