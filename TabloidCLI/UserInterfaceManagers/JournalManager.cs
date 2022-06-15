using System;
using System.Collections.Generic;
using TabloidCLI.Models;
using TabloidCLI.Repositories;

namespace TabloidCLI.UserInterfaceManagers
{
    public class JournalManager : IUserInterfaceManager
    {
        private readonly IUserInterfaceManager _parentUI;
        private JournalRepository _journalRepository;
        private string _connectionString;

        public JournalManager(IUserInterfaceManager parentUI, string connectionString)
        {
            _parentUI = parentUI;
            _journalRepository = new JournalRepository(connectionString);
            _connectionString = connectionString;
        }

        public IUserInterfaceManager Execute()
        {
            Console.WriteLine("Journal Menu");
            Console.WriteLine(" 1) List Journals");
            Console.WriteLine(" 2) Journal Details");
            Console.WriteLine(" 3) Add Journal");
            Console.WriteLine(" 4) Edit Journal");
            Console.WriteLine(" 5) Remove Journal");
            Console.WriteLine(" 0) Go Back");

            Console.Write("> ");
            string choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    List();
                    return this;
                case "3":
                    Add();
                    Console.WriteLine($"You've successfully added a journal entry.");
                    return this;
                case "4":
                    List();
                    Edit();
                    Console.WriteLine($"You've successfully updated your journal entry!");
                    return this;
                case "5":
                    List();
                    Remove();
                    return this;
                default:
                    Console.WriteLine("Invalid Selection");
                    return this;
            }
            
        }

        private void List()
        {
            List<Journal> journals = _journalRepository.GetAll();
            foreach (Journal j in journals)
            {
                Console.WriteLine($"Id: {j.Id}) Date: {j.CreateDateTime} | Title: {j.Title} | Content: {j.Content}");
                Console.WriteLine("");
            }
        }

        //private Journal Choose(string prompt = null)
        //{
        //    if (prompt == null)
        //    {
        //        prompt = "Please choose an Journal:";
        //    }

        //    Console.WriteLine(prompt);

        //    List<Journal> Journals = _journalRepository.GetAll();

        //    for (int i = 0; i < Journals.Count; i++)
        //    {
        //        Journal Journal = Journals[i];
        //        Console.WriteLine($" {i + 1}) {Journal.FullName}");
        //    }
        //    Console.Write("> ");

        //    string input = Console.ReadLine();
        //    try
        //    {
        //        int choice = int.Parse(input);
        //        return Journals[choice - 1];
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine("Invalid Selection");
        //        return null;
        //    }
        //}

        private void Add()
        {
            Console.WriteLine("New Journal");
            Journal journal = new Journal();

            Console.Write("Journal Title: ");
            journal.Title = Console.ReadLine();

            Console.Write("Journal Content: ");
            journal.Content = Console.ReadLine();

            journal.CreateDateTime = DateTime.Now;

            _journalRepository.Insert(journal);
        }

        private void Edit()
        {
            Journal journalToEdit;
            Console.Write("Please provide the id of the journal you'd like to edit: ");
            int journalId = int.Parse(Console.ReadLine());
            journalToEdit = _journalRepository.Get(journalId);
            while (journalToEdit == null)
            {
                Console.WriteLine("Whoops, try again");
                Console.Write("Please provide the id of the journal you'd like to edit: ");
                journalId = int.Parse(Console.ReadLine());
                journalToEdit = _journalRepository.Get(journalId);
            }

            Console.WriteLine();
            Console.Write("New Journal Title - (blank to leave unchanged): ");
            string title = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(title))
            {
                journalToEdit.Title = title;
            }
            Console.Write("New Journal Content - (blank to leave unchanged): ");
            string content = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(content))
            {
                journalToEdit.Content = content;
            }

            _journalRepository.Update(journalToEdit);
        }

        private void Remove()
        {
            Journal journalToDelete;
            Console.Write("Please provide the id of the journal you'd like to delete: ");
            int journalId = int.Parse(Console.ReadLine());
            journalToDelete = _journalRepository.Get(journalId);
            if (journalToDelete != null)
            {
                _journalRepository.Delete(journalToDelete.Id);
            }
        }
    }
}
