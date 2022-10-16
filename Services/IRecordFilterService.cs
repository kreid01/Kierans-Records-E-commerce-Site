namespace RecordShop.Services
{
    public interface IRecordFilterService
    {
        Task<int> RecordNameCount(string value);

        Task MigrateData();
    }
}