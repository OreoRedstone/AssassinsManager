using Quartz;

namespace AssassinsManager.Core.Jobs;

public class AnnouncementJob : IJob
{
    public static event EventHandler<AnnouncementEventArgs> AnnouncementEvent;

    public Task Execute(IJobExecutionContext context)
    {
        RaiseEvent(new()
        {
            Announcement = context.MergedJobDataMap["Announcement"].ToString(),
            Timestamp = DateTime.Now
        });

        return Task.CompletedTask;
    }

    public static void RaiseEvent(AnnouncementEventArgs args)
    {
        AnnouncementEvent?.Invoke(null, args);
    }
}

public class AnnouncementEventArgs : EventArgs
{
    public string Announcement;
    public DateTime Timestamp;
}
