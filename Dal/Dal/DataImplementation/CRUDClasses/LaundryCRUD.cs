
namespace Dal
{
    public class LaundryCRUD : ILaundryCRUD
    {
        private readonly IMongoCollection<Laundry> _laundryCollection;
        private FilterDefinitionBuilder<Laundry> _filterBuilder = Builders<Laundry>.Filter;

        public LaundryCRUD(IDBConnection db)
        {
            _laundryCollection = db.LaundryCollection;
        }
        public async Task<string> CreateAsync(Laundry dataObject)
        {
            try
            {
                if (dataObject == null)
                {
                    throw new ArgumentNullException($"in {nameof(CreateAsync)} function. laundry did not receive details(null)");
                }

                var createFilter = _filterBuilder.Eq("Date", dataObject.Date);
                var createUser = await _laundryCollection.FindAsync(createFilter);

                if (createUser.FirstOrDefault() != null)
                {
                    throw new ExistsDataObjectExceotion($" laundry is already exist in {dataObject.Date}");
                }
                await _laundryCollection.InsertOneAsync(dataObject);
                return dataObject.ID;
            }
            catch (TimeoutException ex) { throw ex; }
            catch (MongoWriteException ex) { throw ex; }
            catch (MongoBulkWriteException ex) { throw ex; }
            catch (ArgumentNullException ex) { throw ex; }
            catch (ExistsDataObjectExceotion ex) { throw ex; }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }
        public Task<bool> UpdateAsync(Laundry laundry)
        {
            throw new Exception("laundry don't have an update option");
        }
        public async Task<bool> DeleteAsync(string id)
        {
            try
            {
                ObjectId objectId = new(id);
                var deleteFilter = _filterBuilder.Eq("_id", objectId);
                var deletewashAble = await _laundryCollection.Find(deleteFilter).FirstOrDefaultAsync();

                if (deletewashAble == null)
                {
                    throw new NotExistsDataObjectException($"The item with this id {id} is not defined");
                }

                await _laundryCollection.DeleteOneAsync(deleteFilter);
                deletewashAble = await _laundryCollection.Find(deleteFilter).FirstOrDefaultAsync();

                if (deletewashAble == null) return true;
                return false;
            }

            catch (TimeoutException ex) { throw ex; }
            catch (MongoWriteException ex) { throw ex; }
            catch (MongoBulkWriteException ex) { throw ex; }
            catch (NotExistsDataObjectException ex) { throw ex; }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }
        public async Task<Laundry> ReadAsync(string managerId )
        {
            try
            {
                var getFilter = _filterBuilder.Eq("ManagerId", managerId) ;
                var getUser = await _laundryCollection.Find(getFilter).FirstOrDefaultAsync();

                if (getUser == null)
                {
                    throw new NotExistsDataObjectException($"No laundries matched this manager Id : {managerId}");
                }
                return getUser;
            }
            catch (TimeoutException ex) { throw ex; }
            catch (MongoConnectionException ex) { throw ex; }
            catch (NullReferenceException) { throw new NullReferenceException(""); }
            catch (NotExistsDataObjectException ex) { throw ex; }
        }
        public async Task<List<Laundry>> ReadAllAsync(string managerId)//mangerId is checked on bl;
        {
            try
            {
                var getAllFilter = _filterBuilder.Eq("ManagerId", managerId);
                var users = await _laundryCollection.Find(getAllFilter).ToListAsync();

                /*if (users.FirstOrDefault() == null)
                {
                    throw new NotExistsDataObjectException($"No users matched manager ID - {managerId}");
                }*/
                return users;
            }
            catch (TimeoutException ex) { throw ex; }
            catch (MongoConnectionException ex) { throw ex; }
            catch (NotExistsDataObjectException ex) { throw ex; }
            catch (ObjectDisposedException ex) { throw ex; }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }


    }

}

