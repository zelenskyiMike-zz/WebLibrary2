namespace WebLibrary2.Domain.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using WebLibrary2.Domain.Entity;
    using WebLibrary2.Domain.Entity.ArticleEntity;
    using WebLibrary2.Domain.Entity.BookEntity;
    using WebLibrary2.Domain.Entity.MagazineEntity;
    using WebLibrary2.Domain.Entity.PublicationEntity;

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
            BookGenre g1 = new BookGenre() { GenreID = 1, GenreName = "Роман" };
            BookGenre g2 = new BookGenre() { GenreID = 2, GenreName = "Поэзия" };
            BookGenre g3 = new BookGenre() { GenreID = 3, GenreName = "Рассказ" };
            BookGenre g4 = new BookGenre() { GenreID = 4, GenreName = "Тёмное фэнтези" };
            BookGenre g5 = new BookGenre() { GenreID = 5, GenreName = "Антиутопия" };
            BookGenre g6 = new BookGenre() { GenreID = 6, GenreName = "Реализм" };
            BookGenre g7 = new BookGenre() { GenreID = 7, GenreName = "Повесть" };
            BookGenre g8 = new BookGenre() { GenreID = 8, GenreName = "Роман-поэма" };
            BookGenre g9 = new BookGenre() { GenreID = 9, GenreName = "Сказка" };
            BookGenre g10 = new BookGenre() { GenreID = 10, GenreName = "Фэнтези" };
            context.Genres.AddRange(new List<BookGenre> { g1, g2, g3, g4, g5, g6, g7, g8, g9, g10 });
            context.SaveChanges();
        }
        private void SeedBooks(WebLibrary2.Domain.Concrete.EFDbContext context)
        {
            if (context.Books.Any())
            {
                return;
            }

            Book b1 = new Book() { BookID = 1, GenreID = 1, BookName = "Война и Мир. Том I", YearOfPublish = 1865, Authors = new List<Author>() };
            Book b2 = new Book() { BookID = 2, GenreID = 2, BookName = "Война и Мир. Том II", YearOfPublish = 1866, Authors = new List<Author>() };
            Book b3 = new Book() { BookID = 3, GenreID = 3, BookName = "Полное собрание стихотворений в одном томе", YearOfPublish = 1867, Authors = new List<Author>() };
            Book b4 = new Book() { BookID = 4, GenreID = 4, BookName = "Простые рассказы с гоp", YearOfPublish = 1885, Authors = new List<Author>() };
            Book b5 = new Book() { BookID = 5, GenreID = 5, BookName = "Книга джунглей", YearOfPublish = 1965, Authors = new List<Author>() };
            Book b6 = new Book() { BookID = 6, GenreID = 6, BookName = "Тёмная башня V: Волки Кальи", YearOfPublish = 1835, Authors = new List<Author>() };
            Book b7 = new Book() { BookID = 7, GenreID = 7, BookName = "Вино из одуванчиков", YearOfPublish = 1265, Authors = new List<Author>() };
            Book b8 = new Book() { BookID = 8, GenreID = 8, BookName = "Марсианские хроники", YearOfPublish = 1665, Authors = new List<Author>() };
            Book b9 = new Book() { BookID = 9, GenreID = 7, BookName = "Три товарища", YearOfPublish = 1835, Authors = new List<Author>() };
            Book b10 = new Book() { BookID = 10, GenreID = 6, BookName = "Вий", YearOfPublish = 1165, Authors = new List<Author>() };
            Book b11 = new Book() { BookID = 11, GenreID = 5, BookName = "Ночь перед Рождеством", YearOfPublish = 1875, Authors = new List<Author>() };
            Book b12 = new Book() { BookID = 12, GenreID = 4, BookName = "Преступление и наказание", YearOfPublish = 1863, Authors = new List<Author>() };
            Book b13 = new Book() { BookID = 13, GenreID = 3, BookName = "Мастер и Маргарита", YearOfPublish = 1825, Authors = new List<Author>() };
            Book b14 = new Book() { BookID = 14, GenreID = 2, BookName = "Собачье сердце", YearOfPublish = 1869, Authors = new List<Author>() };
            Book b15 = new Book() { BookID = 15, GenreID = 1, BookName = "Мойдодыр", YearOfPublish = 1665, Authors = new List<Author>() };
            Book b16 = new Book() { BookID = 16, GenreID = 1, BookName = "Гарри Поттер и узник Азкабана", YearOfPublish = 1765, Authors = new List<Author>() };
            Book b17 = new Book() { BookID = 17, GenreID = 2, BookName = "Success", YearOfPublish = 1869, Authors = new List<Author>() };
            Book b18 = new Book() { BookID = 18, GenreID = 3, BookName = "Весна и люди", YearOfPublish = 1965, Authors = new List<Author>() };
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

        private void SeedArticles(WebLibrary2.Domain.Concrete.EFDbContext context)
        {
            if (context.Articles.Any())
            {
                return;
            }

            Article ar1 = new Article() { ArticleGenreID = 1, ArticleName = "Article 1", DateOfArticlePublish = new DateTime(2003, 9, 30), Authors = new List<Author>() };
            Article ar2 = new Article() { ArticleGenreID = 2, ArticleName = "Article 2", DateOfArticlePublish = new DateTime(2013, 4, 30), Authors = new List<Author>() };
            Article ar3 = new Article() { ArticleGenreID = 3, ArticleName = "Article 3", DateOfArticlePublish = new DateTime(2004, 5, 20), Authors = new List<Author>() };
            Article ar4 = new Article() { ArticleGenreID = 4, ArticleName = "Article 4", DateOfArticlePublish = new DateTime(2015, 6, 30), Authors = new List<Author>() };
            Article ar5 = new Article() { ArticleGenreID = 5, ArticleName = "Article 5", DateOfArticlePublish = new DateTime(2006, 7, 10), Authors = new List<Author>() };
            Article ar6 = new Article() { ArticleGenreID = 6, ArticleName = "Article 6", DateOfArticlePublish = new DateTime(2007, 8, 11), Authors = new List<Author>() };
            Article ar7 = new Article() { ArticleGenreID = 7, ArticleName = "Article 7", DateOfArticlePublish = new DateTime(2008, 9, 16), Authors = new List<Author>() };
            Article ar8 = new Article() { ArticleGenreID = 8, ArticleName = "Article 8", DateOfArticlePublish = new DateTime(2017, 11, 24), Authors = new List<Author>() };

            context.Articles.AddRange(new List<Article> { ar1, ar2, ar3, ar4, ar5, ar6, ar7, ar8 });

            context.SaveChanges();
        }
        private void SeedArticlesGenres(WebLibrary2.Domain.Concrete.EFDbContext context)
        {
            if (context.ArticleGenres.Any())
            {
                return;
            }

            ArticleGenre arG1 = new ArticleGenre() { ArticleGenreName = "Новостная" };
            ArticleGenre arG2 = new ArticleGenre() { ArticleGenreName = "Инструкция" };
            ArticleGenre arG3 = new ArticleGenre() { ArticleGenreName = "Обор" };
            ArticleGenre arG4 = new ArticleGenre() { ArticleGenreName = "Рецензия" };
            ArticleGenre arG5 = new ArticleGenre() { ArticleGenreName = "Отзыв" };
            ArticleGenre arG6 = new ArticleGenre() { ArticleGenreName = "Исследования" };
            ArticleGenre arG7 = new ArticleGenre() { ArticleGenreName = "Интервью" };
            ArticleGenre arG8 = new ArticleGenre() { ArticleGenreName = "Рекомендация" };

            context.ArticleGenres.AddRange(new List<ArticleGenre> { arG1, arG2, arG3, arG4, arG5, arG6, arG7, arG8 });

            context.SaveChanges();
        }

        private void SeedPublications(WebLibrary2.Domain.Concrete.EFDbContext context)
        {
            if (context.Publications.Any())
            {
                return;
            }

            Publication ar1 = new Publication() { PublicationGenreID = 1, PublicationName = "Публикация 1", DateOfPublicationPublish = new DateTime(2003, 9, 30), Authors = new List<Author>() };
            Publication ar2 = new Publication() { PublicationGenreID = 2, PublicationName = "Публикация 2", DateOfPublicationPublish = new DateTime(2013, 4, 30), Authors = new List<Author>() };
            Publication ar3 = new Publication() { PublicationGenreID = 3, PublicationName = "Публикация 3", DateOfPublicationPublish = new DateTime(2004, 5, 20), Authors = new List<Author>() };
            Publication ar4 = new Publication() { PublicationGenreID = 4, PublicationName = "Публикация 4", DateOfPublicationPublish = new DateTime(2015, 6, 30), Authors = new List<Author>() };
            Publication ar5 = new Publication() { PublicationGenreID = 5, PublicationName = "Публикация 5", DateOfPublicationPublish = new DateTime(2006, 7, 10), Authors = new List<Author>() };
            Publication ar6 = new Publication() { PublicationGenreID = 1, PublicationName = "Публикация 6", DateOfPublicationPublish = new DateTime(2007, 8, 11), Authors = new List<Author>() };
            Publication ar7 = new Publication() { PublicationGenreID = 2, PublicationName = "Публикация 7", DateOfPublicationPublish = new DateTime(2008, 9, 16), Authors = new List<Author>() };
            Publication ar8 = new Publication() { PublicationGenreID = 3, PublicationName = "Публикация 8", DateOfPublicationPublish = new DateTime(2003, 5, 30), Authors = new List<Author>() };
            Publication ar9 = new Publication() { PublicationGenreID = 4, PublicationName = "Публикация 9", DateOfPublicationPublish = new DateTime(2009, 9, 21), Authors = new List<Author>() };
            Publication ar10 = new Publication() { PublicationGenreID =5, PublicationName = "Публикация 10", DateOfPublicationPublish = new DateTime(2001, 3, 15), Authors = new List<Author>() };
            Publication ar11 = new Publication() { PublicationGenreID = 1, PublicationName ="Публикация 11", DateOfPublicationPublish = new DateTime(2000, 1, 5), Authors = new List<Author>() };


            context.Publications.Add(ar1);
            context.Publications.Add(ar2);
            context.Publications.Add(ar3);
            context.Publications.Add(ar4);
            context.Publications.Add(ar5);
            context.Publications.Add(ar6);
            context.Publications.Add(ar7);
            context.Publications.Add(ar8);
            context.Publications.Add(ar9);
            context.Publications.Add(ar10);
            context.Publications.Add(ar11);


            context.SaveChanges();
        }
        private void SeedPublicationGenres(WebLibrary2.Domain.Concrete.EFDbContext context)
        {
            if (context.PublicationGenres.Any())
            {
                return;
            }

            PublicationGenre pG1 = new PublicationGenre() { PublicationGenreName = "Монография " };
            PublicationGenre pG2 = new PublicationGenre() { PublicationGenreName = "Научный реферат" };
            PublicationGenre pG3 = new PublicationGenre() { PublicationGenreName = "Информативный реферат" };
            PublicationGenre pG4 = new PublicationGenre() { PublicationGenreName = "Тезис" };
            PublicationGenre pG5 = new PublicationGenre() { PublicationGenreName = "Научная статья" };


            context.PublicationGenres.AddRange(new List<PublicationGenre> { pG1, pG2, pG3, pG4, pG5 });

            context.SaveChanges();
        }

        private void SeedMagazines(WebLibrary2.Domain.Concrete.EFDbContext context)
        {
            if (context.Magazines.Any())
            {
                return;
            }

            Magazine m1 = new Magazine() { MagazineGenreID= 1, MagazineName = "Журнал 1", DateOfMagazinePublish = new DateTime(2003, 9, 30), Authors = new List<Author>() };
            Magazine m2 = new Magazine() { MagazineGenreID= 2, MagazineName = "Журнал 2", DateOfMagazinePublish = new DateTime(2013, 4, 30), Authors = new List<Author>() };
            Magazine m3 = new Magazine() { MagazineGenreID= 3, MagazineName = "Журнал 3", DateOfMagazinePublish = new DateTime(2004, 5, 20), Authors = new List<Author>() };
            Magazine m4 = new Magazine() { MagazineGenreID= 4, MagazineName = "Журнал 4", DateOfMagazinePublish = new DateTime(2015, 6, 30), Authors = new List<Author>() };
            Magazine m5 = new Magazine() { MagazineGenreID= 5, MagazineName = "Журнал 5", DateOfMagazinePublish = new DateTime(2006, 7, 10), Authors = new List<Author>() };
            Magazine m6 = new Magazine() { MagazineGenreID= 1, MagazineName = "Журнал 6", DateOfMagazinePublish = new DateTime(2007, 8, 11), Authors = new List<Author>() };
            Magazine m7 = new Magazine() { MagazineGenreID= 2, MagazineName = "Журнал 7", DateOfMagazinePublish = new DateTime(2008, 9, 16), Authors = new List<Author>() };
            Magazine m8 = new Magazine() { MagazineGenreID= 3, MagazineName = "Журнал 8", DateOfMagazinePublish = new DateTime(2003, 5, 30), Authors = new List<Author>() };
            Magazine m9 = new Magazine() { MagazineGenreID= 4, MagazineName = "Журнал 9", DateOfMagazinePublish = new DateTime(2009, 9, 20), Authors = new List<Author>() };
            Magazine m10 = new Magazine() { MagazineGenreID= 5, MagazineName = "Журнал 10", DateOfMagazinePublish = new DateTime(2001, 3, 15), Authors = new List<Author>() };
            Magazine m11 = new Magazine() { MagazineGenreID= 1, MagazineName = "Журнал 11", DateOfMagazinePublish = new DateTime(2000, 1, 5), Authors = new List<Author>() };

            context.Magazines.Add(m1);
            context.Magazines.Add(m2);
            context.Magazines.Add(m3);
            context.Magazines.Add(m4);
            context.Magazines.Add(m5);
            context.Magazines.Add(m6);
            context.Magazines.Add(m7);
            context.Magazines.Add(m8);
            context.Magazines.Add(m9);
            context.Magazines.Add(m10);
            context.Magazines.Add(m11);


            context.SaveChanges();
        }
        private void SeedMagazineGenres(WebLibrary2.Domain.Concrete.EFDbContext context)
        {
            if (context.MagazineGenres.Any())
            {
                return;
            }

            MagazineGenre mG1 = new MagazineGenre() { MagazineGenreName = "Общественно-политический" };
            MagazineGenre mG2 = new MagazineGenre() { MagazineGenreName = "Научно-популярный" };
            MagazineGenre mG3 = new MagazineGenre() { MagazineGenreName = "Популярный" };
            MagazineGenre mG4 = new MagazineGenre() { MagazineGenreName = "Производственно-практический" };
            MagazineGenre mG5 = new MagazineGenre() { MagazineGenreName = "Литературно-художественный" };


            context.MagazineGenres.AddRange(new List<MagazineGenre> { mG1, mG2, mG3, mG4, mG5 });

            context.SaveChanges();
        }

        protected override void Seed(WebLibrary2.Domain.Concrete.EFDbContext context)
        {
            SeedAuthors(context);

            SeedGenres(context);
            SeedBooks(context);

            SeedArticlesGenres(context);
            SeedArticles(context);

            SeedPublicationGenres(context);
            SeedPublications(context);

            SeedMagazineGenres(context);
            SeedMagazines(context);

            base.Seed(context);
        }
    }
}
