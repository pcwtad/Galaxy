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
    public class DetailsImgConfigs : IEntityTypeConfiguration<DetailsImg>
    {
        public void Configure(EntityTypeBuilder<DetailsImg> builder)
        {
            builder.ToTable("T_DetailsImgs");
            builder.HasKey(x => x.Id);
        }
    }
}
