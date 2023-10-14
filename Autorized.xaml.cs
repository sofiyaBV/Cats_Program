﻿using System;
using System.Data;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using MaterialDesignThemes.Wpf;
using System.Diagnostics;
using System.Threading;
using DBCats;
using Microsoft.EntityFrameworkCore;

namespace Cats_Program
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class Autorized : Window
    {
        private SnackbarMessageQueue messageQueue = new SnackbarMessageQueue();
        public Autorized()
        {
            InitializeComponent();
            Snackbar.MessageQueue = messageQueue;
        }


        private async void Enter_Click(object sender, RoutedEventArgs e)
        {
            string login = Login.Text;
            string password = Pass.Password;

            try
            {
                bool isUserAuthenticated = await Authenticate(login, password);

                if (isUserAuthenticated)
                {
                    // Успешная аутентификация, выполните действия, которые вам необходимы
                    ShowSnackbar("Успешный вход!");
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

        private void Reg_Click(object sender, RoutedEventArgs e)
        {
            // Создание и отображение окна регистрации
            Reg registrationWindow = new Reg();
            registrationWindow.ShowDialog();
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

        private void ShowSnackbar(string message)
        {
            messageQueue.Enqueue(message);
        }
    }
}
