
namespace Dal
{
    public interface IWashAbleCRUD : IDataCRUD<WashAble>
    {
        Task<List<WashAble>> ReadAllAsync(string userId);
    }
}