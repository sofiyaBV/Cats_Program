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
    }
}
