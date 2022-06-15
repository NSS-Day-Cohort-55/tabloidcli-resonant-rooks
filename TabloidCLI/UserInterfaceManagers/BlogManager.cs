﻿using System;
using System.Collections.Generic;
using TabloidCLI.Models;
using TabloidCLI.Repositories;

namespace TabloidCLI.UserInterfaceManagers
{
    public class BlogManager : IUserInterfaceManager
    {
        private readonly IUserInterfaceManager _parentUI;
        private BlogRepository _blogRepository;
        private string _connectionString;

        public BlogManager(IUserInterfaceManager parentUI, string connectionString)
        {
            _parentUI = parentUI;
            _blogRepository = new BlogRepository(connectionString);
            _connectionString = connectionString;
        }

        public IUserInterfaceManager Execute()
        {
            Console.WriteLine("Blog Menu");
            Console.WriteLine(" 1) List Blogs");
            Console.WriteLine(" 2) Blog Details");
            Console.WriteLine(" 3) Add Blog");
            Console.WriteLine(" 4) Edit Blog");
            Console.WriteLine(" 5) Remove Blog");
            Console.WriteLine(" 0) Go Back");

            Console.Write("> ");
            string choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    List();
                    return this;
                case "2":
                    Blog blog = Choose();
                    if(blog == null)
                    {
                        return this;
                    }
                    else
                    {
                        return new BlogDetailManager(this, _connectionString, blog.Id);
                    }
                case "3":
                    Add();
                    Console.WriteLine("Blog has been successfully added to the Database.");
                    return this;
                case "4":
                    Edit();
                    Console.WriteLine("Blog has been updated in the Database.");
                    return this;
                case "5":
                    Remove();
                    Console.WriteLine("Blog has been successfully removed from the Database. ");
                    return this;
                default:
                    Console.WriteLine("Invalid Selection");
                    return this;
            }
        }

        private Blog Choose(string prompt = null)
        {
            if(prompt == null)
            {
                prompt = "Please Choose a Blog: ";
            }
            Console.WriteLine(prompt);
            List<Blog> blogs = _blogRepository.GetAll();

            for(int i = 0; i < blogs.Count; i++)
            {
                Blog blog = blogs[i];
                Console.WriteLine($"{i + 1}) {blog.Title}");
            }
            Console.Write("> ");
            string input = Console.ReadLine();
            try
            {
                int choice = int.Parse(input);
                return blogs[choice - 1];
            }
            catch (Exception ex)
            {
                Console.WriteLine("Invalid Selection");
                return null;
            }
        }

        private void Add()
        {
            Console.WriteLine("--New Blog-- ");
            Blog blog = new Blog();
            Console.Write("Blog Title: ");
            blog.Title = Console.ReadLine();
            Console.WriteLine("Blog Url: ");
            blog.Url = Console.ReadLine();
            _blogRepository.Insert(blog);
        }

        private void List()
        {
            List<Blog> blogs = _blogRepository.GetAll();

            Console.WriteLine("---------------------------------------------------------");
            Console.WriteLine("Blog List: ");
            Console.WriteLine("---------------------------------------------------------");


            foreach (Blog blog in blogs)
            {
                Console.WriteLine($"{blog.Title} - {blog.Url} ");
                Console.WriteLine("---------------------------------------------------------");
            }
        }

        private void Remove()
        {
            Blog blogToDelete = Choose("Which Blog would you like to remove?");
            if(blogToDelete != null)
            {
                _blogRepository.Delete(blogToDelete.Id);
            }
        }

        private void Edit()
        {
            Blog blogToEdit = Choose("Which Blog would you like to edit?");
            if(blogToEdit == null)
            {
                return;
            }
            Console.WriteLine();
            Console.WriteLine("New Title (blank to leave unchanged): ");
            string title = Console.ReadLine();
            if(!string.IsNullOrWhiteSpace(title))
            {
                blogToEdit.Title = title;
            }
            Console.Write("New Url (blank to leave unchanged): ");
            string url = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(url))
            {
                blogToEdit.Url = url;
            }
            _blogRepository.Update(blogToEdit);
        }


    }
}
