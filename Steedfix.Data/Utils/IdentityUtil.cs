using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Steedfix.Domain;

namespace Steedfix.Data.Utils
{
    public class IdentityUtil
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public IdentityUtil(SteedfixContext context)
        {
            _userManager = new UserManager<User>(new UserStore<User>(context));
            _roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
        }

        /// <summary>
        /// Seed some users
        /// </summary>
        public void SeedUsers(string adminUserName)
        {
            if (!_roleManager.RoleExists("Admin")) _roleManager.Create(new IdentityRole() { Name = "Admin" });
            if (!_roleManager.RoleExists("Member")) _roleManager.Create(new IdentityRole() { Name = "Member" });

            CreateUser(adminUserName, new[] { "Steedfix", "Admin", "admin@steedfix.com" });
            AddUserToRole(adminUserName, "Admin");

            CreateUser("BradleyWiggins", new[] { "Bradley", "Wiggins", "bradley.wiggins@steedfix.com" });
            AddUserToRole("BradleyWiggins", "Member");

            CreateUser("TonyMartin", new[] { "Tony", "Martin", "tony.martin@steedfix.com" });
            AddUserToRole("TonyMartin", "Member");

            CreateUser("JensVoigt", new[] { "Jens ", "Voigt", "jens.voigt@steedfix.com" });
            AddUserToRole("JensVoigt", "Member");

        }

        /// <summary>
        /// Get a user id for a user name
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public string GetUserId(string userName)
        {
            var user = _userManager.FindByName(userName);
            return user != null ? user.Id : Guid.Empty.ToString();
        }

        /// <summary>
        /// Create a user with properties (if they don't exist already)
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="properties"></param>
        private void CreateUser(string userName, string[] properties)
        {
            if (_userManager.FindByName(userName) == null)
            {
                var user = new User()
                {
                    UserName = userName,
                    FirstName = properties[0],
                    Surname = properties[1],
                    Email = properties[2]
                };

                _userManager.Create(user, "password1");
            }
        }

        /// <summary>
        /// Add a user to a role (if they are not alredy in the role)
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="roleName"></param>
        private void AddUserToRole(string userName, string roleName)
        {
            var user = _userManager.FindByName(userName);

            if (user == null) return;

            if (!_userManager.IsInRole(user.Id, roleName))
            {
                _userManager.AddToRole(user.Id, roleName);
            }
        }
    }
}
