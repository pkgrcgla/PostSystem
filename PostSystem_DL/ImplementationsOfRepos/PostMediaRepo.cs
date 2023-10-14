using PostSystem_DL.ContextInfo;
using PostSystem_DL.InterfaceOfRepos;
using PostSystem_EL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PostSystem_DL.ImplementationsOfRepos
{
    public class PostMediaRepo : Repository<PostMedia, long>, IPostMediaRepo
    {
        public PostMediaRepo(PostSystemContext context) : base(context)
        {
        }
    }
}
