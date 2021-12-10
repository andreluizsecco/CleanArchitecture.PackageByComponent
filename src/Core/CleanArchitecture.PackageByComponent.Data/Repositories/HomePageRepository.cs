using CleanArchitecture.PackageByComponent.Core.Domain.Interfaces.Repositories;

namespace CleanArchitecture.PackageByComponent.Core.Data.Repositories
{
    public class HomePageRepository : IHomePageRepository
    {
        public string GetTitle() => "Default Title";

        public string GetSubTitle() =>
            throw new System.NotImplementedException();
    }
}
