using Amazon.S3;
using Amazon.S3.Model;
using Amazon.SimpleNotificationService;
using Amazon.SimpleNotificationService.Model;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/weatherforecast", () =>
{
    var forecast = Enumerable.Range(1, 5).Select(index =>
        new WeatherForecast
        (
            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            Random.Shared.Next(-20, 55),
            summaries[Random.Shared.Next(summaries.Length)]
        ))
        .ToArray();
    return forecast;
})
.WithName("GetWeatherForecast")
.WithOpenApi();

app.MapGet("/long-running-request", async (CancellationToken cancellationToken) =>
{
    var randomId = Guid.NewGuid();
    var results = new List<string>();

    for (int i = 0; i < 100; i++)
    {
        if (cancellationToken.IsCancellationRequested)
        {
            Console.WriteLine("IsCancellationRequested = true");
            return Results.StatusCode(499);
        }

        await Task.Delay(1000);
        var result = $"{randomId} - Result {i}";
        Console.WriteLine(result);
        results.Add(result);
    }

    return Results.Ok(results);
})
    .WithName("GetAllData")
    .WithOpenApi();

app.MapPost("/upload-large-file", async ([FromForm] FileUploadRequest request, CancellationToken cancellationToken) =>
{
    try
    { 
        // Cannot run as I don't have profile
        var s3Client = new AmazonS3Client();
        await s3Client.PutObjectAsync(new PutObjectRequest()
        {
            BucketName = "user-service-large-messages",
            Key = $"{Guid.NewGuid()} - {request.File.FileName}",
            InputStream = request.File.OpenReadStream()
        }, cancellationToken);

        // Side effect
        await PerformAdditionalTasks(cancellationToken);
        return Results.NoContent();
    }
    catch (OperationCanceledException)
    {
        return Results.StatusCode(499);
    }
})
    .WithName("UploadLargeFile")
    .DisableAntiforgery()
    .WithOpenApi();

static async Task PerformAdditionalTasks(CancellationToken cancellationToken)
{
    await Task.Delay(1000, cancellationToken);

    var snsClient = new AmazonSimpleNotificationServiceClient();
    await snsClient.PublishAsync(new PublishRequest()
    {
        TopicArn = "<SNS TOPIC ARN>",
        Message = "UserUploadedFileEvent"
    }, cancellationToken);
}

app.Run();
record FileUploadRequest(IFormFile File)
{
}
internal record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
