using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebLibrary2.DataAccessLayer.Interfaces;
using WebLibrary2.EntitiesLayer.Entities;
using WebLibrary2.ViewModelsLayer.ViewModels;

namespace WebLibrary2.BusinessLogicLayer.Sevices
{
    public class BookService
    {
        IUnitOfWork unitOfWork;
        public BookService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public IEnumerable<GetBookView> GetAllBooks()
        {
            List<Book> books = unitOfWork.Bo
            return 
        }
    }
}
