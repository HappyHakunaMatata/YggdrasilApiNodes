using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Xunit.Abstractions;
using Xunit.Sdk;
using YggdrasilApiNodes.Interfaces;
using YggdrasilApiNodes.Models;
using YggdrasilApiNodes.Services;
using YggdrasilApiNodes.Services.Adapter;

namespace YggdrasilApiNodesUnitTest;

public class AdapterJsonTest
{


    private readonly ITestOutputHelper _testOutputHelper;


    public AdapterJsonTest(ITestOutputHelper testOutputHelper)
    {
        _testOutputHelper = testOutputHelper;
    }


    [Fact]
    public void GetCountries()
    {
        IAdaptee adaptee = new JsonAdaptee();
        var adapter = new Adapter(adaptee);
        #region null test
        var result = adapter.GetCountries(null);
        var json = (JsonResult)result;
        var txt = JsonConvert.SerializeObject(json.Value);
        var obj = System.Text.Json.JsonSerializer.Deserialize<List<Country>>(txt);
        Assert.True(obj?.Count == 0);
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
        json = (JsonResult)result;
        txt = JsonConvert.SerializeObject(json.Value);
        obj = System.Text.Json.JsonSerializer.Deserialize<List<Country>>(txt);
        Assert.True(obj?.Count == 1);
        Assert.True(obj.First().Id == 1);
        Assert.True(obj.First().Name == "Test");
        Assert.True(string.IsNullOrEmpty(obj.First().region));
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
        json = (JsonResult)result;
        txt = JsonConvert.SerializeObject(json.Value);
        obj = System.Text.Json.JsonSerializer.Deserialize<List<Country>>(txt);
        Assert.True(obj?.Count == 0);
        #endregion
        #region empty
        data = new List<Country>();
        result = adapter.GetCountries(data);
        json = (JsonResult)result;
        txt = JsonConvert.SerializeObject(json.Value);
        obj = System.Text.Json.JsonSerializer.Deserialize<List<Country>>(txt);
        Assert.True(obj?.Count == 0);
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
        json = (JsonResult)result;
        txt = JsonConvert.SerializeObject(json.Value);
        obj = System.Text.Json.JsonSerializer.Deserialize<List<Country>>(txt);
        Assert.True(obj?.Count == 2);
        Assert.True(obj?[0].Name == "Test");
        Assert.True(obj?[1].Name == "OK");
        #endregion
    }


    [Fact]
    public void GetIpAddresses()
    {
        IAdaptee adaptee = new JsonAdaptee();
        var adapter = new Adapter(adaptee);
        #region null test
        var result = adapter.GetIpAddresses(null);
        var json = (JsonResult)result;
        var txt = JsonConvert.SerializeObject(json.Value);
        var obj = System.Text.Json.JsonSerializer.Deserialize<List<Model.Peer>>(txt);
        Assert.True(obj?.Count == 0);
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
        json = (JsonResult)result;
        txt = JsonConvert.SerializeObject(json.Value);
        obj = System.Text.Json.JsonSerializer.Deserialize<List<Model.Peer>>(txt);
        Assert.True(obj?.Count == 1);
        Assert.True(obj?.First().hostAddress == "tcp://194.156.98.6:7676");
        Assert.True(obj?.First().IPs?.Count == 2);
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
        json = (JsonResult)result;
        txt = JsonConvert.SerializeObject(json.Value);
        obj = System.Text.Json.JsonSerializer.Deserialize<List<Model.Peer>>(txt);
        Assert.True(obj?.Count == 0);
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
        json = (JsonResult)result;
        txt = JsonConvert.SerializeObject(json.Value);
        obj = System.Text.Json.JsonSerializer.Deserialize<List<Model.Peer>>(txt);
        Assert.True(obj?.Count == 0);
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
        json = (JsonResult)result;
        txt = JsonConvert.SerializeObject(json.Value);
        obj = System.Text.Json.JsonSerializer.Deserialize<List<Model.Peer>>(txt);
        Assert.True(obj?.Count == 0);
        #endregion
    }
    

    [Fact]
    public void GetPeers()
    {
        IAdaptee adaptee = new JsonAdaptee();
        var adapter = new Adapter(adaptee);
        #region null test
        var result = adapter.GetPeers(null);
        var json = (JsonResult)result;
        var txt = JsonConvert.SerializeObject(json.Value);
        var obj = System.Text.Json.JsonSerializer.Deserialize<List<Model.Peer>>(txt);
        Assert.True(obj?.Count == 0);
        #endregion
        #region empty test
        var data = new List<Peer>();
        result = adapter.GetPeers(data);
        json = (JsonResult)result;
        txt = JsonConvert.SerializeObject(json.Value);
        obj = System.Text.Json.JsonSerializer.Deserialize<List<Model.Peer>>(txt);
        Assert.True(obj?.Count == 0);
        #endregion
        #region normal test
        data = new List<Peer>();
        Peer peer = new Peer()
        {
            Id = 1,
            hostAddress = "tcp://194.156.98.6:7676",
            info = "Some info here"
        };
        data.Add(peer);
        result = adapter.GetPeers(data);
        json = (JsonResult)result;
        txt = JsonConvert.SerializeObject(json.Value);
        obj = System.Text.Json.JsonSerializer.Deserialize<List<Model.Peer>>(txt);
        Assert.True(obj?.Count == 1);
        Assert.True(obj?.First().info == "Some info here");
        Assert.True(obj?.First().hostAddress == "tcp://194.156.98.6:7676");
        Assert.True(obj?.First().Uptime == "unidentified");
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
        json = (JsonResult)result;
        txt = JsonConvert.SerializeObject(json.Value);
        obj = System.Text.Json.JsonSerializer.Deserialize<List<Model.Peer>>(txt);
        Assert.True(obj?.Count == 0);
        #endregion
    }


    [Fact]
    public void GetLocations()
    {
        IAdaptee adaptee = new JsonAdaptee();
        var adapter = new Adapter(adaptee);
        #region null test
        var result = adapter.GetLocations(null);
        var json = (JsonResult)result;
        var txt = JsonConvert.SerializeObject(json.Value);
        var obj = System.Text.Json.JsonSerializer.Deserialize<List<Location>>(txt);
        Assert.True(obj?.Count == 0);
        #endregion
        #region empty test
        result = adapter.GetLocations(new List<string?>());
        json = (JsonResult)result;
        txt = JsonConvert.SerializeObject(json.Value);
        obj = System.Text.Json.JsonSerializer.Deserialize<List<Location>>(txt);
        Assert.True(obj?.Count == 0);
        #endregion
        #region normal test
        List<string?> locations = new()
        {
           "asia", "test"
        };
        result = adapter.GetLocations(locations);
        json = (JsonResult)result;
        txt = JsonConvert.SerializeObject(json.Value);
        obj = System.Text.Json.JsonSerializer.Deserialize<List<Location>>(txt);
        Assert.True(obj?.Count == 2);
        Assert.True(obj?.First().location == "asia");
        Assert.True(obj?.First().Id == 0);
        #endregion
        #region empty name test
        locations = new()
        {
           ""
        };
        result = adapter.GetLocations(locations);
        json = (JsonResult)result;
        txt = JsonConvert.SerializeObject(json.Value);
        obj = System.Text.Json.JsonSerializer.Deserialize<List<Location>>(txt);
        Assert.True(obj?.Count == 0);
        #endregion
    }


    [Fact]
    public void GetPeer()
    {
        IAdaptee adaptee = new JsonAdaptee();
        var adapter = new Adapter(adaptee);
        #region null test
        var result = adapter.GetPeer(null);
        var json = (JsonResult)result;
        var txt = JsonConvert.SerializeObject(json.Value);
        var obj = System.Text.Json.JsonSerializer.Deserialize<Model.Peer>(txt);
        Assert.True(obj == null);
        #endregion
        #region normal test
        Peer peer = new Peer()
        {
            Id = 1,
            hostAddress = "tcp://194.156.98.6:7676",
            info = "Some info here"
        };
        result = adapter.GetPeer(peer);
        json = (JsonResult)result;
        txt = JsonConvert.SerializeObject(json.Value);
        var resultPeer = System.Text.Json.JsonSerializer.Deserialize<Model.Peer>(txt);
        Assert.True(resultPeer?.Id == 1);
        Assert.True(resultPeer?.hostAddress == "tcp://194.156.98.6:7676");
        Assert.True(resultPeer?.info == "Some info here");
        #endregion
        #region empty name test
        peer = new Peer()
        {
            Id = 1,
            hostAddress = "",
            info = "Some info here"
        };
        result = adapter.GetPeer(peer);
        json = (JsonResult)result;
        txt = JsonConvert.SerializeObject(json.Value);
        var obj2 = System.Text.Json.JsonSerializer.Deserialize<Model.Peer>(txt);
        Assert.True(obj2 == null);
        #endregion
    }


    [Fact]
    public void Exception()
    {
        IAdaptee adaptee = new JsonAdaptee();
        var adapter = new Adapter(adaptee);
        #region null test
        var result = adapter.Exception(null);
        var json = (JsonResult)result;
        var txt = JsonConvert.SerializeObject(json.Value);
        var obj = System.Text.Json.JsonSerializer.Deserialize<ExceptionModel>(txt);
        Assert.True(obj == null);
        #endregion
        #region normal test
        ExceptionModel exception = new()
        {
            Domain = 400,
            Info = "test",
            Location = "test",
        };
        result = adapter.Exception(exception);
        json = (JsonResult)result;
        txt = JsonConvert.SerializeObject(json.Value);
        var resException = System.Text.Json.JsonSerializer.Deserialize<ExceptionModel>(txt);
        Assert.True(resException?.Domain == 400);
        Assert.True(resException?.Info == "test");
        #endregion
    }

}

