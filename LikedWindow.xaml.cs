using Cats_Program.Domain.Models;
using System.Collections.Generic;
using System.Windows;

namespace Cats_Program
{
    /// <summary>
    /// Interaction logic for LikedWindow.xaml
    /// </summary>
    public partial class LikedWindow : Window
    {
        private const int defWinHeight = 450;
        private List<Fact_and_Photo_Cat> likedCats = new List<Fact_and_Photo_Cat>();
        public LikedWindow(List<Fact_and_Photo_Cat> likedCat)
        {
            InitializeComponent();
            likedCats = likedCat;
            likedCatsLV.ItemsSource = likedCats;
        }

        private void window_SizeChanged(object sender, RoutedEventArgs e)
        {
            double modifier = (window.Height / defWinHeight) * 10;
            btnLike.Height *= modifier;    
            btnMain.Height *= modifier;
            btnLogout.Height *= modifier;
            
        }

        private void btnLogout_Click(object sender, RoutedEventArgs e)
        {
            WindowGamePytnashki catWindow = new WindowGamePytnashki(likedCats);
            catWindow.Show();
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
