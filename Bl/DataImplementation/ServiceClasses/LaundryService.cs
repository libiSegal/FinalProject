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
                Laundry laundry = ConvertLaundryDTOToLaundry(laundryDTO);
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
                return ConvertLaundryToLaundryDTO(laundry);
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
        public async Task<bool> UpdateObject(LaundryDTO laundryDTO, string id)
        {
            try
            {
                return await _laundryCRUD.UpdateAsync(ConvertLaundryDTOToLaundry(laundryDTO));
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
                laundries.ForEach(l => laundryDTO.Add(ConvertLaundryToLaundryDTO(l)));
                return laundryDTO;
            }
            catch (TimeoutException ex) { throw ex; }
            catch (MongoConnectionException ex) { throw ex; }
            catch (NotExistsDataObjectException ex) { throw ex; }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }
        public LaundryDTO ConvertLaundryToLaundryDTO(Laundry laundry)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<Laundry, Laundry>());
            var mapper = config.CreateMapper();
            return mapper.Map<LaundryDTO>(laundry);
        }
        public Laundry ConvertLaundryDTOToLaundry(LaundryDTO laundryDTO)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<LaundryDTO, Laundry>());
            var mapper = config.CreateMapper();
            return mapper.Map<Laundry>(laundryDTO);
        }

        
    }
}
