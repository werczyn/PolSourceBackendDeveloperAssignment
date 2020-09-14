using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PolSourceBackendDeveloperAssignment.Models
{
    public class NoteHistory
    {
        [ForeignKey("Note"), Key, Column(Order = 1)]
        public int IdNote { get; set; }

        [Key, Column(Order = 2)]
        public int Version { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Content { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime Created { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime Modified { get; set; }

    }
}
