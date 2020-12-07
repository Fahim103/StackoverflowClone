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
            Id(p => p.Id).GeneratedBy.Identity();
            Map(p => p.Title).Length(512);
            Map(p => p.Content).Length(2000);
            Map(p => p.CreatedAt);
            References(p => p.ApplicationUser);
            HasMany(p => p.PostPoints).Cascade.AllDeleteOrphan();
            HasMany(p => p.Comments).Cascade.AllDeleteOrphan();
        }
    }
}
