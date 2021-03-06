﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Blog.Business;
using Blog.Data;

namespace Blog.Presentation
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Post, PostModel>()
                .ReverseMap()
                .ForMember(p => p.Publication, c => c.MapFrom(p => DateTime.Now));

            CreateMap<User, PostAuthorInfoModel>();

            CreateMap<User, UserModel>()
                .ReverseMap();

            CreateMap<Data.Blog, BlogModel>()
                .ReverseMap();

            CreateMap<Data.Blog, BlogListItemModel>();
        }
    }
}
