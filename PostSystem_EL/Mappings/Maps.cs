using AutoMapper;
using PostSystem_EL.Entities;
using PostSystem_EL.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PostSystem_EL.Mappings
{
    public class Maps:Profile
    {
        public Maps()
        {
            CreateMap<UserPost,UserPostDTO>().ReverseMap();
            CreateMap<PostTag,PostTagDTO>().ReverseMap();
            CreateMap<PostMedia,PostMediaDTO>().ReverseMap();
        }
    }
}
