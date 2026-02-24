using ExampleCompanyApp.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExampleCompanyApp.Repository.Seeds
{
    public class ProductFeatureSeed : IEntityTypeConfiguration<ProductFeature>
    {
        public void Configure(EntityTypeBuilder<ProductFeature> builder)
        {
            builder.HasData(
                new ProductFeature
                {
                    Id = 1,
                    Color = "Siyah",
                    Height = "500",
                    Width = "400",
                    ProductId = 1    // Creality Ender 3 (3D Yazıcı) ile eşleşti
                },
                new ProductFeature
                {
                    Id = 2,
                    Color = "Mavi",
                    Height = "15",
                    Width = "53",
                    ProductId = 2    // Arduino Uno R3 ile eşleşti
                },
                new ProductFeature
                {
                    Id = 3,
                    Color = "Yeşil",
                    Height = "17",
                    Width = "85",
                    ProductId = 4    // Raspberry Pi 5 ile eşleşti 
                }
            );
        }
    }
}