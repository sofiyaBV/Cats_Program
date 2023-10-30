using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cats_Program.Domain.Models
{
    internal class LikedImage
    {
        public int Id { get; set; }
        public string Facts { get; set; }
        public byte[] Image { get; set; }
    }
}
