using System;
using System.Collections.Generic;
using TabloidCLI.Models;
using TabloidCLI.Repositories;

namespace TabloidCLI.UserInterfaceManagers
{
    public class PostManager : IUserInterfaceManager
    {
        private readonly IUserInterfaceManager _parentUI;
        private PostRepository _postRepository;
        private string _connectionString;
        private AuthorRepository _authorRepository;
        private BlogRepository _blogRepository;

        public PostManager(IUserInterfaceManager parentUI, string connectionString)
        {
            _parentUI = parentUI;
            _postRepository = new PostRepository(connectionString);
            _connectionString = connectionString;
            _authorRepository = new AuthorRepository(connectionString);
            _blogRepository = new BlogRepository(connectionString);
        }

        public IUserInterfaceManager Execute()
        {
            Console.WriteLine("Post Menu");
            Console.WriteLine(" 1) List Posts");
            Console.WriteLine(" 2) Post Details");
            Console.WriteLine(" 3) Add Post");
            Console.WriteLine(" 4) Edit Post");
            Console.WriteLine(" 5) Remove Post");
            Console.WriteLine(" 0) Go Back");

            Console.Write("> ");
            string choice = Console.ReadLine();
            switch(choice)
            {
                case "1":
                    List();
                    return this;
                case "3":
                    Add();
                    return this;
                default:
                    Console.WriteLine("Invalid Selection");
                    return this;
            }
            
        }

        private void List()
        {
            List<Post> posts = _postRepository.GetAll();
            foreach (Post p in posts)
            {
                Console.WriteLine($"Title: {p.Title} | URL: {p.Url}");
            }
        }

        private void Add()
        {
            Post post = new Post();
            Console.Write("New Post Title: ");
            post.Title = Console.ReadLine();

            Console.WriteLine("New Post URL: ");
            post.Url = Console.ReadLine();

            Console.WriteLine("New Post Publish Date: ");
            post.PublishDateTime = DateTime.Parse(Console.ReadLine());

            Console.WriteLine("Choose an Author for the Post:");
            List<Author> authors = _authorRepository.GetAll();

            foreach (Author author in authors)
            {
                Console.WriteLine($"{author.Id}) {author.FullName}");
            }
            int postAuthorId = int.Parse(Console.ReadLine());
            post.Author = _authorRepository.Get(postAuthorId);

            Console.WriteLine("Choose a Blog for the Post:");
            List<Blog> blogs = _blogRepository.GetAll();

            foreach (Blog blog in blogs)
            {
                Console.WriteLine($"{blog.Id}) {blog.Title}");
            }
            int postBlogId = int.Parse(Console.ReadLine());
            post.Blog = _blogRepository.Get(postBlogId);

            _postRepository.Insert(post);
        }
    }
}
