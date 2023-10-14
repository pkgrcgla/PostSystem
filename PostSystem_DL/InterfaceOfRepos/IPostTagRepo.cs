using PostSystem_EL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PostSystem_DL.InterfaceOfRepos
{
    public interface IPostTagRepo: IRepository<PostTag,long>
    {
    }
}
