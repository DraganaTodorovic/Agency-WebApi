namespace Agency.Migrations
{
    using Agency.Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Agency.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Agency.Models.ApplicationDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.

            context.Agenti.AddOrUpdate(
                   new Agent() { Id = 1, ImeiPrezime = "Pera Peric", Licenca = "Lic1", GodinaRodjenja = 1960, BrojProdatihNekretnina = 15 },
                   new Agent() { Id = 2, ImeiPrezime = "Mika Mikic", Licenca = "Lic2", GodinaRodjenja = 1970, BrojProdatihNekretnina = 10 },
                   new Agent() { Id = 3, ImeiPrezime = "Zika Zikic", Licenca = "Lic3", GodinaRodjenja = 1980, BrojProdatihNekretnina = 5 }
               );
            context.SaveChanges();

            context.Nekretnine.AddOrUpdate(
                    new Nekretnina() { Id = 1, Mesto = "Novi Sad", AgencijskaOznaka = "Nek01", GodinaIzgradnje = 1974, Kvadratura = 50, Cena = 40000, AgentId = 1 },
                    new Nekretnina() { Id = 2, Mesto = "Beogard", AgencijskaOznaka = "Nek02", GodinaIzgradnje = 1990, Kvadratura = 60, Cena = 50000, AgentId = 2 },
                    new Nekretnina() { Id = 3, Mesto = "Subotica", AgencijskaOznaka = "Nek03", GodinaIzgradnje = 1995, Kvadratura = 55, Cena = 45000, AgentId = 3 },
                    new Nekretnina() { Id = 4, Mesto = "Zrenjanin", AgencijskaOznaka = "Nek04", GodinaIzgradnje = 2010, Kvadratura = 70, Cena = 60000, AgentId = 1 }
                );
            context.SaveChanges();

        }
    }
}
