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
        public byte[] ImageData { get; set; }

        public SaveImage()
        {
        }

        public SaveImage(int id, string facts, byte[] imageData)
        {
            Id = id;
            Facts = facts;
            ImageData = imageData;
        }

        public override string ToString()
        {
            return $"Id:{Id} | Facts:{Facts} | ImageData: {ImageData.Length} bytes";
        }
    }
}
