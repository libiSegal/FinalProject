
namespace Dal.DataImplementation.CRUDClasses;
public class CommonGroupDataCRUD : ICommonGroupDataCRUD
{
    private readonly IMongoCollection<CommonGroupData> _commonGroupDataCollection;
    private FilterDefinitionBuilder<CommonGroupData> _filterBuilder;

    public CommonGroupDataCRUD(IDBConnection db)
    {
        _commonGroupDataCollection = db.CommonGroupDataCollection;
        _filterBuilder = Builders<CommonGroupData>.Filter;
    }
    #region Create function 
    public async Task<string> CreateAsync(CommonGroupData commonGroupData)
    {
        try
        {
            if (commonGroupData == null)
            
                throw new ArgumentNullException($"in {nameof(CreateAsync)} function. commonGroupData object did not receive details(null)");
            
            var getFilter = _filterBuilder.Eq("ManagerID", commonGroupData.ManagerID);
            var getCommonGroupDataCollection = await _commonGroupDataCollection.Find(getFilter).FirstOrDefaultAsync();
            if (getCommonGroupDataCollection != null) throw new ExistsDataObjectExceotion($"There exists commom group data for this manager id - {commonGroupData.ManagerID}");

            await _commonGroupDataCollection.InsertOneAsync(commonGroupData);
            return commonGroupData.ID;
        }
        catch (TimeoutException ex) { throw ex; }
        catch (MongoWriteException ex) { throw ex; }
        catch (MongoBulkWriteException ex) { throw ex; }
        catch (ArgumentNullException ex) { throw ex; }
        catch (ExistsDataObjectExceotion ex) { throw ex; }
        catch (Exception ex) { throw new Exception(ex.Message); }
    }
    #endregion

    #region Update function
    public async Task<bool> UpdateAsync(CommonGroupData commonGroupData)
    {
        try
        {
            var updateFilter = _filterBuilder.Eq("_id", new ObjectId(commonGroupData.ID));
            var updateCommonGroupData = await _commonGroupDataCollection.Find(updateFilter).FirstOrDefaultAsync();

            if (updateCommonGroupData == null)
            {
                throw new NotExistsDataObjectException($"There is no object with {commonGroupData.ID} id ");
            }

            await _commonGroupDataCollection.ReplaceOneAsync(updateFilter, commonGroupData);
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
    public async Task<bool> DeleteAsync(string managerId)
    {
        try
        {
            var deleteFilter = _filterBuilder.Eq("ManagerID", new ObjectId(managerId));
            var deleteCommonGroupData = await _commonGroupDataCollection.Find(deleteFilter).FirstOrDefaultAsync();

            if (deleteCommonGroupData == null)
            {
                throw new NotExistsDataObjectException($"The item with this manager Id {managerId} is not defined");
            }

            await _commonGroupDataCollection.DeleteOneAsync(deleteFilter);
            deleteCommonGroupData = await _commonGroupDataCollection.Find(deleteFilter).FirstOrDefaultAsync();

            if (deleteCommonGroupData == null) return true;
            return false;
        }

        catch (TimeoutException ex) { throw ex; }
        catch (MongoWriteException ex) { throw ex; }
        catch (MongoBulkWriteException ex) { throw ex; }
        catch (NotExistsDataObjectException ex) { throw ex; }
        catch (Exception ex) { throw new Exception(ex.Message); }
    }

    #endregion

    #region Read function
    public async Task<CommonGroupData> ReadAsync(string managerId)
    {
        try
        {
            var getFilter = _filterBuilder.Eq("ManagerID", managerId);
            var getCommonGroupDataCollection = await _commonGroupDataCollection.Find(getFilter).FirstOrDefaultAsync();


            if (getCommonGroupDataCollection == null)
            {
                throw new NotExistsDataObjectException($"No common group data matched this manager Id : {managerId}");
            }
            return getCommonGroupDataCollection;
        }
        catch (TimeoutException ex) { throw ex; }
        catch (MongoConnectionException ex) { throw ex; }
        catch (NullReferenceException) { throw new NullReferenceException(""); }
        catch (NotExistsDataObjectException ex) { throw ex; }
    }
    #endregion

}