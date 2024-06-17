using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using Moq;
using Newtonsoft.Json;
using SMBLibrary.SMB2;

namespace CJF.Sgcpj.Judicatura.Application.IntegrationTests;
internal static class TestCommon
{
    public static Mock<HttpRequest> CreateMockRequest(object body, IDictionary<string, string>? queryStrings = null)
    {
        using var memoryStream = new MemoryStream();
        using var writer = new StreamWriter(memoryStream);

        var json = JsonConvert.SerializeObject(body);

        writer.Write(json);
        writer.Flush();

        memoryStream.Position = 0;

        var mockRequest = new Mock<HttpRequest>();
        mockRequest.Setup(x => x.Body).Returns(memoryStream);
        mockRequest.Setup(x => x.ContentType).Returns("application/json");

        if (queryStrings != null && queryStrings.Any())
        {
            var dictionary = new Dictionary<string, StringValues>();

            foreach (var qS in queryStrings)
            {
                dictionary.Add(qS.Key, qS.Value);
            }

            var queryCol = new QueryCollection(dictionary);
            mockRequest.Setup(x => x.Query).Returns(queryCol);
        }

        return mockRequest;
    }

    public static HttpRequest CreatePostMockRequest(object body)
    {
        var json = JsonConvert.SerializeObject(body);

        var memoryStream = new MemoryStream(System.Text.Encoding.UTF8.GetBytes(json));

        var context = new DefaultHttpContext();
        var request = context.Request;
        request.Body = memoryStream;
        request.ContentType = "application/json";

        return request;
    }

    public static HttpRequest CreatePostFormCollectionMockRequest(IDictionary<string, string>? queryStrings = null)
    {
        var mockRequest = new Mock<HttpRequest>();

        if (queryStrings != null && queryStrings.Any())
        {
            var dictionary = new Dictionary<string, StringValues>();

            foreach (var qS in queryStrings)
            {
                dictionary.Add(qS.Key, qS.Value);
            }

            var formCol = new FormCollection(dictionary);
            mockRequest.Setup(x => x.ReadFormAsync(It.IsAny<CancellationToken>()).Result).Returns(formCol);
        }

        return mockRequest.Object;
    }

    public static HttpRequest CreatePostFormCollectionMockRequest(IDictionary<string, string> files, IDictionary<string, string>? queryStrings = null)
    {
        var mockRequest = new Mock<HttpRequest>();

        if (queryStrings != null && queryStrings.Any() && files.Any())
        {
            var dictionary = new Dictionary<string, StringValues>();

            foreach (var qS in queryStrings)
            {
                dictionary.Add(qS.Key, qS.Value);
            }

            var formFileCollection = new FormFileCollection();
            
            foreach (var file in files)
            {
                var fileName = Path.GetFileName(file.Value);
                var formFile = CreateFormFile("file", file.Key, file.Value);
                formFileCollection.Add(formFile);
            }

            var formCol = new FormCollection(dictionary, formFileCollection);
            mockRequest.Setup(x => x.ReadFormAsync(It.IsAny<CancellationToken>()).Result).Returns(formCol);
        }

        return mockRequest.Object;
    }

    public static IFormFile CreateFormFile(string keyname, string filePath, string contentType)
    {
        using (var stream = File.OpenRead(filePath))
            return new FormFile(stream, 0, stream.Length, keyname, filePath)
            {
                Headers = new HeaderDictionary(),
                ContentType = contentType
            };
    }
}
