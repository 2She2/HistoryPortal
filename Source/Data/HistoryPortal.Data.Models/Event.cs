using HistoryPortal.Data.Common.Models;
using System;
using System.Collections.Generic;
namespace HistoryPortal.Data.Models
{
    public class Event : DeletableEntity
    {
        public Event()
        {
            this.Comments = new HashSet<EventComment>();
            this.Votes = new HashSet<Vote>();
            this.Offers = new HashSet<Offer>();
            this.Users = new HashSet<ApplicationUser>();
        }

        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public DateTime Date { get; set; }

        public int? LocationId { get; set; }

        public virtual Location Location { get; set; }

        public string AuthorId { get; set; }

        public virtual ApplicationUser Author { get; set; }

        public virtual ICollection<EventComment> Comments { get; set; }

        public virtual ICollection<Vote> Votes { get; set; }

        public virtual ICollection<Offer> Offers { get; set; }

        public virtual ICollection<ApplicationUser> Users { get; set; }
    }
}
