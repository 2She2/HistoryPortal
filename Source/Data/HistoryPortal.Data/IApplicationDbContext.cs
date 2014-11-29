using HistoryPortal.Data.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HistoryPortal.Data
{
    public interface IApplicationDbContext
    {
        IDbSet<ApplicationUser> Users { get; set; }

        IDbSet<Article> Articles { get; set; }

        IDbSet<Category> Categories { get; set; }

        IDbSet<Comment> Comments { get; set; }

        IDbSet<EventComment> EventComments { get; set; }

        IDbSet<Event> Events { get; set; }

        IDbSet<Location> Locations { get; set; }

        IDbSet<Offer> Offers { get; set; }

        IDbSet<Tag> Tags { get; set; }

        IDbSet<Vote> Votes { get; set; }

        DbContext DbContext { get; }

        int SaveChanges();

        void Dispose();

        DbEntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class;

        IDbSet<T> Set<T>() where T : class;
    }
}
