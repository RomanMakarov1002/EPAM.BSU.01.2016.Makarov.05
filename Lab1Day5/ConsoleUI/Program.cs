using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BooksCollection;
using System.IO;
using System.Runtime.CompilerServices;
using NLog;

namespace ConsoleUI
{
    class Program
    {
        static void Main(string[] args)
        {
            if (File.Exists("books"))
                File.Delete("books");

            if (File.Exists("file.txt"))
                File.Delete("file.txt");
            if (File.Exists("lessDetailedFile.txt"))
                File.Delete("lessDetailedFile.txt");
            Logger logger = LogManager.GetCurrentClassLogger();
            Collection col = new Collection("books");
            col.AddBook(new Book("SomeBook", 110, "Remark", "England", "history", 1960));
            col.AddBook(new Book("Azbyka", 100, "fsfs", "fsfbc", "fsfs", 1895));
            col.AddBook(new Book("Bykvar", 50, "SomeAuthor", "Minsk", "ForChildren", 2001));
            col.AddBook(new Book("War and Peace", 2500, "Tolstoy", "Moscow", "novel", 1980));
            col.AddBook(new Book("Crime and Justice", 1000, "Dostoevskiy", "Moscow", "novel", 1970));
            col.RemoveBook("SomeBook");
            SortByName sort1 = new SortByName();
           
            foreach (var item in col.books)
            {
                Console.WriteLine(item);
            }

            Console.WriteLine("------------------------- List of books ");
            col.SortBooksByTag(sort1);
            foreach (var item in col.books)
            {
                Console.WriteLine(item);
            }

            Console.WriteLine("------------------------- List of books after sort");
            col.SortBooksByTag(new SortByPages());
            foreach (var item in col.books)
            {
                Console.WriteLine(item);
            }
            Console.WriteLine("------------------------- List of books after sort by pages");
            List <Book> finded= col.FindByTag("Azbyka");
            foreach (var item in finded)
            {
                Console.WriteLine(item);
            }
            Console.WriteLine("------------------------- founded books");
            
            try
            {
                col.AddBook(new Book("Azbyka", 100, "fsfs", "fsfbc", "fsfs", 1895));
            }
            catch (AddBookException ex)
            {
                Console.WriteLine(ex.Message);
                logger.Error(ex);
            }
            try
            {
                col.RemoveBook("abcdef");
            }
            catch (RemoveBookException ex)
            {
                
                Console.WriteLine(ex.Message);
                logger.Error(ex);
            }
            try
            {
                col.RemoveBook(new Book("SomeBook", 110, "Remark", "England", "history", 1960));
            }
            catch (RemoveBookException ex)
            {
                
                Console.WriteLine(ex.Message);
                logger.Error(ex);
            }
            logger.Info(col.books[0].ToString);
            Console.ReadKey();
        }
    }
}
