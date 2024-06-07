using System;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Xunit.Abstractions;
using YamlDotNet.Serialization;
using YggdrasilApiNodes.Interfaces;
using YggdrasilApiNodes.Models;
using YggdrasilApiNodes.Services;
using YggdrasilApiNodes.Services.Adapter;
using static System.Net.Mime.MediaTypeNames;

namespace YggdrasilApiNodesUnitTest
{
	public class AdapterXmlTest
	{
        private readonly ITestOutputHelper _testOutputHelper;


        public AdapterXmlTest(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
        }

        [Fact]
        public void GetCountries()
        {
            IAdaptee adaptee = new XMLAdaptee();
            var adapter = new Adapter(adaptee);
            #region null test
            var result = adapter.GetCountries(null);
            var xml = (ContentResult)result;
            if (string.IsNullOrEmpty(xml.Content))
            {
                Assert.True(false);
                return;
            }
            using (var stream = new MemoryStream(Encoding.Unicode.GetBytes(xml.Content)))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(List<Country>));
                var obj = (List<Country>?)serializer.Deserialize(stream);
                Assert.True(obj?.Count == 0);
            }
            #endregion

            
            #region normal
            var data = new List<Country>();
            var country = new Country()
            {
                Id = 1,
                Name = "Test",
                region = "Region",
            };
            data.Add(country);
            result = adapter.GetCountries(data);
            xml = (ContentResult)result;
            if (string.IsNullOrEmpty(xml.Content))
            {
                Assert.True(false);
                return;
            }
            using (var stream = new MemoryStream(Encoding.Unicode.GetBytes(xml.Content)))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(List<Country>));
                var obj = (List<Country>?)serializer.Deserialize(stream);
                Assert.True(obj?.Count == 1);
                Assert.True(obj.First()._ID == "1");
                Assert.True(obj.First().Name == "Test");
                Assert.True(string.IsNullOrEmpty(obj.First().region));
            }
            #endregion
            #region null name
            data = new List<Country>();
            country = new Country()
            {
                Id = 1,
                Name = null,
                region = null,
            };
            data.Add(country);
            result = adapter.GetCountries(data);
            xml = (ContentResult)result;
            if (string.IsNullOrEmpty(xml.Content))
            {
                Assert.True(false);
                return;
            }
            using (var stream = new MemoryStream(Encoding.Unicode.GetBytes(xml.Content)))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(List<Country>));
                var obj = (List<Country>?)serializer.Deserialize(stream);
                Assert.True(obj?.Count == 0);
            }
            #endregion
            #region empty
            data = new List<Country>();
            result = adapter.GetCountries(data);
            xml = (ContentResult)result;
            if (string.IsNullOrEmpty(xml.Content))
            {
                Assert.True(false);
                return;
            }
            using (var stream = new MemoryStream(Encoding.Unicode.GetBytes(xml.Content)))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(List<Country>));
                var obj = (List<Country>?)serializer.Deserialize(stream);
                Assert.True(obj?.Count == 0);
            }
            #endregion
            #region multiply
            data = new List<Country>();
            country = new Country()
            {
                Id = 1,
                Name = null,
                region = null,
            };
            var country2 = new Country()
            {
                Id = 2,
                Name = "Test",
                region = "Hello",
            };
            var country3 = new Country()
            {
                Id = 3,
                Name = "",
                region = "Test",
            };
            var country4 = new Country()
            {
                Id = 4,
                Name = "OK",
                region = "Region",
            };
            data.Add(country);
            data.Add(country2);
            data.Add(country3);
            data.Add(country4);
            result = adapter.GetCountries(data);
            xml = (ContentResult)result;
            if (string.IsNullOrEmpty(xml.Content))
            {
                Assert.True(false);
                return;
            }
            using (var stream = new MemoryStream(Encoding.Unicode.GetBytes(xml.Content)))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(List<Country>));
                var obj = (List<Country>?)serializer.Deserialize(stream);
                Assert.True(obj?.Count == 2);
                Assert.True(obj?[0].Name == "Test");
                Assert.True(obj?[1].Name == "OK");
            }
            #endregion
        }

        [Fact]
        public void GetIpAddresses()
        {
            IAdaptee adaptee = new XMLAdaptee();
            var adapter = new Adapter(adaptee);
            #region null test
            var result = adapter.GetIpAddresses(null);
            var xml = (ContentResult)result;
            if (string.IsNullOrEmpty(xml.Content))
            {
                Assert.True(false);
                return;
            }
            using (var stream = new MemoryStream(Encoding.Unicode.GetBytes(xml.Content)))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(List<Model.Peer>));
                var obj = (List<Model.Peer>?)serializer.Deserialize(stream);
                Assert.True(obj?.Count == 0);
            }
            #endregion
            #region normal test
            List<Peer> peers = new List<Peer>();
            IPAddressEntity iPAddressEntity = new()
            {
                Id = 1,
                ipAddress = System.Net.IPAddress.Parse("0.0.0.0")
            };
            IPAddressEntity iPAddressEntity2 = new()
            {
                Id = 2,
                ipAddress = System.Net.IPAddress.Parse("127.0.0.1")
            };
            Peer peer = new()
            {
                hostAddress = "tcp://194.156.98.6:7676",
                ipAddresses = new List<IPAddressEntity>()
            {
                iPAddressEntity, iPAddressEntity2
            },
            };
            peers.Add(peer);
            result = adapter.GetIpAddresses(peers);
            xml = (ContentResult)result;
            if (string.IsNullOrEmpty(xml.Content))
            {
                Assert.True(false);
                return;
            }
            using (var stream = new MemoryStream(Encoding.Unicode.GetBytes(xml.Content)))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(List<Model.Peer>));
                var obj = (List<Model.Peer>?)serializer.Deserialize(stream);
                Assert.True(obj?.Count == 1);
                Assert.True(obj?.First().hostAddress == "tcp://194.156.98.6:7676");
                Assert.True(obj?.First()?.IPs?.Count == 2);
            }
            #endregion
            #region null ipAddresses list
            peers = new List<Peer>();
            peer = new()
            {
                hostAddress = "tcp://194.156.98.6:7676",
                ipAddresses = null,
            };
            peers.Add(peer);
            result = adapter.GetIpAddresses(peers);
            xml = (ContentResult)result;
            if (string.IsNullOrEmpty(xml.Content))
            {
                Assert.True(false);
                return;
            }
            using (var stream = new MemoryStream(Encoding.Unicode.GetBytes(xml.Content)))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(List<Model.Peer>));
                var obj = (List<Model.Peer>?)serializer.Deserialize(stream);
                Assert.True(obj?.Count == 0);
            }
            #endregion
            #region empty ipAddresses list
            peers = new List<Peer>();
            peer = new()
            {
                hostAddress = "tcp://194.156.98.6:7676",
                ipAddresses = new List<IPAddressEntity>(),
            };
            peers.Add(peer);
            result = adapter.GetIpAddresses(peers);
            xml = (ContentResult)result;
            if (string.IsNullOrEmpty(xml.Content))
            {
                Assert.True(false);
                return;
            }
            using (var stream = new MemoryStream(Encoding.Unicode.GetBytes(xml.Content)))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(List<Model.Peer>));
                var obj = (List<Model.Peer>?)serializer.Deserialize(stream);
                Assert.True(obj?.Count == 0);
            }
            
            #endregion
            #region empty host address
            peers = new List<Peer>();
            peer = new()
            {
                hostAddress = "",
                ipAddresses = new List<IPAddressEntity>()
            {
                iPAddressEntity, iPAddressEntity2
            },
            };
            peers.Add(peer);
            result = adapter.GetIpAddresses(peers);
            xml = (ContentResult)result;
            if (string.IsNullOrEmpty(xml.Content))
            {
                Assert.True(false);
                return;
            }
            using (var stream = new MemoryStream(Encoding.Unicode.GetBytes(xml.Content)))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(List<Model.Peer>));
                var obj = (List<Model.Peer>?)serializer.Deserialize(stream);
                Assert.True(obj?.Count == 0);
            }
            #endregion
        }

        [Fact]
        public void GetPeers()
        {
            IAdaptee adaptee = new XMLAdaptee();
            var adapter = new Adapter(adaptee);
            #region null test
            var result = adapter.GetPeers(null);
            var xml = (ContentResult)result;
            if (string.IsNullOrEmpty(xml.Content))
            {
                Assert.True(false);
                return;
            }
            using (var stream = new MemoryStream(Encoding.Unicode.GetBytes(xml.Content)))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(List<Model.Peer>));
                var obj = (List<Model.Peer>?)serializer.Deserialize(stream);
                Assert.True(obj?.Count == 0);
            }
            #endregion
            #region empty test
            var data = new List<Peer>();
            result = adapter.GetPeers(data);
            xml = (ContentResult)result;
            if (string.IsNullOrEmpty(xml.Content))
            {
                Assert.True(false);
                return;
            }
            using (var stream = new MemoryStream(Encoding.Unicode.GetBytes(xml.Content)))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(List<Model.Peer>));
                var obj = (List<Model.Peer>?)serializer.Deserialize(stream);
                Assert.True(obj?.Count == 0);
            }
            #endregion
            #region normal test
            data = new List<Peer>();
            Peer peer = new Peer()
            {
                Id = 1,
                hostAddress = "tcp://194.156.98.6:7676",
                info = "Some info here",
                _Uptime = "unidentified"
            };
            data.Add(peer);
            result = adapter.GetPeers(data);
            xml = (ContentResult)result;
            if (string.IsNullOrEmpty(xml.Content))
            {
                Assert.True(false);
                return;
            }
            using (var stream = new MemoryStream(Encoding.Unicode.GetBytes(xml.Content)))
            {
                _testOutputHelper.WriteLine(xml.Content);
                XmlSerializer serializer = new XmlSerializer(typeof(List<Model.Peer>));
                var obj = (List<Model.Peer>?)serializer.Deserialize(stream);
                Assert.True(obj?.Count == 1);
                Assert.True(obj?.First().info == "Some info here");
                Assert.True(obj?.First().hostAddress == "tcp://194.156.98.6:7676");
                Assert.True(obj?.First().Uptime == "unidentified");
            }
            #endregion
            #region empty address test
            data = new List<Peer>();
            peer = new Peer()
            {
                Id = 1,
                hostAddress = "",
                _Uptime = "1",
            };
            data.Add(peer);
            result = adapter.GetPeers(data);
            xml = (ContentResult)result;
            if (string.IsNullOrEmpty(xml.Content))
            {
                Assert.True(false);
                return;
            }
            using (var stream = new MemoryStream(Encoding.Unicode.GetBytes(xml.Content)))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(List<Model.Peer>));
                var obj = (List<Model.Peer>?)serializer.Deserialize(stream);
                Assert.True(obj?.Count == 0);
            }
            #endregion
        }


        [Fact]
        public void GetLocations()
        {
            IAdaptee adaptee = new XMLAdaptee();
            var adapter = new Adapter(adaptee);
            #region null test
            var result = adapter.GetLocations(null);
            var xml = (ContentResult)result;
            if (string.IsNullOrEmpty(xml.Content))
            {
                Assert.True(false);
                return;
            }
            using (var stream = new MemoryStream(Encoding.Unicode.GetBytes(xml.Content)))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(List<Location>));
                var obj = (List<Location>?)serializer.Deserialize(stream);
                Assert.True(obj?.Count == 0);
            }
            #endregion
            #region empty test
            result = adapter.GetLocations(new List<string?>());
            xml = (ContentResult)result;
            if (string.IsNullOrEmpty(xml.Content))
            {
                Assert.True(false);
                return;
            }
            using (var stream = new MemoryStream(Encoding.Unicode.GetBytes(xml.Content)))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(List<Location>));
                var obj = (List<Location>?)serializer.Deserialize(stream);
                Assert.True(obj?.Count == 0);
            }
            #endregion
            #region normal test
            List<string?> locations = new()
            {
                "asia", "test"
            };
            result = adapter.GetLocations(locations);
            xml = (ContentResult)result;
            if (string.IsNullOrEmpty(xml.Content))
            {
                Assert.True(false);
                return;
            }
            using (var stream = new MemoryStream(Encoding.Unicode.GetBytes(xml.Content)))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(List<Location>));
                var obj = (List<Location>?)serializer.Deserialize(stream);
                Assert.True(obj?.Count == 2);
                Assert.True(obj?.First().location == "asia");
                Assert.True(obj?.First().Id == 0);
            }
            #endregion
            #region empty name test
            locations = new() { "" };
            result = adapter.GetLocations(locations);
            xml = (ContentResult)result;
            if (string.IsNullOrEmpty(xml.Content))
            {
                Assert.True(false);
                return;
            }
            using (var stream = new MemoryStream(Encoding.Unicode.GetBytes(xml.Content)))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(List<Location>));
                var obj = (List<Location>?)serializer.Deserialize(stream);
                Assert.True(obj?.Count == 0);
            }
            #endregion
        }

        [Fact]
        public void GetPeer()
        {
            IAdaptee adaptee = new XMLAdaptee();
            var adapter = new Adapter(adaptee);
            #region null test
            var result = adapter.GetPeer(null);
            var xml = (ContentResult)result;
            if (string.IsNullOrEmpty(xml.Content))
            {
                Assert.True(false);
                return;
            }
            using (var stream = new MemoryStream(Encoding.Unicode.GetBytes(xml.Content)))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(Model.Peer));
                var obj = (Model.Peer?)serializer.Deserialize(stream);
                Assert.True(obj == null);
            }
            #endregion
            #region normal test
            Peer peer = new Peer()
            {
                _ID = "1",
                hostAddress = "tcp://194.156.98.6:7676",
                info = "Some info here"
            };
            result = adapter.GetPeer(peer);
            xml = (ContentResult)result;
            if (string.IsNullOrEmpty(xml.Content))
            {
                Assert.True(false);
                return;
            }
            using (var stream = new MemoryStream(Encoding.Unicode.GetBytes(xml.Content)))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(Model.Peer));
                var obj = (Model.Peer?)serializer.Deserialize(stream);
                Assert.True(obj?.Id == 1);
                Assert.True(obj?.hostAddress == "tcp://194.156.98.6:7676");
                Assert.True(obj?.info == "Some info here");
            }
            #endregion
            #region empty name test
            peer = new Peer()
            {
                Id = 1,
                hostAddress = "",
                info = "Some info here"
            };
            result = adapter.GetPeer(peer);
            xml = (ContentResult)result;
            if (string.IsNullOrEmpty(xml.Content))
            {
                Assert.True(false);
                return;
            }
            using (var stream = new MemoryStream(Encoding.Unicode.GetBytes(xml.Content)))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(Model.Peer));
                var obj = (Model.Peer?)serializer.Deserialize(stream);
                Assert.True(obj == null);
            }
            #endregion
        }

        [Fact]
        public void Exception()
        {
            IAdaptee adaptee = new XMLAdaptee();
            var adapter = new Adapter(adaptee);
            #region null test
            var result = adapter.Exception(null);
            var xml = (ContentResult)result;
            if (string.IsNullOrEmpty(xml.Content))
            {
                Assert.True(false);
                return;
            }
            using (var stream = new MemoryStream(Encoding.Unicode.GetBytes(xml.Content)))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(ExceptionModel));
                var obj = (ExceptionModel?)serializer.Deserialize(stream);
                Assert.True(obj == null);
            }
            #endregion
            #region normal test
            ExceptionModel exception = new()
            {
                Domain = 400,
                Info = "test",
                Location = "test",
            };
            result = adapter.Exception(exception);
            xml = (ContentResult)result;
            if (string.IsNullOrEmpty(xml.Content))
            {
                Assert.True(false);
                return;
            }
            using (var stream = new MemoryStream(Encoding.Unicode.GetBytes(xml.Content)))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(ExceptionModel));
                var obj = (ExceptionModel?)serializer.Deserialize(stream);
                Assert.True(obj?.Domain == 400);
                Assert.True(obj?.Info == "test");
            }
            #endregion
        }

    }
}

