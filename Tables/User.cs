using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBCats.Tables
{
    public class User
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public User()
        {
        }

        public User(int id, string login, string password)
        {
            Id = id;
            Login = login;
            Password = password;
        }

        public override string ToString()
        {
            return $"Id:{Id} | Login:{Login} | Password: {Password}";
        }
    }
}
