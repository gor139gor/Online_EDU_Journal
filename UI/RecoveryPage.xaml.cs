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
using UI;

namespace WPF_edu_journal
{
    /// <summary>
    /// Interaction logic for RecoveryPage.xaml
    /// </summary>
    public partial class RecoveryPage : Window
    {
        public RecoveryPage()
        {
            InitializeComponent();
        }

        private void RemindPasswordButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow loginPage = new MainWindow();

            // Встановіть сторінку логіну як вміст поточного вікна
            Application.Current.MainWindow = loginPage;
            loginPage.Show();

            // Закрийте поточне вікно RecoveryPage
            this.Close();
        }
    }
}
