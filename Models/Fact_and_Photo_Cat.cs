using Cats_Program.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cats_Program.Models
{
    public class Fact_and_Photo_Cat
    {
        public Cat_Photo photo { get; set; }
        public Cat_Fact factsCat { get; set; }

        public Fact_and_Photo_Cat(Cat_Photo photo, Cat_Fact factsCat)
        {
            this.photo = photo;
            this.factsCat = factsCat;
        }
    }
}
