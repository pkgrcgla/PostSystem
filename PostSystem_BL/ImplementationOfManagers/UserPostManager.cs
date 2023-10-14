using AutoMapper;
using PostSystem_BL.InterfacesOfManagers;
using PostSystem_DL.InterfaceOfRepos;
using PostSystem_EL.Entities;
using PostSystem_EL.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PostSystem_BL.ImplementationOfManagers
{
    public class UserPostManager : Manager<UserPostDTO,UserPost,long>,IUserPostManager
    {
        public UserPostManager(IUserPostRepo repo, IMapper mapper) : base(repo, mapper, new string[] { "AppUser" })
        {

        }
    }
}
