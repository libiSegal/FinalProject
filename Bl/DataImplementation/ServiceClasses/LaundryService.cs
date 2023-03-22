using AutoMapper;
using Dal;
using Dal.Exceptions;
using MongoDB.Driver;


namespace BL
{
    public class LaundryService : ILaundryService
    {
        private readonly ILaundryCRUD _laundryCRUD;
        public LaundryService(ILaundryCRUD laundryCRUD)
        {
            _laundryCRUD = laundryCRUD;
        }
        public Task<string> CreateObject(LaundryDTO laundryDTO)
        {
            try
            {
                Laundry laundry = MapLaundryDTO_Laundry(laundryDTO);
                return _laundryCRUD.CreateAsync(laundry);
            }
            catch (TimeoutException ex) { throw ex; }
            catch (MongoWriteException ex) { throw ex; }
            catch (MongoBulkWriteException ex) { throw ex; }
            catch (ExistsDataObjectExceotion ex) { throw ex; }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }
        public async Task<LaundryDTO> GetObject(string id)
        {//id = barcode.data.id
            try
            {
                Laundry laundry = await _laundryCRUD.ReadAsync(id);
                return MapLaundry_LaundryDTO(laundry);
            }
            catch (TimeoutException ex) { throw ex; }
            catch (MongoConnectionException ex) { throw ex; }
            catch (NullReferenceException ex) { throw ex; }
            catch (NotExistsDataObjectException ex) { throw ex; }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }
        public Task<bool> DeleteObject(string id)
        {
            try
            {
                return _laundryCRUD.DeleteAsync(id);
            }
            catch (TimeoutException ex) { throw ex; }
            catch (MongoWriteException ex) { throw ex; }
            catch (MongoBulkWriteException ex) { throw ex; }
            catch (NotExistsDataObjectException ex) { throw ex; }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }
        public async Task<bool> UpdateObject(LaundryDTO laundryDTO)
        {
            try
            {
                return await _laundryCRUD.UpdateAsync(MapLaundryDTO_Laundry(laundryDTO));
            }
            catch (TimeoutException ex) { throw ex; }
            catch (MongoWriteException ex) { throw ex; }
            catch (MongoBulkWriteException ex) { throw ex; }
            catch (NotExistsDataObjectException ex) { throw ex; }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }
        public async Task<List<LaundryDTO>> GetAll(string managerId)
        {
            try
            {
                List<LaundryDTO> laundryDTO = new();
                List<Laundry> laundries = await _laundryCRUD.ReadAllAsync(managerId);
                laundries.ForEach(l => laundryDTO.Add(MapLaundry_LaundryDTO(l)));
                return laundryDTO;
            }
            catch (TimeoutException ex) { throw ex; }
            catch (MongoConnectionException ex) { throw ex; }
            catch (NotExistsDataObjectException ex) { throw ex; }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }
        public LaundryDTO MapLaundry_LaundryDTO(Laundry laundry)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<Laundry, Laundry>());
            var mapper = config.CreateMapper();
            return mapper.Map<LaundryDTO>(laundry);
        }
        public Laundry MapLaundryDTO_Laundry(LaundryDTO laundryDTO)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<LaundryDTO, Laundry>());
            var mapper = config.CreateMapper();
            return mapper.Map<Laundry>(laundryDTO);
        }
       /* public async Task<List<string>> UpdateLaudryList(ManagerDTO managerDTO, Manager managerFromDB)
        {
            try
            {
                List<string> resultList = new();
                List<Task<bool>> deleteTasks = new();
                List<Task<string>> createTasks = new();

                //check if we need to add new users;
                List<LaundryDTO> laundriesToCreate = managerDTO.LaundriesDTO.FindAll(laundry => laundry.ID == "");
                if (laundriesToCreate != null)
                {
                    laundriesToCreate.ForEach(laundry => createTasks.Add(_laundryCRUD.CreateAsync(MapLaundryDTO_Laundry(laundry))));
                }

                managerDTO.LaundriesDTO.RemoveAll(laundry => laundry.ID == "");
                resultList.AddRange(Task.WhenAll(createTasks).Result);
                managerDTO.LaundriesDTO.ForEach(laundry => resultList.Add(laundry.ID));

                //check if we need to delete an item;
                List<string> laundriesIdToRemove = managerFromDB.LaundriesID.Except(managerDTO.LaundriesDTO.Select(laundry => laundry.ID).ToList()).ToList();

                if (laundriesIdToRemove != null)
                {
                    laundriesIdToRemove.ForEach(user => deleteTasks.Add(_laundryCRUD.DeleteAsync(user)));
                    //Task.WhenAll(deleteTasks);
                }
                //check if we need to update one or more items;
                List<LaundryDTO> usersForManagerFromDB = await GetAll(managerFromDB.ID);
                managerDTO.LaundriesDTO.Except(usersForManagerFromDB).ToList().
                    ForEach(laundry => _laundryCRUD.UpdateAsync(MapLaundryDTO_Laundry(laundry)));
                return resultList;
            }


            catch (TimeoutException ex) { throw ex; }
            catch (MongoWriteException ex) { throw ex; }
            catch (MongoBulkWriteException ex) { throw ex; }
            catch (NotExistsDataObjectException ex) { throw ex; }
            catch (Exception ex) { throw new Exception(ex.Message); }

        }*/

    }
}
