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
    /// Interaktionslogik für AddQuestionWindow.xaml
    /// </summary>
    public partial class AddQuestionWindow : Window
    {
        public AddQuestionWindow()
        {
            InitializeComponent();
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.MainWindow.Show();
            Close();
        }

        private void AddQuestion_Click(object sender, RoutedEventArgs e)
        {
            if (QuestionBox.Text != String.Empty && AnswerBox.Text != String.Empty && TopicBox.Text != String.Empty)
            {
                QuestionPool.Instance.Add(QuestionBox.Text, AnswerBox.Text, TopicBox.Text);
            }
        }
    }
}
