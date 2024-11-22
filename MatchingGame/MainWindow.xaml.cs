using MatchingGame.DTOs;
using MatchingGame.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Threading;

namespace MatchingGame
{
    public partial class MainWindow : Window
    {
        private List<Button> btnColors = new List<Button>();
        private List<Color> cardColors = new List<Color>();

        private Button firstCard, secondCard;
        private Color firstColor, secondColor;
        private DispatcherTimer flipBackTimer;
        private DispatcherTimer showCardsTimer;
        private short matchesCount = 0;
        private int score = 0;
        private bool allowClick = false;
        private bool thirdTimeClick = false;
        private static string imageURL = "C:/backsidecard.png";

        public MainWindow()
        {
            StartGame();
        }
        private void StartGame()
        {
            InitializeComponent();
            InitializeCardColors();
            CreateCards();
            SetTimers();
            SetDefaults();
        }

        private void InitializeCardColors()
        {
            cardColors = new List<Color>() {
            MatchingColors.MatchingColor1,
            MatchingColors.MatchingColor2,
            MatchingColors.MatchingColor3,
            MatchingColors.MatchingColor4,
            MatchingColors.MatchingColor5,
            MatchingColors.MatchingColor6,
            MatchingColors.MatchingColor7,
            MatchingColors.MatchingColor8,
            MatchingColors.MatchingColor1,
            MatchingColors.MatchingColor2,
            MatchingColors.MatchingColor3,
            MatchingColors.MatchingColor4,
            MatchingColors.MatchingColor5,
            MatchingColors.MatchingColor6,
            MatchingColors.MatchingColor7,
            MatchingColors.MatchingColor8,};
        }
        private void CreateCards()
        {
            ShuffleCards();

            int rows = 4;
            int columns = 4;
            MainGrid.RowDefinitions.Clear();
            MainGrid.ColumnDefinitions.Clear();
            btnColors.Clear();

            for (int i = 0; i < rows; i++)
            {
                MainGrid.RowDefinitions.Add(new RowDefinition());
            }
            for (int j = 0; j < columns; j++)
            {
                MainGrid.ColumnDefinitions.Add(new ColumnDefinition());
            }

            int index = 0;
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    CreateButton(i, j, index);
                    index++;
                }
            }
        }
        private void CreateButton(int row, int column, int index)
        {
            Button cardButton = new Button();
            cardButton.Name = "CardButton" + index;
            cardButton.Padding = new Thickness(0);
            cardButton.Background = new SolidColorBrush(cardColors[index]);
            cardButton.Foreground = new SolidColorBrush(cardColors[index]);
            cardButton.Click += Card_Click;

            cardButton.BorderThickness = new Thickness(1);
            cardButton.BorderBrush = Brushes.Black;

            Image cardImage = new Image();
            cardImage.Source = new BitmapImage(new Uri(imageURL));
            cardImage.Visibility = Visibility.Hidden;
            cardButton.Width = cardImage.Width;
            cardButton.Height = cardImage.Height;

            cardButton.Content = cardImage;

            AnimateButton(cardButton);
            Grid.SetRow(cardButton, row);
            Grid.SetColumn(cardButton, column);

            MainGrid.Children.Add(cardButton);
            btnColors.Add(cardButton);
        }
        public void AnimateButton(Button cardButton)
        {
            RotateTransform rotateTransform = new RotateTransform();
            cardButton.RenderTransform = rotateTransform;
            cardButton.RenderTransformOrigin = new Point(0.5, 0.5);

            cardButton.FocusVisualStyle = null;
            cardButton.Click += (sender, e) => FlipButton_Click(sender, e, rotateTransform);
        }

        private void SetTimers()
        {
            flipBackTimer = new DispatcherTimer
            {
                Interval = TimeSpan.FromMilliseconds(2000)
            };
            flipBackTimer.Tick += FlipBackTimer_Tick;
            showCardsTimer = new DispatcherTimer
            {
                Interval = TimeSpan.FromMilliseconds(5000)
            };
            showCardsTimer.Tick += ShowCardsTimer_Tick;
            showCardsTimer.Start();
        }
        private void SetDefaults()
        {
            score = 0;
            matchesCount = 0;
            allowClick = false;
            thirdTimeClick = false;
            lblMessage.Foreground = new SolidColorBrush(Colors.Pink);
            lblScoreValue.Text = score.ToString();
            firstCard = null;
            secondCard = null;
            firstColor = Colors.Transparent;
            secondColor = Colors.Transparent;

            ShowMessage(score, matchesCount, null);

        }
        private void ShowCardsTimer_Tick(object sender, EventArgs e)
        {

            showCardsTimer.Stop();
            allowClick = true;
            foreach (var card in btnColors)
            {
                Image clickedImage = (Image)card.Content;

                clickedImage.Visibility = Visibility.Visible;
            }
        }

        private void ShuffleCards()
        {
            Random rand = new Random();
            var shuffledColors = cardColors.OrderBy(x => rand.Next()).ToList();
            cardColors.Clear();
            cardColors.AddRange(shuffledColors);
        }

        private void RestartGameButton_Click(object sender, RoutedEventArgs e)
        {
            SetDefaults();
            ShuffleCards();
            short index = 0;
            foreach (var card in btnColors)
            {
                card.Background = new SolidColorBrush(cardColors[index]);
                card.Visibility = Visibility.Visible;
                Image clickedImage = (Image)card.Content;

                clickedImage.Visibility = Visibility.Hidden;
                index++;
            }
            showCardsTimer.Start();
            //SetTimers();

        }

        private void Card_Click(object sender, RoutedEventArgs e)
        {
            if (firstCard != null && secondCard != null)
            {
                thirdTimeClick = true;
                return;
            }
            if (!allowClick) return;
            thirdTimeClick = false;
            Button clickedCard = (Button)sender;
            Image clickedImage = (Image)clickedCard.Content;


            if (firstCard == null)
            {
                firstCard = clickedCard;
                firstColor = ((SolidColorBrush)clickedCard.Background).Color;
            }
            else if (secondCard == null && clickedCard != firstCard)
            {
                secondCard = clickedCard;
                secondColor = ((SolidColorBrush)clickedCard.Background).Color;
                flipBackTimer.Start();
            }
            clickedImage.Visibility = Visibility.Hidden;
        }

        private void FlipButton_Click(object sender, RoutedEventArgs e, RotateTransform rotateTransform)
        {
            if (!allowClick || thirdTimeClick) return;
            DoubleAnimation rotateAnimation = new DoubleAnimation
            {
                To = 10,
                Duration = new Duration(System.TimeSpan.FromSeconds(0.5)),  // Duration of the animation
                AutoReverse = true  // Optional: Reverse the flip back to original position
            };
            rotateTransform.BeginAnimation(RotateTransform.AngleProperty, rotateAnimation);
        }

        private void FlipBackTimer_Tick(object sender, EventArgs e)

        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                flipBackTimer.Stop();
            });
            var foundIt = false;
            allowClick = true;

            if (firstColor == secondColor)
            {
                score++;
                matchesCount++;
                firstCard.Visibility = Visibility.Hidden;
                secondCard.Visibility = Visibility.Hidden;
                foundIt = true;
                lblMessage.Foreground = new SolidColorBrush(Colors.Pink);
                //score--;

            }
            else
            {
                score--;
                var firstImage = (Image)firstCard.Content;
                var secondImage = (Image)secondCard.Content;

                firstImage.Visibility = Visibility.Visible;
                secondImage.Visibility = Visibility.Visible;
                lblMessage.Foreground = new SolidColorBrush(Colors.Red);
            }
            firstCard = null;
            secondCard = null;

            lblScoreValue.Text = Convert.ToString(score);
            ShowMessage(score, matchesCount, foundIt);
        }

        private void ShowMessage(int score, short matchCount, bool? foundIt)
        {
            lblMessage.Text = MessageService.GetMessage(score, matchCount, foundIt);
        }
    }
}
