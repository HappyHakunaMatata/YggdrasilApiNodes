using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Threading;
using Microsoft.AspNetCore.Components;
using YggdrasilApiNodes.Models;

namespace YggdrasilApiNodes.Services
{
	public class Composite: Component
	{
        protected List<Component> _children;
        private readonly Object _mutex;
        private bool _IsInner = false;

        public bool IsInner
        {
            get
            {
                return _IsInner;
            }
            set
            {
                _IsInner = value;
            }
        }

        public int Count
        {
            get
            {
                return _children.Count;
            }
        }

        public Composite()
        {
            _children = new List<Component>();
            _mutex = new Object();
        }

        public List<Leath?> GetLeaths()
        {
            try
            {
                return _children.Select(i => i as Leath).Where(leath => leath != null).ToList();
            }
            catch
            {
                throw;
            }
        }

        private volatile List<Peer> _peers = new List<Peer>();
        public async Task GetPeersParallel()
        {
            ArgumentNullException.ThrowIfNull(_children);
            try
            {
                var options = new ParallelOptions();
                if (!IsInner)
                {
                    options.MaxDegreeOfParallelism = 4;
                }
                else
                {
                    options.MaxDegreeOfParallelism = 1;
                }
                
                await Parallel.ForEachAsync(_children, options, async (leath, ctx) =>
                {
                    if (leath.IsComposite())
                    {
                        var composite = leath as Composite;
                        if (composite != null)
                        {
                            var leathPeers = await composite.GetPeers();
                            lock (_mutex)
                            {
                                _peers.AddRange(leathPeers);
                            }
                        }
                    }
                    else
                    {
                        var html = await leath.GetHTMLContent();
                        if (string.IsNullOrEmpty(html))
                        {
                            return;
                        }
                        var leathOne = leath as Leath;
                        if (leathOne != null)
                        {
                            var peer = leathOne.GetPeers(html);
                            lock (_mutex)
                            {
                                _peers.AddRange(peer);
                            }
                            return;
                        }
                        var leathTwo = leath as LeathTwo;
                        if (leathTwo != null)
                        {
                            var peer = leathTwo.GetPeers(html);
                            lock (_mutex)
                            {
                                _peers.AddRange(peer);
                            }
                            return;
                        }
                    }
                });
            }
            catch
            {
                throw;
            }
        }


        public override async Task<List<Peer>> GetPeers()
        {
            ArgumentNullException.ThrowIfNull(_children);
            try
            {
                var peers = new List<Peer>();
                foreach (var i in _children)
                {
                    if (i.IsComposite())
                    {
                        var composite = i as Composite;
                        if (composite != null)
                        {
                            var leathPeers = await composite.GetPeers();
                            peers.AddRange(leathPeers);
                        }
                    }
                    else
                    {
                        var html = await i.GetHTMLContent();
                        if (string.IsNullOrEmpty(html))
                        {
                            continue;
                        }
                        var leath = i as Leath;
                        if (leath != null)
                        {
                            var peer = leath.GetPeers(html);
                            peers.AddRange(peer);
                            continue;
                        }
                        var leathTwo = i as LeathTwo;
                        if (leathTwo != null)
                        {
                            var peer = leathTwo.GetPeers(html);
                            peers.AddRange(peer);
                            continue;
                        }
                    }
                }
                return peers;
            }
            catch
            {
                throw;
            }
        }

        public void AddLeath(string component)
        {
            ArgumentNullException.ThrowIfNull(component);
            var leath = new Leath(component);
            if (leath.IsCreated())
            {
                this._children.Add(leath);
            }
        }

        public void AddLeathTwo(string component)
        {
            ArgumentNullException.ThrowIfNull(component);
            var leath = new LeathTwo(component);
            if (leath.IsCreated())
            {
                this._children.Add(leath);
            }
        }

        public void Add(Composite composite)
        {
            ArgumentNullException.ThrowIfNull(composite);
            composite.IsInner = true;
            this._children.Add(composite);
        }

        public void Add(Component component)
        {
            ArgumentNullException.ThrowIfNull(component);
            this._children.Add(component);
        }

        public void Remove(Component component)
        {
            ArgumentNullException.ThrowIfNull(component);
            this._children.Remove(component);
        }

        public void AddRange(List<Component> component)
        {
            ArgumentNullException.ThrowIfNull(component);
            try
            {
                this._children.AddRange(component);
            }
            catch
            {
                throw;
            }
        }

        public void AddRange(List<LeathTwo> component)
        {
            ArgumentNullException.ThrowIfNull(component);
            try
            {
                this._children.AddRange(component);
            }
            catch
            {
                throw;
            }
        }

        public void AddRange(List<Leath> component)
        {
            ArgumentNullException.ThrowIfNull(component);
            try
            {
                this._children.AddRange(component);
            }
            catch
            {
                throw;
            }
        }

        public void AddRangeLeath(List<string> component)
        {
            ArgumentNullException.ThrowIfNull(component);
            try
            {
                this._children.AddRange(component.Select(i => new Leath(i))
                                .Where(leath => leath.IsCreated()));
            }
            catch
            {
                throw;
            }
        }

        public void AddRangeLeathTwo(List<string> component)
        {
            ArgumentNullException.ThrowIfNull(component);
            try
            {
                this._children.AddRange(component.Select(i => new LeathTwo(i))
                                .Where(leath => leath.IsCreated()));
            }
            catch
            {
                throw;
            }
        }

        public override bool IsComposite()
        {
            return true;
        }
    }
}

