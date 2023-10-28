using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserWebApi.Models;

namespace UserWebApi.Test
{
    public static class UserTestData
    {
        public static List<User> GetExistingUserProfile()
        {
            return new List<User>()
            {
                new User
                {
                    UserId = 1,
                    Username = "test1",
                    Name = "test1",
                    Email = "test1@test.com",
                    Password = "678",
                    Role = 0,
                    CreatedBy = "test1",
                    DateTimeCreated = DateTime.Now,
                    UpdatedBy = "test1",
                    DateTimeUpdated = DateTime.Now,
                },
                new User
                {
                    UserId = 2,
                    Username = "test2",
                    Name = "test2",
                    Email = "test2@test.com",
                    Password = "678",
                    Role = 0,
                    CreatedBy = "test2",
                    DateTimeCreated = DateTime.Now,
                    UpdatedBy = "test2",
                    DateTimeUpdated = DateTime.Now,
                }
            };
        }
    }
}
