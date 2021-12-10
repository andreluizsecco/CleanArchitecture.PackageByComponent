using CleanArchitecture.PackageByComponent.Core.Domain.Constants;
using CleanArchitecture.PackageByComponent.Core.Domain.Interfaces.Services;
using CleanArchitecture.PackageByComponent.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.FeatureManagement;
using Microsoft.FeatureManagement.Mvc;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace CleanArchitecture.PackageByComponent.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IHomePageService _homePageService;
        private readonly IFeatureManager _featureManager;


        public HomeController(IHomePageService homePageService,
                              IFeatureManager featureManager,
                              ILogger<HomeController> logger)
        {
            _homePageService = homePageService;
            _featureManager = featureManager;
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            ViewBag.Title = _homePageService.GetTitle();
            
            if (await _featureManager.IsEnabledAsync(FeatureFlags.ShowSubTitle))
                ViewBag.SubTitle = _homePageService.GetSubTitle();
            
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
