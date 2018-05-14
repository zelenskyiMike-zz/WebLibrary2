using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebLibrary2.DataAccessLayer.Concrete;
using WebLibrary2.EntitiesLayer.Entities;
using WebLibrary2.ViewModelsLayer.ViewModels;

namespace WebLibrary2.BusinessLogicLayer.Sevices
{
    public class ArticleService
    {
        private readonly DbContext context;
        private readonly ArticleRepository articleRepository;
        private readonly GenericRepository<Article> genericRepository;
        private readonly ArticleAuthorsRepository articleAuthorsRepository;

        public ArticleService(DbContext context, ArticleRepository articleRepository, GenericRepository<Article> genericRepository, ArticleAuthorsRepository articleAuthorsRepository)
        {
            this.context = context;
            this.articleRepository = articleRepository;
            this.genericRepository = genericRepository;
            this.articleAuthorsRepository = articleAuthorsRepository;
        }

        public IEnumerable<GetArticleGenreView> GetAllArticles()
        {
            var articles = context.ArticleGenres.ToList();
            var articlesMapped = Mapper.Map<IEnumerable<ArticleGenre>,IEnumerable<GetArticleGenreView>>(articles);
            return articlesMapped;
        }
        public void CreateArticle(GetArticleView articleVM)
        {
            var article = Mapper.Map<GetArticleView,Article>(articleVM);
            genericRepository.Create(article);
        }
        public GetArticleView GetArticleByID(int id)
        {
            var article = genericRepository.GetByID(id);
            var articleMapped = Mapper.Map<Article, GetArticleView>(article);
            return articleMapped;
        }
        public GetAllArticlesView GetArticleDetails(int id)
        {
            var article = articleRepository.GetArticleDetails(id);
            var articleMapped = Mapper.Map<Article, GetAllArticlesView>(article);
            return articleMapped;
        }
        public IEnumerable<GetAuthorView> GetAuthorsNotExistInArticle(GetAllArticlesView article)
        {
            var articleForSearch = Mapper.Map<GetAllArticlesView, Article>(article);
            var listAuthors = articleRepository.GetAuthorsNotExistInArticle(articleForSearch);
            var listAuthorsMapped = Mapper.Map<IEnumerable<Author>,IEnumerable<GetAuthorView>>(listAuthors);
            return listAuthorsMapped;
        }
        public void EditArticle(GetAllArticlesView articleVM, int[] authorIDsForDelete, int[] authorIDsForInsert)
        {
            var article = Mapper.Map<GetAllArticlesView, Article>(articleVM);
            genericRepository.Update(article);
            articleAuthorsRepository.DeleteAuthorFromArticle(articleVM.ArticleID, authorIDsForDelete);
            articleAuthorsRepository.AddAuthorToArticle(articleVM.ArticleID, authorIDsForInsert);
            articleRepository.Save();
        }
        public void DeleteArticle(int id)
        {
            var article = genericRepository.GetByID(id);
            genericRepository.Remove(article);
        }
        public IEnumerable<GetAllArticlesView> GetAllArticlesWithGenres()
        {
            var articles = articleRepository.GetAllArticlesWithGenres();
            var articlesMaped = Mapper.Map<IEnumerable<Article>,IEnumerable<GetAllArticlesView>>(articles);
            return articlesMaped;
        }
    }
}
