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
        
        public ViewResult List(string category)
        {
            IEnumerable<Candy> candies;
            string currentCategory;

            if (String.IsNullOrEmpty(category))
            {
                //get all candies
                candies = _candyRepository.GetAllCandy.OrderBy(c=> c.CandyId);
                currentCategory = "All Candy";
            }
            else
            {
                //get all candies for the category
                candies = _candyRepository.GetAllCandy.Where(c => c.Category.CategoryName == category).OrderBy(s => s.CandyId);
                currentCategory = _categoryRepository.GetAllCategories.FirstOrDefault(c => c.CategoryName == category)?.CategoryName;
            }

            return View(new CandyListViewModel
            {
                Candies = candies,
                CurrentCategory = currentCategory
            });
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