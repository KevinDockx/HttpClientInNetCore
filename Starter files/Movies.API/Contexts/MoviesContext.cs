using Microsoft.EntityFrameworkCore;
using Movies.API.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Movies.API.Contexts
{
    public class MoviesContext : DbContext
    {
        public DbSet<Movie> Movies { get; set; }

        public MoviesContext(DbContextOptions<MoviesContext> options)
            : base(options)
        {
        }

        // seed the database with data
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Director>().HasData(
                new Director()
                {
                    Id = Guid.Parse("d28888e9-2ba9-473a-a40f-e38cb54f9b35"),
                    FirstName = "Quentin",
                    LastName = "Tarantino"
                },
                new Director()
                {
                    Id = Guid.Parse("da2fd609-d754-4feb-8acd-c4f9ff13ba96"),
                    FirstName = "Joel",
                    LastName = "Coen"
                },
                new Director()
                {
                    Id = Guid.Parse("c19099ed-94db-44ba-885b-0ad7205d5e40"),
                    FirstName = "Martin",
                    LastName = "Scorsese"
                },
                new Director()
                {
                    Id = Guid.Parse("0c4dc798-b38b-4a1c-905c-a9e76dbef17b"),
                    FirstName = "David",
                    LastName = "Fincher"
                },
                new Director()
                {
                    Id = Guid.Parse("937b1ba1-7969-4324-9ab5-afb0e4d875e6"),
                    FirstName = "Bryan",
                    LastName = "Singer"
                },
                new Director()
                {
                    Id = Guid.Parse("7a2fbc72-bb33-49de-bd23-c78fceb367fc"),
                    FirstName = "James",
                    LastName = "Cameron"
                });

            modelBuilder.Entity<Movie>().HasData(
                new Movie
                {
                    Id = Guid.Parse("5b1c2b4d-48c7-402a-80c3-cc796ad49c6b"),
                    DirectorId = Guid.Parse("d28888e9-2ba9-473a-a40f-e38cb54f9b35"),
                    Title = "Pulp Fiction",
                    Description = "The lives of two mob hitmen, a boxer, a gangster's wife, and a pair of diner bandits intertwine in four tales of violence and redemption.",
                    ReleaseDate = new DateTimeOffset(new DateTime(1994,11,9)),
                    Genre = "Crime, Drama"
                },
                new Movie
                {
                    Id = Guid.Parse("6e87f657-f2c1-4d90-9b37-cbe43cc6adb9"),
                    DirectorId = Guid.Parse("d28888e9-2ba9-473a-a40f-e38cb54f9b35"),
                    Title = "Jackie Brown",
                    Description = "A middle-aged woman finds herself in the middle of a huge conflict that will either make her a profit or cost her life.",
                    ReleaseDate = new DateTimeOffset(new DateTime(1997, 12, 25)),
                    Genre = "Crime, Drama"
                },
                new Movie
                {
                    Id = Guid.Parse("d8663e5e-7494-4f81-8739-6e0de1bea7ee"),
                    DirectorId = Guid.Parse("da2fd609-d754-4feb-8acd-c4f9ff13ba96"),
                    Title = "The Big Lebowski",
                    Description = "The Dude (Lebowski), mistaken for a millionaire Lebowski, seeks restitution for his ruined rug and enlists his bowling buddies to help get it.",
                    ReleaseDate = new DateTimeOffset(new DateTime(1998, 3, 6)),
                    Genre = "Comedy, Crime"
                },
                  new Movie
                  {
                      Id = Guid.Parse("f9a16fee-4c49-41bb-87a1-bbaad0cd1174"),
                      DirectorId = Guid.Parse("c19099ed-94db-44ba-885b-0ad7205d5e40"),
                      Title = "Casino",
                      Description = "A tale of greed, deception, money, power, and murder occur between two best friends: a mafia enforcer and a casino executive, compete against each other over a gambling empire, and over a fast living and fast loving socialite.",
                      ReleaseDate = new DateTimeOffset(new DateTime(1995, 11, 22)),
                      Genre = "Crime, Drama"
                  },
                  new Movie
                  {
                      Id = Guid.Parse("bb6a100a-053f-4bf8-b271-60ce3aae6eb5"),
                      DirectorId = Guid.Parse("0c4dc798-b38b-4a1c-905c-a9e76dbef17b"),
                      Title = "Fight Club",
                      Description = "An insomniac office worker and a devil-may-care soapmaker form an underground fight club that evolves into something much, much more.",
                      ReleaseDate = new DateTimeOffset(new DateTime(1999, 10, 15)),
                      Genre = "Drama"
                  },
                  new Movie
                  {
                      Id = Guid.Parse("3d2880ae-5ba6-417c-845d-f4ebfd4bcac7"),
                      DirectorId = Guid.Parse("937b1ba1-7969-4324-9ab5-afb0e4d875e6"),
                      Title = "The Usual Suspects",
                      Description = "A sole survivor tells of the twisty events leading up to a horrific gun battle on a boat, which began when five criminals met at a seemingly random police lineup.",
                      ReleaseDate = new DateTimeOffset(new DateTime(1995, 9, 15)),
                      Genre = "Crime, Thriller"
                  },
                  new Movie
                  {
                      Id = Guid.Parse("26fcbcc4-b7f7-47fc-9382-740c12246b59"),
                      DirectorId = Guid.Parse("7a2fbc72-bb33-49de-bd23-c78fceb367fc"),
                      Title = "Terminator 2: Judgment Day",
                      Description = "A cyborg, identical to the one who failed to kill Sarah Connor, must now protect her teenage son, John Connor, from a more advanced and powerful cyborg.",
                      ReleaseDate = new DateTimeOffset(new DateTime(1991, 7, 3)),
                      Genre = "Action, Sci-Fi"
                  });

            base.OnModelCreating(modelBuilder);
        }

    }
}
