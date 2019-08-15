using MainProjectFull.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MainProjectFull.DAL
{
    public class CodeContext : IdentityDbContext<Users,Role,int>
    {
        public DbSet<Company> Company { get; set; }
        public DbSet<Cv> Cv { get; set; }
        public DbSet<Experience> Experience { get; set; }
        public DbSet<Languages> Languages { get; set; }
        public DbSet<Follow> Follow { get; set; }
        public DbSet<Role> Role { get; set; }
        public DbSet<Messages> Messages { get; set; }
        public DbSet<Notify> Notify { get; set; }
        public DbSet<Position> Position { get; set; }
        public DbSet<Profile> Profile { get; set; }
        public DbSet<Blocklist> Blocklist { get; set; }
        public DbSet<SkillsHeader> SkillsHeader { get; set; }
        public DbSet<SkillsHeaderPosition> SkillsHeaderPosition { get; set; }
        public DbSet<SocialAct> SocialAct { get; set; }
        public DbSet<Users> Users { get; set; }
        public DbSet<UsersLanguages> UsersLanguages { get; set; }
        public DbSet<University> University { get; set; }
        public DbSet<Portfolio> Portfolio { get; set; }
        public DbSet<PortfolioImages> PortfolioImages { get; set; }
        public DbSet<Comments> Comments { get; set; }
        public CodeContext(DbContextOptions<CodeContext> options):base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            
        }
    }
}
