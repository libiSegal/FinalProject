﻿using AutoMapper;
using Dal;
using Dal.Exceptions;
using MongoDB.Driver;


namespace BL
{
    public class LaundryService : ILaundryService
    {
         readonly IMapper _mapper;
         readonly ILaundryCRUD _laundryCRUD;
        public LaundryService(IMapper mapper, ILaundryCRUD laundryCRUD)
        {
           _mapper = mapper;
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
        public LaundryDTO MapLaundry_LaundryDTO(Laundry laundry) => _mapper.Map<LaundryDTO>(laundry);
      

        public Laundry MapLaundryDTO_Laundry(LaundryDTO laundryDTO) => _mapper.Map<Laundry>(laundryDTO);
      

      

    }
}
