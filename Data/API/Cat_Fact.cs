using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace Cats_Program.Data.API
{
    public class Cat_Fact
    {
        public string Data { get; set; }

        public Cat_Fact(string data)
        {
            Data = data;
        }

        public override string ToString()
        {
            return Data;
        }
    }
}
