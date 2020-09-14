
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PolSourceBackendDeveloperAssignment.Models
{
    public class NotesContext : DbContext
    {
        public NotesContext(DbContextOptions opt) : base(opt)
        {
        }

        public DbSet<Note> Notes { get; set; }
        public DbSet<NoteHistory> NoteHistories { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
            modelBuilder.Entity<NoteHistory>().HasKey(Note => new { Note.IdNote, Note.Version });

            //default GetDate
            modelBuilder.Entity<Note>().Property(Note => Note.Created).HasDefaultValueSql("GETDATE()");
            modelBuilder.Entity<Note>().Property(Note => Note.Modified).HasDefaultValueSql("GETDATE()");

            modelBuilder.Entity<NoteHistory>().Property(Note => Note.Modified).HasDefaultValueSql("GETDATE()");
            base.OnModelCreating(modelBuilder);
        }


    }
}
