using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System;
using System.Windows;
using System.Windows.Media.Imaging;
using Cats_Program.Data.API;
using Cats_Program.Domain.Models;

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
        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            await ShowAsync();
        }
        List<Cat_Fact> factsCets = new List<Cat_Fact>();
        List<Cat_Photo> catPhotos = new List<Cat_Photo>();
        List<Fact_and_Photo_Cat> products = new List<Fact_and_Photo_Cat>();
        private async Task GetRandomCat(int count)
        {
            Cat_API_Client catApiClient = new Cat_API_Client();
            try
            {
                for (int i = 0; i < count; i++)
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

                            // Создание экземпляра CatPhoto с изображением
                            Cat_Photo catPhoto = new Cat_Photo(catImage);
                            //MessageBox.Show(catPhoto.ImageURL.Format.ToString());
                            // Добавление catPhoto в список ListView
                            //LV.Items.Add(catPhoto);
                            catPhotos.Add(catPhoto);

                        }
                    }
                    else
                    {
                        MessageBox.Show("Не удалось получить изображение кота.");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Произошла ошибка: " + ex.Message);
            }
        }
        private async Task GetRandomFact(int count)
        {
            Fact_API_Client factsApiClient = new Fact_API_Client();
            try
            {
                for (int i = 0; i < count; i++)
                {
                    string cat = await factsApiClient.GetFact();
                    if (cat != null)
                    {
                        Cat_Fact fact = new Cat_Fact(cat);
                        //MessageBox.Show(fact.ToString());
                        factsCets.Add(fact);
                    }
                    else
                    {
                        MessageBox.Show("Не удалось получить изображение кота.");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Произошла ошибка: " + ex.Message);
            }
        }
        private async Task ShowAsync()
        {
            await GetRandomCat(10);
            await GetRandomFact(10);

            int maxCount = Math.Min(catPhotos.Count, factsCets.Count);
            for (int i = 0; i < maxCount; i++)
            {
                products.Add(new Fact_and_Photo_Cat(catPhotos[i], factsCets[i]));
            }
           
            LV.ItemsSource = products;
        }

       
    }
}
