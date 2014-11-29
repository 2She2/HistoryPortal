using HistoryPortal.Data.Common.Models;
using System;
namespace HistoryPortal.Data.Models
{
    public class Offer : DeletableEntity
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string AuthorId { get; set; }

        public virtual ApplicationUser Author { get; set; }
    }
}
