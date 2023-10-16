using Cats_Program.API;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System;
using System.Windows;
using System.Windows.Media.Imaging;
using Cats_Program.Models;

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

        private async void tb_next_click(object sender, RoutedEventArgs e)
        {
            page++;
            await ShowAsync();
        }
    }
}
