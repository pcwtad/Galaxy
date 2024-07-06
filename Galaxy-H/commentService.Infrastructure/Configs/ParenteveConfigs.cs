using CommentService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace commentService.Infrastructure.Configs
{
    public class ParenteveConfigs : IEntityTypeConfiguration<Parenteve>
    {
        public void Configure(EntityTypeBuilder<Parenteve> builder)
        {
            builder.ToTable("T_Parenteves");
            builder.HasKey(x => x.Id);
        }
    }
}
