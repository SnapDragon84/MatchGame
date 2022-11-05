using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MatchGame
{
    //To add a timer to our game code, we must 1st add the following import from another namespace
    using System.Windows.Threading;
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DispatcherTimer timer = new DispatcherTimer();  //initializes a new instance of the DispatcherTimer class
        int tenthsOfSecondsElapsed; //to keep track of the elapsed time
        int matchesFound;   //to keep track of the number of matches the player has found

        public MainWindow() //constructor
        {
            timer.Interval = TimeSpan.FromSeconds(.1);  //tell the timer how frequently to "tick"
            timer.Tick += Timer_Tick;   //as soon as we typed += the IDE displayed "Press TAB to insert" & added "Timer_Tick;" & the method below

            InitializeComponent();
            SetUpGame();    //call to method()
        }

        //The Timer_Tick method will update a TextBlock that spans the entire bottom row of the grid.
        private void Timer_Tick(object sender, EventArgs e)
        {
            tenthsOfSecondsElapsed++;
            timeTextBlock.Text = (tenthsOfSecondsElapsed / 10F).ToString("0.0s"); //replaces text "Elapsed time"
            if (matchesFound == 8)
            {
                timer.Stop();
                timeTextBlock.Text = timeTextBlock.Text + " - Play again?";
            }
            //throw new NotImplementedException();
        }

        private void SetUpGame()
        {            
            //throw new NotImplementedException();
            List<string> animalEmoji = new List<string>()   //Windows LoGo button on keyboard + period (.)
            {
                "🐙", "🐙",
                "🐨", "🐨",
                "🐘", "🐘",
                "🐅", "🐅",
                "🐫", "🐫",
                "🐖", "🐖",
                "🦆", "🦆",
                "🦅", "🦅",
            };

            Random random = new Random();   //create a new random number generator

            foreach(TextBlock textBlock in mainGrid.Children.OfType<TextBlock>())   //iterates over/trhough each TextBlock in mainGrid
            {
                if (textBlock.Name != "timeTextBlock")
                {
                    textBlock.Visibility = Visibility.Visible;
                    int index = random.Next(animalEmoji.Count); //index is assigned random.Next (...Count) gets the number of elements from in/left in List <T>
                    string nextEmoji = animalEmoji[index];  //nextEmoji is assigned animalEmoji at index selected from above
                    textBlock.Text = nextEmoji; //nextEmoji is assigned to this particular TextBlock in the mainGrid panel
                    animalEmoji.RemoveAt(index);    //as each animalEmoji is chosen by index, it is then removed from List<T> to avoid duplication
                }
            }

            timer.Start();
            tenthsOfSecondsElapsed = 0;
            matchesFound = 0;   //initially set to 0 here
        }

        //fields
        private TextBlock lastTextBlockClicked; //VS prompted for "accessibility modifier required" -> chose private
        private bool findingMatch = false;

        private void TextBlock_MouseDown(object sender, MouseButtonEventArgs e) //auto created when double-click in MouseDown property in Event Handler
        {
            TextBlock textBlock = sender as TextBlock;
            if (findingMatch == false)
            {
                textBlock.Visibility = Visibility.Hidden;
                lastTextBlockClicked = textBlock;
                findingMatch = true;
            }
            else if (textBlock.Text == lastTextBlockClicked.Text)
            {
                matchesFound++;
                textBlock.Visibility = Visibility.Hidden;
                findingMatch = false;
            }
            else
            {
                lastTextBlockClicked.Visibility = Visibility.Visible;
                findingMatch = false;
            }
        }

        private void timeTextBlock_MouseDown(object sender, MouseButtonEventArgs e) //checks the matchesFound field
        {
            if(matchesFound == 8)
            {
                SetUpGame();    //resets the game if all 8 pairs have been found; otherwise it will do nothing because the game is still running
            }
        }
    }
}
