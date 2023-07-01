using Asal.Library.Abstraction.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Asal.Library.core.Interfaces
{
    public interface IPackageService
    {
        Task<IEnumerable<NugetPackageDetailsViewModel>> GetPackages();
    }
}
