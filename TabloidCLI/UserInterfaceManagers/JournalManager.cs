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
                case "3":
                    Add();
                    Console.WriteLine($"You've successfully added a journal entry.");
                    return this;
                default:
                    Console.WriteLine("Invalid Selection");
                    return this;
            }
            
        }

        //private void List()
        //{
        //    List<Journal> Journals = _journalRepository.GetAll();
        //    foreach (Journal Journal in Journals)
        //    {
        //        Console.WriteLine(Journal);
        //    }
        //}

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

            DateTime test = DateTime.Now;

            journal.CreateDateTime = test;

            _journalRepository.Insert(journal);
        }

        //private void Edit()
        //{
        //    Journal JournalToEdit = Choose("Which Journal would you like to edit?");
        //    if (JournalToEdit == null)
        //    {
        //        return;
        //    }

        //    Console.WriteLine();
        //    Console.Write("New first name (blank to leave unchanged: ");
        //    string firstName = Console.ReadLine();
        //    if (!string.IsNullOrWhiteSpace(firstName))
        //    {
        //        JournalToEdit.FirstName = firstName;
        //    }
        //    Console.Write("New last name (blank to leave unchanged: ");
        //    string lastName = Console.ReadLine();
        //    if (!string.IsNullOrWhiteSpace(lastName))
        //    {
        //        JournalToEdit.LastName = lastName;
        //    }
        //    Console.Write("New bio (blank to leave unchanged: ");
        //    string bio = Console.ReadLine();
        //    if (!string.IsNullOrWhiteSpace(bio))
        //    {
        //        JournalToEdit.Bio = bio;
        //    }

        //    _journalRepository.Update(JournalToEdit);
        //}

        //private void Remove()
        //{
        //    Journal JournalToDelete = Choose("Which Journal would you like to remove?");
        //    if (JournalToDelete != null)
        //    {
        //        _journalRepository.Delete(JournalToDelete.Id);
        //    }
        //}
    }
}
