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
        private QuestionChooseWindow _questionChoosewindow;
        private int _points;
        public GameWindow(QuestionChooseWindow win)
        {
            InitializeComponent();
            _questionChoosewindow = win;
        }

        public void ShowQuestion(QuestionAnswerPair question)
        {
            txtBlockQuestion.Text = question.Question;
            _points = question.Points;
        }

        private void FalseButton_Click(object sender, RoutedEventArgs e)
        {
            Hide();
            _questionChoosewindow.Show();
            _questionChoosewindow.QuestionFalse();
        }

        private void CorrectButton_Click(object sender, RoutedEventArgs e)
        {
            Hide();
            _questionChoosewindow.Show();
            _questionChoosewindow.QuestionRight(_points);
        }
    }
}
