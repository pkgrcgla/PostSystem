using AutoMapper;
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
    public class PostTagManager : Manager<PostTagDTO, PostTagDTO, int>
    {
        public PostTagManager(IRepository<PostTagDTO, int> repo, IMapper mapper) : base(repo, mapper, new string[] { "UserPost"})
        {
        }
    }
}
