using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RD.Models;
using RD.Models.Sections;
using RD.Models.Sections.ExpectSect;
using RD.Models.Sections.IdenSect;
using RD.Models.Sections.StatSect;
using System;
using System.Collections.Generic;
using System.Text;


namespace RinDate.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Image> Images { get; set; }
        public DbSet<Tribe> Tribes { get; set; }
        public DbSet<ExpItems> ExpItems { get; set; }
        public DbSet<MeetItems> MeetItems { get; set; }
        public DbSet<Pronouns> Pronouns { get; set; }
        public DbSet<State> States { get; set; }
        public DbSet<Ethicity> Ethicities { get; set; }
        public DbSet<Relationship> Relationships { get; set; }
        public DbSet<Body> Bodies { get; set; }
        public DbSet<Role> Roles { get; set; }

    }
}
