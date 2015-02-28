using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.RightsManagement;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Fragespiel_WPF
{
    /// <summary>
    /// Interaktionslogik für QuestionChooseWindow.xaml
    /// </summary>
    public partial class QuestionChooseWindow : Window
    {
        private GameWindow _gameWindow;

        private Label _lblGroup1;
        private Label _lblGroup2;
        private Label _lblGroup3;
        private Label _lblGroup4;

        private Player[] _players = new[]
        {new Player("Gruppe 1"), new Player("Gruppe 2"), new Player("Gruppe 3"), new Player("Gruppe 4")};

        private Player _curPlayer;

        public QuestionChooseWindow()
        {
            InitializeComponent();
            BuildQuestionGrid();
            _gameWindow = new GameWindow(this);
            _gameWindow.Hide();
            _curPlayer = _players[0];
        }

        private void BuildQuestionGrid()
        {
            string[] topics = QuestionPool.Instance.GetTopics();
            int[] points = QuestionPool.Instance.GetPoints();

            Grid grid = new Grid();
            grid.ShowGridLines = true;

            foreach (string topic in topics)
            {
                ColumnDefinition colDef = new ColumnDefinition();
                grid.ColumnDefinitions.Add(colDef);
            }
            grid.ColumnDefinitions.Add(new ColumnDefinition());

            foreach (int point in points)
            {
                RowDefinition rowDef = new RowDefinition();
                grid.RowDefinitions.Add(rowDef);
            }
            grid.RowDefinitions.Add(new RowDefinition());

            int i = 1;
            foreach (string topic in topics)
            {
                Label lbl = new Label();
                lbl.Content = topic;
                lbl.FontSize = 36;

                grid.Children.Add(lbl);
                Grid.SetColumn(lbl, i);
                Grid.SetRow(lbl, 0);
                i++;
            }

            int j = 1;
            foreach (int point in points)
            {
                Label lbl = new Label();
                lbl.Content = point;
                lbl.FontSize = 36;

                grid.Children.Add(lbl);
                Grid.SetColumn(lbl, 0);
                Grid.SetRow(lbl, j);
                j++;
            }

            for (int x = 1; x < grid.ColumnDefinitions.Count; x++)
            {
                for (int y = 1; y < grid.RowDefinitions.Count; y++)
                {
                    Button button = new Button();
                    button.Click += (sender, args) =>
                    {
                        Button clicked = sender as Button;
                        int column = Grid.GetColumn(clicked);
                        int row = Grid.GetRow(clicked);
                        string[] topicss = QuestionPool.Instance.GetTopics();
                        int[] pointss = QuestionPool.Instance.GetPoints();
                        var question = QuestionPool.Instance.GetQuestionByTopicAndAnswer(topicss[column - 1], points[row - 1]);
                        _gameWindow.ShowQuestion(question);
                        _gameWindow.Show();
                        clicked.Background = Brushes.Black;
                        Hide();
                    };

                    grid.Children.Add(button);
                    Grid.SetColumn(button, x);
                    Grid.SetRow(button, y);
                }
            }

            grid.RowDefinitions.Add(new RowDefinition());

            Label lblGroupPoints = new Label();
            lblGroupPoints.FontSize = 36;
            lblGroupPoints.Content = "Gruppenpunkte";
            Grid.SetColumn(lblGroupPoints, 0);
            Grid.SetRow(lblGroupPoints, grid.RowDefinitions.Count - 1);
            grid.Children.Add(lblGroupPoints);

            _lblGroup1 = new Label();
            _lblGroup1.FontSize = 36;
            _lblGroup1.Foreground = Brushes.Red;
            _lblGroup1.Content = "0";
            Grid.SetColumn(_lblGroup1, 1);
            Grid.SetRow(_lblGroup1, grid.RowDefinitions.Count - 1);
            grid.Children.Add(_lblGroup1);

            _lblGroup2 = new Label();
            _lblGroup2.FontSize = 36;
            _lblGroup2.Content = "0";
            Grid.SetColumn(_lblGroup2, 2);
            Grid.SetRow(_lblGroup2, grid.RowDefinitions.Count - 1);
            grid.Children.Add(_lblGroup2);

            _lblGroup3 = new Label();
            _lblGroup3.FontSize = 36;
            _lblGroup3.Content = "0";
            Grid.SetColumn(_lblGroup3, 3);
            Grid.SetRow(_lblGroup3, grid.RowDefinitions.Count - 1);
            grid.Children.Add(_lblGroup3);

            _lblGroup4 = new Label();
            _lblGroup4.FontSize = 36;
            _lblGroup4.Content = "0";
            Grid.SetColumn(_lblGroup4, 4);
            Grid.SetRow(_lblGroup4, grid.RowDefinitions.Count - 1);
            grid.Children.Add(_lblGroup4);

            GridWindow.Children.Add(grid);
        }

        public void QuestionFalse()
        {
            SwitchPlayer();
        }

        public void QuestionRight(int points)
        {
            _curPlayer.Points += points;

            SwitchPlayer();
        }

        private void SwitchPlayer()
        {
            switch (_curPlayer.Name)
            {
                case "Gruppe 1":
                    _lblGroup1.Content = _players[0].Points;
                    _lblGroup1.Foreground = Brushes.Black;
                    _lblGroup2.Foreground = Brushes.Red;
                    _curPlayer = _players[1];
                    break;
                case "Gruppe 2":
                    _lblGroup2.Content = _players[1].Points;
                    _lblGroup2.Foreground = Brushes.Black;
                    _lblGroup3.Foreground = Brushes.Red;
                    _curPlayer = _players[2];
                    break;
                case "Gruppe 3":
                    _lblGroup3.Content = _players[2].Points;
                    _lblGroup3.Foreground = Brushes.Black;
                    _lblGroup4.Foreground = Brushes.Red;
                    _curPlayer = _players[3];
                    break;
                case "Gruppe 4":
                    _lblGroup4.Content = _players[3].Points;
                    _lblGroup4.Foreground = Brushes.Black;
                    _lblGroup1.Foreground = Brushes.Red;
                    _curPlayer = _players[0];
                    break;
            }
        }
    }
}
