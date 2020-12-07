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
            Id(p => p.Id).GeneratedBy.Identity();
            Map(p => p.IsUpvoted);
            References(p => p.ApplicationUser);
            References(p => p.Post);
        }
    }
}
