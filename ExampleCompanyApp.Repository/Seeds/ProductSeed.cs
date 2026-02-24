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
    public class ProductSeed : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasData(
                // Kategori 3: 3D Yazıcı & Filament
                new Product { Id = 1, Name = "Creality Ender 3 V3 SE 3D Yazıcı", Price = 8500, Stock = 10, CategoryId = 3, CreatedDate = new DateTime(2024, 1, 1) },

                // Kategori 1: Arduino & Geliştirme Kartları
                new Product { Id = 2, Name = "Arduino Uno R3 (Klon - CH340)", Price = 185, Stock = 100, CategoryId = 1, CreatedDate = new DateTime(2024, 1, 1) },
                new Product { Id = 3, Name = "Arduino Başlangıç Seti (Robotistan)", Price = 450, Stock = 50, CategoryId = 1, CreatedDate = new DateTime(2024, 1, 1) },

                // Kategori 11: Raspberry Pi & Single Board
                new Product { Id = 4, Name = "Raspberry Pi 5 - 8GB RAM", Price = 3200, Stock = 15, CategoryId = 11, CreatedDate = new DateTime(2024, 1, 1) },
                new Product { Id = 5, Name = "Raspberry Pi 4 Lisanslı Güç Adaptörü", Price = 350, Stock = 40, CategoryId = 11, CreatedDate = new DateTime(2024, 1, 1) },

                // Kategori 6: Güç Kaynakları & Bataryalar
                new Product { Id = 6, Name = "Lipo Batarya 11.1V 2200mAh", Price = 750, Stock = 25, CategoryId = 6, CreatedDate = new DateTime(2024, 1, 1) },

                // Kategori 2: Sensörler & Modüller
                new Product { Id = 7, Name = "HC-SR04 Ultrasonik Mesafe Sensörü", Price = 45, Stock = 200, CategoryId = 2, CreatedDate = new DateTime(2024, 1, 1) },
                new Product { Id = 8, Name = "DHT11 Isı ve Nem Sensörü", Price = 65, Stock = 150, CategoryId = 2, CreatedDate = new DateTime(2024, 1, 1) },
                new Product { Id = 9, Name = "Toprak Nem Algılama Sensörü", Price = 35, Stock = 80, CategoryId = 2, CreatedDate = new DateTime(2024, 1, 1) }
            );
        }
    }
}