namespace Dal.DataImplementation.CRUDClasses;

public class ManagerCRUD : IManagerCRUD
{
    private readonly IMongoCollection<Manager> _managersCollection;
    private FilterDefinitionBuilder<Manager> _filterBuilder;

    public ManagerCRUD(IDBConnection db)
    {
        _managersCollection = db.ManagersCollection;
        _filterBuilder = Builders<Manager>.Filter;
    }

    #region Read by name and password function
    public async Task<Manager> ReadAsync(string name, string password)
    {
        try
        {
            var getFilter = _filterBuilder.Eq("Name", name) & _filterBuilder.Eq("Password", password);
            var getManager = await _managersCollection.Find(getFilter).FirstOrDefaultAsync();

            if (getManager == null)
            {
                throw new NotExistsDataObjectException($"No manger matched : {name} - {password} ");
            }
            return getManager;
        }
        catch (TimeoutException ex) { throw ex; }
        catch (MongoConnectionException ex) { throw ex; }
        catch (NullReferenceException) { throw new NullReferenceException(""); }
        catch (NotExistsDataObjectException ex) { throw ex; }
        catch (Exception ex) { throw new Exception(ex.Message); }
    }

    #endregion

    #region Create function
    public async Task<string> CreateAsync(Manager manager)
    {
        try
        {
            if (manager == null)
            {
                throw new ArgumentNullException($"in {nameof(CreateAsync)} function. manger did not receive details(null)");
            }

            var createFilter = _filterBuilder.Eq("Name", manager.Name) & _filterBuilder.Eq("Password", manager.Password);
            var createUser = await _managersCollection.FindAsync(createFilter);

            if (createUser.FirstOrDefault() != null)
            {
                throw new ExistsDataObjectExceotion($"This manger is exist {manager.Name} , {manager.Password}");
            }

            await _managersCollection.InsertOneAsync(manager);
            return manager.ID;

        }
        catch (TimeoutException ex) { throw ex; }
        catch (MongoWriteException ex) { throw ex; }
        catch (MongoBulkWriteException ex) { throw ex; }
        catch (ArgumentNullException ex) { throw ex; }
        catch (ExistsDataObjectExceotion ex) { throw ex; }
        catch (Exception ex) { throw new Exception(ex.Message); }
    }

    #endregion

    #region Delete function
    public async Task<bool> DeleteAsync(string id)
    {
        try
        {
            var deleteFilter = _filterBuilder.Eq("_id", new ObjectId(id));
            var deleteManager = await _managersCollection.FindAsync(deleteFilter);

            if (deleteManager.FirstOrDefault() == null)
            {
                throw new NotExistsDataObjectException($"The user with this id {id} is not defined");
            }

            await _managersCollection.DeleteOneAsync(deleteFilter);
            deleteManager = await _managersCollection.FindAsync(deleteFilter);

            if (deleteManager.FirstOrDefault() == null) return true;
            return false;
        }

        catch (TimeoutException ex) { throw ex; }
        catch (MongoWriteException ex) { throw ex; }
        catch (MongoBulkWriteException ex) { throw ex; }
        catch (NotExistsDataObjectException ex) { throw ex; }
        catch (Exception ex) { throw new Exception(ex.Message); }
    }

    #endregion

    #region Update function
    public async Task<bool> UpdateAsync(Manager manager)
    {
        try
        {
            var updateFilter = _filterBuilder.Eq("_id", new ObjectId(manager.ID));
            var updateUser = await _managersCollection.Find(updateFilter).FirstOrDefaultAsync();

            if (updateUser == null)
            {
                throw new NotExistsDataObjectException($"This user {manager.Name} ,{manager.Password} is not defined");
            }

            await _managersCollection.ReplaceOneAsync(updateFilter, manager);
            return true;
        }
        catch (TimeoutException ex) { throw ex; }
        catch (MongoWriteException ex) { throw ex; }
        catch (MongoBulkWriteException ex) { throw ex; }
        catch (NotExistsDataObjectException ex) { throw ex; }
        catch (Exception ex) { throw new Exception(ex.Message); }

    }

    #endregion

    #region Read by id function
    public async Task<Manager> ReadAsync(string id)
    {
        try
        {
            var getFilter = _filterBuilder.Eq("_id", new ObjectId(id));
            var getManager = await _managersCollection.Find(getFilter).FirstOrDefaultAsync();

            if (getManager == null)
            {
                throw new NotExistsDataObjectException($"No manager matched : {id} ");
            }
            return getManager;
        }
        catch (TimeoutException ex) { throw ex; }
        catch (MongoConnectionException ex) { throw ex; }
        catch (NullReferenceException ex) { throw ex; }
        catch (NotExistsDataObjectException ex) { throw ex; }
        catch (Exception ex) { throw new Exception(ex.Message); }
    }
    #endregion
}