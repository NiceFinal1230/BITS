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
                        Name = "Helldivers 2",
                        Developer = "Arrowhead Game Studios",
                        Publisher = "Play Station Plubishing LLC",
                        Description = "The Galaxy’s Last Line of Offence. Enlist in the Helldivers and join the fight for freedom across a hostile galaxy in a fast, frantic, and ferocious third-person shooter.",
                        Genres = new List<Genre> { Genre.Action, Genre.Shooter },
                        PreviewImages = new List<string> { "https://reservoircreative.studio/app/uploads/2023/09/rsvr-sony-keyart_helldivers2_preorderversion_web.jpg",
                        "https://gameranx.com/wp-content/uploads/2023/09/Helldivers-2-Bile-Titan-Liberation-Gameplay-scaled.jpg",
                        "https://media.bleacherreport.com/image/upload/c_fill,g_faces,w_3800,h_2000,q_95/v1707834714/jig2wigcxan0jzfbjzax.jpg",
                        "https://www.dexerto.com/cdn-image/wp-content/uploads/2024/02/20/helldivers-2-how-to-reinforce-teamates.jpg?width=3840&quality=60&format=auto",
                        "https://lh7-us.googleusercontent.com/5DL4Kr8JUrOtL-ILUkFBWFC_tbvXSN09g7qWxYb341G8uGfPmdd0S1vDqhp1uAnPKN7F9EAmPN_XUh7U6x4cx2b4MVDf8AQGoKPbUbnY3k4VB1ujpJ6osa2aeAKU73L9tKe3g1NGZ2Xy3W9krCgafQ",
                        "https://www.gameshub.com/wp-content/uploads/sites/5/2024/02/helldivers-2-gameplay.jpeg",
                        "https://cdn.gamerbraves.com/2024/02/Helldivers-2-Gameplay-Screenshot-03.jpg"},
                        Features = new List<Features> { Features.SinglePlayer, Features.CloudSaves, Features.Multiplayer, Features.Coop },
                        ReleaseDate = new DateTime(2024, 02, 08),
                        //LastUpdatedBy = context.User.Find(1),
                        LastUpdated = DateTime.Now,
                        OSMinimum = "Windows 10",
                        OSRecommended = "Windows 10",
                        ProcessorMinimum = "Intel Core i7-4790K or AMD Ryzen 5 1500X",
                        ProcessorRecommended = "Intel Core i7-9700K or AMD Ryzen 7 3700X",
                        MemoryMinimum = "8 GB RAM",
                        MemoryRecommended = "16 GB RAM",
                        StorageMinimum = "100 GB available space",
                        StorageRecommended = "100 GB available space",
                        GraphicsMinimum = "NVIDIA GeForce GTX 1050 Ti or AMD Radeon RX 470",
                        GraphicsRecommended = "NVIDIA GeForce RTX 2060 or AMD Radeon RX 6600XT",
                        ProductType = "Base Game",
                        RefundType = "Self-Refundable",
                        Price = 54.90
                    },
                    new Product
                    {
                        Name = "Overcooked! 2",
                        Developer = "Ghost Town Games Ltd, Team17",
                        Publisher = "Team17",
                        Description = "Overcooked returns with a brand-new helping of chaotic cooking action! Journey back to the Onion Kingdom and assemble your team of chefs in classic couch co-op or online play for up to four players. Hold onto your aprons… it’s time to save the world again!",
                        Genres = new List<Genre> { Genre.Casual, Genre.Simulation },
                        PreviewImages = new List<string> { "https://assets.nintendo.com/image/upload/c_fill,w_1200/q_auto:best/f_auto/dpr_2.0/ncom/software/switch/70010000003402/057249cf707a2a733c876c0eb453bb018ee6ea09f3aea5c350f4d76f70840d00",
                        "https://assets1.ignimgs.com/2018/07/31/overcooked-2-42856314492-o-1533075019368.jpg",
                        "https://i.ytimg.com/vi/YPlxlu4y0oA/maxresdefault.jpg",
                        "https://cdn.vox-cdn.com/thumbor/vsB0q-248WsozOXJlWU3rVsIgR8=/0x0:1280x720/1200x800/filters:focal(499x265:703x469)/cdn.vox-cdn.com/uploads/chorus_image/image/60756679/overcooked.0.jpg",
                        "https://www.nintendoworldreport.com/media/47486/1/2.jpg",
                        "https://cdn2.unrealengine.com/yes-chef-what-overcooked-2-and-moving-out-can-reveal-about-your-friends-1920x1080-9e4822dc725c.jpg",
                        "https://assets1.ignimgs.com/2018/07/31/overcooked-2-42856314982-o-1533075019372.jpg"},
                        Features = new List<Features> { Features.Coop, Features.Multiplayer, Features.CloudSaves },
                        ReleaseDate = new DateTime(2021, 06, 17),
                        //LastUpdatedBy = context.User.Find(2),
                        LastUpdated = DateTime.Now,
                        OSMinimum = "Windows 7 - 64 bit",
                        OSRecommended = "Windows 7 - 64 bit",
                        ProcessorMinimum = "Intel i3-2100 / AMD A8-5600k",
                        ProcessorRecommended = "Intel i5-650 / AMD A10-5800K",
                        MemoryMinimum = "4 GB RAM",
                        MemoryRecommended = "8 GB RAM",
                        StorageMinimum = "3 GB available space",
                        StorageRecommended = "3 GB available space",
                        GraphicsMinimum = "GeForce GTX 630 / Radeon HD 6570",
                        GraphicsRecommended = "Nvidia GeForce GTX 650 / Radeon HD 7510",
                        ProductType = "Base Game",
                        RefundType = "Self-Refundable",
                        Price = 29.00
                    },
                    new Product
                    {
                        Name = "Hades II",
                        Developer = "Supergiant Games",
                        Publisher = "Supergiant Games",
                        Description = "Battle beyond the Underworld using dark sorcery to take on the Titan of Time in this bewitching sequel to the award-winning rogue-like dungeon crawler.",
                        Genres = new List<Genre> { Genre.Action, Genre.RPG },
                        PreviewImages = new List<string> { "https://shared.akamai.steamstatic.com/store_item_assets/steam/apps/1145350/capsule_616x353.jpg?t=1715727000",
                        "https://i.kinja-img.com/image/upload/c_fit,q_60,w_645/4fa61fad72c9f06ddc448a752697fdd6.jpg",
                        "https://media.wired.com/photos/663d22bd3aeee12d6ca99815/master/pass/hades2-tips-culture.jpg",
                        "https://deltiasgaming.com/wp-content/uploads/2024/05/Hades-2-New-Magic-Combat-from-Technical-Test.jpg",
                        "https://images.ctfassets.net/5owu3y35gz1g/1bjuKMwtSJIFELYT79ceDs/3777d4004f6aaf8d485a90eb28fa01c5/hades2_apr30_01.jpg",
                        "https://media.wired.com/photos/66391dc77edd38d23022a658/4:3/w_1440,h_1080,c_limit/Hades-II-Dropped-Early-Culture-hades2_apr30_09.jpg",
                        "https://static0.gamerantimages.com/wordpress/wp-content/uploads/2024/05/hades-2-save.jpg"},
                        Features = new List<Features> { Features.CloudSaves, Features.SinglePlayer },
                        ReleaseDate = new DateTime(2024, 05, 07),
                        //LastUpdatedBy = context.User.Find(3),
                        LastUpdated = DateTime.Now,
                        OSMinimum = "Windows 10 64-bit",
                        OSRecommended = "Windows 10 64-bit",
                        ProcessorMinimum = "Dual Core 2.4 GHz",
                        ProcessorRecommended = "Quad Core 2.4ghz",
                        MemoryMinimum = "8 GB RAM",
                        MemoryRecommended = "16 GB RAM",
                        StorageMinimum = "10 GB available space",
                        StorageRecommended = "10 GB available space",
                        GraphicsMinimum = "GeForce GTX 950, Radeon R7 360, or Intel HD Graphics 630",
                        GraphicsRecommended = "GeForce RTX 2060, Radeon RX 5600 XT, or Intel Arc A580",
                        ProductType = "Base Game",
                        RefundType = "Self-Refundable",
                        Price = 26.00
                    },
                    new Product
                    {
                        Name = "Ghost of Tsushima DIRECTOR's CUT",
                        Developer = "Sucker Punch Productions, Nixxes Software",
                        Publisher = "Playstation Publishing LLC",
                        Description = "A storm is coming. Venture into the complete Ghost of Tsushima DIRECTOR’S CUT on PC; forge your own path through this open-world action adventure and uncover its hidden wonders. Brought to you by Sucker Punch Productions, Nixxes Software and PlayStation Studios.",
                        Genres = new List<Genre> { Genre.Action, Genre.Adventure },
                        PreviewImages = new List<string> { "https://image.api.playstation.com/vulcan/ap/rnd/202106/2322/c16gs6a7lbAYzPf7ZTikbH1c.png",
                        "https://m.media-amazon.com/images/I/81XmpLlEkhS._AC_UF1000,1000_QL80_.jpg",
                        "https://cdn.mos.cms.futurecdn.net/a6AbJzCQ7fujWzv3nKXYLQ.jpg",
                        "https://assets.gqindia.com/photos/612488ece3e4c0bf841d090b/16:9/w_2560%2Cc_limit/18AF3C01-36FE-4E86-BB92-B9E4412E8639.jpeg",
                        "https://assetsio.gnwcdn.com/ghost-of-tsushima-directors-cut-adds-aloy-inspired-armour-in-latest-patch-1644348544971.jpg?width=1600&height=900&fit=crop&quality=100&format=png&enable=upscale&auto=webp",
                        "https://cdn.arstechnica.net/wp-content/uploads/2021/08/Ghost-of-Tsushima-PS5-7.jpg",
                        "https://sm.pcmag.com/t/pcmag_uk/review/g/ghost-of-t/ghost-of-tsushima-directors-cut-for-playstation-5_j84k.1920.jpg"},
                        Features = new List<Features> { Features.CloudSaves, Features.SinglePlayer, Features.Multiplayer, Features.Coop },
                        ReleaseDate = new DateTime(2024, 05, 16),
                        //LastUpdatedBy = context.User.Find(3),
                        LastUpdated = DateTime.Now,
                        OSMinimum = "Windows 10 64-bit",
                        OSRecommended = "Windows 10 64-bit",
                        ProcessorMinimum = "Intel Core i3-7100 or AMD Ryzen 3 1200",
                        ProcessorRecommended = "Intel Core i5-8600 or AMD Ryzen 5 3600",
                        MemoryMinimum = "8 GB RAM",
                        MemoryRecommended = "16 GB RAM",
                        StorageMinimum = "75 GB available space",
                        StorageRecommended = "75 GB available space",
                        GraphicsMinimum = "NVIDIA GeForce GTX 960 or AMD Radeon RX 5500 XT",
                        GraphicsRecommended = "NVIDIA GeForce RTX 2060 or AMD Radeon RX 5600 XT",
                        ProductType = "Base Game",
                        RefundType = "Self-Refundable",
                        Price = 79.90
                    }
                );
            }


            context.SaveChanges();
        }
    }
}
