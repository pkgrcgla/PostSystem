using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PostSystem_EL.Entities;
using PostSystem_EL.IdentityModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PostSystem_DL.ContextInfo
{
    public class PostSystemContext : IdentityDbContext<AppUser, AppRole, string>
    {
        public PostSystemContext(DbContextOptions<PostSystemContext> opt) : base(opt) { }

        public virtual DbSet<UserPost> UserPostTable { get; set; }
        public virtual DbSet<PostTag> PostTagTable { get; set; }
        public virtual DbSet<PostMedia> PostMediaTable { get; set; }
    }
}
