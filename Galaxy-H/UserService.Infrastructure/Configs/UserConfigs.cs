using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserService.Domain.Entities;

namespace UserService.Infrastructure.Configs
{
    public class UserConfigs : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("T_Users");
            builder.HasKey(x=>x.Id);
            builder.Property(x => x.UserName).HasMaxLength(8);
            builder.Property(x => x.UserPasswod).HasMaxLength(18);
            builder.Property(x => x.Userintroduce).HasMaxLength(50);
            //builder.HasOne(x => x.HeadImg).WithOne(x => x.user).HasForeignKey<UserImgFile>(e => e.Id);
        }
    }
}
