using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMan.Entity
{
    class HashedMPasswordEntity
    {
        public int Id { get; set; }
        public byte[] salt { get; set; }
        public string HashedMasterPassword { get; set; }
    }
}
