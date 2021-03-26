using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Start_To_Finish.Models
{
    public class Note
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        public string NoteInfo { get; set; }
        public string YoutubeInfo { get; set; }
        public string GoogleMapsInfo { get; set; }
        public string SpotifyInfo { get; set; }
        public bool isToDo { get; set; }
        public bool isInProgress { get; set; }
        public bool isComplete { get; set; }
        [BindProperty, Required]
        public string Option { get; set; } = "Neither";
        public string[] Options = new[] { "Responsibility", "Learning Opportunity", "Neither" };

        [ForeignKey("ToDoListMaker")]
        public int ToDoListMakerId { get; set; }
    }
}
