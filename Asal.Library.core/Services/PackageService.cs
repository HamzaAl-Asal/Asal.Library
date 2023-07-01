using Asal.Library.Abstraction.ViewModels;
using Asal.Library.core.Interfaces;
using Asal.Library.core.Models.Data;
using AutoMapper;
using NuGet.Common;
using NuGet.Configuration;
using NuGet.Protocol.Core.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Asal.Library.core.Services
{
    public class PackageService : IPackageService
    {
        private readonly AsalLibraryDbContext context;
        private readonly IMapper mapper;
        const string packageSource = "https://api.nuget.org/v3/index.json";
        public PackageService(AsalLibraryDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }


        private async Task<List<PackageDetailViewModel>> GetPackagesDetails()
        {
            var packages = await Task.FromResult(mapper.Map<List<PackageDetailViewModel>>(context.PackageDetail.ToList()));

            if (!packages.Any())
                return new List<PackageDetailViewModel>();

            return await Task.FromResult(packages);
        }

        private async Task<NugetPackageDetailsViewModel> GetPackage(string packageId)
        {
            var source = new PackageSource(packageSource);
            var providers = Repository.Provider.GetCoreV3();

            var repository = new SourceRepository(source, providers);

            var result = await GetSearchResponse(repository, packageId);

            if (result is null)
            {
                throw new InvalidOperationException($"Ooops, No results found for {packageId} !");
            }

            var package = result.FirstOrDefault();
            var latestVersion = await package.GetVersionsAsync();

            //DB details
            var packageDetail = await GetPackagesDetails();
            var f = packageDetail.Where(x => x.Name == package.Identity.Id);

            return await Task.FromResult(new NugetPackageDetailsViewModel
            {
                Name = package.Identity.Id,
                Title = package.Title,
                Description = package.Description,
                Owner = package.Owners,
                TotalDownloads = package.DownloadCount ?? 0,
                LatestVersion = latestVersion.LastOrDefault().Version.ToString(),
                IconeUrl = package.IconUrl.ToString(),

                Id = f.FirstOrDefault().Id,
                NugetOrgLink = f.FirstOrDefault().NugetOrgLink,
                PackageManagerCommand = f.FirstOrDefault().PackageManagerCommand,
                ReadMe = f.FirstOrDefault().ReadMe
            });

        }

        private async Task<IEnumerable<IPackageSearchMetadata>> GetSearchResponse(SourceRepository repository, string packageId)
        {
            var searchResponse = await repository.GetResourceAsync<PackageSearchResource>();
            var searchFilter = new SearchFilter(false);

            return await Task.FromResult(await searchResponse.SearchAsync($"packageId: {packageId}",
                searchFilter,
                0,
                1,
                NullLogger.Instance,
                CancellationToken.None));
        }

        public async Task<IEnumerable<NugetPackageDetailsViewModel>> GetPackages()
        {
            var packages = new List<NugetPackageDetailsViewModel>();

            foreach (var package in await GetPackagesDetails())
            {
                var packageDetail = await GetPackage(package.Name);
                packages.Add(packageDetail);
            }

            return packages;
        }
    }
}
