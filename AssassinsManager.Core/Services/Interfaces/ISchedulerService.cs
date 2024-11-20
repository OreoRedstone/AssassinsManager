using Quartz;

namespace AssassinsManager.Core.Services.Interfaces;

public interface ISchedulerService
{
    public Task<ITrigger> GetTrigger(string name);
    public Task<IEnumerable<ITrigger>> GetTriggers();
    public Task CreateAnnouncementTrigger(string name, string announcement, DateTime triggerTime);
}