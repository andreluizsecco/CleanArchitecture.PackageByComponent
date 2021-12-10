using CleanArchitecture.PackageByComponent.Core.Domain.Interfaces.Repositories;
using CleanArchitecture.PackageByComponent.Core.Domain.Interfaces.Services;
using System;

namespace CleanArchitecture.PackageByComponent.Core.Domain.Services
{
    public class HomePageService : IHomePageService
    {
        private readonly IHomePageRepository _homePageRepository;

        public HomePageService(IHomePageRepository homePageRepository) =>
            _homePageRepository = homePageRepository;

        public string GetTitle()
        {
            var title = _homePageRepository.GetTitle();

            //Business Rule
            return $"{title} - {DateTime.Now.ToShortTimeString()}";
        }

        public string GetSubTitle()
        {
            var subTitle = _homePageRepository.GetSubTitle();

            //Business Rule
            return $"{subTitle} - All Rights reserved.";
        }
    }
}
