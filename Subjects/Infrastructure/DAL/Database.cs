using AuthenticationApplication.Infrastructure.Entities;
using AuthenticationApplication.Infrastructure.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AuthenticationApplication.Infrastructure.DAL
{
    public class Database
    {
        public static List<User> Users = new List<User>()
        {
            new User() { Id = 1, Username = "a", Password = "a", Role = UserRole.Client }
        };

        public static List<Subject> Subjects = new List<Subject>()
        {
            new Subject() { Name = "Mathematical Analysis" },
            new Subject() { Name = "Geometry" },
            new Subject() { Name = "Algebra" }
        };
    }
}