namespace Asal.Library.Abstraction.ViewModels
{
    public class NugetPackageDetailsViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Title { get; set; }
        public string NugetOrgLink { get; set; }
        public string PackageManagerCommand { get; set; }
        public string ReadMe { get; set; }
        public long TotalDownloads { get; set; }
        public string Owner { get; set; }
        public string Description { get; set; }
        public string LatestVersion { get; set; }
        public string IconeUrl { get; set; }
    }
}
