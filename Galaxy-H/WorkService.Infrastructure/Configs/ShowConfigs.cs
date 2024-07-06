using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkService.Domain.Entities;

namespace WorkService.Infrastructure.Configs
{
    public class ShowConfigs : IEntityTypeConfiguration<Show>
    {
        public void Configure(EntityTypeBuilder<Show> builder)
        {
            builder.ToTable("T_Shows");
            builder.HasKey(x => x.Id);
        }
    }
}
