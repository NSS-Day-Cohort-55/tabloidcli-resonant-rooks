﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TabloidCLI.Models;
using TabloidCLI.Repositories;

namespace TabloidCLI.UserInterfaceManagers
{
    public class PostDetailsManager : IUserInterfaceManager
    {
        private IUserInterfaceManager _parentUI;
        private PostRepository _postRepository;
        private NoteRepository _noteRepository;
        private int _postId;
        private string _connectionString;

        public PostDetailsManager(IUserInterfaceManager parentUI, string connectionString, int postId)
        {
            _parentUI = parentUI;
            _postRepository = new PostRepository(connectionString);
            _postId = postId;
            _noteRepository = new NoteRepository(connectionString);
            _connectionString = connectionString;

        }

        public IUserInterfaceManager Execute()
        {
            Post post = _postRepository.Get(_postId);

            Console.WriteLine($"{post.Title} Details");
            Console.WriteLine(" 1) View");
            Console.WriteLine(" 2) Add Tag");
            Console.WriteLine(" 3) Remove Tag");
            Console.WriteLine(" 4) Note Management");
            Console.WriteLine(" 0) Go Back");

            Console.Write("> ");
            string choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    View();
                    return this;
                case "2":
                    return this;
                case "3":
                    
                    return this;
                case "4":
                    return new NoteManager(this, _connectionString, _postId);
                case "0":
                    return _parentUI;
                default:
                    Console.WriteLine("Invalid Selection");
                    return this;
            }
        }

        private void View()
        {
            Post post = _postRepository.Get(_postId);
            Console.WriteLine();
            Console.WriteLine($"Title: {post.Title}");
            Console.WriteLine($"URL: {post.Url}");
            Console.WriteLine($"Publication Date: {post.PublishDateTime}");
            Console.WriteLine();
        }

        private void AddTag()
        {
      
        }

        private void RemoveTag()
        {
 
        }
    }
}

