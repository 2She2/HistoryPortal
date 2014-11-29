using HistoryPortal.Data.Common.Models;
namespace HistoryPortal.Data.Models
{
    public class Location : DeletableEntity
    {
        public int Id { get; set; }

        public string Area { get; set; }

        public string City { get; set; }

        public string Longitude { get; set; }

        public string Latitude { get; set; }

        public string Details { get; set; }
    }
}
