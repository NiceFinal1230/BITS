using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using BITS.Data;
using System;
using System.Linq;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using Mono.TextTemplating;
using System.Composition;
using System.Data;

namespace BITS.Models;
public static class SeedData
{
    public static void Initialize(IServiceProvider serviceProvider)
    {
        using (var context = new BITSContext(
            serviceProvider.GetRequiredService<
                DbContextOptions<BITSContext>>()))
        {
            // Look for any movies.
            if (!context.User.Any())
            {
                context.User.AddRange(
                    new User
                    {
                        UserName = "jdoe",
                        Email = "jdoe@example.com",
                        Name = "John Doe",
                        Roles = "Admin",
                        Salt = "randomSalt1",
                        HashedPassword = "hashedPassword1",
                        PhoneNumber = "123-456-7890",
                        StreetAddress = "123 Main St",
                        PostCode = "12345",
                        Suburb = "Downtown",
                        State = "NY",
                        CardNumber = "4111111111111111",
                        CardOwner = "John Doe",
                        Expiry = "12/25",
                        CVV = "123"
                    },
                    new User
                    {
                        UserName = "asmith",
                        Email = "asmith@example.com",
                        Name = "Alice Smith",
                        Roles = "User",
                        Salt = "randomSalt2",
                        HashedPassword = "hashedPassword2",
                        PhoneNumber = "234-567-8901",
                        StreetAddress = "456 Elm St",
                        PostCode = "67890",
                        Suburb = "Suburbia",
                        State = "CA",
                        CardNumber = "4222222222222222",
                        CardOwner = "Alice Smith",
                        Expiry = "11/24",
                        CVV = "456"
                    },
                    new User
                    {
                        UserName = "bjones",
                        Email = "bjones@example.com",
                        Name = "Bob Jones",
                        Roles = "Moderator",
                        Salt = "randomSalt3",
                        HashedPassword = "hashedPassword3",
                        PhoneNumber = "345-678-9012",
                        StreetAddress = "789 Oak St",
                        PostCode = "54321",
                        Suburb = "Midtown",
                        State = "TX",
                        CardNumber = "4333333333333333",
                        CardOwner = "Bob Jones",
                        Expiry = "10/23",
                        CVV = "789"
                    }
                );
            }

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
                        Features = new List<Features> { Features.SinglePlayer, Features.CloudSaves },
                        ReleaseDate = new DateTime(2023, 6, 1),
                        LastUpdatedBy = context.User.Find(1),
                        LastUpdated = DateTime.Now
                    },
                    new Product
                    {
                        Name = "Puzzle Adventure",
                        Developer = "Puzzle Corp",
                        Publisher = "Puzzle Publisher",
                        Description = "A challenging puzzle adventure game.",
                        Genres = new List<Genre> { Genre.Puzzle, Genre.Adventure },
                        Features = new List<Features> { Features.SinglePlayer, Features.Multiplayer },
                        ReleaseDate = new DateTime(2022, 11, 20),
                        LastUpdatedBy = context.User.Find(2),
                        LastUpdated = DateTime.Now
                    },
                    new Product
                    {
                        Name = "Multiplayer Strategy",
                        Developer = "Strategy Masters",
                        Publisher = "Strategic Games",
                        Description = "A strategy game with multiplayer features.",
                        Genres = new List<Genre> { Genre.Strategy, Genre.Simulation },
                        Features = new List<Features> { Features.Multiplayer, Features.Coop },
                        ReleaseDate = new DateTime(2024, 2, 15),
                        LastUpdatedBy = context.User.Find(3),
                        LastUpdated = DateTime.Now
                    }
                );
            }
            

            context.SaveChanges();
        }
    }
}
