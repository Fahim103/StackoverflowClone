using FluentNHibernate.Mapping;
using StackOverflow.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StackOverflow.Core.Mappings
{
    public class PostMappings : ClassMap<Post>
    {
        public PostMappings()
        {
            Id(e => e.Id).GeneratedBy.Identity();
            Map(e => e.Title);
            Map(e => e.Content);
            Map(e => e.CreatedAt);
            References<ApplicationUser>(x => x.UserId).Column("UserId");
        }
    }
}
