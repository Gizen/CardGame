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

namespace CardGameProject
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        int deckSize;
        int cardNumber = 1;
        int[] deck;
        Random rnd = new Random();
        int score = 0;
        bool higherOrLower;
        int nextCard = 0;
        bool choice;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            Reset();
            deckSize = Convert.ToInt32(textBoxDeckSize.Text);
            if (deckSize > 1)
            {
                deck = new int[deckSize];
                for (int i = 0; i < deckSize; i++)
                {
                    deck[i] = i + 1;
                }
                Shuffle();
                ChangeText();
            }
            else
            {
                textBlockGameOver.Text = "Stop trying to screw with my program and pick a number bigger than 1!";
            }

        }
        public void ScoreCalc()
        {
            if (choice == higherOrLower)
            {
                score++;
                cardNumber++;
                ChangeText();
            }
            else
            {
                cardNumber++;
                ChangeText();
            }
        }
        public void Reset()
        {
            score = 0;
            cardNumber = 1;
            nextCard = 0;
            buttonHigher.IsEnabled = true;
            buttonLower.IsEnabled = true;
            textBlockGameOver.Text = "";
            textBlockScore.Text = "";

        }
        public void Shuffle()
        {
            for (int i = 0; i < 1000000; i++)
            {
                int slot1 = rnd.Next(0, deckSize);
                int slot2 = slot1 - 1;
                if (slot2 == -1)
                {
                    slot2 = deckSize - 1;
                }
                int number1 = deck[slot1];
                int number2 = deck[slot2];
                deck[slot1] = number2;
                deck[slot2] = number1;

            }
        }
        public void WhenToStop()
        {
            if (cardNumber == deckSize)
            {
                buttonHigher.IsEnabled = false;
                buttonLower.IsEnabled = false;
                textBlockGameOver.Text = "Game Over, Press Start To Play Another Round.";
            }
        }
        public void NextCard()
        {
            if (deck[nextCard] > deck[nextCard + 1])
            {
                higherOrLower = false;
                nextCard++;
            }
            else if (deck[nextCard] < deck[nextCard + 1])
            {
                higherOrLower = true;
                nextCard++;
            }
        }

        private void buttonLower_Click(object sender, RoutedEventArgs e)
        {
            choice = false;
            NextCard();
            ScoreCalc();
            WhenToStop();
        }

        private void buttonHigher_Click(object sender, RoutedEventArgs e)
        {
            choice = true;
            NextCard();
            ScoreCalc();
            WhenToStop();
        }
        public void ChangeText()
        {
            textBlockCard.Text = Convert.ToString(deck[nextCard]);
            textBlockScore.Text = "Score: " + Convert.ToString(score);
            textBlockCardsLeft.Text = "Card " + cardNumber + " of " + deckSize;
        }
    }
}
