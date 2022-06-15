using System;
using System.Collections.Generic;
using System.Linq;
using TabloidCLI.Models;
using TabloidCLI.Repositories;

namespace TabloidCLI.UserInterfaceManagers
{
    public class TagManager : IUserInterfaceManager
    {
        private readonly IUserInterfaceManager _parentUI;
        private TagRepository _tagRepository;
        private string _connectionString;

        public TagManager(IUserInterfaceManager parentUI, string connectionString)
        {
            _parentUI = parentUI;
            _tagRepository = new TagRepository(connectionString);
            _connectionString = connectionString;
        }

        public IUserInterfaceManager Execute()
        {
            Console.WriteLine("Tag Menu");
            Console.WriteLine(" 1) List Tags");
            Console.WriteLine(" 2) Add Tag");
            Console.WriteLine(" 3) Edit Tag");
            Console.WriteLine(" 4) Remove Tag");
            Console.WriteLine(" 0) Go Back");

            Console.Write("> ");
            string choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    List();
                    return this;
                case "2":
                    Add();
                    return this;
                case "3":
                    Edit();
                    return this;
                case "4":
                    Remove();
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
            List<Tag> tags = _tagRepository.GetAll();
            foreach (Tag tag in tags)
            {
                Console.WriteLine(tag);
            }
        }

        private void Add()
        {
            Console.WriteLine("New Tag");
            Tag tag = new Tag();

            Console.Write("Name: ");
            tag.Name = Console.ReadLine();

            _tagRepository.Insert(tag);
        }

        private void Edit()
        {
            Console.WriteLine("Which tag would you like to edit?");
             List<Tag> tags = _tagRepository.GetAll();
            foreach (Tag tag in tags)
            {
                Console.WriteLine($"{tag.Id}) {tag.Name}");
            }
            int tagId = int.Parse(Console.ReadLine());
            Tag tagToEdit = tags.FirstOrDefault(t => t.Id == tagId);
            Console.Write("New Name: ");
            tagToEdit.Name = Console.ReadLine();
            _tagRepository.Update(tagToEdit);
            Console.WriteLine("Tag has been updated");
        }

        private void Remove()
        {
            List<Tag> tags = _tagRepository.GetAll();
            foreach (Tag t in tags)
            {
                 Console.WriteLine($"{t.Id}) {t.Name}");
            }
            Console.WriteLine("Select tag to delete");
            int tagToDelete = int.Parse(Console.ReadLine());
            _tagRepository.Delete(tagToDelete);
            Console.WriteLine($"Tag {tagToDelete} has been deleted");
        }
    }
}
