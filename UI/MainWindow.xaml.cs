using System.Diagnostics;
using System.Windows;
using BLL;
using DAL;
using WPF_edu_journal;

namespace UI
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            var username = usernameTextBox.Text;
            var password = passwordBox.Password;
            var authService = new AuthenticationService(new JournalDbContext());

            // Перевірка через AuthenticationService
            var pageName = authService.AuthenticateUser(username, password);

            if (pageName != null)
            {
                // Успішний вхід
                NavigateToPage(pageName);
            }
            else
            {
                MessageBox.Show("Невірний логін або пароль. Спробуйте ще раз.");
            }
        }

        private void NavigateToPage(string pageName)
        {
            Window? page = pageName switch
            {
                "AdminPage" => new AdminPage(),
                "StudentPage" => new StudentPage(),
                "TeacherPage" => new TeacherPage(),
                _ => null
            };

            page?.Show();
            this.Close();
        }

        private void RecoveryLink_Click(object sender, RoutedEventArgs e)
        {
            RecoveryPage recoveryPage = new RecoveryPage();
            recoveryPage.Show();
            this.Close();
        }

        private void Hyperlink_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Process.Start(new ProcessStartInfo
                {
                    FileName = "https://t.me/EduJ_Support",
                    UseShellExecute = true
                });
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Помилка. Не вдається відкрити посилання. Пряме посилання: https://t.me/EduJ_Support {ex.Message}");
            }
        }

    }
}