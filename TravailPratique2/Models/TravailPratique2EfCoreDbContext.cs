using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravailPratique2.Models
{
    class TravailPratique2EfCoreDbContext : DbContext
    {
        public DbSet<Patient> Patients { get; set; }
        public  DbSet<Medecin> Medecins { get; set; }

        public DbSet<Prediction> Predictions { get; set; }

        public DbSet<Performance> Performances { get; set; }

        public DbSet<Historique> Historiques { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder dbContextOptionsBuilder)
        {
            string connection_string = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=master;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False";
            string database_name = "Tp2DB2";
            dbContextOptionsBuilder.UseSqlServer($"{connection_string};Database={database_name};");

        }
    }
}
