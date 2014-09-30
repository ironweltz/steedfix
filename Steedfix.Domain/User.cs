using System.Runtime.Serialization;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Newtonsoft.Json;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Steedfix.Domain
{
    [DataContract]
    public class User: IdentityUser
    {
        public string Title { get; set; }

        public string FirstName { get; set; }

        public string Surname { get; set; }

        [DataMember]
        public override string UserName { get; set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<User> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }

    }
}
