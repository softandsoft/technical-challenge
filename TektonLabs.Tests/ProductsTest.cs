using Newtonsoft.Json;
using System.Net;
using System.Text;
using TektonLabs.Domain;
using TektonLabs.Domain.DTOs.Response;

namespace TektonLabs.Tests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void GetByIdTest()
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://localhost:7243/api/v1/Products/1");
            request.Method = "GET";

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            StreamReader reader = new StreamReader(response.GetResponseStream());
            string json = reader.ReadToEnd();

            WebApiResponse result = JsonConvert.DeserializeObject<WebApiResponse>(json);

            Assert.IsTrue(result.Success);
            Assert.AreEqual(null, result.Errors);

            string productJson = JsonConvert.SerializeObject(result.Response.Data);

            ProductResponse product = JsonConvert.DeserializeObject<ProductResponse>(productJson);

            Assert.AreEqual("Mouse", product.Name);
        }

        [Test]
        public void CreateTest()
        {
            string postdata = "{\"name\": \"Hub\",\"description\": \"gdfsgdfsg\",\"stock\": 2,\"price\": 100,\"status\": \"1\",\"creationUser\": \"cvallejo\"}";

            byte[] data = Encoding.UTF8.GetBytes(postdata);

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://localhost:7243/api/v1/Products");
            request.Method = "POST";
            request.ContentLength = data.Length;
            request.ContentType = "application/json";

            var requestStream = request.GetRequestStream();
            requestStream.Write(data, 0, data.Length);

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            StreamReader reader = new StreamReader(response.GetResponseStream());
            string json = reader.ReadToEnd();

            WebApiResponse result = JsonConvert.DeserializeObject<WebApiResponse>(json);

            Assert.IsTrue(result.Success);
            Assert.AreEqual(null, result.Errors);
        }

        [Test]
        public void UpdateTest()
        {
            string postdata = "{\"name\": \"parlante modificado 2\",\"description\": \"gfdgfddddddddddddddd\",\"stock\": 20,\"price\": 120,\"status\": \"0\",\"modificationUser\": \"jcarrera\"}";

            byte[] data = Encoding.UTF8.GetBytes(postdata);

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://localhost:7243/api/v1/Products/3");
            request.Method = "PUT";
            request.ContentLength = data.Length;
            request.ContentType = "application/json";

            var requestStream = request.GetRequestStream();
            requestStream.Write(data, 0, data.Length);

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            StreamReader reader = new StreamReader(response.GetResponseStream());
            string json = reader.ReadToEnd();

            WebApiResponse result = JsonConvert.DeserializeObject<WebApiResponse>(json);

            Assert.IsTrue(result.Success);
            Assert.AreEqual(null, result.Errors);
        }
    }
}