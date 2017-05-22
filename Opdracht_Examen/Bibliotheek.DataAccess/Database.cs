using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;
using System.Data.Entity;
using Bibliotheek.DataModel;
using System.Threading.Tasks;

namespace Bibliotheek.DataAccess
{
    public class Database : DbContext
    {
        public static readonly string connectionString = "Server=tcp:my.djohnnie.be; Database=Opdracht2_Johan; User ID =Programmeren13822@programmeren3; Password = Programmeren3;" +
           " Trusted_Connection = False;";

        //private static readonly string connectionString = "Server=tcp:ACERSERVER2016; Database=Opdracht1; User ID =Programmeren3; Password =!SQLServer2016auth!;";

        public DbSet<Boek> Boeken { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public Database() : base(connectionString) { }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Boek>().ToTable("Boeken");
            modelBuilder.Entity<Boek>().HasKey(x => x.Code);
            modelBuilder.Entity<Boek>().Property(x => x.Titel).IsRequired();

            modelBuilder.Entity<Genre>().ToTable("Genres");
            modelBuilder.Entity<Genre>().HasKey(x => x.Code);
            modelBuilder.Entity<Genre>().Property(x => x.Omschrijving).IsRequired();
            base.OnModelCreating(modelBuilder);
        }
    }
}
