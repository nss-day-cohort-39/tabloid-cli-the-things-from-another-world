using System;
using System.Collections.Generic;
using System.Text;
using TabloidCLI.Models;

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
            Console.WriteLine(" 1) List All Entires");
            Console.WriteLine(" 2) Add Entry");
            Console.WriteLine(" 3) Edit Entry");
            Console.WriteLine(" 4) Remove Entry");
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
                //case "3":
                //    Edit();
                //    return this;
                //case "4":
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
            List<Journal> journalEntries = _journalRepository.GetAll();
            foreach (Journal journal in journalEntries)
            {
                Console.WriteLine(journal);
            }
        }
        private void Add()
        {
            Console.WriteLine("New Author");
            Author author = new Author();

            Console.Write("First Name: ");
            author.FirstName = Console.ReadLine();

            Console.Write("Last Name: ");
            author.LastName = Console.ReadLine();

            Console.Write("Bio: ");
            author.Bio = Console.ReadLine();

            _authorRepository.Insert(author);
        }

    }
}
