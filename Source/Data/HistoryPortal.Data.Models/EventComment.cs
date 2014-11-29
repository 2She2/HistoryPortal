using HistoryPortal.Data.Common.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HistoryPortal.Data.Models
{
    public class EventComment : DeletableEntity
    {
        [Key]
        public int Id { get; set; }

        public string Content { get; set; }

        public string AuthorId { get; set; }

        public virtual ApplicationUser Author { get; set; }

        public string AuthorName { get; set; }

        public int EventId { get; set; }

        public virtual Event Event { get; set; }
    }
}
