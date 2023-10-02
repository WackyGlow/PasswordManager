using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMan.Entity
{
    internal class UserEntity
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Website { get; set; }
        public string HashedPassword { get; set; }
    }
}
