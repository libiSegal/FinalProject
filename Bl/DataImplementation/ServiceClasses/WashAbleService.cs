using AutoMapper;
using Dal;
using Dal.Exceptions;
using MongoDB.Driver;


namespace BL
{
    public class WashAbleService : IWashAbleService
    {
        private readonly IWashAbleCRUD _washAbleCRUD;
        public WashAbleService(IWashAbleCRUD washAbleCRUD)
        {
            _washAbleCRUD = washAbleCRUD;
        }
        public Task<string> CreateObject(WashAbleDTO washAbleDTO)
        {
            try
            {
                WashAble washAble = ConvertWashAbleDTOToWashAble(washAbleDTO);
                return _washAbleCRUD.CreateAsync(washAble);
            }
            catch (TimeoutException ex) { throw ex; }
            catch (MongoWriteException ex) { throw ex; }
            catch (MongoBulkWriteException ex) { throw ex; }
            catch (ExistsDataObjectExceotion ex) { throw ex; }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }
        public async Task<WashAbleDTO> GetObject(string id)
        {//id = barcode.data.id
            try
            {
                WashAble washAble = await _washAbleCRUD.ReadAsync(id);
                return ConvertWashAbleToWashAbleDTO(washAble);
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
                return _washAbleCRUD.DeleteAsync(id);
            }
            catch (TimeoutException ex) { throw ex; }
            catch (MongoWriteException ex) { throw ex; }
            catch (MongoBulkWriteException ex) { throw ex; }
            catch (NotExistsDataObjectException ex) { throw ex; }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }
        public async Task<bool> UpdateObject(WashAbleDTO washAbleBl, string id)
        {
            try
            {
                WashAble oldWashAble = await _washAbleCRUD.ReadAsync(id);
                WashAble washAble = ConvertWashAbleDTOToWashAble(washAbleBl);
                washAble.ID = oldWashAble.ID;
                return await _washAbleCRUD.UpdateAsync(washAble);
            }
            catch (TimeoutException ex) { throw ex; }
            catch (MongoWriteException ex) { throw ex; }
            catch (MongoBulkWriteException ex) { throw ex; }
            catch (NotExistsDataObjectException ex) { throw ex; }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }
        public async Task<List<WashAbleDTO>> GetAll(string userId)
        {
            try
            {
                List<WashAbleDTO> washAblesBl = new();
                List<WashAble> washAbles = await _washAbleCRUD.ReadAllAsync(userId);
                washAbles.ForEach(w => washAblesBl.Add(ConvertWashAbleToWashAbleDTO(w)));
                return washAblesBl;
            }
            catch (TimeoutException ex) { throw ex; }
            catch (MongoConnectionException ex) { throw ex; }
            catch (NotExistsDataObjectException ex) { throw ex; }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }

        public WashAbleDTO ConvertWashAbleToWashAbleDTO(WashAble washAble)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<WashAble, WashAbleDTO>());
            var mapper = config.CreateMapper();
            return mapper.Map<WashAbleDTO>(washAble); 
        }
        public WashAble ConvertWashAbleDTOToWashAble(WashAbleDTO washAbleDTO)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<WashAbleDTO, WashAble>());
            var mapper = config.CreateMapper();
            return mapper.Map<WashAble>(washAbleDTO);
        }

        public List<WashAbleDTO> GetWashAblesItems(List<string> washAbleIDs)
        {
            try
            {
                List<WashAbleDTO> washAbles = new();
                washAbleIDs.ForEach(async washAble =>
                {
                    washAbles.Add(await GetObject(washAble));
                });
                return washAbles;//exit
            }
            catch (AggregateException ex) { throw new Exception(ex.Message); }

        }

        public List<string> GetWashAblesId(List<WashAbleDTO> washAbles)
        {
            List<string> washAbleIds = new();
            washAbles.ForEach(washable =>
            {
                washAbleIds.Add(washable.ID);
            });
            return washAbleIds;
        }
        public async Task<List<string>> UpdateItemsList(UserDTO userDTO, User userFromDB)
        {
            try
            {
                List<string> resultList = new();
                //check if we need to add a new item;
                List<Task<string>> createTasks = new();
                List<WashAbleDTO> itemsToCreate = userDTO.Items.FindAll(item => item.ID == "");
                if (itemsToCreate != null)
                {
                    itemsToCreate.ForEach(item => createTasks.Add(_washAbleCRUD.CreateAsync(ConvertWashAbleDTOToWashAble(item))));
                }

                userDTO.Items.RemoveAll(item => item.ID == "");
                resultList.AddRange(Task.WhenAll(createTasks).Result);
                userDTO.Items.ForEach(item => resultList.Add(item.ID));
                
                //check if we need to delete a item;
                List<string> itemsIdToRemove = userFromDB.ItemsId.Except(userDTO.Items.Select(item => item.ID).ToList()).ToList();
                List<Task<bool>> deleteTasks = new();
                if (itemsIdToRemove != null)
                {
                    itemsIdToRemove.ForEach(item => deleteTasks.Add(_washAbleCRUD.DeleteAsync(item)));
                    //Task.WhenAll(deleteTasks);
                }
                //check if we need to update one or more items;
                List<WashAbleDTO> washAblesForUserFromDB = await GetAll(userFromDB.ID);
                userDTO.Items.Except(washAblesForUserFromDB).ToList().
                    ForEach(item => _washAbleCRUD.UpdateAsync(ConvertWashAbleDTOToWashAble(item)));
                return resultList;
            }


            catch (TimeoutException ex) { throw ex; }
            catch (MongoWriteException ex) { throw ex; }
            catch (MongoBulkWriteException ex) { throw ex; }
            catch (NotExistsDataObjectException ex) { throw ex; }
            catch (Exception ex) { throw new Exception(ex.Message); }

        }
    }
}



















