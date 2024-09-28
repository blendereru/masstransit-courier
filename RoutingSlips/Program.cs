using MassTransit;
using RoutingSlips.Models;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddHttpClient();
builder.Services.AddMassTransit(x =>
{
    x.AddConsumer<SubmitImageConsumer>();
    x.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host("rabbitmq://localhost", h =>
        {
            h.Username(builder.Configuration["UserSettings:Username"]!);
            h.Password(builder.Configuration["UserSettings:Password"]!);
        });
        cfg.ReceiveEndpoint("image-queue", conf =>
        {
            conf.ConfigureConsumer<SubmitImageConsumer>(context);
        });
        cfg.ConfigureEndpoints(context);
    });
});
var app = builder.Build();
app.MapGet("/", async (IPublishEndpoint endpoint) =>
{
    await endpoint.Publish<SubmitImage>(new
    {
        ImageId = Guid.NewGuid()
    });
});
app.Run();