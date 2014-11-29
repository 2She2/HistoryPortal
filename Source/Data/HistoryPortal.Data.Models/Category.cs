namespace HistoryPortal.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using HistoryPortal.Data.Common.Models;

    public class Category : DeletableEntity
    {
        public Category()
        {
            this.Articles = new HashSet<Article>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public virtual ICollection<Article> Articles { get; set; }
    }
}
