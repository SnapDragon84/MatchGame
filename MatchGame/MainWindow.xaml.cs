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
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow() //constructor
        {
            InitializeComponent();
            SetUpGame();    //call to method()
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
                int index = random.Next(animalEmoji.Count); //index is assigned random.Next (...Count) gets the number of elements from in/left in List <T>
                string nextEmoji = animalEmoji[index];  //nextEmoji is assigned animalEmoji at index selected from above
                textBlock.Text = nextEmoji; //nextEmoji is assigned to this particular TextBlock in the mainGrid panel
                animalEmoji.RemoveAt(index);    //as each animalEmoji is chosen by index, it is then removed from List<T> to avoid duplication
            }
        }
    }
}
