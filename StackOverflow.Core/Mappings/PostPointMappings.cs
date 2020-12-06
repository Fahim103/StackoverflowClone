using FluentNHibernate.Mapping;
using StackOverflow.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StackOverflow.Core.Mappings
{
    class PostPointMappings : ClassMap<PostPoint>
    {
        public PostPointMappings()
        {
            Id(e => e.Id).GeneratedBy.Identity();
            Map(e => e.IsUpvoted);
            References<ApplicationUser>(x => x.UserId).Column("UserId");
            References<Post>(x => x.PostId).Column("PostId");
        }
    }
}
