namespace HistoryPortal.Web.ViewModels.Articles
{
    using System;
    using System.Linq;

    using HistoryPortal.Data.Models;
    using HistoryPortal.Web.Infrastructure.Mapping;
    using HistoryPortal.Web.ViewModels.Comments;
    using System.Collections.Generic;
    using HistoryPortal.Web.ViewModels.Votes;
    using AutoMapper;

    public class ArticlesViewModel : IMapFrom<Article>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public IEnumerable<CommentViewModel> Comments { get; set; }

        public DateTime CreatedOn { get; set; }

        public CategoriesViewModel Category { get; set; }

        public int Ups { get; set; }

        public int Downs { get; set; }

        public void CreateMappings(IConfiguration configuration)
        {
            configuration.CreateMap<Article, ArticlesViewModel>()
               .ForMember(a => a.Ups, opt => opt.MapFrom(u => u.Votes.Where(v => v.State == VoteState.Up).Count()))
               .ForMember(a => a.Downs, opt => opt.MapFrom(u => u.Votes.Where(v => v.State == VoteState.Down).Count()))
               .ReverseMap();
        }
    }
}