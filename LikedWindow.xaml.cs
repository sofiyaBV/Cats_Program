using System.Windows;

namespace Cats_Program
{
    /// <summary>
    /// Interaction logic for LikedWindow.xaml
    /// </summary>
    public partial class LikedWindow : Window
    {
        private const int defWinHeight = 450;

        public LikedWindow()
        {
            InitializeComponent();
        }

        private void window_SizeChanged(object sender, RoutedEventArgs e)
        {
            double modifier = (window.Height / defWinHeight) * 10;
            btnLike.Height *= modifier;
            //likeImg.Height *= modifier;
            btnMain.Height *= modifier;
            //mainImg.Height *= modifier;
            btnLogout.Height *= modifier;
            //logoutImg.Height *= modifier;
            //fact.FontSize = fact.FontSize *modifier;
        }

        private void btnLogout_Click(object sender, RoutedEventArgs e)
        {
            SignUpWindow signUpWindow = new SignUpWindow();
            signUpWindow.Show();
            Close();
        }

        private void btnMain_Click(object sender, RoutedEventArgs e)
        {
            CatWindow catWindow = new CatWindow();
            catWindow.Show();
            Close();
        }
    }
}
