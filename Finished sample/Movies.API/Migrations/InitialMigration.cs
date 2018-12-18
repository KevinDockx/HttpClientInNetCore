using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Movies.API.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Directors",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    FirstName = table.Column<string>(maxLength: 200, nullable: false),
                    LastName = table.Column<string>(maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Directors", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Movies",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Title = table.Column<string>(maxLength: 200, nullable: false),
                    Description = table.Column<string>(maxLength: 2000, nullable: true),
                    Genre = table.Column<string>(maxLength: 200, nullable: true),
                    ReleaseDate = table.Column<DateTimeOffset>(nullable: false),
                    DirectorId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Movies", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Movies_Directors_DirectorId",
                        column: x => x.DirectorId,
                        principalTable: "Directors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Directors",
                columns: new[] { "Id", "FirstName", "LastName" },
                values: new object[,]
                {
                    { new Guid("d28888e9-2ba9-473a-a40f-e38cb54f9b35"), "Quentin", "Tarantino" },
                    { new Guid("da2fd609-d754-4feb-8acd-c4f9ff13ba96"), "Joel", "Coen" },
                    { new Guid("c19099ed-94db-44ba-885b-0ad7205d5e40"), "Martin", "Scorsese" },
                    { new Guid("0c4dc798-b38b-4a1c-905c-a9e76dbef17b"), "David", "Fincher" },
                    { new Guid("937b1ba1-7969-4324-9ab5-afb0e4d875e6"), "Bryan", "Singer" },
                    { new Guid("7a2fbc72-bb33-49de-bd23-c78fceb367fc"), "James", "Cameron" }
                });

            migrationBuilder.InsertData(
                table: "Movies",
                columns: new[] { "Id", "Description", "DirectorId", "Genre", "ReleaseDate", "Title" },
                values: new object[,]
                {
                    { new Guid("5b1c2b4d-48c7-402a-80c3-cc796ad49c6b"), "The lives of two mob hitmen, a boxer, a gangster's wife, and a pair of diner bandits intertwine in four tales of violence and redemption.", new Guid("d28888e9-2ba9-473a-a40f-e38cb54f9b35"), "Crime, Drama", new DateTimeOffset(new DateTime(1994, 11, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 1, 0, 0, 0)), "Pulp Fiction" },
                    { new Guid("6e87f657-f2c1-4d90-9b37-cbe43cc6adb9"), "A middle-aged woman finds herself in the middle of a huge conflict that will either make her a profit or cost her life.", new Guid("d28888e9-2ba9-473a-a40f-e38cb54f9b35"), "Crime, Drama", new DateTimeOffset(new DateTime(1997, 12, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 1, 0, 0, 0)), "Jackie Brown" },
                    { new Guid("d8663e5e-7494-4f81-8739-6e0de1bea7ee"), "The Dude (Lebowski), mistaken for a millionaire Lebowski, seeks restitution for his ruined rug and enlists his bowling buddies to help get it.", new Guid("da2fd609-d754-4feb-8acd-c4f9ff13ba96"), "Comedy, Crime", new DateTimeOffset(new DateTime(1998, 3, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 1, 0, 0, 0)), "The Big Lebowski" },
                    { new Guid("f9a16fee-4c49-41bb-87a1-bbaad0cd1174"), "A tale of greed, deception, money, power, and murder occur between two best friends: a mafia enforcer and a casino executive, compete against each other over a gambling empire, and over a fast living and fast loving socialite.", new Guid("c19099ed-94db-44ba-885b-0ad7205d5e40"), "Crime, Drama", new DateTimeOffset(new DateTime(1995, 11, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 1, 0, 0, 0)), "Casino" },
                    { new Guid("bb6a100a-053f-4bf8-b271-60ce3aae6eb5"), "An insomniac office worker and a devil-may-care soapmaker form an underground fight club that evolves into something much, much more.", new Guid("0c4dc798-b38b-4a1c-905c-a9e76dbef17b"), "Drama", new DateTimeOffset(new DateTime(1999, 10, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 2, 0, 0, 0)), "Fight Club" },
                    { new Guid("3d2880ae-5ba6-417c-845d-f4ebfd4bcac7"), "A sole survivor tells of the twisty events leading up to a horrific gun battle on a boat, which began when five criminals met at a seemingly random police lineup.", new Guid("937b1ba1-7969-4324-9ab5-afb0e4d875e6"), "Crime, Thriller", new DateTimeOffset(new DateTime(1995, 9, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 2, 0, 0, 0)), "The Usual Suspects" },
                    { new Guid("26fcbcc4-b7f7-47fc-9382-740c12246b59"), "AA cyborg, identical to the one who failed to kill Sarah Connor, must now protect her teenage son, John Connor, from a more advanced and powerful cyborg.", new Guid("7a2fbc72-bb33-49de-bd23-c78fceb367fc"), "Action, Sci-Fi", new DateTimeOffset(new DateTime(1991, 7, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 2, 0, 0, 0)), "Terminator 2: Judgment Day" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Movies_DirectorId",
                table: "Movies",
                column: "DirectorId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Movies");

            migrationBuilder.DropTable(
                name: "Directors");
        }
    }
}
