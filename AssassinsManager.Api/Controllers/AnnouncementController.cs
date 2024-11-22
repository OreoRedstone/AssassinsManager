using AssassinsManager.Core.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AssassinsManager.Api.Controllers;

[ApiController]
[Authorize(Roles="Cabal")]
[Route("api/announcements")]
public class AnnouncementController(ISchedulerService schedulerService) : ControllerBase
{
    private readonly ISchedulerService _schedulerService = schedulerService;

    [HttpGet("api/announcements/{name}")]
    public async Task<IActionResult> GetAnnouncement(string name)
    {
        return Ok(await _schedulerService.GetTrigger(name));
    }

    [HttpGet]
    public async Task<IActionResult> GetAnnouncements()
    {
        return Ok(await _schedulerService.GetTriggers());
    }

    [HttpPost]
    public async Task<IActionResult> CreateAnnouncement([FromBody] Announcement announcement)
    {
        await _schedulerService.CreateAnnouncementTrigger(announcement.Name, announcement.AnnouncementText, announcement.Timestamp);
        return Ok(announcement);
    }
}

public class Announcement
{
    public string Name { get; set; }
    public string AnnouncementText { get; set; }
    public DateTime Timestamp { get; set; }
}