using MassTransit;

namespace RoutingSlips.Models;

public class SubmitImageConsumer : IConsumer<SubmitImage>
{
    public async Task Consume(ConsumeContext<SubmitImage> context)
    {
        var routingSlipBuilder = new RoutingSlipBuilder(context.Message.ImageId);
        routingSlipBuilder.AddActivity(nameof(DownloadImageActivity), new Uri("queue:temp-image-activity"), new
        {
            ImageUri = new Uri("https://localhost:7162/get-image"),
            WorkPath = @"C:\Program Files\temp"
        });
        routingSlipBuilder.AddVariable("ImageId", context.Message.ImageId);
        var routingSlip = routingSlipBuilder.Build();
        await context.Execute(routingSlip);
    }
}