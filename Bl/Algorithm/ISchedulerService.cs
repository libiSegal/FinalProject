namespace Bl.Algorithm
{
    public interface ISchedulerService
    {
        Dictionary<string, List<WashAbleDTO>> Scheduler(ManagerDTO manager);
    }
}