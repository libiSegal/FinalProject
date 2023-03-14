
namespace Dal
{
    public class UserCRUD : IUserCRUD//we till have job in this class;
    {
        private readonly IMongoCollection<User> _usersCollection;
        private FilterDefinitionBuilder<User> _filterBuilder = Builders<User>.Filter;

        public UserCRUD(IDBConnection db)
        {
            _usersCollection = db.UsersCollection;
        }
        #region Read function
        public async Task<User> ReadAsync(string password, string name )
        {
            try
            {
                var getFilter = _filterBuilder.Eq("Name", name) & _filterBuilder.Eq("Password", password);
                var getUser = await _usersCollection.Find(getFilter).FirstOrDefaultAsync();

                if (getUser == null)
                {
                    throw new NotExistsDataObjectException($"No user matched : {name} - {password} ");
                }
                return getUser;
            }
            catch (TimeoutException ex) { throw ex; }
            catch (MongoConnectionException ex) { throw ex; }
            catch (NullReferenceException) { throw new NullReferenceException(""); }
            catch (NotExistsDataObjectException ex) { throw ex; }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }
        #endregion

        #region ReadAll function
        public async Task<List<User>> ReadAllAsync(string managerId)//mangerId is checked on bl;
        {
            try
            {
                var getAllFilter = _filterBuilder.Eq("ManagerId", managerId);
                var users = await _usersCollection.Find(getAllFilter).ToListAsync();

                if (users.FirstOrDefault() == null)
                {
                    throw new NotExistsDataObjectException($"No users matched manager ID - {managerId}");
                }
                return users;
            }
            catch (TimeoutException ex) { throw ex; }
            catch (MongoConnectionException ex) { throw ex; }
            catch (NotExistsDataObjectException ex) { throw ex; }
            catch (ObjectDisposedException ex) { throw ex; }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }
        #endregion
        #region Create function
        public async Task<string> CreateAsync(User dataObject)
        {
            try
            {
                if (dataObject == null)
                {
                    throw new ArgumentNullException($"in {nameof(CreateAsync)} function. user did not receive details(null)");
                }
           
                var createFilter = _filterBuilder.Eq("Name", dataObject.Name) & _filterBuilder.Eq("Password", dataObject.Password);
                var createUser = await _usersCollection.FindAsync(createFilter);

                if (createUser.FirstOrDefault() != null)
                {
                    throw new ExistsDataObjectExceotion($"This user is exist {dataObject.Name} , {dataObject.Password}");
                }

                await _usersCollection.InsertOneAsync(dataObject);
                return dataObject.ID ;

            }
            catch (TimeoutException ex) { throw ex; }
            catch (MongoWriteException ex) { throw ex; }
            catch (MongoBulkWriteException ex) { throw ex; }
            catch (ArgumentNullException ex) { throw ex; }
            catch (ExistsDataObjectExceotion ex) { throw ex; }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }
        #endregion
        public async Task<bool> DeleteAsync(string id)
        {
            try
            {
                ObjectId objectId = new(id);
                var deleteFilter = _filterBuilder.Eq("_id", objectId);
                var deleteUser = await _usersCollection.FindAsync(deleteFilter);

                if (deleteUser.FirstOrDefault() == null)
                {
                    throw new NotExistsDataObjectException($"The user with this id {id} is not defined");
                }

                await _usersCollection.DeleteOneAsync(deleteFilter);
                deleteUser = await _usersCollection.FindAsync(deleteFilter);

                if (deleteUser.FirstOrDefault() == null) return true;
                return false;
            }

            catch (TimeoutException ex) { throw ex; }
            catch (MongoWriteException ex) { throw ex; }
            catch (MongoBulkWriteException ex) { throw ex; }
            catch (NotExistsDataObjectException ex) { throw ex; }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }
 
        public async Task<bool> UpdateAsync(User user)
        {
            try
            {
                ObjectId objectId = new(user.ID);
                var updateFilter = _filterBuilder.Eq("_id", objectId);
                var updateUser = await _usersCollection.Find(updateFilter).FirstOrDefaultAsync();

                if (updateUser == null)
                {
                    throw new NotExistsDataObjectException($"This user {user.Name} ,{user.Password} is not defined");
                }  
                
                await _usersCollection.ReplaceOneAsync(updateFilter, user);
                return true;
            }
            catch (TimeoutException ex) { throw ex; }
            catch (MongoWriteException ex) { throw ex; }
            catch (MongoBulkWriteException ex) { throw ex; }
            catch (NotExistsDataObjectException ex) { throw ex; }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }

        public async Task<User> ReadAsync(string id)
        {

            try
            {
                var getFilter = _filterBuilder.Eq("_id",new ObjectId(id)) ;
                var getUser = await _usersCollection.Find(getFilter).FirstOrDefaultAsync();

                if (getUser == null)
                {
                    throw new NotExistsDataObjectException($"No user matched : {id} ");
                }
                return getUser;
            }
            catch (TimeoutException ex) { throw ex; }
            catch (MongoConnectionException ex) { throw ex; }
            catch (NullReferenceException) { throw new NullReferenceException(""); }
            catch (NotExistsDataObjectException ex) { throw ex; }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }
    }
    }

