using HistoryPortal.Data.Common.Models;
using System;
namespace HistoryPortal.Data.Models
{
    public class Comment : DeletableEntity
    {
        public int Id { get; set; }

        public string Content { get; set; }

        public string AuthorId { get; set; }

        public virtual ApplicationUser Author { get; set; }

        public string AuthorName { get; set; }

        public int ArticleId { get; set; }

        public virtual Article Article { get; set; }
    }
}
