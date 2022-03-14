using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

using System.Linq;
using System.Threading.Tasks;

namespace ChatApplication.Models
{
   

        public class ApplicationContext : DbContext
        {
            public DbSet<Person> Person { get; set; }
             public DbSet<Chat> Chat { get; set; }

        public ApplicationContext(DbContextOptions<ApplicationContext> options)
                : base(options)
            {
                Database.EnsureCreated();   // создаем базу данных при первом обращении
            }
     
    }

    }

