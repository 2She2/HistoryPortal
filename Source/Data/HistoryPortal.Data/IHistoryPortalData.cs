using HistoryPortal.Data.Common.Repository;
using HistoryPortal.Data.Models;
namespace HistoryPortal.Data
{
    public interface IHistoryPortalData
    {
        IApplicationDbContext Context { get; }

        IRepository<ApplicationUser> Users { get; }

        IDeletableEntityRepository<Article> Articles { get; }

        IDeletableEntityRepository<Category> Categories { get; }

        IDeletableEntityRepository<Comment> Comments { get; }

        IDeletableEntityRepository<Event> Events { get; }

        IDeletableEntityRepository<EventComment> EventComments { get; }

        IDeletableEntityRepository<Location> Locations { get; }

        IDeletableEntityRepository<Offer> Offers { get; }

        IDeletableEntityRepository<Tag> Tags { get; }

        IDeletableEntityRepository<Vote> Votes { get; }

        int SaveChanges();
    }
}
