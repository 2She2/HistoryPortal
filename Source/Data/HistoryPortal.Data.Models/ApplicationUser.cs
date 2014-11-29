namespace HistoryPortal.Data.Models
{
    using System;
    using System.Security.Claims;
    using System.Threading.Tasks;

    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;

    using HistoryPortal.Data.Common.Models;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Collections.Generic;

    // , IAuditInfo, IDeletableEntity
    public class ApplicationUser : IdentityUser
    {
        public ApplicationUser()
        {
            // This will prevent UserManager.CreateAsyncfrom causing exception
            //this.CreatedOn = DateTime.Now;
            this.Articles = new HashSet<Article>();
        }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }

        //public DateTime CreatedOn { get; set; }

        //public bool PreserveCreatedOn { get; set; }

        //public DateTime? ModifiedOn { get; set; }

        //[Index]
        //public bool IsDeleted { get; set; }

        //public DateTime? DeletedOn { get; set; }

        public virtual ICollection<Article> Articles { get; set; }
    }
}
