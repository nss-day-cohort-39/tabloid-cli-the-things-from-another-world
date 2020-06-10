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

        public PostManager(IUserInterfaceManager parentUI, string connectionString)
        {
            _parentUI = parentUI;
            _postRepository = new PostRepository(connectionString);
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
            Console.WriteLine(" 0) Go Back");

            Console.Write("> ");
            string choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    List();
                    return this;
                //case "2":
                //    Add();44
                //    return this;
                //case "3":
                //    Edit();
                //    return this;
                //case "4":
                //    Remove();
                //    return this;
                //case "5":
                //    Remove();
                //    return this;
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
Blog TItle: {post.Blog.Title}
Author: {post.Author.FullName}");
            }
        }

        //private Author Choose(string prompt = null)
        //{
        //    if (prompt == null)
        //    {
        //        prompt = "Please choose an Author:";
        //    }

        //    Console.WriteLine(prompt);

        //    List<Author> authors = _authorRepository.GetAll();

        //    for (int i = 0; i < authors.Count; i++)
        //    {
        //        Author author = authors[i];
        //        Console.WriteLine($" {i + 1}) {author.FullName}");
        //    }
        //    Console.Write("> ");

        //    string input = Console.ReadLine();
        //    try
        //    {
        //        int choice = int.Parse(input);
        //        return authors[choice - 1];
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine("Invalid Selection");
        //        return null;
        //    }
        //}

        //private void Add()
        //{
        //    Console.WriteLine("New Author");
        //    Author author = new Author();

        //    Console.Write("First Name: ");
        //    author.FirstName = Console.ReadLine();

        //    Console.Write("Last Name: ");
        //    author.LastName = Console.ReadLine();

        //    Console.Write("Bio: ");
        //    author.Bio = Console.ReadLine();

        //    _authorRepository.Insert(author);
        //}

        //private void Edit()
        //{
        //    Author authorToEdit = Choose("Which author would you like to edit?");
        //    if (authorToEdit == null)
        //    {
        //        return;
        //    }

        //    Console.WriteLine();
        //    Console.Write("New first name (blank to leave unchanged: ");
        //    string firstName = Console.ReadLine();
        //    if (!string.IsNullOrWhiteSpace(firstName))
        //    {
        //        authorToEdit.FirstName = firstName;
        //    }
        //    Console.Write("New last name (blank to leave unchanged: ");
        //    string lastName = Console.ReadLine();
        //    if (!string.IsNullOrWhiteSpace(lastName))
        //    {
        //        authorToEdit.LastName = lastName;
        //    }
        //    Console.Write("New bio (blank to leave unchanged: ");
        //    string bio = Console.ReadLine();
        //    if (!string.IsNullOrWhiteSpace(bio))
        //    {
        //        authorToEdit.Bio = bio;
        //    }

        //    _authorRepository.Update(authorToEdit);
        //}

        //private void Remove()
        //{
        //    Author authorToDelete = Choose("Which author would you like to remove?");
        //    if (authorToDelete != null)
        //    {
        //        _authorRepository.Delete(authorToDelete.Id);
        //    }
        //}
    }
}
