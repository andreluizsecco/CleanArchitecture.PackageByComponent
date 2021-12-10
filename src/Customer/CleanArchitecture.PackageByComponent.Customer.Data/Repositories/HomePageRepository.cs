
using CleanArchitecture.PackageByComponent.Core.Domain.Interfaces.Repositories;

namespace CleanArchitecture.PackageByComponent.Customer.Data.Repositories
{
    public class HomePageRepository : IHomePageRepository
    {
        public string GetTitle() => "Customer2 Title";

        public string GetSubTitle() => "Customer Name";
    }
}
