
namespace Dal
{
    public class WashAbleCRUD : IWashAbleCRUD
    {
        //we have to find a way to check if item is exists on create func.

        private readonly IMongoCollection<WashAble> _washAblesCollection;
        private FilterDefinitionBuilder<WashAble> _filterBuilder = Builders<WashAble>.Filter;

        public WashAbleCRUD(IDBConnection db)
        {
            _washAblesCollection = db.WashAblesCollection;
        }

        #region Create function
        public  async Task<string> CreateAsync(WashAble dataObject)
        {
            try
            {
                if (dataObject == null)
                {
                    throw new ArgumentNullException($"in {nameof(CreateAsync)} function, wash able item did not receive details(null)");
                }

            await _washAblesCollection.InsertOneAsync(dataObject);
            return dataObject.ID;
                
            }
            catch (TimeoutException ex) { throw ex; }
            catch (MongoWriteException ex) { throw ex; }
            catch (MongoBulkWriteException ex) { throw ex; }
            catch (ArgumentNullException ex) { throw ex; }
            catch (ExistsDataObjectExceotion ex) { throw ex; }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }
        #endregion

        #region Read function
        public async Task<WashAble> ReadAsync(string id)
        {
            try
            {
                var getFilter = _filterBuilder.Eq("_id",new ObjectId(id));
                var getWashAble = await _washAblesCollection.Find(getFilter).FirstOrDefaultAsync();

                if (getWashAble == null)
                {
                    throw new NotExistsDataObjectException($"No item matched for - {id}");
                }

                return getWashAble;
            }
            catch (TimeoutException ex) { throw ex; }
            catch (MongoConnectionException ex) { throw ex; }
            catch (NullReferenceException ex) { throw ex; }
            catch (NotExistsDataObjectException ex) { throw ex; }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }
        #endregion

        #region ReadAll function
        public async Task<List<WashAble>> ReadAllAsync(string userId)
        {
            try
            {
                var getAllFilter = _filterBuilder.Eq("UserID",userId);
                var items = await _washAblesCollection.Find(getAllFilter).ToListAsync();
                return items;
            }
            catch (TimeoutException ex) { throw ex; }
            catch (MongoConnectionException ex) { throw ex; }
            catch (NotExistsDataObjectException ex) { throw ex; }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }
        #endregion

        #region Update function
        public async Task<bool> UpdateAsync(WashAble item)
        {
            try
            {
                var updateFilter = _filterBuilder.Eq("_id",new ObjectId(item.ID));
                var updateUser = await _washAblesCollection.Find(updateFilter).FirstOrDefaultAsync();

                if (updateUser == null)
                {
                    throw new NotExistsDataObjectException($"This item {item.Name} ,{item.ID} is not defined");
                }

                await _washAblesCollection.ReplaceOneAsync(updateFilter, item);
                return true;
            }
            catch (TimeoutException ex) { throw ex; }
            catch (MongoWriteException ex) { throw ex; }
            catch (MongoBulkWriteException ex) { throw ex; }
            catch (NotExistsDataObjectException ex) { throw ex; }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }
        #endregion

        #region Delete function
        public async Task<bool> DeleteAsync(string id)
        {
            try
            {
                var deleteFilter = _filterBuilder.Eq("_id",new ObjectId(id));
                var deletewashAble = await _washAblesCollection.Find(deleteFilter).FirstOrDefaultAsync();

                if (deletewashAble == null)
                {
                    throw new NotExistsDataObjectException($"The item with this id {id} is not defined");
                }

                await _washAblesCollection.DeleteOneAsync(deleteFilter);
                deletewashAble = await _washAblesCollection.Find(deleteFilter).FirstOrDefaultAsync();

                if (deletewashAble == null) return true;
                return false;
            }

            catch (TimeoutException ex) { throw ex; }
            catch (MongoWriteException ex) { throw ex; }
            catch (MongoBulkWriteException ex) { throw ex; }
            catch (NotExistsDataObjectException ex) { throw ex; }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }
        #endregion

    }
}
