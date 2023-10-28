using DBCats.Tables;
using DBCats;
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
using MaterialDesignThemes.Wpf;
using Microsoft.EntityFrameworkCore;

namespace Cats_Program
{
    /// <summary>
    /// Interaction logic for LogInWindow.xaml
    /// </summary>
    public partial class LogInWindow : Window
    {
        private SnackbarMessageQueue messageQueue = new SnackbarMessageQueue();

        public LogInWindow()
        {
            InitializeComponent();
            Snackbar.MessageQueue = messageQueue;
        }

        private async void LogIn_Click(object sender, RoutedEventArgs e)
        {
            string login = Login.Text;
            string password = Password.Password;

            try
            {
                bool isUserAuthenticated = await Authenticate(login, password);

                if (isUserAuthenticated)
                {
                    // Успешная аутентификация, выполните действия, которые вам необходимы
                    ShowSnackbar("Успешный вход!");
                    CatWindow catWindow = new CatWindow();
                    catWindow.Show();
                    Close();

                }
                else
                {
                    // Пользователь не найден, выполните действия по обработке ошибки
                    ShowSnackbar("Ошибка аутентификации");
                }
            }
            catch (Exception ex)
            {
                // Обработка ошибок базы данных
                ShowSnackbar("Произошла ошибка: " + ex.Message);
            }

        }

        private void Create_Click(object sender, RoutedEventArgs e)
        {
            SignUpWindow signup = new SignUpWindow();
            signup.Show();
            Close();
        }
        private void ShowSnackbar(string message)
        {
            messageQueue.Enqueue(message);
        }
        private async Task<bool> Authenticate(string login, string password)
        {
            using (var context = new CatsDBContext())
            {
                // Проверяем, существует ли пользователь с указанным логином и паролем
                var user = await context.User.SingleOrDefaultAsync(u => u.Login == login && u.Password == password);

                return user != null;
            }
        }

    }
}
