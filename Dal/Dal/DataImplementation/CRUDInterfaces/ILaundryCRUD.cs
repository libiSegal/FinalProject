namespace Dal
{
    public interface ILaundryCRUD :IDataCRUD<Laundry>
    {
        Task<List<Laundry>> ReadAllAsync(string managerId);
    }
}