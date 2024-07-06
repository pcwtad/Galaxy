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
    internal class ChildeveConfigs : IEntityTypeConfiguration<Childeve>
    {
        public void Configure(EntityTypeBuilder<Childeve> builder)
        {
            builder.ToTable("T_Childeves");
            builder.HasKey(x => x.Id);
        }
    }
}
