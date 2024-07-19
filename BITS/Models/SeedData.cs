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
using Microsoft.AspNetCore.Identity;

namespace BITS.Models;
public static class SeedData
{
    public static void Initialize(IServiceProvider serviceProvider)
    {

        using (var context = new BITSContext( serviceProvider.GetRequiredService< DbContextOptions<BITSContext>>()))
        {
            if (!context.Source.Any())
            {
                context.Source.AddRange(
                    new Source
                    {
                        Name = "Amazon",
                        ExternalLink = "",
                        Types = "Internal"
                    },
                    new Source
                    {
                        Name = "Steam",
                        ExternalLink = "www.steam.com",
                        Types = "External"
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
                    },
                    new Product
                    {
                        Name = "Baldur's Gate 3",
                        Developer = "Larian Studios",
                        Publisher = "Larian Studios",
                        Description = "Baldur’s Gate 3 is a story-rich, party-based RPG set in the universe of Dungeons & Dragons, where your choices shape a tale of fellowship and betrayal, survival and sacrifice, and the lure of absolute power.",
                        Genres = new List<Genre> { Genre.Adventure, Genre.RPG, Genre.Strategy },
                        PreviewImages = new List<string> { "https://image.api.playstation.com/vulcan/ap/rnd/202302/2321/3098481c9164bb5f33069b37e49fba1a572ea3b89971ee7b.jpg",
                        "https://shared.akamai.steamstatic.com/store_item_assets/steam/apps/1086940/ss_c73bc54415178c07fef85f54ee26621728c77504.1920x1080.jpg?t=1721123311",
                        "https://shared.akamai.steamstatic.com/store_item_assets/steam/apps/1086940/ss_73d93bea842b93914d966622104dcb8c0f42972b.1920x1080.jpg?t=1721123311",
                        "https://rpgamer.com/wp-content/uploads/2020/02/baldurs-gate-iii_2020-02-27_001-scaled.jpg",
                        "https://cdn.arstechnica.net/wp-content/uploads/2020/02/15_BaldursGate3_Gameplay_Screenshot.jpg",
                        "https://techcrunch.com/wp-content/uploads/2023/08/UI-7.png",
                        "https://www.gamespot.com/a/uploads/original/1727/17277836/4173616-baldurs-gate-3-stealing.jpg"},
                        Features = new List<Features> { Features.CloudSaves, Features.SinglePlayer, Features.Coop },
                        ReleaseDate = new DateTime(2023, 08, 03),
                        //LastUpdatedBy = context.User.Find(3),
                        LastUpdated = DateTime.Now,
                        OSMinimum = "Windows 10 64-bit",
                        OSRecommended = "Windows 10 64-bit",
                        ProcessorMinimum = "Intel I5 4690 or AMD FX 8350",
                        ProcessorRecommended = "Intel i7 8700K or AMD r5 3600",
                        MemoryMinimum = "8 GB RAM",
                        MemoryRecommended = "16 GB RAM",
                        StorageMinimum = "150 GB available space",
                        StorageRecommended = "150 GB available space",
                        GraphicsMinimum = "Nvidia GTX 970 or RX 480 (4GB+ of VRAM)",
                        GraphicsRecommended = "Nvidia 2060 Super or RX 5700 XT (8GB+ of VRAM)",
                        ProductType = "Base Game",
                        RefundType = "Self-Refundable",
                        Price = 69.00
                    },
                    new Product
                    {
                        Name = "Cyberpunk 2077",
                        Developer = "CD PROJEKT RED",
                        Publisher = "CD PROJEKT RED",
                        Description = "Cyberpunk 2077 is an open-world, action-adventure RPG set in the dark future of Night City — a dangerous megalopolis obsessed with power, glamor, and ceaseless body modification.",
                        Genres = new List<Genre> { Genre.RPG },
                        PreviewImages = new List<string> { "https://static.wikia.nocookie.net/cyberpunk/images/9/99/Cyberpunk2077-keyart-male_v.png/revision/latest?cb=20201201001241",
                        "https://cdn.decrypt.co/wp-content/uploads/2024/02/Decrypt-Wallpaper-Format-1600-x-900-px-20-gID_7.png",
                        "https://www.escapistmagazine.com/wp-content/uploads/2023/09/cyberpunk2.0.jpg?fit=1920%2C1080",
                        "https://gamingbolt.com/wp-content/uploads/2021/06/The-Ascent_05.jpg",
                        "https://cdn.vox-cdn.com/thumbor/IkzqVHPukD7YrKZDYBQ4X9DszQE=/1400x788/filters:format(jpeg)/cdn.vox-cdn.com/uploads/chorus_asset/file/23374056/ezgif.com_gif_maker.jpg",
                        "https://shared.akamai.steamstatic.com/store_item_assets/steam/apps/2236190/ss_ea1b6841ff2a0408df287d839d7e39d89d8aba2a.1920x1080.jpg?t=1674235344",
                        "https://oyster.ignimgs.com/mediawiki/apis.ign.com/cyberpunk-2077/0/04/Cyberpunk2077_Phantom_Liberty_A_Spy_Walks_Into_A_Bar_PC_RGB-en.jpg"},
                        Features = new List<Features> { Features.CloudSaves, Features.SinglePlayer },
                        ReleaseDate = new DateTime(2020, 12, 10),
                        //LastUpdatedBy = context.User.Find(3),
                        LastUpdated = DateTime.Now,
                        OSMinimum = "Windows 10 64-bit",
                        OSRecommended = "Windows 10 64-bit",
                        ProcessorMinimum = "Core i7-6700 or Ryzen 5 1600",
                        ProcessorRecommended = "Core i7-12700 or Ryzen 7 7800X3D",
                        MemoryMinimum = "12 GB RAM",
                        MemoryRecommended = "16 GB RAM",
                        StorageMinimum = "70 GB available space",
                        StorageRecommended = "70 GB available space",
                        GraphicsMinimum = "GeForce GTX 1060 6GB or Radeon RX 580 8GB or Arc A380",
                        GraphicsRecommended = "GeForce RTX 2060 SUPER or Radeon RX 5700 XT or Arc A770",
                        ProductType = "Base Game",
                        RefundType = "Self-Refundable",
                        Price = 69.00
                    },
                    new Product
                    {
                        Name = "Red Dead Redemption 2",
                        Developer = "Rockstar Games",
                        Publisher = "Rockstar Games",
                        Description = "Winner of over 175 Game of the Year Awards and recipient of over 250 perfect scores, RDR2 is the epic tale of outlaw Arthur Morgan and the infamous Van der Linde gang, on the run across America at the dawn of the modern age. Also includes access to the shared living world of Red Dead Online.",
                        Genres = new List<Genre> { Genre.Action, Genre.Adventure },
                        PreviewImages = new List<string> { "https://image.api.playstation.com/cdn/UP1004/CUSA03041_00/Hpl5MtwQgOVF9vJqlfui6SDB5Jl4oBSq.png",
                        "https://media.gq-magazine.co.uk/photos/5d13a9f17fcc8e449d8211f2/16:9/w_2560%2Cc_limit/red-dead-redemption-02-22oct18_b.jpg",
                        "https://cdn.mos.cms.futurecdn.net/mZjPUKs99v2woErVN6GYBJ-1200-80.jpg",
                        "https://assetsio.gnwcdn.com/red-dead-redemption-2-review-1540465569009.jpg?width=1600&height=900&fit=crop&quality=100&format=png&enable=upscale&auto=webp",
                        "https://media.gq-magazine.co.uk/photos/5d13ae679fa6014038839ef1/16:9/w_2560%2Cc_limit/red-dead-redemption-03-22oct18_b.jpg",
                        "https://static0.gamerantimages.com/wordpress/wp-content/uploads/2024/04/red-dead-redemption-2-plains.jpg",
                        "https://www.hollywoodreporter.com/wp-content/uploads/2018/10/red_dead_redemption_2_screengrab.jpg?w=1296&h=730&crop=1"},
                        Features = new List<Features> { Features.CloudSaves, Features.SinglePlayer, Features.Coop },
                        ReleaseDate = new DateTime(2019, 12, 06),
                        //LastUpdatedBy = context.User.Find(3),
                        LastUpdated = DateTime.Now,
                        OSMinimum = "Windows 10 64-bit",
                        OSRecommended = "Windows 10 64-bit",
                        ProcessorMinimum = "Intel® Core™ i5-2500K or AMD FX-6300",
                        ProcessorRecommended = "Intel® Core™ i7-4770K or AMD Ryzen 5 1500X",
                        MemoryMinimum = "8 GB RAM",
                        MemoryRecommended = "12 GB RAM",
                        StorageMinimum = "150 GB available space",
                        StorageRecommended = "150 GB available space",
                        GraphicsMinimum = "Nvidia GeForce GTX 770 2GB or AMD Radeon R9 280 3GB",
                        GraphicsRecommended = "Nvidia GeForce GTX 1060 6GB or AMD Radeon RX 480 4GB",
                        ProductType = "Base Game",
                        RefundType = "Self-Refundable",
                        Price = 79.90
                    },
                    new Product
                    {
                        Name = "Grand Theft Auto V",
                        Developer = "Rockstar North",
                        Publisher = "Rockstar Games",
                        Description = "Grand Theft Auto V for PC offers players the option to explore the award-winning world of Los Santos and Blaine County in resolutions of up to 4k and beyond, as well as the chance to experience the game running at 60 frames per second.",
                        Genres = new List<Genre> { Genre.Action, Genre.Adventure },
                        PreviewImages = new List<string> { "https://wallpapers.com/images/featured/grand-theft-auto-v-naej4yiap4gnxh2o.jpg",
                        "https://wallpapers.com/images/featured/gta-5-qpjtjdxwbwrk4gyj.jpg",
                        "https://1734811051.rsc.cdn77.org/data/images/full/385639/rare-gta-5-player-completes-no-damage-run-in-nine-hours-he-has-only-1-hp-throughout-his-gameplay.jpg",
                        "https://s2.dmcdn.net/v/U_tj31aWj5eOclsji/x1080",
                        "https://staticg.sportskeeda.com/editor/2023/09/c0363-16948874860303-1920.jpg",
                        "https://static0.gamerantimages.com/wordpress/wp-content/uploads/2022/03/grand-theft-auto-5-michael-trevor-and-franklin.jpg",
                        "https://images.alphacoders.com/563/thumb-1920-563020.jpg"},
                        Features = new List<Features> { Features.CloudSaves, Features.SinglePlayer, Features.Coop },
                        ReleaseDate = new DateTime(2015, 04, 14),
                        //LastUpdatedBy = context.User.Find(3),
                        LastUpdated = DateTime.Now,
                        OSMinimum = "Windows 10 64-bit",
                        OSRecommended = "Windows 10 64-bit",
                        ProcessorMinimum = "Intel Core 2 Quad CPU Q6600 @ 2.40GHz (4 CPUs) or AMD Phenom 9850 Quad-Core Processor (4 CPUs) @ 2.5GHz",
                        ProcessorRecommended = "Intel Core i5 3470 @ 3.2GHz (4 CPUs) or AMD X8 FX-8350 @ 4GHz (8 CPUs)",
                        MemoryMinimum = "4 GB RAM",
                        MemoryRecommended = "8 GB RAM",
                        StorageMinimum = "120 GB available space",
                        StorageRecommended = "120 GB available space",
                        GraphicsMinimum = "NVIDIA 9800 GT 1GB or AMD HD 4870 1GB (DX 10, 10.1, 11)",
                        GraphicsRecommended = "NVIDIA GTX 660 2GB or AMD HD 7870 2GB",
                        ProductType = "Base Game",
                        RefundType = "Self-Refundable",
                        Price = 55.80
                    },
                    new Product
                    {
                        Name = "Homeworld 3",
                        Developer = "Blackbird Interactive",
                        Publisher = "Gearbox Publishing",
                        Description = "Tactical, beautiful, and wholly unique, the GOTY-winning sci-fi RTS returns with Homeworld 3. Assume control and battle through fleet combat in dazzling, fully 3D space while the award-winning story unfolds on a galactic scale.",
                        Genres = new List<Genre> { Genre.Simulation, Genre.Strategy },
                        PreviewImages = new List<string> { "https://lh7-us.googleusercontent.com/5j4FfS5Ey8yigN8I0OhSYAAlN-xyHd_LlTZCReCeZvsABHgRpta2WsxiuB0a9IRLcBKCL5mbPTOb92B4FoAW66p9ERNS8fyRoLRoCI18z2aJNU_bguMJ5xCkVz2DSSpKLhJ2F13G2qx4EesDJdqeyg",
                        "https://assetsio.gnwcdn.com/2_ooZjqL2.jpg?width=1600&height=900&fit=crop&quality=100&format=png&enable=upscale&auto=webp",
                        "https://wallpapercave.com/wp/wp10407090.jpg",
                        "https://images3.alphacoders.com/110/1106487.jpg",
                        "https://images2.alphacoders.com/110/1106485.jpg",
                        "https://shared.akamai.steamstatic.com/store_item_assets/steam/apps/1840080/ss_7503e27e878fef4f562361ea1c3a165bff8f66aa.1920x1080.jpg?t=1715643382",
                        "https://shared.akamai.steamstatic.com/store_item_assets/steam/apps/1840080/ss_6c76d1c9317ef6e7c6e13dde8a8b299094c0891b.1920x1080.jpg?t=1715643382"},
                        Features = new List<Features> { Features.CloudSaves, Features.SinglePlayer, Features.Coop },
                        ReleaseDate = new DateTime(2024, 05, 14),
                        //LastUpdatedBy = context.User.Find(3),
                        LastUpdated = DateTime.Now,
                        OSMinimum = "Windows 10 64-bit/Windows 11 64-bit",
                        OSRecommended = "Windows 10 64-bit/Windows 11 64-bit",
                        ProcessorMinimum = "Intel i5-6600 or AMD Ryzen 5 1600X",
                        ProcessorRecommended = "Intel i5-9600K or AMD Ryzen 5 3600X",
                        MemoryMinimum = "12 GB RAM",
                        MemoryRecommended = "16 GB RAM",
                        StorageMinimum = "40 GB available space",
                        StorageRecommended = "40 GB available space",
                        GraphicsMinimum = "Nvidia GTX 1060 or AMD RX 480 or Intel ARC A380",
                        GraphicsRecommended = "Nvidia GTX 1080 TI or AMD RX5700 o Intel ARC A580",
                        ProductType = "Base Game",
                        RefundType = "Self-Refundable",
                        Price = 79.90
                    },
                    new Product
                    {
                        Name = "Horizon Forbidden West™ Complete Edition",
                        Developer = "Guerrilla, Nixxes Software",
                        Publisher = "PlayStation Publishing LLC",
                        Description = "Experience the epic Horizon Forbidden West™ in its entirety with bonus content and the Burning Shores expansion included. The Burning Shores add-on contains additional content for Aloy’s adventure, including new storylines, characters, and experiences in a stunning yet hazardous new area.",
                        Genres = new List<Genre> { Genre.Action, Genre.RPG },
                        PreviewImages = new List<string> { "https://cdn1.epicgames.com/offer/24cc2629b0594bf29340f6acf9816af8/EGS_HorizonForbiddenWestCompleteEdition_GuerrillaGamesNixxesSoftware_S1_2560x1440-90cc343f3fcef2f750f13d8a2d84893b",
                        "https://wallpapercave.com/wp/wp13637146.jpg",
                        "https://wallpapercave.com/wp/wp13637160.jpg",
                        "https://futurefive.co.nz/uploads/story/2024/04/08/Horizon_FW_PC_03.webp",
                        "https://external-preview.redd.it/horizon-forbidden-west-complete-edition-lands-on-pc-today-v0-heQKjfSvitmh6wrEkcxwxhbnEoQhse9cBvmrtKjiLvw.jpg?auto=webp&s=d43beed456016096ede851c5bbdd9e8386dc09c5",
                        "https://live.staticflickr.com/65535/51207430423_d77ab8ab95_h.jpg",
                        "https://live.staticflickr.com/65535/51208293245_a720da81cc_h.jpg"},
                        Features = new List<Features> { Features.CloudSaves, Features.SinglePlayer },
                        ReleaseDate = new DateTime(2024, 03, 21),
                        //LastUpdatedBy = context.User.Find(3),
                        LastUpdated = DateTime.Now,
                        OSMinimum = "Windows 10 64-bit (version 1909 or higher)",
                        OSRecommended = "Windows 10 64-bit (version 1909 or higher)",
                        ProcessorMinimum = "Intel Core i3-8100 or AMD Ryzen 3 1300X",
                        ProcessorRecommended = "Intel Core i5-8600 or AMD Ryzen 5 3600",
                        MemoryMinimum = "16 GB RAM",
                        MemoryRecommended = "16 GB RAM",
                        StorageMinimum = "150 GB available space",
                        StorageRecommended = "150 GB available space",
                        GraphicsMinimum = "NVIDIA GeForce GTX 1650 4GB or AMD Radeon RX 5500XT 4GB",
                        GraphicsRecommended = "NVIDIA GeForce RTX 3060 or AMD Radeon RX 5700",
                        ProductType = "Base Game",
                        RefundType = "Self-Refundable",
                        Price = 79.90
                    },
                    new Product
                    {
                        Name = "V Rising",
                        Developer = "Stunlock Studios",
                        Publisher = "Stunlock Studios",
                        Description = "Awaken as a Vampire. Hunt for blood in nearby settlements to regain your strength and evade the scorching sun to survive. Raise your castle and thrive in an ever-changing, open world full of mystery. Gain allies online and conquer the land of the living.",
                        Genres = new List<Genre> { Genre.Action, Genre.Adventure }, //Genre.Massively_Multiplayer },
                        PreviewImages = new List<string> { "https://gaming-cdn.com/images/products/11030/orig/v-rising-pc-game-steam-cover.jpg?v=1715247025",
                        "https://assetsio.gnwcdn.com/v-rising-cargo.jpg?width=1200&height=1200&fit=bounds&quality=70&format=jpg&auto=webp",
                        "https://cdn.mos.cms.futurecdn.net/VJP6sdBK4iLmc2p2X3wyGK.jpg",
                        "https://www.gamespace.com/wp-content/uploads/2022/05/20220525131116_1.jpg",
                        "https://oyster.ignimgs.com/mediawiki/apis.ign.com/v-rising/f/f2/V-rising-production-structures-6.jpg",
                        "https://static0.gamerantimages.com/wordpress/wp-content/uploads/2024/02/v-rising-ps5-release-2024-2.jpg",
                        "https://organner.pl/img/games/266/32.jpg"},
                        Features = new List<Features> { Features.CloudSaves, Features.SinglePlayer, Features.Coop },
                        ReleaseDate = new DateTime(2024, 05, 08),
                        //LastUpdatedBy = context.User.Find(3),
                        LastUpdated = DateTime.Now,
                        OSMinimum = "Windows 10 64 bit",
                        OSRecommended = "Windows 10 64 bit",
                        ProcessorMinimum = "Intel Core i5-6600, 3.3 GHz or AMD Ryzen 5 1500X, 3.5 GHz",
                        ProcessorRecommended = "Intel Core i5-11600K, 3.9 GHz or AMD Ryzen 5 5600X, 3.7 GHz",
                        MemoryMinimum = "12 GB RAM",
                        MemoryRecommended = "12 GB RAM",
                        StorageMinimum = "7 GB available space",
                        StorageRecommended = "7 GB available space",
                        GraphicsMinimum = "NVIDIA GeForce GTX 750 Ti (Maxwell or newer), 2 GB or AMD Radeon R7 360, 2 GB",
                        GraphicsRecommended = "NVIDIA GeForce GTX 1070, 8 GB or AMD Radeon RX 590, 8 GB",
                        ProductType = "Base Game",
                        RefundType = "Self-Refundable",
                        Price = 29.00
                    },
                    new Product
                    {
                        Name = "Little Kitty, Big City",
                        Developer = "Double Dagger Studio",
                        Publisher = "Double Dagger Studio",
                        Description = "You're a curious little kitty with a big personality, on an adventure to find your way back home. Explore the city, make new friends with stray animals, wear delightful hats, and leave more than a little chaos in your wake. After all, isn't that what cats do best?",
                        Genres = new List<Genre> { Genre.Adventure, Genre.Casual, Genre.Indie, Genre.Simulation }, 
                        PreviewImages = new List<string> { "https://assets.nintendo.com/image/upload/ar_16:9,b_auto:border,c_lpad/b_white/f_auto/q_auto/dpr_1.5/c_scale,w_1000/ncom/software/switch/70010000061050/813e3bd006428f1a6618bf73584b107d262ce6f663428bca0566e0ab49540dd2",
                        "https://the-indie-in-former.com/wp-content/uploads/2022/11/Little-Kitty-Big-City.png",
                        "https://www.siliconera.com/wp-content/uploads/2024/05/where-are-the-little-kitty-big-city-tennis-balls-1-e1715313280540.jpeg",
                        "https://www.digitaltrends.com/wp-content/uploads/2024/05/little-kitty-big-city-cat-holding-bread.jpg?fit=1920%2C1080&p=1",
                        "https://www.gameinformer.com/sites/default/files/styles/full/public/2024/05/07/1e1636e9/little_kitty_big_city_review_promo.jpg",
                        "https://gamingtrend.com/wp-content/uploads/2024/05/ss_a48194f62eb8bbcd812ac861c48001598e68c1b2.1920x1080.jpg",
                        "https://shared.akamai.steamstatic.com/store_item_assets/steam/apps/1177980/ss_e2cc2cfc276533e1a0518387c0deabd49f30986a.1920x1080.jpg?t=1715566251"},
                        Features = new List<Features> { Features.CloudSaves, Features.SinglePlayer },
                        ReleaseDate = new DateTime(2024, 05, 09),
                        //LastUpdatedBy = context.User.Find(3),
                        LastUpdated = DateTime.Now,
                        OSMinimum = "WIN8-64 bit",
                        ProcessorMinimum = "Intel i5-760 (4*2800), AMD Phenom II",
                        MemoryMinimum = "4 GB RAM",
                        StorageMinimum = "4 GB available space",
                        GraphicsMinimum = "Nvidia GeForce GTX 650 / Radeon HD 7510",
                        ProductType = "Base Game",
                        RefundType = "Self-Refundable",
                        Price = 22.00
                    },
                    new Product
                    {
                        Name = "Dying Light",
                        Developer = "Techland",
                        Publisher = "Techland",
                        Description = "First-person action survival game set in a post-apocalyptic open world overrun by flesh-hungry zombies. Roam a city devastated by a mysterious virus epidemic. Scavenge for supplies, craft weapons, and face hordes of the infected.",
                        Genres = new List<Genre> { Genre.Action, Genre.RPG }, 
                        PreviewImages = new List<string> { "https://miro.medium.com/v2/resize:fit:2000/1*B-U71IHMDTKxUmu78WM_6w.png",
                        "https://dyinglightgame.com/img/screenshots/dl1/2.jpg",
                        "https://dyinglightgame.com/img/screenshots/dl1/3.jpg",
                        "https://dyinglightgame.com/img/screenshots/dl1/4.jpg",
                        "https://dyinglightgame.com/img/screenshots/dl1/5.jpg",
                        "https://dyinglightgame.com/img/screenshots/dl1/6.jpg",
                        "https://dyinglightgame.com/img/screenshots/dl1/8.jpg"},
                        Features = new List<Features> { Features.CloudSaves, Features.SinglePlayer, Features.Coop },
                        ReleaseDate = new DateTime(2015, 01, 26),
                        //LastUpdatedBy = context.User.Find(3),
                        LastUpdated = DateTime.Now,
                        OSMinimum = "Windows® 7 64-bit or Windows® 8 64-bit or Windows® 8.1 64-bit",
                        OSRecommended = "Windows® 7 64-bit or Windows® 8 64-bit or Windows® 8.1 64-bit",
                        ProcessorMinimum = "Intel® Core™ i5-2500 @3.3 GHz or AMD FX-8320 @3.5 GHz",
                        ProcessorRecommended = "Intel® Core™ i5-4670K @3.4 GHz or AMD FX-8350 @4.0 GHz ",
                        MemoryMinimum = "4 GB RAM DDR3",
                        MemoryRecommended = "8 GB RAM DDR3",
                        StorageMinimum = "40 GB available space",
                        StorageRecommended = "40 GB available space",
                        GraphicsMinimum = "NVIDIA® GeForce® GTX 560 or AMD Radeon™ HD 6870 (1GB VRAM)",
                        GraphicsRecommended = "NVIDIA® GeForce® GTX 780 or AMD Radeon™ R9 290 (2GB VRAM)",
                        ProductType = "Base Game",
                        RefundType = "Self-Refundable",
                        Price = 26.99
                    }

                );
            }


            context.SaveChanges();
        }
        var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
        SeedRoles(roleManager).Wait();
    }

    public static string[] roleNames = { "Admin", "Employee", "Customer" };
    private static async Task SeedRoles(RoleManager<IdentityRole> roleManager)
    {

        foreach (var roleName in roleNames)
        {
            var roleExists = await roleManager.RoleExistsAsync(roleName);
            if (!roleExists)
            {
                await roleManager.CreateAsync(new IdentityRole(roleName));
            }
        }
    }
}
