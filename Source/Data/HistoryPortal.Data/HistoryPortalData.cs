using HistoryPortal.Data.Common.Models;
using HistoryPortal.Data.Common.Repository;
using HistoryPortal.Data.Models;
using HistoryPortal.Data.Repositories.Base;
using System;
using System.Collections.Generic;
using System.Data.Entity;
namespace HistoryPortal.Data
{
    public class HistoryPortalData : IHistoryPortalData
    {
        private readonly IApplicationDbContext context;
        private readonly IDictionary<Type, object> repositories;

        //public HistoryPortalData()
        //    : this(new ApplicationDbContext())
        //{

        //}

        public HistoryPortalData(IApplicationDbContext context)
        {
            this.context = context;
            this.repositories = new Dictionary<Type, object>();
        }

        public IApplicationDbContext Context
        {
            get
            {
                return this.context;
            }
        }

        public IRepository<ApplicationUser> Users
        {
            get { return this.GetRepository<ApplicationUser>(); }
        }

        public IDeletableEntityRepository<Article> Articles
        {
            get { return this.GetDeletableEntityRepository<Article>(); }
        }

        public IDeletableEntityRepository<Category> Categories
        {
            get { return this.GetDeletableEntityRepository<Category>(); }
        }

        public IDeletableEntityRepository<Comment> Comments
        {
            get { return this.GetDeletableEntityRepository<Comment>(); }
        }

        public IDeletableEntityRepository<EventComment> EventComments
        {
            get { return this.GetDeletableEntityRepository<EventComment>(); }
        }

        public IDeletableEntityRepository<Event> Events
        {
            get { return this.GetDeletableEntityRepository<Event>(); }
        }

        public IDeletableEntityRepository<Location> Locations
        {
            get { return this.GetDeletableEntityRepository<Location>(); }
        }

        public IDeletableEntityRepository<Offer> Offers
        {
            get { return this.GetDeletableEntityRepository<Offer>(); }
        }

        public IDeletableEntityRepository<Tag> Tags
        {
            get { return this.GetDeletableEntityRepository<Tag>(); }
        }

        public IDeletableEntityRepository<Vote> Votes
        {
            get { return this.GetDeletableEntityRepository<Vote>(); }
        }

        public int SaveChanges()
        {
            return this.context.SaveChanges();
        }

        public void Dispose()
        {
            this.Dispose(true);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (this.context != null)
                {
                    this.context.Dispose();
                }
            }
        }

        private IRepository<T> GetRepository<T>() where T : class
        {
            var typeOfRepository = typeof(T);
            if (!this.repositories.ContainsKey(typeOfRepository))
            {
                var newRepository = Activator.CreateInstance(typeof(GenericRepository<T>), context);
                this.repositories.Add(typeOfRepository, newRepository);
            }

            return (IRepository<T>)this.repositories[typeOfRepository];
        }

        private IDeletableEntityRepository<T> GetDeletableEntityRepository<T>() where T : class, IDeletableEntity
        {
            if (!this.repositories.ContainsKey(typeof(T)))
            {
                var type = typeof(DeletableEntityRepository<T>);
                this.repositories.Add(typeof(T), Activator.CreateInstance(type, this.context));
            }

            return (IDeletableEntityRepository<T>)this.repositories[typeof(T)];
        }
    }
}
