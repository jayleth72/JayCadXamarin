using System;
using System.Collections.Generic;
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

        /// <summary>
        /// Returns a List of RoundingDisplays
        /// </summary>
        /// <returns>The rounding for display list.</returns>
        public Task <List<RoundingForDisplay>> GetRoundingForDisplayList()
        {
            return database.Table<RoundingForDisplay>().ToListAsync();
        }

        /// <summary>
        /// Returns a RoundingDisplay for a specified ID
        /// </summary>
        /// <returns>The rounding for DI splay.</returns>
        /// <param name="id">ID for a RoundingDisplay.</param>
        public Task <RoundingForDisplay> GetRoundingForDIsplay(int id)
        {
            return database.Table<RoundingForDisplay>().Where(rd => rd.RoundingId == id).FirstOrDefaultAsync();
        }

        /// <summary>
        /// Returns a RoundingDisplay for a specfied name
        /// </summary>
        /// <returns>The rounding for DI splay from name.</returns>
        /// <param name="roundingName">Rounding name.</param>
        public Task<RoundingForDisplay> GetRoundingForDIsplayFromName(string roundingName)
        {
            return database.Table<RoundingForDisplay>().Where(rd => rd.RoundingName == roundingName).FirstOrDefaultAsync();
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

        /// <summary>
        /// Returns the number of rows in the RoundingDisplay table
        /// </summary>
        /// <returns>The rounding display count.</returns>
        public Task<int> GetRoundingDisplayCount()
        {
			return database.Table<RoundingForDisplay>().CountAsync();
        }
    }
}
