using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBCats.Tables
{
    public class SaveImage
    {
        public int Id { get; set; }
        public string Facts { get; set; }
        public byte[] Image { get; set; }

        public SaveImage()
        {
        }

        public SaveImage(int id, string facts, byte[] image)
        {
            Id = id;
            Facts = facts;
            Image = image;
        }

        public override string ToString()
        {
            return $"Id:{Id} | Facts:{Facts} | ImageData: {Image.Length} bytes";
        }
    }
}
