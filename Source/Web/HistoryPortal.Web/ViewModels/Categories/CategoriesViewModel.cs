namespace HistoryPortal.Web.ViewModels
{
    using HistoryPortal.Data.Models;
    using HistoryPortal.Web.Infrastructure.Mapping;
    using System.ComponentModel.DataAnnotations;

    public class CategoriesViewModel : IMapFrom<Category>
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}