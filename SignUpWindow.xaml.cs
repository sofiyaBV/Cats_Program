using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using MaterialDesignThemes.Wpf;
using System.Diagnostics;
using System.Threading;
using DBCats;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Windows;
using System;
using DBCats.Tables;
using System.Linq;

namespace Cats_Program
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class SignUpWindow : Window
    {
        public SignUpWindow()
        {
            InitializeComponent();
        }

        private void CreateAccount_Click(object sender, RoutedEventArgs e)
        {
            string login = Login.Text;
            string password = Password.Password;

            if (!string.IsNullOrEmpty(login) && !string.IsNullOrEmpty(password))
            {
                using (var context = new CatsDBContext())
                {
                    // Проверка уникальности логина
                    var existingUser = context.User.FirstOrDefault(u => u.Login == login);
                    if (existingUser != null)
                    {
                        MessageBox.Show("Пользователь с таким логином уже существует.");
                    }
                    else
                    {
                        // Добавление нового пользователя в базу данных
                        context.User.Add(new User { Login = login, Password = password });
                        context.SaveChanges();
                        MessageBox.Show("Регистрация завершена. Теперь вы можете войти.");
                        Close(); // Закрыть окно регистрации
                    }
                }
            }
            else
            {
                MessageBox.Show("Заполните все поля.");
            }
        }
        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            Close(); // Закрыть окно регистрации
        }

        private void Enter_Click(object sender, RoutedEventArgs e)
        {
            Reg reg = new Reg();
            reg.Show();
            Close();
        }
    }
}
