using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Start_To_Finish.Models
{
    public class NoteToDo
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        public string NoteInfo { get; set; }
        public string YoutubeInfo { get; set; }
        public string GoogleMapsInfo { get; set; }
        public string SpotifyInfo { get; set; }

        [ForeignKey("ToDoListMaker")]
        public int ToDoListMakerId { get; set; }
    }
}
