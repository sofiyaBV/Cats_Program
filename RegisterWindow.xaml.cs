using System.Windows;

namespace Cats_Program
{
    /// <summary>
    /// Interaction logic for RegisterWindow.xaml
    /// </summary>
    public partial class RegisterWindow : Window
    {
        public RegisterWindow()
        {
            InitializeComponent();
        }

        private void btnSignUp_Click(object sender, RoutedEventArgs e)
        {
            SignUpWindow signUpWindow = new SignUpWindow();
            signUpWindow.Show();
            Close();

        }

        private void btLogin_Click(object sender, RoutedEventArgs e)
        {
            LogInWindow login = new LogInWindow();
            login.Show();
            Close();

        }
    }
}
