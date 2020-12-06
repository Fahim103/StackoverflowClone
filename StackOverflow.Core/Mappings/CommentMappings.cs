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
            Id(e => e.Id).GeneratedBy.Identity();
            Map(e => e.Content);
            Map(e => e.CreatedAt);
            Map(e => e.Points);
            Map(e => e.IsAccepted);
            References<ApplicationUser>(x => x.UserId).Column("UserId");
        }
    }
}
