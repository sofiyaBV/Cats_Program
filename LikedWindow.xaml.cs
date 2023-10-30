using Cats_Program.Data.API;
using Cats_Program.Domain.Models;
using DBCats.Tables;
using DBCats;
using System.Collections.Generic;
using System.Windows;
using System.Linq;
using System.Windows.Media.Imaging;
using System.IO;
using Microsoft.EntityFrameworkCore;

namespace Cats_Program
{
    /// <summary>
    /// Interaction logic for LikedWindow.xaml
    /// </summary>
    public partial class LikedWindow : Window
    {
        private const int defWinHeight = 450;
        private List<Fact_and_Photo_Cat> likedCats = new List<Fact_and_Photo_Cat>();
        public LikedWindow()
        {
            InitializeComponent();
            //likedCats = likedCat;
            //likedCatsLV.ItemsSource = likedCats;

            
        }
        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            likedCats = GetLikedCatsFromDatabase();
            
            likedCatsLV.ItemsSource = likedCats;
            //bt_next.Width = window.Width - 100;
        }

        private List<Fact_and_Photo_Cat> GetLikedCatsFromDatabase()
        {
            List<Fact_and_Photo_Cat> likedCats = new List<Fact_and_Photo_Cat>();

            using (var dbContext = new CatsDBContext())
            {
                var savedImages = dbContext.SaveImage.AsNoTracking().ToList();
                foreach (var savedImage in savedImages)
                {
                    // Створюємо BitmapImage з байтів зображення
                    BitmapImage bitmapImage = new BitmapImage();
                    using (MemoryStream memoryStream = new MemoryStream(savedImage.Image))
                    {
                        bitmapImage.BeginInit();
                        bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                        bitmapImage.StreamSource = memoryStream;
                        bitmapImage.EndInit();
                    }

                    // Створюємо об'єкт Cat_Photo та Cat_Fact
                    Cat_Photo catPhoto = new Cat_Photo(bitmapImage);
                    Cat_Fact catFact = new Cat_Fact(savedImage.Facts);

                    // Створюємо Fact_and_Photo_Cat та додаємо до списку likedCats
                    Fact_and_Photo_Cat factAndPhoto = new Fact_and_Photo_Cat(catPhoto, catFact);
                    likedCats.Add(factAndPhoto);
                }
            }

            return likedCats;
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
            WindowGamePyatnashki catWindow = new WindowGamePyatnashki(likedCats);
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
