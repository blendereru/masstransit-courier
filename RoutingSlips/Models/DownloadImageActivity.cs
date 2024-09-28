using MassTransit;

namespace RoutingSlips.Models;

public class DownloadImageActivity :
    IExecuteActivity<DownloadImageArguments>
{
    private readonly HttpClient _client;
    public DownloadImageActivity(IHttpClientFactory clientFactory)
    {
        _client = clientFactory.CreateClient();
    }
    public async Task<ExecutionResult> Execute(ExecuteContext<DownloadImageArguments> context)
    {
        var args = context.Arguments;
        var imageSavePath = Path.Combine(args.WorkPath, context.GetVariable<Guid>("ImageId") + ".jpg");
        using (var response = await _client.GetAsync(args.ImageUri))
        {
            response.EnsureSuccessStatusCode();
            await using var fileStream = new FileStream(imageSavePath, FileMode.Create);
            await response.Content.CopyToAsync(fileStream);
        }
        
        return context.Completed<DownloadImageLog>(new
        {
            ImageSavedPath =  imageSavePath
        });
    }
}
