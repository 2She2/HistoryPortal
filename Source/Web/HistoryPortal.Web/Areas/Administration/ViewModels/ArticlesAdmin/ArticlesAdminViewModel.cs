using AutoMapper;
using HistoryPortal.Data.Models;
using HistoryPortal.Web.Areas.Administration.ViewModels.Base;
using HistoryPortal.Web.Areas.Administration.ViewModels.CategoriesAdminViewModel;
using HistoryPortal.Web.Infrastructure.Mapping;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HistoryPortal.Web.Areas.Administration.ViewModels.ArticlesAdmin
{
    public class ArticlesAdminViewModel : IMapFrom<Article>, IHaveCustomMappings
    {
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 3)]
        public string Title { get; set; }

        [StringLength(100, MinimumLength = 3)]
        public string AuthorName { get; set; }

        public CategoryViewModel Category { get; set; }

        public ArticleState State { get; set; }

        [HiddenInput(DisplayValue = false)]
        public int Comments { get; set; }

        [HiddenInput(DisplayValue = false)]
        public int Votes { get; set; }

        [HiddenInput(DisplayValue = false)]
        public DateTime CreatedOn { get; set; }

        [HiddenInput(DisplayValue = false)]
        public DateTime? ModifiedOn { get; set; }

        public void CreateMappings(IConfiguration configuration)
        {
            configuration.CreateMap<Article, ArticlesAdminViewModel>()
               .ForMember(a => a.AuthorName, v => v.MapFrom(a => a.Author.UserName))
               .ForMember(a => a.Comments, v => v.MapFrom(a => a.Comments.Count()))
               .ForMember(a => a.Votes, v => v.MapFrom(a => a.Votes.Count()))
               .ReverseMap();
        }
    }
}