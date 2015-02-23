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
using System.Windows.Shapes;

namespace Fragespiel_WPF
{
    /// <summary>
    /// Interaktionslogik für GameWindow.xaml
    /// </summary>
    public partial class GameWindow : Window
    {
        private Player[] _players;
        private int _curPlayerIndex;
        private Player _curPlayer;
        public GameWindow()
        {
            InitializeComponent();
            _players = new[] 
            { new Player("Gruppe 1"), new Player("Gruppe 2"), new Player("Gruppe 3"), new Player("Gruppe 4") };

            _curPlayer = _players[0];
            _curPlayerIndex = 0;
        }

        public void Start()
        {
            lblChoose.Content = _curPlayer.Name + ", wählen Sie ein Thema";
            if (QuestionPool.Instance.GetTopics().Length == 0)
            {
                EndGame();
            }
            cmbChoose.Items.Clear();
            foreach (String topic in QuestionPool.Instance.GetTopics())
            {
                cmbChoose.Items.Add(topic);
            }
        }

        private void EndGame()
        {
            Player winner = new Player("dummy");
            foreach (Player player in _players)
            {
                if (winner.Points < player.Points)
                {
                    winner = player;
                }
            }
            MessageBox.Show("Gewinner ist " + winner.Name, "Game Over");
            Application.Current.MainWindow.Show();
            Close();
        }

        private void cmbChoose_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cmbChoose.Items.Count > 0 && cmbChoose.SelectedItem != null)
            {
                lblQuestion.Content = QuestionPool.Instance.GetRandomQuestion((String) cmbChoose.SelectedItem).Question;
            }
        }

        private void FalseButton_Click(object sender, RoutedEventArgs e)
        {
            SwitchPlayer();
        }

        private void CorrectButton_Click(object sender, RoutedEventArgs e)
        {
            _curPlayer.Points++;
            SwitchPlayer();
        }

        private void SwitchPlayer()
        {
            lblQuestion.Content = String.Empty;

            if (_curPlayerIndex == 3)
            {
                _curPlayerIndex = 0;
            }
            else
            {
                _curPlayerIndex++;
            }

            _curPlayer = _players[_curPlayerIndex];

            Start();
        }

    }
}
