using AssassinsManager.Api.Controllers;

namespace AssassinsManager.Api.Services;

public class EventService
{
    private readonly IHost _webApplication;

    public EventService(IHost webApplication)
    {
        AnnouncementJob.AnnouncementEvent += AnnouncementEventHandler;
        _webApplication = webApplication;
    }

    private async void AnnouncementEventHandler(object? sender, AnnouncementEventArgs e)
    {
        BlogController controller = Extensions.GetScopedService<BlogController>(_webApplication);
        await controller.CreateBlog(new Blog()
        {
            Text = e.Announcement,
            Timestamp = e.Timestamp
        });
    }
}