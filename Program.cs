using CircuitBreaker.Services;
using Polly;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        builder.Services.AddScoped<ISalesbuzzService, SalesbuzzService>();
        builder.Services.AddHttpClient<IHttpClientService, HttpClientService>(x => x.BaseAddress = new Uri("http://localhost:7199"))
            .SetHandlerLifetime(TimeSpan.FromSeconds(20))
            .AddPolicyHandler(GetCircuitBreakerPolicy());

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }

    static IAsyncPolicy<HttpResponseMessage> GetCircuitBreakerPolicy()
        {
            return Policy<HttpResponseMessage>
                 .Handle<TimeoutException>()
                 .Or<TaskCanceledException>()
                 .CircuitBreakerAsync(2, TimeSpan.FromSeconds(30));
        }
}