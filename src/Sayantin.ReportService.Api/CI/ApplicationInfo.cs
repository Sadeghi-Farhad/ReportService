using System.Reflection;
using System.Xml;

namespace ReportService.Api.CI
{
    public class ApplicationInfo
    {
        private readonly Assembly? assembly;
        private readonly List<string> extraDependencies =
        [
            "Exchange-MBx1.crouse.ir",
            "Exchange-MBx2.crouse.ir",
            "Exchange-MBx3.crouse.ir",
            "Exchange-MBx4.crouse.ir"
        ];
        private List<string> topLevelPackages = [];

        public Version? Version { get; }
        public string Title { get; }
        public string Authors { get; }
        public string SupportEmail { get; }
        public string Company { get; }
        public string AuthType { get; }
        public string Environment { get; }
        public string LastUpdated { get; }

        public ApplicationInfo(WebApplicationBuilder builder)
        {
            assembly = Assembly.GetEntryAssembly();

            Version = assembly?.GetName().Version;
            Title = assembly?.GetName().Name ?? string.Empty;
            Authors = assembly?.GetCustomAttributes<AssemblyMetadataAttribute>()?.FirstOrDefault(attr => attr.Key == "Authors")?.Value ?? "-";
            SupportEmail = "crousesystem@crouse.ir";
            Company = assembly?.GetCustomAttribute<AssemblyCompanyAttribute>()?.Company ?? "-";
            AuthType = assembly?.GetCustomAttributes<AssemblyMetadataAttribute>()?.FirstOrDefault(attr => attr.Key == "AuthenticationType")?.Value ?? string.Empty;
            Environment = builder?.Environment.EnvironmentName ?? string.Empty;
            LastUpdated = assembly?.GetCustomAttributes<AssemblyMetadataAttribute>().FirstOrDefault(attr => attr.Key == "LastUpdated")?.Value ?? string.Empty;
        }

        public override string ToString()
        {
            var fullDescription = $@"
            <hr/><br/>**Environment:** {Environment}
            <br/>**Authors**: {Authors}
            <br/>**Company**: {Company}
            <br/>**Authentication Type**: {AuthType}
            <br/>**Last Updated**: {LastUpdated}
            <hr/>
            **Dependencies:**<br/>";

            try
            {
                LoadTopLevelNuGetPackages();
                fullDescription += string.Join("<br/>", topLevelPackages.Concat(extraDependencies).ToList());
            }
            catch (Exception ex)
            {
                fullDescription += ex.Message;
            }

            return fullDescription;
        }

        private void LoadTopLevelNuGetPackages()
        {
            var assembly = Assembly.GetExecutingAssembly();
            var resourceName = assembly.GetManifestResourceNames()
                .FirstOrDefault(n => n.EndsWith(".csproj"));

            if (resourceName == null)
                throw new Exception("Could not find embedded .csproj.");

            using var stream = assembly.GetManifestResourceStream(resourceName);
            if (stream == null)
                throw new Exception("Failed to read .csproj resource.");

            var doc = new XmlDocument();
            doc.Load(stream);

            var packageRefs = doc.GetElementsByTagName("PackageReference");

            foreach (XmlNode node in packageRefs)
            {
                var name = node.Attributes?["Include"]?.Value;
                var version = node.Attributes?["Version"]?.Value ?? node.SelectSingleNode("Version")?.InnerText;

                if (!string.IsNullOrEmpty(name) && !string.IsNullOrEmpty(version))
                {
                    topLevelPackages.Add($"{name} - {version}");
                }
            }

            topLevelPackages.Sort();
        }
    }
}
