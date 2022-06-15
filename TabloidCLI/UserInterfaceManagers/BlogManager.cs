﻿using System;
using System.Collections.Generic;
using TabloidCLI.Models;
using TabloidCLI.Repositories;

namespace TabloidCLI.UserInterfaceManagers
{
    internal class BlogManager : IUserInterfaceManager
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
                    Console.WriteLine("Blog List: ");
                    List();
                    return this;
                case "3":
                    Add();
                    Console.WriteLine("Your blog has been successfully added to the Database.");
                    return this;
                default:
                    Console.WriteLine("Invalid Selection");
                    return this;
            }
        }

        private void Add()
        {
            Console.WriteLine("New Blog");
            Blog blog = new Blog();
            Console.WriteLine("Blog Title");
            blog.Title = Console.ReadLine();
            Console.WriteLine("Blog Url");
            blog.Url = Console.ReadLine();
            _blogRepository.Insert(blog);
        }

        private void List()
        {
            List<Blog> blogs = _blogRepository.GetAll();
            foreach(Blog blog in blogs)
            {
                Console.WriteLine($"Name: {blog.Title}, Url: {blog.Url} ");
            }
        }


    }
}
