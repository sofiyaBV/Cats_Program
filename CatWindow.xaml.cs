using System.IO;
using System.Threading.Tasks;
using System;
using System.Windows;
using System.Windows.Media.Imaging;
using Cats_Program.Data.API;
using Cats_Program.Domain.Models;
using DBCats.Tables;
using DBCats;
using System.Windows.Input;
using System.Windows.Controls;
using System.Collections.Generic;

namespace Cats_Program
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class CatWindow : Window
    {
        private List<Fact_and_Photo_Cat> likedCats = new List<Fact_and_Photo_Cat>();
        public CatWindow()
        {
            InitializeComponent();
        }
        private const int font_size = 15;
        private const int defWinHeight = 450;

        private int page = 1;
        private Cat_Photo? catPhoto;
        private Cat_Fact? catFact;
        private async Task GetRandomCat()
        {
            Cat_API_Client catApiClient = new Cat_API_Client();
            try
            {

                byte[] catImageData = await catApiClient.GetRandomCatImageAsync();
                if (catImageData != null)
                {
                    using (MemoryStream memoryStream = new MemoryStream(catImageData))
                    {
                        BitmapImage catImage = new BitmapImage();
                        catImage.BeginInit();
                        catImage.CacheOption = BitmapCacheOption.OnLoad;
                        catImage.StreamSource = memoryStream;
                        catImage.EndInit();
                        catPhoto = new Cat_Photo(catImage);
                    }
                }
                else
                {
                    MessageBox.Show("Не удалось получить изображение кота.");
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Произошла ошибка: " + ex.Message);
            }
        }
        private async Task GetRandomFact()
        {
            Fact_API_Client factsApiClient = new Fact_API_Client();
            try
            {
                string cat = await factsApiClient.GetFact();
                if (cat != null)
                {
                    catFact = new Cat_Fact(cat);
                }
                else
                {
                    MessageBox.Show("Не удалось получить изображение кота.");
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Произошла ошибка: " + ex.Message);
            }
        }
        private async Task ShowAsync()
        {
            await GetRandomCat();
            await GetRandomFact();
            Fact_and_Photo_Cat fact_and_photo = new Fact_and_Photo_Cat(catPhoto, catFact);
            photo.Source = null;
            fact.Text = null;
            likedCats.Add(fact_and_photo);
            photo.Source = fact_and_photo.photo.GetPhoto();
            fact.Text = fact_and_photo.factsCat.ToString();
        }

        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            await ShowAsync();
            bt_next.Width = window.Width - 100;
        }

        //private void Image_MouseDown(object sender, MouseButtonEventArgs e)
        //{
        //    if (sender is Image clickedImage)
        //    {
        //        // Получите байты изображения, которое было нажато
        //        byte[] image = GetImage(clickedImage);

        //        // Получите соответствующий факт из списка factsCets
        //        //int clickedImageIndex = LV.Items.IndexOf(clickedImage.DataContext); // Предполагается, что DataContext хранит информацию о факте
        //        //if (clickedImageIndex >= 0 && clickedImageIndex < factsCets.Count)
        //        //{
        //        //    string fact = factsCets[clickedImageIndex].ToString();

        //        //    // Создайте экземпляр CatsDBContext
        //        //    using (var dbContext = new CatsDBContext())
        //        //    {
        //        //        // Создайте новую запись SaveImage и сохраните ее в базе данных
        //        //        SaveImage saveImage = new SaveImage
        //        //        {
        //        //            Facts = fact, 
        //        //            Image = image
        //        //        };
        //        //        dbContext.SaveImage.Add(saveImage);
        //        //        dbContext.SaveChanges();
        //        //    }
        //        //}
        //    }
        //}
        private void Image_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (catPhoto != null)
            {
                using (var dbContext = new CatsDBContext())
                {
                    SaveImage saveImage = new SaveImage
                    {
                        Facts = catFact != null ? catFact.ToString() : null,
                        Image = GetImage(photo)
                    };
                    dbContext.SaveImage.Add(saveImage);
                    dbContext.SaveChanges();
                }
                MessageBox.Show("Изображение сохранено в базе данных.");
            }
            else
            {
                MessageBox.Show("Изображение не доступно для сохранения.");
            }
        }

        private byte[] GetImage(Image image)
        {
            byte[] imageData = null;

            if (image.Source is BitmapImage bitmapImage)
            {
                using (MemoryStream memoryStream = new MemoryStream())
                {
                    JpegBitmapEncoder encoder = new JpegBitmapEncoder();
                    encoder.Frames.Add(BitmapFrame.Create(bitmapImage));
                    encoder.Save(memoryStream);
                    imageData = memoryStream.ToArray();
                }
            }

            return imageData;
        }

        private async void tb_next_click(object sender, RoutedEventArgs e)
        {
            page++;
            await ShowAsync();
        }

        private void window_SizeChanged(object sender, RoutedEventArgs e)
        {
            double modifier = (window.Height / defWinHeight) * 10;
            btnLike.Height *= modifier;
            likeImg.Height *= modifier;
            btnMain.Height *= modifier;
            mainImg.Height *= modifier;
            btnLogout.Height *= modifier;
            logoutImg.Height *= modifier;
            bt_next.Width = window.Width - 100;
            //fact.FontSize = fact.FontSize *modifier;
        }

        private void bt_Like_Click(object sender, RoutedEventArgs e)
        {
            LikedWindow likedWindow = new LikedWindow();
            likedWindow.Show();
            Close();
        }

        private void btnLogout_Click(object sender, RoutedEventArgs e)
        {
            SignUpWindow signUpWindow = new SignUpWindow();
            signUpWindow.Show();
            Close();
        }

      

        private void tb_like_click(object sender, RoutedEventArgs e)
        {
            //if (catPhoto != null && catFact != null)
            //{
            //    likedCats.Add(new Fact_and_Photo_Cat(catPhoto, catFact));
            //    MessageBox.Show("Цей кіт був доданий в обрані!");
            //}
            if (catPhoto != null && catFact != null)
            {
                using (var dbContext = new CatsDBContext())
                {
                    SaveImage likedImage = new SaveImage
                    {
                        Facts = catFact.ToString(),
                        Image = GetImage(photo)
                    };
                    dbContext.SaveImage.Add(likedImage);
                    dbContext.SaveChanges();
                }
                MessageBox.Show("Зображення було додане в обрані!");
            }
        }

        private void btnGame_ClickAsync(object sender, RoutedEventArgs e)
        {
            WindowGamePyatnashki catWindow = new WindowGamePyatnashki(likedCats);
            catWindow.Show();
            Close();
        }
        
    }

}

