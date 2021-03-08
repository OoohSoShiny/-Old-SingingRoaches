using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Media;
using System.IO;

namespace MovingRoaches
{
    public partial class MainFrame : Form
    {

        //Creating arrays and variables used in the whole class
        Label[] labelArray, labelVoiceArray;
        PictureBox[] picbArray;
        int randomNumber = -1, clickOrder = 0, playerPoints = 0;
        Random randomizer, movementRandomizer;
        string[] currentArray, stringArraySentence;
        bool[] checkClicks;
        Point pointZero;
        SoundPlayer[] soundPlayers;
        string[] roaches_Sound_Path_Array;

        public MainFrame()
        {
            InitializeComponent();

            //Initializing the soundplayer Kakalakophon
            roaches_Sound_Path_Array = new string[] {
            @".\kakalakophon\kakalake1.wav",
            @".\kakalakophon\kakalake2.wav",
            @".\kakalakophon\kakalake3.wav",
            @".\kakalakophon\kakalake4.wav",
            @".\kakalakophon\kakalake5.wav",
            @".\kakalakophon\kakalake6.wav" };
            
            //Initialize arrays
            soundPlayers = new SoundPlayer[6];
            labelArray = new Label[] { lblFirstWord, lblSecondWord, lblThirdWord, lblFourthWord, lblFifthWord, lblSixthWord };
            labelVoiceArray = new Label[] { lblVoiceOne, lblVoiceTwo, lblVoiceThree, lblVoiceFour, lblVoiceFive, lblVoiceSixth };
            picbArray = new PictureBox[] { picbRoachOne, picbRoachTwo, picbRoachThree, picbRoachFour, picbRoachFive, picbRoachSix };
            checkClicks = new bool[] { false, false, false, false, false, false };
            pointZero = new Point(0, 0);

            movementRandomizer = new Random();
            randomizer = new Random();
            Timer MainTimer = new Timer();
            Timer VoiceTimer = new Timer();
        }

        private void btnStart_Click(object sender, EventArgs e)
        {   
            //Checks if Classic or challenger mode, and takes the responding array
            if (radioClassic.Checked)
            { stringArraySentence = new string[] { "Wenn_bei_Capri_die rote_Sonne_im Meer versinkt" }; }
            else
            { 
                stringArraySentence = new string[] { "Wenn_bei_Capri_die rote_Sonne_im Meer versinkt", 
                "bei_Wenn_die rote_Sonne_Capri_im Meer versinkt", "im Meer versinkt_Sonne_die rote_Capri_bei_Wenn",
                "Capri_bei_die rote_Wenn_im Meer versinkt_Sonne","Wenn_im Meer versinkt_die rote_Capri_Sonne_bei",
                "Wenn_die rote_Capri_Sonne_im Meer versinkt_bei", "bei_Wenn_im Meer versinkt_Capri_die rote_Sonne",
                "bei_im Meer versinkt_Sonne_Capri_die rote_Wenn", "Wenn_bei_im Meer versinkt_die rote_Sonne_Capri"}; 
            }

            //Giving the roaches the pictures
            picbRoachOne.Image = new Bitmap(@".\kakalakophon\kakalake1.bmp");
            picbRoachTwo.Image = new Bitmap(@".\kakalakophon\kakalake2.bmp");
            picbRoachThree.Image = new Bitmap(@".\kakalakophon\kakalake3.bmp");
            picbRoachFour.Image = new Bitmap(@".\kakalakophon\kakalake4.bmp");
            picbRoachFive.Image = new Bitmap(@".\kakalakophon\kakalake5.bmp");
            picbRoachSix.Image = new Bitmap(@".\kakalakophon\kakalake6.bmp");


                MainTimer.Start();
                VoiceTimer.Start();
                RoachPlacement();

                foreach (PictureBox roaches in picbArray)
                {
                    roaches.Visible = true;
                    roaches.Enabled = true;
                }

            //taking care of buttons which should be visible / invisible for the game
            btnStart.Enabled = false;
            btnStart.Visible = false;
            panelRadio.Enabled = false;
            panelRadio.Visible = false;

            btnReplace.Enabled = true;
            btnReplace.Visible = true;

            //Generating random number to determine which sentence to use
            randomNumber = randomizer.Next(0, stringArraySentence.Length);
            
            //Choosing a sentence and putting it in its own array for further working
            currentArray = stringArraySentence[randomNumber].Split('_');

            //Checking the order of words and give the right the roach the right sound file
            for (int i = 0; i < 6; i++)
            {
                labelArray[i].Text = currentArray[i];
                labelVoiceArray[i].BackColor = Color.Transparent;
                labelVoiceArray[i].Parent = picbArray[i];

                Sound_File_Distributor(i);
            }
        }

        //Letting the roaches wiggle around
        private void MainTimer_Tick(object sender, EventArgs e)
        {
            lblPointsNumber.Text = playerPoints.ToString();

            foreach(PictureBox pictureBox in picbArray)
            {   
                //Where is the Roach
                Point currentPosition = pictureBox.Location;
                
                //Changing X
                int locationChange = movementRandomizer.Next(-1, 2);
                currentPosition.X += locationChange;
                Point futurePosition = new Point(currentPosition.X, 0);

                //Changing Y
                locationChange = movementRandomizer.Next(-1, 2);
                currentPosition.Y += locationChange;
                futurePosition.Y = currentPosition.Y;

                //Placing the Roach at the new position
                pictureBox.Location = futurePosition;
            }
        }
        #region Roaches
        private void picbRoachOne_Click(object sender, EventArgs e)
        {
            Roach_Play_Sound(0);
            Roach_Click_Voice(0);
            Checking_Click_Order(0);
        }

        private void picbRoachSix_Click(object sender, EventArgs e)
        {
            Roach_Play_Sound(5);
            Roach_Click_Voice(5);
            Checking_Click_Order(5);
        }

        private void picbRoachTwo_Click(object sender, EventArgs e)
        {
            Roach_Play_Sound(1);
            Roach_Click_Voice(1);
            Checking_Click_Order(1);
        }

        private void picbRoachThree_Click(object sender, EventArgs e)
        {
            Roach_Play_Sound(2);
            Roach_Click_Voice(2);
            Checking_Click_Order(2);
        }

        private void picbRoachFour_Click(object sender, EventArgs e)
        {
            Roach_Play_Sound(3);
            Roach_Click_Voice(3);
            Checking_Click_Order(3);        
        }

        private void picbRoachFive_Click(object sender, EventArgs e)
        {
            Roach_Play_Sound(4);
            Roach_Click_Voice(4);
            Checking_Click_Order(4);     
        }
        #endregion

        //Shows label with word when clicked on Roach
        private void Roach_Click_Voice(int i)
        {
            labelVoiceArray[i].Enabled = true;
            labelVoiceArray[i].Visible = true;
            labelVoiceArray[i].Width = 120;
            labelVoiceArray[i].Height = 20;
            labelVoiceArray[i].Text = currentArray[i];
            labelVoiceArray[i].Location = pointZero;
        }
        
        //Placing the roaches around the field
        private void RoachPlacement()
        {   //This approach only checks the last placement, however, if he cant find one the whole program is stuck
            /* Point newLocation = new Point(0, 0);
            
            for (int i = 0; i < 6; i++)
            {
                bool checkValidity;
                bool placeFound = false;
                
                while(!placeFound)
                {   
                    //Creating a random point for pic placement
                    placeFound = false;
                    newLocation = new Point(randomizer.Next(10, 455), randomizer.Next(65, 315));
                    checkValidity = true;

                    //If the new place gets too close to another picturebox, it has to retry
                    //e is smaller than i so it doesnt compare the place with the current box, since it gets relocated anyway
                    for (int e = 0; e < i; e++)
                        {
                        if (picbArray[e].Location.X - newLocation.X >= 120 || picbArray[e].Location.X - newLocation.X <= -120
                                || picbArray[e].Location.Y - newLocation.Y >= 100 || picbArray[e].Location.Y - newLocation.Y <= -100)
                            {}
                           else
                            { checkValidity = false; }
                        }
                    //New place result, if valid, the loop starts for the next pic, if not, repeat this instance
                    if (checkValidity)
                    {placeFound = true;}
                }                
                picbArray[i].Location = newLocation;
            }*/

            //This Loop takes longer, but there are no chances of crashes (place all pictures, then check if distance is okay)
            bool placeFound = false;
            while(!placeFound)
            {
                foreach(PictureBox pictureBox in picbArray)
                {pictureBox.Location = new Point(randomizer.Next(10, 455), randomizer.Next(65, 315)); }
                bool checkValidity = true;
                
                for(int i = 0; i < 6; i++)
                {
                    for (int e = 0; e < 6; e++)
                    {
                        if(e == i)
                        { continue; }
                        if (picbArray[e].Location.X - picbArray[i].Location.X >= 120 || picbArray[e].Location.X - picbArray[i].Location.X <= -120
                        || picbArray[e].Location.Y - picbArray[i].Location.Y >= 100 || picbArray[e].Location.Y - picbArray[i].Location.Y <= -100)
                        { }
                        else
                        { checkValidity = false; }
                    }
                }
                if (checkValidity)
                { placeFound = true; }
            }
        }

        //Main job is to decrease size of labels and disable if too small (roughly after 1 second)
        private void VoiceTimer_Tick(object sender, EventArgs e)
        {
            foreach(Label label in labelVoiceArray)
            {
                label.Width--;
                label.Height--;

                if(label.Height < 5)
                {
                    label.Visible = false;
                    label.Enabled = false;
                }
            }
        }

        //"Erschrecken" button - giving roaches new places
        private void btnReplace_Click(object sender, EventArgs e)
        {
            RoachPlacement();
            playerPoints--;
        }

        private void Roach_Play_Sound(int index)
        {
                soundPlayers[index].Play();       
        }

        private void Checking_Click_Order(int index)
        {

            //Checks if the roach clicked is in line with the click order, else it resets to 0;
            if(index == clickOrder)
            {
                clickOrder++;
                checkClicks[index] = true;
            }
            else
            {
                for(int i = 0; i < 6; i++)
                { checkClicks[i] = false; clickOrder = 0; }
            }
            
            //Checks if the last bool was set to true, generates a new random number (to choose a sentence), resets click order
            //and the bool array
            if(checkClicks[5] == true)
            {   //Taking a new array
                randomNumber = randomizer.Next(0, stringArraySentence.Length);
                currentArray = stringArraySentence[randomNumber].Split('_');

                //Messagebox informing player that he was indeed successful
                MessageBox.Show("Richtige Reihenfolge gedrückt! 10 Punkte erhalten, aber die Kakerlaken haben sich einen neuen Platz gesucht!",
                    "Erfolg!", MessageBoxButtons.OK, MessageBoxIcon.None);
                
                for (int i = 0; i < 6; i++)
                { 
                    labelArray[i].Text = currentArray[i]; 
                    labelVoiceArray[i].Enabled = false;
                    labelVoiceArray[i].Visible = false;

                    Sound_File_Distributor(i);
                }

                //resets click order to zero & all false, gives points and moves roaches to new locations
                for (int i = 0; i < 6; i++)
                { 
                    checkClicks[i] = false;
                    clickOrder = 0;
                }
                playerPoints += 10;
                RoachPlacement();
            }            
        }

        //Switch to distribute the sound file to the word said
        private void Sound_File_Distributor(int index)
        {
            switch (currentArray[index])
            {
                case "wenn":
                case "Wenn":
                    soundPlayers[index] = new SoundPlayer(roaches_Sound_Path_Array[2]);
                    break;
                case "bei":
                case "Bei":
                    soundPlayers[index] = new SoundPlayer(roaches_Sound_Path_Array[4]);
                    break;
                case "Capri":
                    soundPlayers[index] = new SoundPlayer(roaches_Sound_Path_Array[1]);
                    break;
                case "die rote":
                    soundPlayers[index] = new SoundPlayer(roaches_Sound_Path_Array[3]);
                    break;
                case "Sonne":
                    soundPlayers[index] = new SoundPlayer(roaches_Sound_Path_Array[5]);
                    break;
                case "im Meer versinkt":
                    soundPlayers[index] = new SoundPlayer(roaches_Sound_Path_Array[0]);
                    break;
            }
        }
    }
}