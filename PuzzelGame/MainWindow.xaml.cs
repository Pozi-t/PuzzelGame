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

namespace PuzzelGame
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Button ButtonFirst, ButtonSecond;
        Button[,] gameField;
        public MainWindow()
        {
            ButtonFirst = null;
            ButtonSecond = null;
            Button button;
            string con, example = "GameButton";
            gameField = new Button[5, 5];
            int i = 0, j = 0;
            InitializeComponent();
            foreach (var item in gameBoard.Children)
            {
                if (item is Button)
                {
                    button = (Button)item;
                    con = (string)button.Content;
                    if (con.Contains(example))
                    {
                        con = con.Split(' ')[1];
                        i = int.Parse(con);
                        j = i % 10;
                        i = i / 10;
                        gameField[i, j] = button;
                        gameField[i, j].Background = Brushes.Yellow;
                    }
                }
            }
            NewGame();
        }
        private void Change(object sender, RoutedEventArgs e)
        {
            if (ButtonFirst is null) setFirst((Button)e.Source);
            else
            {
                int i = 0, j = 0;
                string con;
                ButtonSecond = (Button)e.Source;
                if (ButtonSecond.Background == Brushes.Yellow)
                {
                    con = (string)ButtonFirst.Content;
                    con = con.Split(' ')[1];
                    i = int.Parse(con);
                    j = i % 10;
                    i = i / 10;

                    if (i != 0)
                        if (gameField[i - 1, j] == ButtonSecond)
                            successfulChange();
                    if(i!= 4)
                        if (gameField[i + 1, j] == ButtonSecond)
                            successfulChange();
                    if(j!= 0)
                        if (gameField[i, j-1] == ButtonSecond)
                            successfulChange();
                    if(j!= 4)
                        if (gameField[i, j+1] == ButtonSecond)
                            successfulChange();
                    nullFirst();
                }
                else {
                    nullFirst();
                    setFirst(ButtonSecond);
                }
            }
        }        
        private void successfulChange()
        {
            ButtonSecond.Background = ButtonFirst.Background;
            ButtonFirst.Background = Brushes.Yellow;
            if (checkWin()) win();
        }
        private void setFirst(Button b)
        {
            b.BorderBrush = Brushes.Black;
            b.BorderThickness = new Thickness(10);
            ButtonFirst = b;
        }
        private void win()
        {
            MessageBox.Show("Поздравляю вы прошли игру");
            closeBoard();
        }
        private void openBoard()
        {
            for (int i = 0; i < 5; i++)
                for (int j = 0; j < 5; j++)
                    gameField[i, j].IsEnabled = true;
        }
        private void closeBoard()
        {
            for (int i = 0; i < 5; i++)
                for (int j = 0; j < 5; j++)
                    gameField[i, j].IsEnabled = false;
        }
        private bool checkWin()
        {
            bool green = true, red = true, blue = true;
            for (int i = 0; i < 5; i++)
            {
                if (gameField[i, 0].Background != Brushes.Green)
                {
                    green = false;
                    break;
                }
                if (gameField[i, 2].Background != Brushes.Blue)
                {
                    blue = false;
                    break;
                }
                if (gameField[i, 4].Background != Brushes.Red)
                {
                    red = false;
                    break;
                }
            }
            return green == red == blue;
        }
        private void nullFirst()
        {
            ButtonFirst.BorderThickness = new Thickness(1);
            ButtonFirst.BorderBrush = null;
            ButtonFirst = null;
        }
        private void colorize(int i)
        {
            for (int j = 0; j < 5; j++)
                if (j % 2 == 0)
                    gameField[j, i].Background = Brushes.Gray;
                else
                    gameField[j, i].Background = Brushes.Yellow;
        }
        private void colorizeChips()
        {
            int red = 5, blue = 5, green = 5, rand;
            Random random = new Random();
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j += 2)
                {
                    do
                    {
                        rand = random.Next(0, 3);
                        if (rand == 0 && red != 0)
                        {
                            gameField[i, j].Background = Brushes.Red;
                            red--;
                        }
                        else if (rand == 1 && blue != 0)
                        {
                            gameField[i, j].Background = Brushes.Blue;
                            blue--;
                        }
                        else if (rand == 2 && green != 0)
                        {
                            gameField[i, j].Background = Brushes.Green;
                            green--;
                        }
                        else gameField[i, j].Background = Brushes.Yellow;
                    } while (gameField[i, j].Background == Brushes.Yellow);
                }
            }
        }
        private void NewGame()
        {
            openBoard();
            colorizeChips();
            colorize(1);
            colorize(3);
        }
        private void Restart(object sender, RoutedEventArgs e)
        {
            NewGame();
        }
        private void Exit(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        
        private void Specification(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Приветствую в игре\n" +
                "Ваша главная задача менять местами цветные блоки и раставить их в линии в зависимости от верних блоков \n" +
                "Менять местами можно только с соседними по вертикали и горизонтали желтыми блоками \n" +
                "С серыми взаимодействовать нельзя \n" +
                "");


        }
    }
}
