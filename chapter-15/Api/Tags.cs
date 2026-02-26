using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
namespace Api;

public class Tags
{
    private readonly ILogger<Tags> _logger;
    public Tags(ILogger<Tags> logger)
    {
        _logger = logger;
    }

    [Function($"{nameof(Tags)}_GetAll")]
    public IActionResult GetAll(
    [HttpTrigger(AuthorizationLevel.Function,
    "get",
    Route = "tags")]
    HttpRequest req,
    [CosmosDBInput(
  databaseName: "SwaBlog",
  containerName: "StringContainer",
  Connection = "CosmosDbConnectionString",
  SqlQuery =@"
    SELECT
    VALUE c.id
    FROM c
    WHERE c.PartitionKey = 'Tags'")]
    IEnumerable<string> tags)
    {
        return new OkObjectResult(tags);
    }
}
