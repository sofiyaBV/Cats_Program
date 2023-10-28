using Cats_Program.Data.API;
using Cats_Program.Domain.Models;
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
    /// Interaction logic for WindowGamePytnashki.xaml
    /// </summary>
    public partial class WindowGamePytnashki : Window
    {
        private List<Fact_and_Photo_Cat> likedCats = new List<Fact_and_Photo_Cat>();
        private int _x;
        private int _y;


        private Dictionary<int, FButton> _buttons =
            new Dictionary<int, FButton>(16);

        public class FButton : Button
        {
            public int X;
            public int Y;
        }
        public WindowGamePytnashki(List<Fact_and_Photo_Cat> likedCat)
        {
            InitializeComponent();
            likedCats = likedCat;
            fullButton();
            Random();
            //likedCatsLV.ItemsSource = likedCats;
        }

        private void fullButton()
        {
            int i = 0;
            foreach (var obj in grid.Children)
            {
                if (obj is FButton)
                {
                    var btn = (FButton)obj;
                    var image = new Image
                    {
                        Source = likedCats[i].photo,

                        //Source = catPhotos[i].ImageURL,
                        Stretch = System.Windows.Media.Stretch.Fill
                    };
                    btn.Content = image;
                    btn.X = Grid.GetRow(btn);
                    btn.Y = Grid.GetColumn(btn);
                    btn.Padding = new Thickness(10);
                    btn.Click += OnFButtonClick;
                    _buttons.Add(i++, btn);

                    var catPhoto = likedCats[i].photo;
                    LV.Items.Add(catPhoto);

                }
            }
        }

        private void OnFButtonClick(object sender, RoutedEventArgs e)
        {
            var button = (FButton)sender;
            int x = Grid.GetRow(button);
            int y = Grid.GetColumn(button);

            var down = Keyboard.IsKeyDown(Key.LeftCtrl);

            if ((down && (Math.Abs(_x - x) == 1
                 || Math.Abs(_y - y) == 1)) ||
                ((Math.Abs(_x - x) == 1 && _y == y)
                 || (Math.Abs(_y - y) == 1 && _x == x)))
            {
                Grid.SetRow(button, _x);
                Grid.SetColumn(button, _y);
                _x = x; _y = y;
            }
            else return;

            if (!_new) return;

            bool ok = _buttons.Values
                .Where(b => b != null)
                .All(b => b.X == Grid.GetRow(b)
                       && b.Y == Grid.GetColumn(b));

            if (!ok) return;

            MessageBox.Show("Игра закончена!");

            _new = false;
        }

        private bool _new;
        private void Random()
        {
            _new = true;
            var r = new Random();
            var buttons = _buttons.Values.Where(b => b != null).ToList();

            int n = buttons.Count;
            for (int i = 1; i <= n; i++)
            {
                int index = r.Next(buttons.Count);
                var button = buttons[index];

                Grid.SetRow(button, i / 4);
                Grid.SetColumn(button, i % 4);
                button.X = i / 4;
                button.Y = i % 4;
                buttons.RemoveAt(index);
            }
        }
    }
    
}



