
namespace Bl.Algorithm;

public class SchedulerService
{
    public SchedulerService()
    {

    }
    //allWashAbles = managerDTO.Items
    public List<WashAbleDTO> GetDirtyWashables(List<WashAbleDTO> allWashAbles) => allWashAbles.FindAll(w => w.Status == Status.dirty);
    
    public List<WashAbleDTO> fill()
    {
        List<WashAbleDTO> washAbleDTOs = new();
        washAbleDTOs.Add(new(Status.washing));
        washAbleDTOs.Add(new(Status.clean));
        washAbleDTOs.Add(new(Status.clean));
        washAbleDTOs.Add(new(Status.dirty));
        washAbleDTOs.Add(new(Status.dirty));
        washAbleDTOs.Add(new(Status.washing));
        return washAbleDTOs;
    }



}
