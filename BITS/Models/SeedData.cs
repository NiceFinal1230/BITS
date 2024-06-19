using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using BITS.Data;
using System;
using System.Linq;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using Mono.TextTemplating;
using System.Composition;
using System.Data;
using BITS.Areas.Identity.Data;

namespace BITS.Models;
public static class SeedData
{
    public static void Initialize(IServiceProvider serviceProvider)
    {
        using (var context = new BITSContext(
            serviceProvider.GetRequiredService<
                DbContextOptions<BITSContext>>()))
        {

            if (!context.Source.Any())
            {
                context.Source.AddRange(
                    new Source
                    {
                        Name = "Internal",
                        ExternalLink = ""
                    },
                    new Source
                    {
                        Name = "External",
                        ExternalLink = "www.steam.com"
                    }
                );
            }

            if (!context.Product.Any())
            {
                context.Product.AddRange(
                    new Product
                    {
                        Name = "Action Game",
                        Developer = "Dev Studio",
                        Publisher = "Game Publisher",
                        Description = "An exciting action-packed game.",
                        Genres = new List<Genre> { Genre.Action, Genre.Adventure },
                        PreviewImages = new List<string> { "https://plus.unsplash.com/premium_photo-1701214498603-59f754e55543?w=500&auto=format&fit=crop&q=60&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxlZGl0b3JpYWwtZmVlZHwxfHx8ZW58MHx8fHx8",
                        "https://images.unsplash.com/photo-1718046254335-d9ff832c9c3c?w=500&auto=format&fit=crop&q=60&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxlZGl0b3JpYWwtZmVlZHwyfHx8ZW58MHx8fHx8",
                        "https://images.unsplash.com/photo-1718115257239-7246f434a7b0?w=500&auto=format&fit=crop&q=60&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxlZGl0b3JpYWwtZmVlZHw0fHx8ZW58MHx8fHx8",
                        "https://images.unsplash.com/photo-1689602037070-fec2eca3f5b2?w=500&auto=format&fit=crop&q=60&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxlZGl0b3JpYWwtZmVlZHw3fHx8ZW58MHx8fHx8",
                        "https://images.unsplash.com/photo-1718019668172-5380fd58f2c5?w=500&auto=format&fit=crop&q=60&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxlZGl0b3JpYWwtZmVlZHw4fHx8ZW58MHx8fHx8",
                        "https://images.unsplash.com/photo-1718078742597-6d07da10820f?w=500&auto=format&fit=crop&q=60&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxlZGl0b3JpYWwtZmVlZHwxMHx8fGVufDB8fHx8fA%3D%3D",
                        "https://plus.unsplash.com/premium_photo-1709309934434-448b98065f66?w=500&auto=format&fit=crop&q=60&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxlZGl0b3JpYWwtZmVlZHwxN3x8fGVufDB8fHx8fA%3D%3D"},
                        Features = new List<Features> { Features.SinglePlayer, Features.CloudSaves },
                        ReleaseDate = new DateTime(2023, 6, 1),
                        //LastUpdatedBy = context.User.Find(1),
                        LastUpdated = DateTime.Now,
                        OSMinimum = "Windows 7",
                        OSRecommended = "Windows 10",
                        ProcessorMinimum = "Intel Core i3",
                        ProcessorRecommended = "Intel Core i5",
                        MemoryMinimum = "4 GB RAM",
                        MemoryRecommended = "8 GB RAM",
                        StorageMinimum = "20 GB available space",
                        StorageRecommended = "50 GB available space",
                        GraphicsMinimum = "NVIDIA GeForce GTX 660",
                        GraphicsRecommended = "NVIDIA GeForce GTX 1060",
                        ProductType = "Base Game",
                        RefundType = "Self-Refundable",
                        Price = 99.99
                    },
                    new Product
                    {
                        Name = "Puzzle Adventure",
                        Developer = "Puzzle Corp",
                        Publisher = "Puzzle Publisher",
                        Description = "A challenging puzzle adventure game.",
                        Genres = new List<Genre> { Genre.Puzzle, Genre.Adventure },
                        PreviewImages = new List<string> { "https://plus.unsplash.com/premium_photo-1701214498603-59f754e55543?w=500&auto=format&fit=crop&q=60&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxlZGl0b3JpYWwtZmVlZHwxfHx8ZW58MHx8fHx8",
                        "https://images.unsplash.com/photo-1718046254335-d9ff832c9c3c?w=500&auto=format&fit=crop&q=60&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxlZGl0b3JpYWwtZmVlZHwyfHx8ZW58MHx8fHx8",
                        "https://images.unsplash.com/photo-1718115257239-7246f434a7b0?w=500&auto=format&fit=crop&q=60&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxlZGl0b3JpYWwtZmVlZHw0fHx8ZW58MHx8fHx8",
                        "https://images.unsplash.com/photo-1689602037070-fec2eca3f5b2?w=500&auto=format&fit=crop&q=60&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxlZGl0b3JpYWwtZmVlZHw3fHx8ZW58MHx8fHx8",
                        "https://images.unsplash.com/photo-1718019668172-5380fd58f2c5?w=500&auto=format&fit=crop&q=60&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxlZGl0b3JpYWwtZmVlZHw4fHx8ZW58MHx8fHx8",
                        "https://images.unsplash.com/photo-1718078742597-6d07da10820f?w=500&auto=format&fit=crop&q=60&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxlZGl0b3JpYWwtZmVlZHwxMHx8fGVufDB8fHx8fA%3D%3D",
                        "https://plus.unsplash.com/premium_photo-1709309934434-448b98065f66?w=500&auto=format&fit=crop&q=60&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxlZGl0b3JpYWwtZmVlZHwxN3x8fGVufDB8fHx8fA%3D%3D"},
                        Features = new List<Features> { Features.SinglePlayer, Features.Multiplayer },
                        ReleaseDate = new DateTime(2022, 11, 20),
                        //LastUpdatedBy = context.User.Find(2),
                        LastUpdated = DateTime.Now,
                        OSMinimum = "Windows 7",
                        OSRecommended = "Windows 10",
                        ProcessorMinimum = "Intel Core i3",
                        ProcessorRecommended = "Intel Core i5",
                        MemoryMinimum = "4 GB RAM",
                        MemoryRecommended = "8 GB RAM",
                        StorageMinimum = "20 GB available space",
                        StorageRecommended = "50 GB available space",
                        GraphicsMinimum = "NVIDIA GeForce GTX 660",
                        GraphicsRecommended = "NVIDIA GeForce GTX 1060",
                        ProductType = "Base Game",
                        RefundType = "Self-Refundable",
                        Price = 99.99
                    },
                    new Product
                    {
                        Name = "Multiplayer Strategy",
                        Developer = "Strategy Masters",
                        Publisher = "Strategic Games",
                        Description = "A strategy game with multiplayer features.",
                        Genres = new List<Genre> { Genre.Strategy, Genre.Simulation },
                        PreviewImages = new List<string> { "https://plus.unsplash.com/premium_photo-1701214498603-59f754e55543?w=500&auto=format&fit=crop&q=60&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxlZGl0b3JpYWwtZmVlZHwxfHx8ZW58MHx8fHx8",
                        "https://images.unsplash.com/photo-1718046254335-d9ff832c9c3c?w=500&auto=format&fit=crop&q=60&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxlZGl0b3JpYWwtZmVlZHwyfHx8ZW58MHx8fHx8",
                        "https://images.unsplash.com/photo-1718115257239-7246f434a7b0?w=500&auto=format&fit=crop&q=60&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxlZGl0b3JpYWwtZmVlZHw0fHx8ZW58MHx8fHx8",
                        "https://images.unsplash.com/photo-1689602037070-fec2eca3f5b2?w=500&auto=format&fit=crop&q=60&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxlZGl0b3JpYWwtZmVlZHw3fHx8ZW58MHx8fHx8",
                        "https://images.unsplash.com/photo-1718019668172-5380fd58f2c5?w=500&auto=format&fit=crop&q=60&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxlZGl0b3JpYWwtZmVlZHw4fHx8ZW58MHx8fHx8",
                        "https://images.unsplash.com/photo-1718078742597-6d07da10820f?w=500&auto=format&fit=crop&q=60&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxlZGl0b3JpYWwtZmVlZHwxMHx8fGVufDB8fHx8fA%3D%3D",
                        "https://plus.unsplash.com/premium_photo-1709309934434-448b98065f66?w=500&auto=format&fit=crop&q=60&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxlZGl0b3JpYWwtZmVlZHwxN3x8fGVufDB8fHx8fA%3D%3D"},
                        Features = new List<Features> { Features.Multiplayer, Features.Coop },
                        ReleaseDate = new DateTime(2024, 2, 15),
                        //LastUpdatedBy = context.User.Find(3),
                        LastUpdated = DateTime.Now,
                        OSMinimum = "Windows 7",
                        OSRecommended = "Windows 10",
                        ProcessorMinimum = "Intel Core i3",
                        ProcessorRecommended = "Intel Core i5",
                        MemoryMinimum = "4 GB RAM",
                        MemoryRecommended = "8 GB RAM",
                        StorageMinimum = "20 GB available space",
                        StorageRecommended = "50 GB available space",
                        GraphicsMinimum = "NVIDIA GeForce GTX 660",
                        GraphicsRecommended = "NVIDIA GeForce GTX 1060",
                        ProductType = "Base Game",
                        RefundType = "Self-Refundable",
                        Price = 99.99
                    }
                );
            }
            

            context.SaveChanges();
        }
    }
}
