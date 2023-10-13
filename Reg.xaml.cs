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

namespace Cats_Program
{
    /// <summary>
    /// Логика взаимодействия для Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        public Window1()
        {
            InitializeComponent();
        }

        private void Reg_Click(object sender, RoutedEventArgs e)
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


    }
}
