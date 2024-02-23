using Entities.Concretes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concretes.EntityFramework.EntityTypeConfigurations
{
    public class BootcampImageConfiguration : IEntityTypeConfiguration<BootcampImage>
    {
        public void Configure(EntityTypeBuilder<BootcampImage> builder)
        {
            builder.ToTable("BootcampImages").HasKey(x => x.Id);
            builder.Property(x => x.Id).HasColumnName("Id");
            builder.Property(x => x.BootcampId).HasColumnName("BootcampId").IsRequired();
            builder.Property(x => x.ImagePath).HasColumnName("ImagePath").IsRequired();
            builder.Property(x => x.CreatedDate).HasColumnName("CreatedDate");
            builder.Property(x => x.UpdatedDate).HasColumnName("UpdatedDate");
            builder.Property(x => x.DeletedDate).HasColumnName("DeletedDate");

            builder.HasOne(x => x.Bootcamp);
        }
    }
}
