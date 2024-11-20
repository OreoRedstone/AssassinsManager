using AssassinsManager.Core.Jobs;
using AssassinsManager.Core.Services.Interfaces;
using Quartz;

namespace AssassinsManager.Core.Services;

public class SchedulerService : ISchedulerService
{
    private IScheduler _scheduler;

    public SchedulerService(ISchedulerFactory schedulerFactory)
    {
        ConfigureAsync(schedulerFactory);
    }

    private async Task ConfigureAsync(ISchedulerFactory schedulerFactory)
    {
        _scheduler = await schedulerFactory.GetScheduler();
    }

    public async Task CreateAnnouncementTrigger(string name, string announcement, DateTime triggerTime)
    {
        if(! await _scheduler.CheckExists(new JobKey("AnnouncementJob")))
            await _scheduler.AddJob(JobBuilder.Create<AnnouncementJob>()
                                        .WithIdentity("AnnouncementJob")
                                        .StoreDurably()
                                        .Build(), false);

        ISimpleTrigger trigger = (ISimpleTrigger) TriggerBuilder.Create()
                                                                .WithIdentity(name)
                                                                .StartAt(triggerTime)
                                                                .ForJob(new JobKey("AnnouncementJob"))
                                                                .UsingJobData("Announcement", announcement)
                                                                .Build();

        await _scheduler.ScheduleJob(trigger);
    }

    public async Task<ITrigger> GetTrigger(string name)
    {
        return await _scheduler.GetTrigger(new TriggerKey(name));
    }

    public async Task<IEnumerable<ITrigger>> GetTriggers()
    {
        return await _scheduler.GetTriggersOfJob(new JobKey("AnnouncementJob"));
    }
}