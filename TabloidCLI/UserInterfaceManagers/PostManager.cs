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
        private AuthorRepository _authorRepository;
        private BlogRepository _blogRepository;

        private string _connectionString;

        public PostManager(IUserInterfaceManager parentUI, string connectionString)
        {
            _parentUI = parentUI;
            _postRepository = new PostRepository(connectionString);
            _authorRepository = new AuthorRepository(connectionString);
            _blogRepository = new BlogRepository(connectionString);
            _connectionString = connectionString;
        }

        public IUserInterfaceManager Execute()
        {
            Console.WriteLine("Post Menu");
            Console.WriteLine(" 1) List Posts");
            Console.WriteLine(" 2) Add Post");
            Console.WriteLine(" 3) Edit Post");
            Console.WriteLine(" 4) Remove Post");
            Console.WriteLine(" 5) Note Management");
            Console.WriteLine(" 6) View Post Details");
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
                //case "5":
                //    Remove();
                //    return this;
                case "6":
                    Post post = Choose();
                    if (post == null)
                    {
                        return this;
                    }
                    else
                    {
                        return new PostDetailManager(this, _connectionString, post.Id);
                    }
                case "0":
                    return _parentUI;
                default:
                    Console.WriteLine("Invalid Selection");
                    return this;
            }
        }

        private void List()
        {
            List<Post> posts = _postRepository.GetAll();
            foreach (Post post in posts)
            {
                Console.WriteLine($@"
Title: {post.Title}
URL: {post.Url}
Published Date: {post.PublishDateTime}
Blog Title: {post.Blog.Title}
Author: {post.Author.FullName}");
            }
        }

        private Post Choose(string prompt = null)
        {
            if (prompt == null)
            {
                prompt = "Please choose a Post:";
            }

            Console.WriteLine(prompt);

            List<Post> posts = _postRepository.GetAll();

            for (int i = 0; i < posts.Count; i++)
            {
                Post post = posts[i];
                Console.WriteLine($" {i + 1}) {post.Title}");
            }
            Console.Write("> ");

            string input = Console.ReadLine();
            try
            {
                int choice = int.Parse(input);
                return posts[choice - 1];
            }
            catch (Exception ex)
            {
                Console.WriteLine("Invalid Selection");
                return null;
            }
        }

        private Author ChooseAuthor(string prompt = null, bool message = true)
        {
            if (prompt == null)
            {
                prompt = "Please choose an Author:";
            }

            Console.WriteLine(prompt);

            List<Author> authors = _authorRepository.GetAll();

            for (int i = 0; i < authors.Count; i++)
            {
                Author author = authors[i];
                Console.WriteLine($" {i + 1}) {author.FullName}");
            }
            Console.Write("> ");

            string input = Console.ReadLine();
            try
            {
                int choice = int.Parse(input);
                return authors[choice - 1];
            }
            catch (Exception ex)
            {
                if (message != false || input != "")
                {
                    Console.WriteLine("Invalid Selection");
                }
                return null;
            }
        }

        private Blog ChooseBlog(string prompt = null, bool message = true)
        {
            if (prompt == null)
            {
                prompt = "Please choose a Blog:";
            }

            Console.WriteLine(prompt);

            List<Blog> blogs = _blogRepository.GetAll();

            for (int i = 0; i < blogs.Count; i++)
            {
                Blog blog = blogs[i];
                Console.WriteLine($" {i + 1}) {blog.Title}");
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
                if (message != false || input != "")
                {
                    Console.WriteLine("Invalid Selection");
                }
                return null;
            }
        }

        private void Add()
        {
            Console.WriteLine("New Post");
            Post post = new Post();

            Console.Write("Title: ");
            post.Title = Console.ReadLine();

            Console.Write("URL: ");
            post.Url = Console.ReadLine();

            Console.Write("Date Published (Enter Format as YYYY-MM-DD: ");
            string dateInput = Console.ReadLine();
            Nullable<DateTime> dateCheck = null;

            //while the date is not yet set...
            while (dateCheck == null)
            {
                try
                {
                    try //try to parse the user input into a DateTime
                    {
                        post.PublishDateTime = DateTime.Parse(dateInput);
                    }
                    catch //if that fails then give them an error
                    {
                        Console.Write("Please input the date in YYYY-MM-DD format: ");
                        throw new System.Exception();
                    }


                    try //check to make sure that the year is greater than the min possible value for SQL dates
                    {
                        if (DateTime.Parse(dateInput).Year < 1753)
                        {
                            throw new System.Exception();
                        }
                    }
                    catch //if it isn't then thow an exception
                    {
                        Console.Write("Please input a date greater than 1753: ");
                        throw new System.Exception();
                    }
                    dateCheck = DateTime.Parse(dateInput);
                }
                catch //if an exception is thrown anywhere in the nested try methods, then prompt the user for a valid date again
                {
                    dateInput = Console.ReadLine();
                }

            }

            Author authorToAdd = ChooseAuthor("Which author would you like to add to the post?");
            while (authorToAdd == null)
            {
                authorToAdd = ChooseAuthor("Which author would you like to add to the post?");
                Console.WriteLine("Please choose from the list above.");
            }

            Blog blogToAdd = ChooseBlog("Which blog would you like to add to the post?");
            while (blogToAdd == null)
            {
                blogToAdd = ChooseBlog("Which blog would you like to add to the post?");
                Console.WriteLine("Please choose from the list above.");
            }

            post.Author = authorToAdd;
            post.Blog = blogToAdd;

            _postRepository.Insert(post);
            Console.WriteLine($"{post.Title} added!");

        }

        private void Edit()
        {
            Post postToEdit = Choose("Which post would you like to edit?");
            if (postToEdit == null)
            {
                return;
            }

            Console.WriteLine();
            Console.Write("New post title name (blank to leave unchanged): ");
            string Title = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(Title))
            {
                postToEdit.Title = Title;
            }
            Console.Write("New Url (blank to leave unchanged): ");
            string Url = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(Url))
            {
                postToEdit.Url = Url;
            }
            Console.Write("New publish date [format: YYYY-MM-DD] (blank to leave unchanged): ");
            string dateInput = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(dateInput))
            {

                Nullable<DateTime> dateCheck = null;

                //while the date is not yet set...
                while (dateCheck == null)
                {
                    try
                    {
                        try //try to parse the user input into a DateTime
                        {
                            postToEdit.PublishDateTime = DateTime.Parse(dateInput);
                        }
                        catch //if that fails then give them an error
                        {
                            Console.Write("Please input the date in YYYY-MM-DD format: ");
                            throw new System.Exception();
                        }


                        try //check to make sure that the year is greater than the min possible value for SQL dates
                        {
                            if (DateTime.Parse(dateInput).Year < 1753)
                            {
                                throw new System.Exception();
                            }
                        }
                        catch //if it isn't then thow an exception
                        {
                            Console.Write("Please input a date greater than 1753: ");
                            throw new System.Exception();
                        }
                        dateCheck = DateTime.Parse(dateInput);
                    }
                    catch //if an exception is thrown anywhere in the nested try methods, then prompt the user for a valid date again
                    {
                        dateInput = Console.ReadLine();
                    }

                }

            }

            Author authorToEdit = ChooseAuthor("New Author (blank to leave unchanged): ", false);
            if (authorToEdit != null)
            {
                postToEdit.Author = authorToEdit;
            }

            Blog blogToAdd = ChooseBlog("Which blog would you like to add to the post? ", false);
            if (blogToAdd != null)
            {
                postToEdit.Blog = blogToAdd;
         
            }

            _postRepository.Update(postToEdit);
            Console.WriteLine($"{postToEdit.Title} edited!");


        }

        private void Remove()
        {
            Post postToDelete = Choose("Which post would you like to remove?");
            if (postToDelete != null)
            {
                _postRepository.Delete(postToDelete.Id);
                
           Console.WriteLine($"{postToDelete.Title} deleted!");

            }
          
            
        }
    }
}
