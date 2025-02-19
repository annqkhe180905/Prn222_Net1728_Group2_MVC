using AutoMapper;
using BLL.DTOs;
using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Mapper
{
    public class MapperConfigure: Profile
    {
        public MapperConfigure() 
        {
            CreateMap<Category,CategoryVM> ().ReverseMap();
            CreateMap<NewsArticle,NewsArticleVM> ().ReverseMap();
            CreateMap<TagVM, Tag>().ReverseMap();
            CreateMap<SystemAccountVM,SystemAccountVM> ().ReverseMap();
        }
    }

}
