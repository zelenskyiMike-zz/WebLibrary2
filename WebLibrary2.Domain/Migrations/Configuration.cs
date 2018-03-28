namespace WebLibrary2.Domain.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using WebLibrary2.Domain.Entity;

    internal sealed class Configuration : DbMigrationsConfiguration<WebLibrary2.Domain.Concrete.EFDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
        }

        private void SeedAuthors(WebLibrary2.Domain.Concrete.EFDbContext context)
        {
            if (context.Authors.Any())
            {
                return;
            }

            Author a1 = new Author() { AuthorID = 1, AuthorName = "Толстой Л.Н." };
            Author a2 = new Author() { AuthorID = 2, AuthorName = "Шевченко Т.Г." };
            Author a3 = new Author() { AuthorID = 3, AuthorName = "Дж.Р. Киплинг " };
            Author a4 = new Author() { AuthorID = 4, AuthorName = "С.Э. Кинг" };
            Author a5 = new Author() { AuthorID = 5, AuthorName = "Рэй Брэдбэри" };
            Author a6 = new Author() { AuthorID = 6, AuthorName = "Э.М.Ремарк" };
            Author a7 = new Author() { AuthorID = 7, AuthorName = "Гоголь Н.В." };
            Author a8 = new Author() { AuthorID = 8, AuthorName = "Достоевкий Ф.М.", };
            Author a9 = new Author() { AuthorID = 9, AuthorName = "Булгаков М.А." };
            Author a10 = new Author() { AuthorID = 10, AuthorName = "Пушкин А.С." };
            Author a11 = new Author() { AuthorID = 11, AuthorName = "Чуковский К.И." };
            Author a12 = new Author() { AuthorID = 12, AuthorName = "Э.М. Хемингуей " };
            Author a13 = new Author() { AuthorID = 13, AuthorName = "Дж.К. Роулинг" };
            Author a14 = new Author() { AuthorID = 14, AuthorName = "Дж.Мартин" };
            Author a15 = new Author() { AuthorID = 15, AuthorName = "Леся Украинка" };

            context.Authors.AddRange(new List<Author> { a1, a2, a3, a4, a5, a6, a7, a8, a9, a10, a11, a12, a13, a14, a15 });
            context.SaveChanges();
        }
        private void SeedGenres(WebLibrary2.Domain.Concrete.EFDbContext context)
        {
            if (context.Genres.Any())
            {
                return;
            }
            Genre g1 = new Genre() { GenreID = 1, GenreName = "Роман" };
            Genre g2 = new Genre() { GenreID = 2, GenreName = "Поэзия" };
            Genre g3 = new Genre() { GenreID = 3, GenreName = "Рассказ" };
            Genre g4 = new Genre() { GenreID = 4, GenreName = "Тёмное фэнтези" };
            Genre g5 = new Genre() { GenreID = 5, GenreName = "Антиутопия" };
            Genre g6 = new Genre() { GenreID = 6, GenreName = "Реализм" };
            Genre g7 = new Genre() { GenreID = 7, GenreName = "Повесть" };
            Genre g8 = new Genre() { GenreID = 8, GenreName = "Роман-поэма" };
            Genre g9 = new Genre() { GenreID = 9, GenreName = "Сказка" };
            Genre g10 = new Genre() { GenreID = 10, GenreName = "Фэнтези" };
            context.Genres.AddRange(new List<Genre> { g1, g2, g3, g4, g5, g6, g7, g8, g9, g10 });
            context.SaveChanges();
        }

        private void SeedBooks(WebLibrary2.Domain.Concrete.EFDbContext context)
        {
            if (context.Books.Any())
            {
                return;
            }

            Book b1 = new Book() { BookID = 1, GenreID = 1, BookName = "Война и Мир. Том I", YearOfPublish = 1865, Authors = new List<Author>()  };
            Book b2 = new Book() { BookID = 2, GenreID = 2, BookName = "Война и Мир. Том II", YearOfPublish = 1866, Authors = new List<Author>() };
            Book b3 = new Book() { BookID = 3, GenreID = 3, BookName = "Полное собрание стихотворений в одном томе", YearOfPublish = 1867, Authors = new List<Author>()  };
            Book b4 = new Book() { BookID = 4, GenreID = 4, BookName = "Простые рассказы с гоp", YearOfPublish = 1885, Authors = new List<Author>()  };
            Book b5 = new Book() { BookID = 5, GenreID = 5, BookName = "Книга джунглей", YearOfPublish = 1965, Authors = new List<Author>()  };
            Book b6 = new Book() { BookID = 6, GenreID = 6, BookName = "Тёмная башня V: Волки Кальи", YearOfPublish = 1835, Authors = new List<Author>()  };
            Book b7 = new Book() { BookID = 7, GenreID = 7, BookName = "Вино из одуванчиков", YearOfPublish = 1265, Authors = new List<Author>() };
            Book b8 = new Book() { BookID = 8, GenreID = 8, BookName = "Марсианские хроники", YearOfPublish = 1665, Authors = new List<Author>() };
            Book b9 = new Book() { BookID = 9, GenreID = 7, BookName = "Три товарища", YearOfPublish = 1835, Authors = new List<Author>()  };
            Book b10 = new Book() { BookID = 10, GenreID = 6, BookName = "Вий", YearOfPublish = 1165, Authors = new List<Author>()  };
            Book b11 = new Book() { BookID = 11, GenreID = 5, BookName = "Ночь перед Рождеством", YearOfPublish = 1875, Authors = new List<Author>() };
            Book b12 = new Book() { BookID = 12, GenreID = 4, BookName = "Преступление и наказание", YearOfPublish = 1863, Authors = new List<Author>()  };
            Book b13 = new Book() { BookID = 13, GenreID = 3, BookName = "Мастер и Маргарита", YearOfPublish = 1825, Authors = new List<Author>()  };
            Book b14 = new Book() { BookID = 14, GenreID = 2, BookName = "Собачье сердце", YearOfPublish = 1869, Authors = new List<Author>() };
            Book b15 = new Book() { BookID = 15, GenreID = 1, BookName = "Мойдодыр", YearOfPublish = 1665, Authors = new List<Author>()  };
            Book b16 = new Book() { BookID = 16, GenreID = 1, BookName = "Гарри Поттер и узник Азкабана", YearOfPublish = 1765, Authors = new List<Author>()  };
            Book b17 = new Book() { BookID = 17, GenreID = 2, BookName = "Success", YearOfPublish = 1869, Authors = new List<Author>() };
            Book b18 = new Book() { BookID = 18, GenreID = 3, BookName = "Весна и люди", YearOfPublish = 1965, Authors = new List<Author>()  };
            Book b19 = new Book() { BookID = 19, GenreID = 4, BookName = "Story of life", YearOfPublish = 1844, Authors = new List<Author>() };

            context.Books.Add(b1);
            context.Books.Add(b2);
            context.Books.Add(b3);
            context.Books.Add(b4);
            context.Books.Add(b5);
            context.Books.Add(b6);
            context.Books.Add(b7);
            context.Books.Add(b8);
            context.Books.Add(b9);
            context.Books.Add(b10);
            context.Books.Add(b11);
            context.Books.Add(b12);
            context.Books.Add(b13);
            context.Books.Add(b14);
            context.Books.Add(b15);
            context.Books.Add(b16);
            context.Books.Add(b17);
            context.Books.Add(b18);
            context.Books.Add(b19);

            context.SaveChanges();
        }

        protected override void Seed(WebLibrary2.Domain.Concrete.EFDbContext context)
        {
            SeedAuthors(context);
            SeedGenres(context);
            SeedBooks(context);

            base.Seed(context);
        }
    }
}
