using System;
using System.Threading.Tasks;
using JayCadSurveyXamarin.Model;
using SQLite;

namespace JayCadSurveyXamarin.Persistence
{
    public class JayCadLocalDB
    {
        readonly SQLiteAsyncConnection database;

        public JayCadLocalDB(string dbPath)
        {
            database = new SQLiteAsyncConnection(dbPath);
            database.CreateTableAsync<RoundingForDisplay>().Wait();
        }

        public Task <RoundingForDisplay> GetRoundingForDIsplay(int id)
        {
            return database.Table<RoundingForDisplay>().Where(rd => rd.RoundingId == id).FirstOrDefaultAsync();
        }


        public Task<int> SaveRoundingDisplayAsync(RoundingForDisplay roundingForDisplay)
        {
            if (roundingForDisplay.RoundingId == 0)
            {
                return database.InsertAsync(roundingForDisplay);
            }
            else
            {
                return database.UpdateAsync(roundingForDisplay);
            }    
        }

        public Task<int> DeleteRoundingDisplayAsync(RoundingForDisplay roundingForDisplay)
        {
            return database.DeleteAsync(roundingForDisplay);
        }
    }
}
