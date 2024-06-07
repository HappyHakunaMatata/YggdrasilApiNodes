using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using YggdrasilApiNodes.Models;
using static System.Net.WebRequestMethods;

namespace YggdrasilApiNodes.Services
{
	public abstract class Component
    {

        protected readonly Uri? uri = null;

        public Component(string uri)
        {
            this.uri = GetUri(uri);
        }

        public static Uri? GetUri(string? uri)
        {
            if (Uri.TryCreate(uri, UriKind.Absolute, out var result))
            {
                return result;
            }
            return null;
        }

        public bool IsCreated()
        {
            if (uri == null)
            {
                return false;
            }
            return true;
        }

        public Component()
        {

        }

        public virtual bool IsComposite()
        {
            return false;
        }

        
        public abstract Task<List<Peer>> GetPeers();



        public async Task<string?> GetHTMLContent()
        {
            ArgumentNullException.ThrowIfNull(uri);
            using (var timeout = new CancellationTokenSource(5000))
            {
                using (HttpClient client = new HttpClient())
                {
                    try
                    {
                        HttpResponseMessage response = await client.GetAsync(uri, timeout.Token);
                        if (response.IsSuccessStatusCode)
                        {
                            var html = await response.Content.ReadAsStringAsync(timeout.Token);
                            return html;
                        }
                        else
                        {
                            return null;
                        }
                        
                    }
                    catch
                    {
                        throw;
                    }
                    finally
                    {
                        client.CancelPendingRequests();
                    }
                }
            }
        }
    }
}

