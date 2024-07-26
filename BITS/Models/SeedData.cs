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
                        ProcessorRecommended = "Intel® Core™ i5-4670K @3.4 GHz or AMD FX-8350 @4.0 GHz",
                        MemoryMinimum = "4 GB RAM DDR3",
                        MemoryRecommended = "8 GB RAM DDR3",
                        StorageMinimum = "40 GB available space",
                        StorageRecommended = "40 GB available space",
                        GraphicsMinimum = "NVIDIA® GeForce® GTX 560 or AMD Radeon™ HD 6870 (1GB VRAM)",
                        GraphicsRecommended = "NVIDIA® GeForce® GTX 780 or AMD Radeon™ R9 290 (2GB VRAM)",
                        ProductType = "Base Game",
                        RefundType = "Self-Refundable",
                        Price = 26.99
                    },
                    new Product
                    {
                        Name = "Hogwarts Legacy",
                        Developer = "Avalanche Software",
                        Publisher = "Warner Bros. Games",
                        Description = "Hogwarts Legacy is an immersive, open-world action RPG. Now you can take control of the action and be at the center of your own adventure in the wizarding world.",
                        Genres = new List<Genre> { Genre.Action, Genre.Adventure, Genre.RPG },
                        PreviewImages = new List<string> { "https://assets.nintendo.eu/image/upload/f_auto/q_auto/v1700461940/NAL/Software%20Nintendo%20Switch/Hogwarts%20Legacy/16x9_NSwitch_HogwartsLegacy.jpg",
                        "https://static0.gamerantimages.com/wordpress/wp-content/uploads/2024/03/hogwarts-legacy-the-10-most-fun-spell-combos.jpg",
                        "https://images.hothardware.com/contentimages/newsitem/60835/content/4x3_1600x1200_highres-hero-hogwarts-legacy-gameplay.jpg",
                        "https://cdn.sortiraparis.com/images/80/66131/817094-hogwarts-legacy-l-heritage-de-poudlard-personnages-combats-le-point-sur-les-annonces.jpg",
                        "https://nichegamer.com/wp-content/uploads/2022/12/hogwarts-legacy-12-18-22-1.jpg",
                        "https://www.dsogaming.com/wp-content/uploads/2023/03/Harry-Potter-Mod-for-Hogwarts-Legacy-header.jpg",
                        "https://cdn.sortiraparis.com/images/80/66131/855816-hogwarts-legacy-comment-ouvrir-les-portes-arithmantiques.jpg"},
                        Features = new List<Features> { Features.CloudSaves, Features.SinglePlayer },
                        ReleaseDate = new DateTime(2023, 02, 11),
                        //LastUpdatedBy = context.User.Find(3),
                        LastUpdated = DateTime.Now,
                        OSMinimum = "Windows 10 64 bit",
                        OSRecommended = "Windows 10 64 bit",
                        ProcessorMinimum = "Intel Core i5-6600 (3.3Ghz) or AMD Ryzen 5 1400 (3.2Ghz)",
                        ProcessorRecommended = "Intel Core i7-8700 (3.2Ghz) or AMD Ryzen 5 3600 (3.6 Ghz)",
                        MemoryMinimum = "16 GB RAM DDR3",
                        MemoryRecommended = "16 GB RAM DDR3",
                        StorageMinimum = "85 GB available space",
                        StorageRecommended = "85 GB available space",
                        GraphicsMinimum = "NVIDIA GeForce GTX 960 4GB or AMD Radeon RX 470 4GB",
                        GraphicsRecommended = "NVIDIA GeForce 1080 Ti or AMD Radeon RX 5700 XT or INTEL Arc A770",
                        ProductType = "Base Game",
                        RefundType = "Self-Refundable",
                        Price = 79.90
                    },
                    new Product
                    {
                        Name = "Alan Wake 2",
                        Developer = "Remedy Entertainment",
                        Publisher = "Epic Games Publishing",
                        Description = "Saga Anderson arrives to investigate ritualistic murders in a small town. Alan Wake pens a dark story to shape the reality around him. These two heroes are somehow connected. Can they become the heroes they need to be?",
                        Genres = new List<Genre> { Genre.Action, Genre.Adventure, Genre.Horror, Genre.Shooter },
                        PreviewImages = new List<string> { "https://image.api.playstation.com/vulcan/ap/rnd/202305/3116/b1641ab1b5ec0c8f76c44e59e9d8a1639c913c98c09d057f.jpg",
                        "https://www.alanwake.com/wp-content/uploads/2021/12/Blog_batch_notes.jpg",
                        "https://www.alanwake.com/wp-content/uploads/2021/08/news_announcing-1300x650.jpg",
                        "https://miro.medium.com/v2/resize:fit:1400/1*28Z8x5Rq5VGlC9BNAp3TbA.jpeg",
                        "https://static0.gamerantimages.com/wordpress/wp-content/uploads/2023/07/alan-wake-2-saga-gameplay.jpg",
                        "https://assetsio.gnwcdn.com/alan-wake-remastered-alan.jpg?width=1600&height=900&fit=crop&quality=100&format=png&enable=upscale&auto=webp",
                        "https://www.gameinformer.com/sites/default/files/styles/full/public/2021/10/01/c025d442/alan5.jpg"},
                        Features = new List<Features> { Features.CloudSaves, Features.SinglePlayer },
                        ReleaseDate = new DateTime(2023, 02, 10),
                        //LastUpdatedBy = context.User.Find(3),
                        LastUpdated = DateTime.Now,
                        OSMinimum = "Windows 10/11 64-bit",
                        OSRecommended = "Windows 10/11 64-bit",
                        ProcessorMinimum = "Intel i5-7600K or AMD equivalent",
                        ProcessorRecommended = "Ryzen 7 3700X or Intel equivalent",
                        MemoryMinimum = "16 GB RAM",
                        MemoryRecommended = "16 GB RAM",
                        StorageMinimum = "90 GB available space",
                        StorageRecommended = "90 GB available space",
                        GraphicsMinimum = "GeForce GTX 1070 / Radeon RX 5600 XT",
                        GraphicsRecommended = "GeForce RTX 3060 / Radeon RX 6600 XT",
                        ProductType = "Base Game",
                        RefundType = "Self-Refundable",
                        Price = 69.80
                    },
                    new Product
                    {
                        Name = "Dead Island 2",
                        Developer = "Deep Silver Dambuster Studios",
                        Publisher = "Deep Silver",
                        Description = "A deadly virus is spreading across Los Angeles, turning inhabitants into zombies. Bitten, infected, but more than just immune, uncover the truth behind the outbreak and discover who -or what- you are.",
                        Genres = new List<Genre> { Genre.Action, Genre.Horror },
                        PreviewImages = new List<string> { "https://image.api.playstation.com/vulcan/ap/rnd/202208/1117/Wk5Ck2VNWZ1aiLj6DF3zrlxx.jpg",
                        "https://m.media-amazon.com/images/I/71q2Gzpg77L._AC_UF1000,1000_QL80_.jpg",
                        "https://toyorgame.com.sg/cdn/shop/products/2_5c2d4238-c0b5-43c5-b50d-d902dc505c35.jpg?v=1669794854&width=4608",
                        "https://www.xtgamer.de/wp-content/uploads/Dead-Island-2-03.jpg",
                        "https://toyorgame.com.sg/cdn/shop/products/1_c7868101-1353-4c0e-aab7-bc587c3bfc96.jpg?v=1669794854&width=4608",
                        "https://toyorgame.com.sg/cdn/shop/products/4_9da97dd4-54dc-4ca7-9e3d-90106a5eae93.jpg?v=1669794854&width=4608",
                        "https://toyorgame.com.sg/cdn/shop/products/5_0f767f26-6b78-4ada-a7f6-a32eb2b20f2e.jpg?v=1669794854&width=5378"},
                        Features = new List<Features> { Features.CloudSaves, Features.SinglePlayer, Features.Coop },
                        ReleaseDate = new DateTime(2015, 01, 26),
                        //LastUpdatedBy = context.User.Find(3),
                        LastUpdated = DateTime.Now,
                        OSMinimum = "Windows 10",
                        OSRecommended = "Windows 10",
                        ProcessorMinimum = "AMD FX-9590 or Intel Core i7-7700HQ",
                        ProcessorRecommended = "Ryzen 5 5600X or Intel Core i9-9900k",
                        MemoryMinimum = "10 GB RAM",
                        MemoryRecommended = "10 GB RAM",
                        StorageMinimum = "70 GB available space",
                        StorageRecommended = "70 GB available space",
                        GraphicsMinimum = "Radeon R9 390X (8192 VRAM) or GeForce GTX 1060 (6144 VRAM)",
                        GraphicsRecommended = "Radeon RX 6800 XT (16384 VRAM) or GeForce RTX 2070 Super (8192 MB)",
                        ProductType = "Base Game",
                        RefundType = "Self-Refundable",
                        Price = 78.01
                    },
                    new Product
                    {
                        Name = "Sleeping Dogs: Definitive Edition",
                        Developer = "United Front Games, Feral Interactive (Mac)",
                        Publisher = "Square Enix, Feral Interactive (Mac)",
                        Description = "The Definitive Edition of the critically acclaimed, award winning open-world action adventure, reworked, rebuilt and re-mastered for the new generation. With all previously available DLC included and a wealth of tech and visual improvements, Hong Kong has never felt so alive.",
                        Genres = new List<Genre> { Genre.Action, Genre.Adventure, Genre.Racing },
                        PreviewImages = new List<string> { "https://gaming-cdn.com/images/products/545/orig/sleeping-dogs-definitive-edition-definitive-edition-pc-mac-game-steam-cover.jpg?v=1704193741",
                        "https://www.gameshome.com.sg/wp-content/uploads/2014/11/814o7QiW9mL._SL1500_.jpg",
                        "https://www.gameshome.com.sg/wp-content/uploads/2014/11/81FnajtEnDL._SL1500_.jpg",
                        "https://shared.akamai.steamstatic.com/store_item_assets/steam/apps/307690/ss_4dcb98a706d4cf45a102cd74fe1c24902ec849e5.1920x1080.jpg?t=1602800785",
                        "https://shared.akamai.steamstatic.com/store_item_assets/steam/apps/307690/ss_a4681f10ebee79a31369504b17303f476ca84a45.1920x1080.jpg?t=1602800785",
                        "https://image.api.playstation.com/vulcan/img/rnd/202010/0921/5ksz2KENAivDQYNCw1UwC16N.jpg",
                        "https://www.gameshome.com.sg/wp-content/uploads/2014/11/816XvkzgbML._SL1500_.jpg"},
                        Features = new List<Features> { Features.CloudSaves, Features.SinglePlayer },
                        ReleaseDate = new DateTime(2014, 10, 08),
                        //LastUpdatedBy = context.User.Find(3),
                        LastUpdated = DateTime.Now,
                        OSMinimum = "Windows Vista 64bit, Window 7 64bit, Windows 8 64bit (32bit O/S not supported)",
                        OSRecommended = "Windows Vista 64bit, Window 7 64bit, Windows 8 64bit (32bit O/S not supported)",
                        ProcessorMinimum = "Core 2 Duo 2.4GHz or Athlon X2 2.7GHz",
                        ProcessorRecommended = "Core i5-2300, Phenom II X4 940 or better",
                        MemoryMinimum = "4 GB",
                        MemoryRecommended = "8 GB",
                        StorageMinimum = "20 GB available space",
                        StorageRecommended = "20 GB available space",
                        GraphicsMinimum = "DirectX 10 or 11 compatible card, ATI Radeon 3870 or higher, NVIDIA GeForce 8800 GT or higher with 512MB graphics memory, Intel HD Graphics 2500 or higher",
                        GraphicsRecommended = "DirectX 10 or 11 compatible card, ATI Radeon 7750, NVIDIA GeForce GTX 560 or higher with 1GB graphics memory, Intel HD Graphics 4000 or higher",
                        ProductType = "Base Game",
                        RefundType = "Self-Refundable",
                        Price = 27.10
                    },
                    new Product
                    {
                        Name = "The Elder Scrolls V: Skyrim Special Edition",
                        Developer = "Bethesda Game Studios",
                        Publisher = "Bethesda Softworks",
                        Description = "Winner of more than 200 Game of the Year Awards, The Elder Scrolls V: Skyrim Special Edition brings the epic fantasy to life in stunning detail. The Special Edition includes the critically acclaimed game and add-ons with all-new features.",
                        Genres = new List<Genre> { Genre.Action, Genre.Open_world, Genre.RPG },
                        PreviewImages = new List<string> { "https://image.api.playstation.com/vulcan/ap/rnd/202110/2019/aDSOgerXg4V6sf5A7VzHiTun.jpg",
                        "https://cdn.wccftech.com/wp-content/uploads/2016/12/Skyrim-Special-Edition-PC-update.jpg",
                        "https://cdn.mos.cms.futurecdn.net/MsJUxavbVfQ2ByWZ4LX6Uj.jpg",
                        "https://oyster.ignimgs.com/wordpress/stg.ign.com/2016/11/skyrim1.png",
                        "https://i0.wp.com/gamingrespawn.com/wp-content/uploads/2016/10/Skyrim_20161027172410.jpg?ssl=1",
                        "https://assets-prd.ignimgs.com/2021/06/02/best-skyrim-mods-featured-01-1622593251173.jpg",
                        "https://cdn.wccftech.com/wp-content/uploads/2016/10/25696-1-1365678484.jpg"},
                        Features = new List<Features> { Features.CloudSaves, Features.SinglePlayer },
                        ReleaseDate = new DateTime(2022, 10, 06),
                        //LastUpdatedBy = context.User.Find(3),
                        LastUpdated = DateTime.Now,
                        OSMinimum = "Windows 7/8.1/10 (64-bit Version)",
                        OSRecommended = "Windows 7/8.1/10 (64-bit Version)",
                        ProcessorMinimum = "Intel i5-750/AMD Phenom II X4-945",
                        ProcessorRecommended = "Intel i5-2400/AMD FX-8320",
                        MemoryMinimum = "8 GB",
                        MemoryRecommended = "8 GB",
                        StorageMinimum = "12 GB available space",
                        StorageRecommended = "12 GB available space",
                        GraphicsMinimum = "NVIDIA GTX 470 1GB /AMD HD 7870 2GB",
                        GraphicsRecommended = "NVIDIA GTX 780 3GB /AMD R9 290 4GB",
                        ProductType = "Base Game",
                        RefundType = "Self-Refundable",
                        Price = 54.99
                    },
                    new Product
                    {
                        Name = "Lords of the Fallen Deluxe Edition",
                        Developer = "HEXWORKS",
                        Publisher = "CI Games",
                        Description = "A vast world awaits in all-new, dark fantasy action-RPG, Lords of the Fallen. As one of the fabled Dark Crusaders, embark on an epic quest to overthrow Adyr, the demon God.",
                        Genres = new List<Genre> { Genre.Action, Genre.RPG },
                        PreviewImages = new List<string> { "https://image.api.playstation.com/vulcan/ap/rnd/202404/2210/f51f6d0574ca0210a16f2b1f6b3ed9feacb325d40f1d764e.jpg",
                        "https://image.api.playstation.com/vulcan/ap/rnd/202310/1010/9963554c205679580fc8e6c20a97a8ccbe32b3ee94484143.jpg",
                        "https://m.media-amazon.com/images/I/81ycERXtOnL.jpg",
                        "https://image.api.playstation.com/vulcan/ap/rnd/202304/2408/c9d4827a19f3bfa48e24c368ad21d4723f76cab66fc9bfdb.jpg",
                        "https://image.api.playstation.com/vulcan/ap/rnd/202304/2408/9a7ca8b89a561d8c795c2433a188f0bc91047a3f90c730e9.jpg",
                        "https://m.media-amazon.com/images/I/910CasGG8vL.jpg",
                        "https://image.api.playstation.com/vulcan/ap/rnd/202304/2408/df6677919697a425b1b72b4f6a859d39d14ccc1ab008af62.jpg"},
                        Features = new List<Features> { Features.CloudSaves, Features.SinglePlayer, Features.Coop, Features.Multiplayer },
                        ReleaseDate = new DateTime(2023, 10, 13),
                        //LastUpdatedBy = context.User.Find(3),
                        LastUpdated = DateTime.Now,
                        OSMinimum = "Windows 10 64bit",
                        OSRecommended = "Windows 10 64bit",
                        ProcessorMinimum = "Intel i5 8400 / AMD Ryzen 5 2600",
                        ProcessorRecommended = "Intel i7 8700 / AMD Ryzen 5 3600",
                        MemoryMinimum = "12 GB",
                        MemoryRecommended = "16 GB",
                        StorageMinimum = "45 GB available space",
                        StorageRecommended = "45 GB available space",
                        GraphicsMinimum = "6GBs VRAM | NVIDIA GTX-1060 | AMD Radeon RX 590",
                        GraphicsRecommended = "8GBs VRAM | NVIDIA RTX-2080 | AMD Radeon RX 6700",
                        ProductType = "Edition",
                        RefundType = "Self-Refundable",
                        Price = 89.90
                    },
                    new Product
                    {
                        Name = "The Witcher 3: Wild Hunt – Complete Edition",
                        Developer = "CD PROJEKT RED",
                        Publisher = "CD PROJEKT S.A.",
                        Description = "The most awarded game of a generation, now enhanced for the next! Experience The Witcher 3: Wild Hunt and its expansions in this definitive collection, boasting improved visuals and performance, new additional content, Photo Mode, and more!",
                        Genres = new List<Genre> {  },
                        PreviewImages = new List<string> { "https://cdn1.epicgames.com/offer/14ee004dadc142faaaece5a6270fb628/EGS_TheWitcher3REDkit_CDPROJEKTRED_DLC_S1_2560x1440-5c08aca3a868d336439fea829937bf7a",
                        "https://cdn2.unrealengine.com/tw3ng-presspreviews-pc-geralt-on-horseback-rgb-en-3840x2160-884cdcf30eed.png",
                        "https://cdn3.vox-cdn.com/uploads/chorus_asset/file/3687334/The_Witcher_3_Wild_Hunt_Some_creatures_will_be_more_prone_to_inflammation_than_others-The_Igni_sign_works_perfectly_on_the_fiend.0.png",
                        "https://cdn.mobilesyrup.com/wp-content/uploads/2021/02/the-witcher-3-scaled.jpg",
                        "https://gameranx.com/wp-content/uploads/2016/08/Witcher3WildHuntGOTY7.png",
                        "https://i0.wp.com/615film.com//wp-content/uploads/2015/05/witcher3.1.jpg?ssl=1",
                        "https://www.gamereactor.eu/media/67/kolladessalackra_1436764b.jpg"},
                        Features = new List<Features> { Features.CloudSaves, Features.SinglePlayer },
                        ReleaseDate = new DateTime(2020, 05, 14),
                        //LastUpdatedBy = context.User.Find(3),
                        LastUpdated = DateTime.Now,
                        OSMinimum = "64-bit Windows 7, 64-bit Windows 8 (8.1)",
                        OSRecommended = "64-bit Windows 10/11",
                        ProcessorMinimum = "Intel CPU Core i5-2500K 3.3GHz / AMD A10-5800K APU (3.8GHz)",
                        ProcessorRecommended = "Intel CPU Core i5 7400 / Ryzen 5 1600",
                        MemoryMinimum = "6 GB",
                        MemoryRecommended = "8 GB",
                        StorageMinimum = "50 GB available space",
                        StorageRecommended = "50 GB available space",
                        GraphicsMinimum = "Nvidia GPU GeForce GTX 660 / AMD GPU Radeon HD 7870",
                        GraphicsRecommended = "Nvidia GeForce GTX 1070 / Radeon RX 480",
                        ProductType = "Base Game",
                        RefundType = "Self-Refundable",
                        Price = 59.99
                    },
                    new Product
                    {
                        Name = "Portal 2",
                        Developer = "Valve",
                        Publisher = "Valve",
                        Description = "The 'Perpetual Testing Initiative' has been expanded to allow you to design co-op puzzles for you and your friends!",
                        Genres = new List<Genre> { Genre.Action, Genre.Adventure },
                        PreviewImages = new List<string> { "https://assets.nintendo.com/image/upload/c_fill,w_1200/q_auto:best/f_auto/dpr_2.0/ncom/software/switch/70010000050313/75484f73fedd25cb830c5d93fbb3fca643a5ec0b09df2815291ead880bc7d6b1",
                        "https://media.wired.com/photos/5932a9c29be5e55af6c26664/master/pass/old_testchamber.jpg",
                        "https://shared.akamai.steamstatic.com/store_item_assets/steam/apps/620/ss_6a4f5afdaa98402de9cf0b59fed27bab3256a6f4.1920x1080.jpg?t=1720037388",
                        "https://www.anuflora.com/game/wp-content/uploads/game/2015/01/portal2-screenshots-7.jpg",
                        "https://i0.wp.com/natalie.tf/wp-content/uploads/2015/06/portal2-1.jpg?ssl=1",
                        "https://www.gamereactor.eu/media/27/portal2_242735b.jpg",
                        "https://1.bp.blogspot.com/-61dWFiXUN80/TfixTssM6FI/AAAAAAAAAmU/hHD2AP2iS9g/s1600/portal2%2B2011-06-11%2B10-27-15-88.bmp"},
                        Features = new List<Features> { Features.CloudSaves, Features.SinglePlayer, Features.Coop },
                        ReleaseDate = new DateTime(2020, 05, 14),
                        //LastUpdatedBy = context.User.Find(3),
                        LastUpdated = DateTime.Now,
                        OSMinimum = "Windows 7 / Vista / XP",
                        ProcessorMinimum = "3.0 GHz P4, Dual Core 2.0 (or higher) or AMD64X2 (or higher)",
                        MemoryMinimum = "2 GB RAM",
                        StorageMinimum = "8 GB available space",
                        GraphicsMinimum = "Video card must be 128 MB or more and with support for Pixel Shader 2.0b (ATI Radeon X800 or higher / NVIDIA GeForce 7600 or higher / Intel HD Graphics 2000 or higher).",
                        ProductType = "Base Game",
                        RefundType = "Self-Refundable",
                        Price = 10.00
                    },
                    new Product
                    {
                        Name = "Wildermyth",
                        Developer = "Worldwalker Games",
                        Publisher = "Worldwalker Games",
                        Description = "Wildermyth follows heroes over their whole careers, from their pitchfork days to their powerful primes, and into old age and memory. It’s a party-based procedural storytelling RPG where tactical combat and story decisions will alter your world and reshape your cast of characters.",
                        Genres = new List<Genre> { Genre.RPG, Genre.Strategy, Genre.Turn_based },
                        PreviewImages = new List<string> { "https://static0.gamerantimages.com/wordpress/wp-content/uploads/2021/06/wildermyth.jpg",
                        "https://www.gameinformer.com/sites/default/files/styles/full/public/2021/07/26/22aee4d7/wilder6.jpg",
                        "https://assetsio.gnwcdn.com/wildermyth.jpg?width=1920&height=1920&fit=bounds&quality=80&format=jpg&auto=webp",
                        "https://sm.ign.com/ign_ap/gallery/w/wildermyth/wildermyth-screenshots_exdx.jpg",
                        "https://cdn.vox-cdn.com/thumbor/m6RVC07DBlLQgXvYXAgQCVQmiv4=/0x0:1920x1080/1200x800/filters:focal(807x387:1113x693)/cdn.vox-cdn.com/uploads/chorus_image/image/66257777/wildermyth_5.0.png",
                        "https://images.squarespace-cdn.com/content/v1/5c1082f850a54fbdb1b67806/1624858445648-8Z69NV0KICXTSCUGYM3Y/Wildermyth+8.jpg",
                        "https://goldenagegamers.com/wp-content/uploads/2021/07/20210717194308_1.jpg"},
                        Features = new List<Features> { Features.CloudSaves, Features.SinglePlayer },
                        ReleaseDate = new DateTime(2022, 04, 23),
                        //LastUpdatedBy = context.User.Find(3),
                        LastUpdated = DateTime.Now,
                        OSMinimum = "Windows 7",
                        OSRecommended = "Windows 7",
                        ProcessorMinimum = "i3 or similar",
                        ProcessorRecommended = "i5 or similar",
                        MemoryMinimum = "3 GB",
                        MemoryRecommended = "4 GB",
                        StorageMinimum = "2 GB available space",
                        StorageRecommended = "2 GB available space",
                        GraphicsMinimum = "OpenGL 3.2",
                        GraphicsRecommended = "OpenGL 3.2",
                        ProductType = "Edition",
                        RefundType = "Self-Refundable",
                        Price = 22.99
                    }
                );
            }


            context.SaveChanges();
        }
        var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
        SeedRoles(roleManager).Wait();
        var userManager = serviceProvider.GetRequiredService<UserManager<BITSUser>>();
        SeedUsers(userManager).Wait();

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

    private static async Task SeedUsers(UserManager<BITSUser> userManager)
    {
        var isAdminExist = await userManager.FindByEmailAsync("admin@gmail.com");

        if (isAdminExist == null)
        {
            BITSUser admin = new BITSUser();
            admin.Name = "GM001";
            admin.Email = "admin@gmail.com";
            admin.UserName = "admin@gmail.com";
            await userManager.CreateAsync(admin, "Abcd1234!");
            await userManager.AddToRoleAsync(admin, "Admin");
        }

        var isEmployeeExist = await userManager.FindByEmailAsync("employee@gmail.com");
        if (isEmployeeExist == null)
        {
            BITSUser employee = new BITSUser();
            employee.Name = "EMP001";
            employee.Email = "employee@gmail.com";
            employee.UserName = "employee@gmail.com";
            await userManager.CreateAsync(employee, "Abcd1234!");
            await userManager.AddToRoleAsync(employee, "Employee");
        }

        var isCustomerExist = await userManager.FindByEmailAsync("customer@gmail.com");
        if (isCustomerExist == null)
        {
            BITSUser customer = new BITSUser();
            customer.Name = "Xiao Ming";
            customer.Email = "customer@gmail.com";
            customer.UserName = "customer@gmail.com";
            await userManager.CreateAsync(customer, "Abcd1234!");
            await userManager.AddToRoleAsync(customer, "Customer");
        }
    }
}
