using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TabloidCLI.Models;
using TabloidCLI.Repositories;

namespace TabloidCLI.UserInterfaceManagers
{
    public class NoteManager : IUserInterfaceManager
    {
        private readonly IUserInterfaceManager _parentUI;
        private NoteRepository _noteRepository;
        private string _connectionString;
        private int _postId;

        public NoteManager(IUserInterfaceManager parentUI, string connectionString, int postId)
        {   
            _parentUI = parentUI;
            _connectionString = connectionString;
            _noteRepository = new NoteRepository(connectionString);
            _postId = postId;
        }
        public IUserInterfaceManager Execute()
        {
            Console.WriteLine("Note Menu");
            Console.WriteLine(" 1) List Notes");
            Console.WriteLine(" 2) Add Notes");
            Console.WriteLine(" 3) Remove Notes");
            Console.WriteLine(" 0) Go Back");

            Console.Write("> ");
            string choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    List();
                    return this;
                case "2":
                    AddNote();
                    return this;
                case "3":
  
                    return this;
                case "0":
                    return _parentUI;
                default:
                    Console.WriteLine("Invalid Selection");
                    return this;
            }
        }
        private void List()
        {
            List<Note> notes = _noteRepository.GetAll(_postId);
            foreach (Note n in notes)
            {
                Console.WriteLine();
                Console.WriteLine($"Title: {n.Title}");
                Console.WriteLine($"Content: {n.Content}");
                Console.WriteLine($"Date Created: {n.CreateDateTime}");
                Console.WriteLine();
            }
        }

        private void AddNote()
        {
            Console.WriteLine("New Note");
            Note note = new Note();

            Console.WriteLine("Title: ");
            note.Title = Console.ReadLine();

            Console.WriteLine("Content: ");
            note.Content = Console.ReadLine();
            note.CreateDateTime = DateTime.Now;
            note.PostId = _postId;

            _noteRepository.Insert(note);

        }

    }
}
