using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBCats.Tables
{
    public class Account
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int SaveImageId { get; set; }



        public Account()
        {
        }

        public Account(int id, int userId, int saveImageId)
        {
            Id = id;
            UserId = userId;
            SaveImageId = saveImageId;
        }
    }
}
