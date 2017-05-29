using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace Quizmaker_Report_Suite.DAL
{
    public class DALContext:DbContext
    {
        public DALContext():base("DefaultConnection")
        {

        }

        public DbSet<Quizmaker_Report_Suite.Models.Quiz> Quizs { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}