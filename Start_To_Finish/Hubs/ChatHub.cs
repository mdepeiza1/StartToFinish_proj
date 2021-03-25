using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Start_To_Finish.Data;
using Microsoft.AspNetCore.SignalR;
using Start_To_Finish.Models;
using System.Linq;
using Start_To_Finish.Controllers;
using System.Security.Claims;

namespace Start_To_Finish.Hubs
{
    public class ChatHub : Hub //added string Name to all methods
    {
        private readonly ApplicationDbContext _context;
        private readonly ToDoListMakersController _controller;
        public ChatHub(ApplicationDbContext context, ToDoListMakersController controller)
        {
            _context = context;
            _controller = controller;
        }

        public async Task ChangeColumnInDatabase(string noteId, string sourceId, string targetId)
        {
            var noteToBeRemoved = _context.Notes.Where(n => n.Id == Int32.Parse(noteId)).FirstOrDefault();
            var toDoListMaker = _context.ToDoListMakers.Where(t => t.Id == noteToBeRemoved.ToDoListMakerId).FirstOrDefault();

            if (sourceId == "To-do")
            {
                if(targetId == "In Progress")
                {
                    toDoListMaker.NotesToDo.Remove(noteToBeRemoved);
                    toDoListMaker.NotesInProgress.Add(noteToBeRemoved);
                    await _context.SaveChangesAsync();
                }
                if (targetId == "Complete")
                {
                    toDoListMaker.NotesToDo.Remove(noteToBeRemoved);
                    toDoListMaker.NotesComplete.Add(noteToBeRemoved);
                    await _context.SaveChangesAsync();
                }
            }

            if (sourceId == "In Progress")
            {
                if (targetId == "To-do")
                {
                    toDoListMaker.NotesInProgress.Remove(noteToBeRemoved);
                    toDoListMaker.NotesToDo.Add(noteToBeRemoved);
                    await _context.SaveChangesAsync();
                }
                if (targetId == "Complete")
                {
                    toDoListMaker.NotesInProgress.Remove(noteToBeRemoved);
                    toDoListMaker.NotesComplete.Add(noteToBeRemoved);
                    await _context.SaveChangesAsync();
                }
            }

            if (sourceId == "Complete")
            {
                if (targetId == "To-do")
                {
                    toDoListMaker.NotesComplete.Remove(noteToBeRemoved);
                    toDoListMaker.NotesToDo.Add(noteToBeRemoved);
                    await _context.SaveChangesAsync();
                }
                if (targetId == "In Progress")
                {
                    toDoListMaker.NotesComplete.Remove(noteToBeRemoved);
                    toDoListMaker.NotesInProgress.Add(noteToBeRemoved);
                    await _context.SaveChangesAsync();
                }
            }
        }
    }
}
