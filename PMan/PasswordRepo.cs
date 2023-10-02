using Konscious.Security.Cryptography;
using PMan.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Intrinsics.Arm;
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

        public User Create(User user)
        {
            var userEntity = _ctx.Add(new UserEntity()
            {
                Id = user.Id, HashedPassword = user.Password, Website = user.Website
            }).Entity;
            _ctx.SaveChanges();
            return user;
        }

    }
}
