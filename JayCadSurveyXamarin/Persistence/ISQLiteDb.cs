using System;
using SQLite;

namespace JayCadSurveyXamarin.Persistence
{
    public interface ISQLiteDb
    {
        SQLiteAsyncConnection GetConnection();
    }
}
