using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MobilOkulProc.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MobilOkulProc.WebAPI.Data
{
    public class MobilOkulContext : DbContext
    {
        public MobilOkulContext()
        {

        }

        public MobilOkulContext(DbContextOptions<MobilOkulContext> options) : base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionBuilder)
        {
            
            base.OnConfiguring(optionBuilder);
            var builder = new ConfigurationBuilder().AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            IConfigurationRoot cfg = builder.Build();
            optionBuilder.UseSqlServer(cfg.GetConnectionString("sqlDatabase"));
           
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {

        }


        public DbSet<SCHOOL> SCHOOLS { get; set; }
        public DbSet<BRANCH> BRANCHS { get; set; }
        public DbSet<CITY> CITYS { get; set; }
        public DbSet<CLASS> CLASSES { get; set; }
        public DbSet<CLASS_SECTION> CLASS_SECTIONS { get; set; }
        public DbSet<EDUCATIONAL_INSTITUTION> EDUCATIONAL_INSTITUTIONs { get; set; }
        public DbSet<EDUCATIONAL_TERM> EDUCATIONAL_TERMS { get; set; }
        public DbSet<EMPLOYEE_TYPE> EMPLOYEE_TYPES { get; set; }
        public DbSet<FEEDBACK> FEEDBACKS { get; set; }
        public DbSet<MESSAGE> MESSAGES { get; set; }
        public DbSet<NEWS> NEWSES { get; set; }
        public DbSet<PARENT> PARENTS { get; set; }
        public DbSet<SCHOOL_EMPLOYER> SCHOOL_EMPLOYERS { get; set; }
        public DbSet<SECTION> SECTIONS { get; set; }
        public DbSet<STUDENT> STUDENTS { get; set; }
        public DbSet<STUDENT_CLASS> STUDENT_CLASSes { get; set; }
        public DbSet<STUDENT_PARENT> STUDENT_PARENTS { get; set; }
        public DbSet<TEACHER> TEACHERS { get; set; }
        public DbSet<USER> USERS { get; set; }

    }
}
