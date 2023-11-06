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

namespace WPF_edu_journal
{
    /// <summary>
    /// Interaction logic for StMainpagecopy.xaml
    /// </summary>
    public partial class StMainpagecopy : Window
    {
        public StMainpagecopy()
        {
            InitializeComponent();
        }
    }
  /*  private void lessonTextBlock_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
    {
        TextBlock clickedTextBlock = sender as TextBlock;

        // Отримуємо текст з TextBlock
        string lessonInfo = clickedTextBlock.Text;
        // Створення нового вікна StMainpagecopy1
        StMainpagecopy1 newWindow = new StMainpagecopy1();

        // Створення та налаштування Image, який ви хочете відобразити
        Image lessonImage = new Image();
        lessonImage.Source = new BitmapImage(new Uri("icons/OpenSubj.png"));
        lessonImage.Width = 24; // Задайте необхідні розміри
        lessonImage.Height = 24;

        // Додайте Image до вікна StMainpagecopy1
        newWindow.Content = lessonImage;

        // Відобразіть нове вікно
        newWindow.Show();
    }*/
}
