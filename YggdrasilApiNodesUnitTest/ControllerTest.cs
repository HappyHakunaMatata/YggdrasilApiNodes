using System;
using System.Diagnostics.Metrics;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using Newtonsoft.Json;
using Xunit.Abstractions;
using Xunit.Sdk;
using YggdrasilApiNodes;
using YggdrasilApiNodes.Controllers;
using YggdrasilApiNodes.Models;

namespace YggdrasilApiNodesUnitTest
{
	public class ControllerTest
	{

        private readonly ILogger<PeerController> _loggerMock;
        private readonly ITestOutputHelper _testOutputHelper;

        public ControllerTest(ITestOutputHelper testOutputHelper)
        {
            _loggerMock = Mock.Of<ILogger<PeerController>>();
            _testOutputHelper = testOutputHelper;
        }

        #region GetPeers Test
        [Fact]
        public void GetPeersJsonNormal()
        {
            var data = new List<Peer>
            {
                new Peer { hostAddress = "tcp://194.156.98.6:7676", },
            }.AsQueryable();

            var mockSet = new Mock<DbSet<Peer>>();
            mockSet.As<IQueryable<Peer>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<Peer>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Peer>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Peer>>().Setup(m => m.GetEnumerator()).Returns(() => data.GetEnumerator());
            var mockContext = new Mock<AppDbContext>();
            mockContext.Setup(m => m.peer).Returns(mockSet.Object);
            var controller = new PeerController(_loggerMock, mockContext.Object);
            var result = (JsonResult)controller.GetPeers(ApiFormat.json);
            Assert.NotNull(result.Value);
            Assert.True(result.ContentType == "application/json");
            Assert.True(result.StatusCode == 200);
        }

        [Fact]
        public void GetPeersJsonNull()
        {
            var data = new List<Peer>
            {
            }.AsQueryable();

            var mockSet = new Mock<DbSet<Peer>>();
            mockSet.As<IQueryable<Peer>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<Peer>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Peer>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Peer>>().Setup(m => m.GetEnumerator()).Returns(() => data.GetEnumerator());
            var mockContext = new Mock<AppDbContext>();
            mockContext.Setup(m => m.peer).Returns(mockSet.Object);
            var controller = new PeerController(_loggerMock, mockContext.Object);
            var result = (JsonResult)controller.GetPeers(ApiFormat.json);
            Assert.NotNull(result);
            Assert.True(result.StatusCode == 200);
            Assert.True(result.ContentType == "application/json");
        }

        [Fact]
        public void GetPeersXmlNormal()
        {
            var data = new List<Peer>
            {
                new Peer { hostAddress = "tcp://194.156.98.6:7676", },
            }.AsQueryable();

            var mockSet = new Mock<DbSet<Peer>>();
            mockSet.As<IQueryable<Peer>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<Peer>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Peer>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Peer>>().Setup(m => m.GetEnumerator()).Returns(() => data.GetEnumerator());
            var mockContext = new Mock<AppDbContext>();
            mockContext.Setup(m => m.peer).Returns(mockSet.Object);
            var controller = new PeerController(_loggerMock, mockContext.Object);
            var result = (ContentResult)controller.GetPeers(ApiFormat.xml);
            Assert.NotNull(result);
            Assert.True(result.ContentType == "application/xml");
            Assert.True(result.StatusCode == 200);
        }

        [Fact]
        public void GetPeersXmlNull()
        {
            var data = new List<Peer>
            {
            }.AsQueryable();

            var mockSet = new Mock<DbSet<Peer>>();
            mockSet.As<IQueryable<Peer>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<Peer>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Peer>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Peer>>().Setup(m => m.GetEnumerator()).Returns(() => data.GetEnumerator());
            var mockContext = new Mock<AppDbContext>();
            mockContext.Setup(m => m.peer).Returns(mockSet.Object);
            var controller = new PeerController(_loggerMock, mockContext.Object);
            var result = (ContentResult)controller.GetPeers(ApiFormat.xml);
            Assert.NotNull(result);
            Assert.True(result.StatusCode == 200);
            Assert.True(result.ContentType == "application/xml");
        }
        #endregion

        #region GetPeer Test
        [Fact]
        public void GetPeerJsonNormal()
        {
            var data = new List<Peer>
            {
                new Peer {Id =1, hostAddress = "tcp://194.156.98.6:7676", },
            }.AsQueryable();

            var mockSet = new Mock<DbSet<Peer>>();
            mockSet.As<IQueryable<Peer>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<Peer>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Peer>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Peer>>().Setup(m => m.GetEnumerator()).Returns(() => data.GetEnumerator());
            var mockContext = new Mock<AppDbContext>();
            mockContext.Setup(m => m.peer).Returns(mockSet.Object);
            var controller = new PeerController(_loggerMock, mockContext.Object);
            var result = (JsonResult)controller.GetPeerByID(1, ApiFormat.json);
            Assert.NotNull(result.Value);
            Assert.True(result.ContentType == "application/json");
            Assert.True(result.StatusCode == 200);
        }

        [Fact]
        public void GetPeerJsonNull()
        {
            var data = new List<Peer>
            {
            }.AsQueryable();

            var mockSet = new Mock<DbSet<Peer>>();
            mockSet.As<IQueryable<Peer>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<Peer>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Peer>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Peer>>().Setup(m => m.GetEnumerator()).Returns(() => data.GetEnumerator());
            var mockContext = new Mock<AppDbContext>();
            mockContext.Setup(m => m.peer).Returns(mockSet.Object);
            var controller = new PeerController(_loggerMock, mockContext.Object);
            var result = (JsonResult)controller.GetPeerByID(1, ApiFormat.json);
            Assert.NotNull(result);
            Assert.True(result.StatusCode == 200);
            Assert.True(result.ContentType == "application/json");
        }

        [Fact]
        public void GetPeerXmlNormal()
        {
            var data = new List<Peer>
            {
                new Peer { Id = 1, hostAddress = "tcp://194.156.98.6:7676", },
            }.AsQueryable();

            var mockSet = new Mock<DbSet<Peer>>();
            mockSet.As<IQueryable<Peer>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<Peer>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Peer>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Peer>>().Setup(m => m.GetEnumerator()).Returns(() => data.GetEnumerator());
            var mockContext = new Mock<AppDbContext>();
            mockContext.Setup(m => m.peer).Returns(mockSet.Object);
            var controller = new PeerController(_loggerMock, mockContext.Object);
            var result = (ContentResult)controller.GetPeerByID(1, ApiFormat.xml);
            Assert.NotNull(result);
            Assert.True(result.ContentType == "application/xml");
            Assert.True(result.StatusCode == 200);
        }

        [Fact]
        public void GetPeerXmlNull()
        {
            var data = new List<Peer>
            {
            }.AsQueryable();

            var mockSet = new Mock<DbSet<Peer>>();
            mockSet.As<IQueryable<Peer>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<Peer>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Peer>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Peer>>().Setup(m => m.GetEnumerator()).Returns(() => data.GetEnumerator());
            var mockContext = new Mock<AppDbContext>();
            mockContext.Setup(m => m.peer).Returns(mockSet.Object);
            var controller = new PeerController(_loggerMock, mockContext.Object);
            var result = (ContentResult)controller.GetPeerByID(1, ApiFormat.xml);
            Assert.NotNull(result);
            Assert.True(result.StatusCode == 200);
            Assert.True(result.ContentType == "application/xml");
        }
        #endregion

        #region GetPeerByDate Test
        [Fact]
        public void GetPeerByDateJsonNormal()
        {
            var data = new List<Peer>
            {
                new Peer {Id =1, hostAddress = "tcp://194.156.98.6:7676", LastOnline = new DateTime(2023, 12, 01) },
            }.AsQueryable();

            var mockSet = new Mock<DbSet<Peer>>();
            mockSet.As<IQueryable<Peer>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<Peer>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Peer>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Peer>>().Setup(m => m.GetEnumerator()).Returns(() => data.GetEnumerator());
            var mockContext = new Mock<AppDbContext>();
            mockContext.Setup(m => m.peer).Returns(mockSet.Object);
            var controller = new PeerController(_loggerMock, mockContext.Object);
            var result = (JsonResult)controller.GetPeerByLastOnline(new DateTime(2023, 11, 24), ApiFormat.json);
            Assert.NotNull(result.Value);
            Assert.True(result.ContentType == "application/json");
            Assert.True(result.StatusCode == 200);
        }

        [Fact]
        public void GetPeerByDateJsonNull()
        {
            var data = new List<Peer>
            {
            }.AsQueryable();

            var mockSet = new Mock<DbSet<Peer>>();
            mockSet.As<IQueryable<Peer>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<Peer>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Peer>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Peer>>().Setup(m => m.GetEnumerator()).Returns(() => data.GetEnumerator());
            var mockContext = new Mock<AppDbContext>();
            mockContext.Setup(m => m.peer).Returns(mockSet.Object);
            var controller = new PeerController(_loggerMock, mockContext.Object);
            var result = (JsonResult)controller.GetPeerByLastOnline(new DateTime(2023, 11, 24), ApiFormat.json);
            Assert.NotNull(result);
            Assert.True(result.StatusCode == 200);
            Assert.True(result.ContentType == "application/json");
        }

        [Fact]
        public void GetPeerByDateXmlNormal()
        {
            var data = new List<Peer>
            {
                new Peer { Id = 1, hostAddress = "tcp://194.156.98.6:7676", LastOnline = new DateTime(2023, 12, 01) },
            }.AsQueryable();

            var mockSet = new Mock<DbSet<Peer>>();
            mockSet.As<IQueryable<Peer>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<Peer>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Peer>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Peer>>().Setup(m => m.GetEnumerator()).Returns(() => data.GetEnumerator());
            var mockContext = new Mock<AppDbContext>();
            mockContext.Setup(m => m.peer).Returns(mockSet.Object);
            var controller = new PeerController(_loggerMock, mockContext.Object);
            var result = (ContentResult)controller.GetPeerByLastOnline(new DateTime(2023, 11, 24), ApiFormat.xml);
            Assert.NotNull(result);
            Assert.True(result.ContentType == "application/xml");
            Assert.True(result.StatusCode == 200);
        }

        [Fact]
        public void GetPeerByDateXmlNull()
        {
            var data = new List<Peer>
            {
            }.AsQueryable();

            var mockSet = new Mock<DbSet<Peer>>();
            mockSet.As<IQueryable<Peer>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<Peer>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Peer>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Peer>>().Setup(m => m.GetEnumerator()).Returns(() => data.GetEnumerator());
            var mockContext = new Mock<AppDbContext>();
            mockContext.Setup(m => m.peer).Returns(mockSet.Object);
            var controller = new PeerController(_loggerMock, mockContext.Object);
            var result = (ContentResult)controller.GetPeerByLastOnline(new DateTime(2023, 11, 24), ApiFormat.xml);
            Assert.NotNull(result);
            Assert.True(result.StatusCode == 200);
            Assert.True(result.ContentType == "application/xml");
        }
        #endregion

        #region GetPeerByStatus Test
        [Fact]
        public void GetPeerByStatusJsonNormal()
        {
            var data = new List<Peer>
            {
                new Peer {Id =1, hostAddress = "tcp://194.156.98.6:7676", LastOnline = new DateTime(2023, 12, 01) },
            }.AsQueryable();

            var mockSet = new Mock<DbSet<Peer>>();
            mockSet.As<IQueryable<Peer>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<Peer>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Peer>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Peer>>().Setup(m => m.GetEnumerator()).Returns(() => data.GetEnumerator());
            var mockContext = new Mock<AppDbContext>();
            mockContext.Setup(m => m.peer).Returns(mockSet.Object);
            var controller = new PeerController(_loggerMock, mockContext.Object);
            var result = (JsonResult)controller.GetPeerByStatus(Status.unidentified, ApiFormat.json);
            Assert.NotNull(result.Value);
            Assert.True(result.ContentType == "application/json");
            Assert.True(result.StatusCode == 200);
        }

        [Fact]
        public void GetPeerByStatusJsonNull()
        {
            var data = new List<Peer>
            {
            }.AsQueryable();

            var mockSet = new Mock<DbSet<Peer>>();
            mockSet.As<IQueryable<Peer>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<Peer>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Peer>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Peer>>().Setup(m => m.GetEnumerator()).Returns(() => data.GetEnumerator());
            var mockContext = new Mock<AppDbContext>();
            mockContext.Setup(m => m.peer).Returns(mockSet.Object);
            var controller = new PeerController(_loggerMock, mockContext.Object);
            var result = (JsonResult)controller.GetPeerByStatus(Status.unidentified, ApiFormat.json);
            Assert.NotNull(result);
            Assert.True(result.StatusCode == 200);
            Assert.True(result.ContentType == "application/json");
        }

        [Fact]
        public void GetPeerByStatusXmlNormal()
        {
            var data = new List<Peer>
            {
                new Peer { Id = 1, hostAddress = "tcp://194.156.98.6:7676", LastOnline = new DateTime(2023, 12, 01) },
            }.AsQueryable();

            var mockSet = new Mock<DbSet<Peer>>();
            mockSet.As<IQueryable<Peer>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<Peer>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Peer>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Peer>>().Setup(m => m.GetEnumerator()).Returns(() => data.GetEnumerator());
            var mockContext = new Mock<AppDbContext>();
            mockContext.Setup(m => m.peer).Returns(mockSet.Object);
            var controller = new PeerController(_loggerMock, mockContext.Object);
            var result = (ContentResult)controller.GetPeerByStatus(Status.unidentified, ApiFormat.xml);
            Assert.NotNull(result);
            Assert.True(result.ContentType == "application/xml");
            Assert.True(result.StatusCode == 200);
        }

        [Fact]
        public void GetPeerByStatusXmlNull()
        {
            var data = new List<Peer>
            {
            }.AsQueryable();

            var mockSet = new Mock<DbSet<Peer>>();
            mockSet.As<IQueryable<Peer>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<Peer>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Peer>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Peer>>().Setup(m => m.GetEnumerator()).Returns(() => data.GetEnumerator());
            var mockContext = new Mock<AppDbContext>();
            mockContext.Setup(m => m.peer).Returns(mockSet.Object);
            var controller = new PeerController(_loggerMock, mockContext.Object);
            var result = (ContentResult)controller.GetPeerByStatus(Status.unidentified, ApiFormat.xml);
            Assert.NotNull(result);
            Assert.True(result.StatusCode == 200);
            Assert.True(result.ContentType == "application/xml");
        }
        #endregion

        #region GetPeerByStatus Test
        [Fact]
        public void GetPeerByCountryJsonNormal()
        {
            var data = new List<Peer>
            {
                new Peer {Id = 1, hostAddress = "tcp://194.156.98.6:7676", info = "information", location = new Location(){ country = new Country(){Name = "Test", Id = 1,}, Id = 1, location = "" },
                ipAddresses = new List<IPAddressEntity>(){new IPAddressEntity()}},
            }.AsQueryable();

            var mockSet = new Mock<DbSet<Peer>>();
            mockSet.As<IQueryable<Peer>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<Peer>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Peer>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Peer>>().Setup(m => m.GetEnumerator()).Returns(() => data.GetEnumerator());
            var mockContext = new Mock<AppDbContext>();
            mockContext.Setup(m => m.peer).Returns(mockSet.Object);
            var controller = new PeerController(_loggerMock, mockContext.Object);
            var result = (JsonResult)controller.GetNodesByCountry("Test", ApiFormat.json);
            Assert.NotNull(result.Value);
            Assert.True(result.ContentType == "application/json");
            Assert.True(result.StatusCode == 200);
        }

        [Fact]
        public void GetPeerByCountryJsonNull()
        {
            var data = new List<Peer>
            {
            }.AsQueryable();

            var mockSet = new Mock<DbSet<Peer>>();
            mockSet.As<IQueryable<Peer>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<Peer>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Peer>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Peer>>().Setup(m => m.GetEnumerator()).Returns(() => data.GetEnumerator());
            var mockContext = new Mock<AppDbContext>();
            mockContext.Setup(m => m.peer).Returns(mockSet.Object);
            var controller = new PeerController(_loggerMock, mockContext.Object);
            var result = (JsonResult)controller.GetNodesByCountry("Test", ApiFormat.json);
            Assert.NotNull(result);
            Assert.True(result.StatusCode == 200);
            Assert.True(result.ContentType == "application/json");
        }

        [Fact]
        public void GetPeerByCountryXmlNormal()
        {
            var data = new List<Peer>
            {
                new Peer { Id = 1, hostAddress = "tcp://194.156.98.6:7676", location = new Location(){ country = new Country(){Name = "Test"}} },
            }.AsQueryable();

            var mockSet = new Mock<DbSet<Peer>>();
            mockSet.As<IQueryable<Peer>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<Peer>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Peer>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Peer>>().Setup(m => m.GetEnumerator()).Returns(() => data.GetEnumerator());
            var mockContext = new Mock<AppDbContext>();
            mockContext.Setup(m => m.peer).Returns(mockSet.Object);
            var controller = new PeerController(_loggerMock, mockContext.Object);
            var result = (ContentResult)controller.GetNodesByCountry("Test", ApiFormat.xml);
            Assert.NotNull(result);
            Assert.True(result.ContentType == "application/xml");
            Assert.True(result.StatusCode == 200);
        }

        [Fact]
        public void GetPeerByCountryXmlNull()
        {
            var data = new List<Peer>
            {
            }.AsQueryable();

            var mockSet = new Mock<DbSet<Peer>>();
            mockSet.As<IQueryable<Peer>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<Peer>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Peer>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Peer>>().Setup(m => m.GetEnumerator()).Returns(() => data.GetEnumerator());
            var mockContext = new Mock<AppDbContext>();
            mockContext.Setup(m => m.peer).Returns(mockSet.Object);
            var controller = new PeerController(_loggerMock, mockContext.Object);
            var result = (ContentResult)controller.GetNodesByCountry("Test", ApiFormat.xml);
            Assert.NotNull(result);
            Assert.True(result.StatusCode == 200);
            Assert.True(result.ContentType == "application/xml");
        }
        #endregion

        #region GetCountries Test
        [Fact]
        public void GetCountriesJsonNormal()
        {
            var country = new Country()
            {
                Id = 1,
                Name = "Test",
                region = "Region",
            };
            var data = new List<Country>() { country }.AsQueryable();
            var mockSet = new Mock<DbSet<Country>>();
            mockSet.As<IQueryable<Country>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<Country>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Country>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Country>>().Setup(m => m.GetEnumerator()).Returns(() => data.GetEnumerator());
            var mockContext = new Mock<AppDbContext>();
            mockContext.Setup(m => m.countries).Returns(mockSet.Object);
            var controller = new PeerController(_loggerMock, mockContext.Object);
            var result = (JsonResult)controller.GetCountries(ApiFormat.json);
            Assert.NotNull(result.Value);
            Assert.True(result.ContentType == "application/json");
            Assert.True(result.StatusCode == 200);
        }

        [Fact]
        public void GetCountriesJsonNull()
        {
            var data = new List<Country>() {}.AsQueryable();
            var mockSet = new Mock<DbSet<Country>>();
            mockSet.As<IQueryable<Country>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<Country>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Country>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Country>>().Setup(m => m.GetEnumerator()).Returns(() => data.GetEnumerator());
            var mockContext = new Mock<AppDbContext>();
            mockContext.Setup(m => m.countries).Returns(mockSet.Object);
            var controller = new PeerController(_loggerMock, mockContext.Object);
            var result = (JsonResult)controller.GetCountries(ApiFormat.json);
            Assert.NotNull(result);
            Assert.True(result.StatusCode == 200);
            Assert.True(result.ContentType == "application/json");
        }

        [Fact]
        public void GetCountriesXmlNormal()
        {
            var country = new Country()
            {
                Id = 1,
                Name = "Test",
                region = "Region",
            };
            var data = new List<Country>() { country }.AsQueryable();
            var mockSet = new Mock<DbSet<Country>>();
            mockSet.As<IQueryable<Country>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<Country>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Country>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Country>>().Setup(m => m.GetEnumerator()).Returns(() => data.GetEnumerator());
            var mockContext = new Mock<AppDbContext>();
            mockContext.Setup(m => m.countries).Returns(mockSet.Object);
            var controller = new PeerController(_loggerMock, mockContext.Object);
            var result = (ContentResult)controller.GetCountries(ApiFormat.xml);
            Assert.NotNull(result);
            Assert.True(result.ContentType == "application/xml");
            Assert.True(result.StatusCode == 200);
        }

        [Fact]
        public void GetCountriesXmlNull()
        {
            var data = new List<Country>() { }.AsQueryable();
            var mockSet = new Mock<DbSet<Country>>();
            mockSet.As<IQueryable<Country>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<Country>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Country>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Country>>().Setup(m => m.GetEnumerator()).Returns(() => data.GetEnumerator());
            var mockContext = new Mock<AppDbContext>();
            mockContext.Setup(m => m.countries).Returns(mockSet.Object);
            var controller = new PeerController(_loggerMock, mockContext.Object);
            var result = (ContentResult)controller.GetCountries(ApiFormat.xml);
            Assert.NotNull(result);
            Assert.True(result.StatusCode == 200);
            Assert.True(result.ContentType == "application/xml");
        }
        #endregion

        #region GetLocations Test
        [Fact]
        public void GetLocationsJsonNormal()
        {
            var location = new Location()
            {
                Id = 1,
                location = "Test",
            };
            var data = new List<Location>() { location }.AsQueryable();
            var mockSet = new Mock<DbSet<Location>>();
            mockSet.As<IQueryable<Location>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<Location>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Location>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Location>>().Setup(m => m.GetEnumerator()).Returns(() => data.GetEnumerator());
            var mockContext = new Mock<AppDbContext>();
            mockContext.Setup(m => m.locations).Returns(mockSet.Object);
            var controller = new PeerController(_loggerMock, mockContext.Object);
            var result = (JsonResult)controller.GetLocations(ApiFormat.json);
            Assert.NotNull(result.Value);
            Assert.True(result.ContentType == "application/json");
            Assert.True(result.StatusCode == 200);
        }

        [Fact]
        public void GetLocationsJsonNull()
        {
            var data = new List<Location>() { }.AsQueryable();
            var mockSet = new Mock<DbSet<Location>>();
            mockSet.As<IQueryable<Location>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<Location>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Location>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Location>>().Setup(m => m.GetEnumerator()).Returns(() => data.GetEnumerator());
            var mockContext = new Mock<AppDbContext>();
            mockContext.Setup(m => m.locations).Returns(mockSet.Object);
            var controller = new PeerController(_loggerMock, mockContext.Object);
            var result = (JsonResult)controller.GetLocations(ApiFormat.json);
            Assert.NotNull(result);
            Assert.True(result.StatusCode == 200);
            Assert.True(result.ContentType == "application/json");
        }

        [Fact]
        public void GetLocationsXmlNormal()
        {
            var location = new Location()
            {
                Id = 1,
                location = "Test",
            };
            var data = new List<Location>() { location }.AsQueryable();
            var mockSet = new Mock<DbSet<Location>>();
            mockSet.As<IQueryable<Location>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<Location>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Location>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Location>>().Setup(m => m.GetEnumerator()).Returns(() => data.GetEnumerator());
            var mockContext = new Mock<AppDbContext>();
            mockContext.Setup(m => m.locations).Returns(mockSet.Object);
            var controller = new PeerController(_loggerMock, mockContext.Object);
            var result = (ContentResult)controller.GetLocations(ApiFormat.xml);
            Assert.NotNull(result);
            Assert.True(result.ContentType == "application/xml");
            Assert.True(result.StatusCode == 200);
        }

        [Fact]
        public void GetLocationsXmlNull()
        {
            var data = new List<Location>() { }.AsQueryable();
            var mockSet = new Mock<DbSet<Location>>();
            mockSet.As<IQueryable<Location>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<Location>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Location>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Location>>().Setup(m => m.GetEnumerator()).Returns(() => data.GetEnumerator());
            var mockContext = new Mock<AppDbContext>();
            mockContext.Setup(m => m.locations).Returns(mockSet.Object);
            var controller = new PeerController(_loggerMock, mockContext.Object);
            var result = (ContentResult)controller.GetLocations(ApiFormat.xml);
            Assert.NotNull(result);
            Assert.True(result.StatusCode == 200);
            Assert.True(result.ContentType == "application/xml");
        }
        #endregion


        #region GetPeerByLocation Test
        [Fact]
        public void GetPeerByLocationJsonNormal()
        {
            var data = new List<Peer>
            {
                new Peer {Id =1, hostAddress = "tcp://194.156.98.6:7676", LastOnline = new DateTime(2023, 12, 01), location = new Location(){ location = "Test" } },
            }.AsQueryable();

            var mockSet = new Mock<DbSet<Peer>>();
            mockSet.As<IQueryable<Peer>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<Peer>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Peer>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Peer>>().Setup(m => m.GetEnumerator()).Returns(() => data.GetEnumerator());
            var mockContext = new Mock<AppDbContext>();
            mockContext.Setup(m => m.peer).Returns(mockSet.Object);
            var controller = new PeerController(_loggerMock, mockContext.Object);
            var result = (JsonResult)controller.GetPeerByLocation("Test", ApiFormat.json);
            Assert.NotNull(result.Value);
            Assert.True(result.ContentType == "application/json");
            Assert.True(result.StatusCode == 200);
        }

        [Fact]
        public void GetPeerByLocationJsonNull()
        {
            var data = new List<Peer>
            {
            }.AsQueryable();

            var mockSet = new Mock<DbSet<Peer>>();
            mockSet.As<IQueryable<Peer>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<Peer>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Peer>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Peer>>().Setup(m => m.GetEnumerator()).Returns(() => data.GetEnumerator());
            var mockContext = new Mock<AppDbContext>();
            mockContext.Setup(m => m.peer).Returns(mockSet.Object);
            var controller = new PeerController(_loggerMock, mockContext.Object);
            var result = (JsonResult)controller.GetPeerByLocation("Test", ApiFormat.json);
            Assert.NotNull(result);
            Assert.True(result.StatusCode == 200);
            Assert.True(result.ContentType == "application/json");
        }

        [Fact]
        public void GetPeerByLocationXmlNormal()
        {
            var data = new List<Peer>
            {
                new Peer {Id =1, hostAddress = "tcp://194.156.98.6:7676", LastOnline = new DateTime(2023, 12, 01), location = new Location(){ location = "Test" } },
            }.AsQueryable();

            var mockSet = new Mock<DbSet<Peer>>();
            mockSet.As<IQueryable<Peer>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<Peer>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Peer>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Peer>>().Setup(m => m.GetEnumerator()).Returns(() => data.GetEnumerator());
            var mockContext = new Mock<AppDbContext>();
            mockContext.Setup(m => m.peer).Returns(mockSet.Object);
            var controller = new PeerController(_loggerMock, mockContext.Object);
            var result = (ContentResult)controller.GetPeerByLocation("Test", ApiFormat.xml);
            Assert.NotNull(result);
            Assert.True(result.ContentType == "application/xml");
            Assert.True(result.StatusCode == 200);
        }

        [Fact]
        public void GetPeerByLocationXmlNull()
        {
            var data = new List<Peer>
            {
            }.AsQueryable();

            var mockSet = new Mock<DbSet<Peer>>();
            mockSet.As<IQueryable<Peer>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<Peer>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Peer>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Peer>>().Setup(m => m.GetEnumerator()).Returns(() => data.GetEnumerator());
            var mockContext = new Mock<AppDbContext>();
            mockContext.Setup(m => m.peer).Returns(mockSet.Object);
            var controller = new PeerController(_loggerMock, mockContext.Object);
            var result = (ContentResult)controller.GetPeerByLocation("Test", ApiFormat.xml);
            Assert.NotNull(result);
            Assert.True(result.StatusCode == 200);
            Assert.True(result.ContentType == "application/xml");
        }
        #endregion

        #region GetIpAddresses Test
        [Fact]
        public void GetIpAddressesJsonNormal()
        {
            var data = new List<Peer>
            {
                new Peer {Id =1, hostAddress = "tcp://194.156.98.6:7676", ipAddresses = new List<IPAddressEntity>(){ new IPAddressEntity() {Id = 1, ipAddress = IPAddress.Parse("194.156.98.6") } }, LastOnline = new DateTime(2023, 12, 01), location = new Location(){ location = "Test" } },
            }.AsQueryable();

            var mockSet = new Mock<DbSet<Peer>>();
            mockSet.As<IQueryable<Peer>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<Peer>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Peer>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Peer>>().Setup(m => m.GetEnumerator()).Returns(() => data.GetEnumerator());
            var mockContext = new Mock<AppDbContext>();
            mockContext.Setup(m => m.peer).Returns(mockSet.Object);
            var controller = new PeerController(_loggerMock, mockContext.Object);
            var result = (JsonResult)controller.GetIpAddresses("tcp://194.156.98.6:7676", ApiFormat.json);
            Assert.NotNull(result.Value);
            Assert.True(result.ContentType == "application/json");
            Assert.True(result.StatusCode == 200);
        }

        [Fact]
        public void GetIpAddressesJsonNull()
        {
            var data = new List<Peer>
            {
            }.AsQueryable();

            var mockSet = new Mock<DbSet<Peer>>();
            mockSet.As<IQueryable<Peer>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<Peer>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Peer>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Peer>>().Setup(m => m.GetEnumerator()).Returns(() => data.GetEnumerator());
            var mockContext = new Mock<AppDbContext>();
            mockContext.Setup(m => m.peer).Returns(mockSet.Object);
            var controller = new PeerController(_loggerMock, mockContext.Object);
            var result = (JsonResult)controller.GetIpAddresses("tcp://194.156.98.6:7676", ApiFormat.json);
            Assert.NotNull(result);
            Assert.True(result.StatusCode == 200);
            Assert.True(result.ContentType == "application/json");
        }

        [Fact]
        public void GetIpAddressesXmlNormal()
        {
            var data = new List<Peer>
            {
                new Peer {Id =1, hostAddress = "tcp://194.156.98.6:7676", ipAddresses = new List<IPAddressEntity>(){ new IPAddressEntity() {Id = 1, ipAddress = IPAddress.Parse("194.156.98.6") } }, LastOnline = new DateTime(2023, 12, 01), location = new Location(){ location = "Test" } },
            }.AsQueryable();

            var mockSet = new Mock<DbSet<Peer>>();
            mockSet.As<IQueryable<Peer>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<Peer>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Peer>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Peer>>().Setup(m => m.GetEnumerator()).Returns(() => data.GetEnumerator());
            var mockContext = new Mock<AppDbContext>();
            mockContext.Setup(m => m.peer).Returns(mockSet.Object);
            var controller = new PeerController(_loggerMock, mockContext.Object);
            var result = (ContentResult)controller.GetIpAddresses("tcp://194.156.98.6:7676", ApiFormat.xml);
            Assert.NotNull(result);
            Assert.True(result.ContentType == "application/xml");
            Assert.True(result.StatusCode == 200);
        }

        [Fact]
        public void GetIpAddressesXmlNull()
        {
            var data = new List<Peer>
            {
            }.AsQueryable();

            var mockSet = new Mock<DbSet<Peer>>();
            mockSet.As<IQueryable<Peer>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<Peer>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Peer>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Peer>>().Setup(m => m.GetEnumerator()).Returns(() => data.GetEnumerator());
            var mockContext = new Mock<AppDbContext>();
            mockContext.Setup(m => m.peer).Returns(mockSet.Object);
            var controller = new PeerController(_loggerMock, mockContext.Object);
            var result = (ContentResult)controller.GetIpAddresses("tcp://194.156.98.6:7676", ApiFormat.xml);
            Assert.NotNull(result);
            Assert.True(result.StatusCode == 200);
            Assert.True(result.ContentType == "application/xml");
        }
        #endregion
    }
}

