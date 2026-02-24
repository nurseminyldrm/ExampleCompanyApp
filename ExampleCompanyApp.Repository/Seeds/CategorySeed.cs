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
    public class CategorySeed : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasData(
                new Category { Id = 1, Name = "Arduino & Geliştirme kartları" },
                new Category { Id = 2, Name = "Sensörler & Modüller" },
                new Category { Id = 3, Name = "3D Yazıcı & Filament" },
                new Category { Id = 4, Name = "Robotik kodlama setleri" },
                new Category { Id = 5, Name = "Motorlar & Sürücüler" },
                new Category { Id = 6, Name = "Güç Kaynakları & bataryalar" },
                new Category { Id = 7, Name = "Elektronik Komponent" },
                new Category { Id = 8, Name = "Prototipleme & Lehimleme" },
                new Category { Id = 9, Name = "Eğitici Robotik kitler" },
                new Category { Id = 10, Name = "Wireless & Bluetooth Modülleri" },
                new Category { Id = 11, Name = "Raspberry Pi & Single Board" },
                new Category { Id = 12, Name = "Ölçü Aletleri & El Aletleri" }
            );
        }
    }
}
