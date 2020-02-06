using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using VideoGamesRepository.Data.Models;
using VideoGamesRepository.Models.Enums;

namespace VideoGamesRepository.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public virtual DbSet<VideoGame> VideoGames { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<IdentityRole>().HasData(new IdentityRole { Name = "Admin", NormalizedName = "Admin".ToUpper() });
            
            builder.Entity<VideoGame>().HasData(new List<VideoGame>() { 
                new VideoGame()
            {
                Id = 1,
                Title = "Final Fantasy 7",
                Genre = Genre.RPG,
                PublishedYear = 1997,
                Price = 34.99
            },
                new VideoGame()
            {
                Id = 2,
                Title = "Final Fantasy 8",
                Genre = Genre.RPG,
                PublishedYear = 1998,
                Price = 24.99
            }, new VideoGame()
            {
                Id = 3,
                Title = "Final Fantasy 9",
                Genre = Genre.RPG,
                PublishedYear = 1999,
                Price = 35.99
            }, new VideoGame()
            {
                Id = 4,
                Title = "Twisted Metal 2",
                Genre = Genre.Racing,
                PublishedYear = 1997,
                Price = 24.99
            }, new VideoGame()
            {
                Id = 5,
                Title = "Breath of the Wild",
                Genre = Genre.RPG,
                PublishedYear = 1997,
                Price = 59.99
            }, new VideoGame()
            {
                Id = 6,
                Title = "Ocarina of time",
                Genre = Genre.Puzzle,
                PublishedYear = 1997,
                Price = 30.99
            }
            }
        );
        }
    }
}
