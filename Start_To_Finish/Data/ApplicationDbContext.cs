using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Start_To_Finish.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Start_To_Finish.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
       
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<ToDoListMaker> ToDoListMakers { get; set; }
        public DbSet<NoteToDo> NotesToDo { get; set; }
        public DbSet<NoteInProgress> NotesInProgress { get; set; }
        public DbSet<NoteComplete> NotesComplete { get; set; }
        public DbSet<Note> Notes { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<IdentityRole>()
                .HasData(
                new IdentityRole
                {
                    Name = "ToDoListMaker",
                    NormalizedName = "ToDoListMaker"
                }
                );
        }
    }
}
