using FluentNHibernate.Mapping;
using StackOverflow.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StackOverflow.Core.Mappings
{
    public class CommentMappings : ClassMap<Comment>
    {
        public CommentMappings()
        {
            Id(c => c.Id).GeneratedBy.Identity();
            Map(c => c.Content).Length(2000);
            Map(c => c.CreatedAt);
            Map(c => c.IsAccepted);
            References(c => c.ApplicationUser);
            HasMany(c => c.CommentPoints).Cascade.AllDeleteOrphan();
            References(c => c.Post);
        }
    }
}
