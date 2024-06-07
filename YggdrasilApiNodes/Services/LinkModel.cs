using System;
namespace YggdrasilApiNodes.Services
{
	public class LinkModel
	{

        public GithubData? githubdata { get; set; } = null;
        public string? defaultweb { get; set; } = null;
        public List<LeathTwo>? GithubLinks { get; set; } = null;

        public Component GetDefault()
        {
            ArgumentNullException.ThrowIfNull(defaultweb);
            try
            {
                var composite = new Composite();
                return new Leath(defaultweb);
            }
            catch
            {
                throw;
            }
        }

        public Component GetGithubLinks()
        {
            ArgumentNullException.ThrowIfNull(GithubLinks);
            try
            {
                if (GithubLinks.Count == 1)
                {
                    return GithubLinks.First();
                }
                var composite = new Composite();
                composite.AddRange(GithubLinks);
                return composite;
            }
            catch
            {
                throw;
            }
        }

        public Component GetComponent()
        {
            ArgumentNullException.ThrowIfNull(githubdata);
            ArgumentNullException.ThrowIfNull(githubdata.leaths);
            try
            {
                if (githubdata.leaths.Count == 1)
                {
                    return new LeathTwo(githubdata.leaths.First());
                }
                var composite = new Composite();
                composite.AddRangeLeathTwo(githubdata.leaths);
                return composite;
            }
            catch
            {
                throw;
            }
        }

        private async Task<List<string?>> GetGithubFolders(string url)
        {
            ArgumentException.ThrowIfNullOrEmpty(url);
            try
            {
                LeathTwo leath = new LeathTwo(url);
                var html = await leath.GetHTMLContent();
                if (string.IsNullOrEmpty(html))
                {
                    return new List<string?>();
                }
                var model = leath.GetModel(html);
                var items = model?.props?.initialPayload?.tree?.items?.Select(n => n).Where(n => n.name != "other" && n.name != "README.md");
                if (items == null)
                {
                    return new List<string?>();
                }
                return items.Select(n => n.name).ToList();
            }
            catch
            {
                throw;
            }
        }

        private async Task<List<string?>> GetGithubMD(string url)
        {
            ArgumentException.ThrowIfNullOrEmpty(url);
            try
            {
                LeathTwo leath = new LeathTwo(url);
                var html = await leath.GetHTMLContent();
                if (string.IsNullOrEmpty(html))
                {
                    return new List<string?>();
                }
                var model = leath.GetModel(html);
                var items = model?.payload?.tree?.items;
                if (items == null)
                {
                    return new List<string?>();
                }
                return items.Select(n => n.path).ToList();
            }
            catch
            {
                throw;
            }
        }

        public async Task<List<LeathTwo>> GetGithubPeerLinks()
        {
            try
            {
                GithubLinks = new List<LeathTwo>();
                var link = githubdata?.root;

                if (string.IsNullOrEmpty(link))
                {
                    return GithubLinks;
                }
                var peers = await GetGithubFolders(link);
                foreach (var i in peers)
                {
                    var result = await GetGithubMD(link + i);
                    var links = result.Select(n => "https://github.com/yggdrasil-network/public-peers/blob/master/" + n).Where(n => !string.IsNullOrEmpty(n)).ToList();
                    var leaths = links.Select(n => new LeathTwo(n) { location = i });
                    GithubLinks.AddRange(leaths);
                }
                return GithubLinks;
            }
            catch
            {
                throw;
            }
        }
    }
}

