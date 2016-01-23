using System;

namespace Sitecore.FakeDb.Sites
{
    using System.Collections.Generic;
    using System.Collections.Specialized;
    using System.Linq;
    using System.Threading;
    using Sitecore.Sites;
    using StringDictionary = Sitecore.Collections.StringDictionary;

    public class FakeConfigSiteProvider : ConfigSiteProvider, IThreadLocalProvider<ConfigSiteProvider>
    {
        private readonly ThreadLocal<ConfigSiteProvider> localProvider = new ThreadLocal<ConfigSiteProvider>();

        private bool disposed;

        public ThreadLocal<ConfigSiteProvider> LocalProvider { get; }

        public override Site GetSite(string siteName)
        {
            return GetSites().FirstOrDefault(t => t.Properties["name"] == siteName);
        }

        public override SiteCollection GetSites()
        {
            if (this.IsLocalProviderSet())
                return this.localProvider.Value.GetSites();
            else
            {
                var result = base.GetSites();

                if (Context.Site != null)
                {
                    var site = new Site(Context.Site.Name, new StringDictionary(ToDictionary(Context.Site.SiteInfo.Properties)));

                    var siteFromConfig = result.FirstOrDefault(t => t.Name == Context.Site.Name);
                    if (siteFromConfig != null)
                    {
                        siteFromConfig.Properties.Clear();
                        siteFromConfig.Properties.AddRange(site.Properties);
                    }
                    else
                    {
                        result.Add(site);
                    }
                }

                return result;
            }
        }

        private static IDictionary<string, string> ToDictionary(NameValueCollection col)
        {
            IDictionary<string, string> dict = new Dictionary<string, string>();
            foreach (var k in col.AllKeys)
            {
                dict.Add(k, col[k]);
            }
            return dict;
        }


        public bool IsLocalProviderSet()
        {
            return this.localProvider.Value != null;
        }


        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (this.disposed)
            {
                return;
            }

            if (!disposing)
            {
                return;
            }

            this.localProvider.Dispose();

            this.disposed = true;
        }

    }

}
