using AutoMapper;
using PostSystem_DL.InterfaceOfRepos;
using PostSystem_EL.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PostSystem_BL.ImplementationOfManagers
{
    public class UserPostManager : Manager<UserPostDTO, UserPostDTO, long>
    {
        public UserPostManager(IRepository<UserPostDTO, long> repo, IMapper mapper) : base(repo, mapper, new string[] { "AppUser" })
        {
        }
    }
}
