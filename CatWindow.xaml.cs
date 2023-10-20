using System.Collections.Generic;
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
using System.Windows.Input;

namespace Cats_Program
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class CatWindow : Window
    {
        public CatWindow()
        {
            InitializeComponent();
        }
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
            photo.Source = fact_and_photo.photo.GetPhoto();
            fact.Text = fact_and_photo.factsCat.ToString();
        }

        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            await ShowAsync();
        }

        private void Image_MouseDown(object sender, MouseButtonEventArgs e)
        {
            //if (sender is Image clickedImage)
            //{
                // Получите байты изображения, которое было нажато
                //byte[] image = GetImage(clickedImage);

                // Получите соответствующий факт из списка factsCets
                //int clickedImageIndex = LV.Items.IndexOf(clickedImage.DataContext); // Предполагается, что DataContext хранит информацию о факте

                //if (clickedImageIndex >= 0 && clickedImageIndex < factsCets.Count)
                //{
                //string fact = factsCets[clickedImageIndex].ToString();

                // Создайте экземпляр CatsDBContext
                //using (var dbContext = new CatsDBContext())
                //{
                // Создайте новую запись SaveImage и сохраните ее в базе данных
                //SaveImage saveImage = new SaveImage
                //{
                //    Facts = fact,
                //    Image = catPhoto
                //};
                //dbContext.SaveImage.Add(saveImage);
                //dbContext.SaveChanges();
                //}
                //}
            //}
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
    }

}

