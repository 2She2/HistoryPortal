using AutoMapper;
using HistoryPortal.Data.Models;
using HistoryPortal.Web.Areas.Administration.ViewModels.Base;
using HistoryPortal.Web.Infrastructure.Mapping;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HistoryPortal.Web.Areas.Administration.ViewModels.CommentsAdmin
{
    public class CommentsAdminViewModel : AdministrationViewModel, IMapFrom<Comment>, IHaveCustomMappings
    {
        public int Id { get; set; }

        [Required]
        public string Content { get; set; }

        public string AuthorName { get; set; }

        public string ArticleTitle { get; set; }

        public void CreateMappings(IConfiguration configuration)
        {
            configuration.CreateMap<Comment, CommentsAdminViewModel>()
               .ForMember(a => a.ArticleTitle, v => v.MapFrom(a => a.Article.Title))
               .ForMember(a => a.AuthorName, v => v.MapFrom(a => a.AuthorName))
               .ReverseMap();
        }
    }
}