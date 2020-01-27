using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CandyShop.Models;
using CandyShop.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace CandyShop.Controllers
{
    public class CandyController : Controller
    {
        private readonly ICandyRepository _candyRepository;
        private readonly ICategoryRepository _categoryRepository;

        public CandyController(ICandyRepository candyRepository , ICategoryRepository categoryRepository)
        {
            this._candyRepository = candyRepository;
            this._categoryRepository = categoryRepository;
        }
        
        public IActionResult List()
        {
            
            var candyListViewModel = new CandyListViewModel();
            candyListViewModel.Candies = _candyRepository.GetAllCandy;
            candyListViewModel.CurrentCategory = "BestSellers";

            return View(candyListViewModel);
        }

        public IActionResult Details(int id)
        {
           var candy =  _candyRepository.GetCandyById(id);

            if (candy == null)
                return NotFound();
            else
                return View(candy);
        }
    }
}