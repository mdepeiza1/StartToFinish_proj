using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Start_To_Finish.Models
{
    public class ToDoListMaker
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }

        [ForeignKey("IdentityUser")]
        public string IdentityUserId { get; set; }
        public IdentityUser IdentityUser { get; set; }
        public IList<Note> Notes { get; set; }
        //[ForeignKey("Note")]
        //public ICollection<Note> NotesToDo { get; set; }
        //[ForeignKey("Note")]
        //public ICollection<Note> NotesInProgress { get; set; }
        //[ForeignKey("Note")]
        //public ICollection<Note> NotesComplete { get; set; }

    }
}
