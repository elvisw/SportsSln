using Microsoft.AspNetCore.Mvc;
using SportsStore.Models;
using System.Linq;
using SportsStore.Models.ViewModels;
using System.ComponentModel;

namespace SportsStore.Controllers
{
    public class HomeController : Controller
    {
        private readonly IStoreRepository _repository;
        public int PageSize { get; set; } = 4;
        public HomeController(IStoreRepository repo)
        {
            _repository = repo;
        }
        public ViewResult Index(string category, int productPage = 1)
            => View(new ProductsListViewModel
            {
                Products = _repository.Products
            .Where(p => category == null || p.Category == category)
            .OrderBy(p => p.ProductID)
            .Skip((productPage - 1) * PageSize)
            .Take(PageSize),
                PagingInfo = new PagingInfo
                {
                    CurrentPage = productPage,
                    ItemsPerPage = PageSize,
                    TotalItems = category == null ?
                    _repository.Products.Count() :
                    _repository.Products.Where(p => category == p.Category).Count()
                },
                CurrentCategory = category
            });

    }
}
