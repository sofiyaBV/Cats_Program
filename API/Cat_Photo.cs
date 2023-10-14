using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace Cats_Program.API
{
    public class Cat_Photo
    {
        public BitmapImage ImageURL { get; set; }

        public Cat_Photo(BitmapImage imageURL)
        {
            ImageURL = imageURL;
        }
    }
}
