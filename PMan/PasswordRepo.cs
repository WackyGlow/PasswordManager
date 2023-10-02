using PMan.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMan
{
    internal class PasswordRepo
    {
        private readonly PasswordDbContext _ctx;
        public PasswordRepo(PasswordDbContext ctx)
        {
            _ctx = ctx;
        }

        public List<UserEntity> ReadAll() 
        {
            return _ctx.PasswordEntries
            .Select(e => new UserEntity
            {
                Id = e.Id,
                Username = e.Username,
                Website = e.Website,
                HashedPassword = e.HashedPassword
            })
            .ToList();
        }

    }
}
