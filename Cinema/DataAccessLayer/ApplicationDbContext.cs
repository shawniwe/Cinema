using Cinema.Entities;
using CinemaNS.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaNS.DataAccessLayer
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<CinemaNS.Entities.Session> Sessions { get; set; }
        public DbSet<Row> Rows { get; set; }
        public DbSet<Place> Places { get; set; }
        public DbSet<Person> Persons { get; set; }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Hall> Halls { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<District> Districts { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<CinemaNS.Entities.Cinema> Cinemas { get; set; }
        public DbSet<MovieActor> MovieActors { get; set; }
        public DbSet<MovieProducer> MovieProducers { get; set; }
        public DbSet<MovieOperator> MovieOperators { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
        //public DbSet<MenuItem> MenuItems { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=Cinema;Trusted_Connection=True;");
        }
    }
}
