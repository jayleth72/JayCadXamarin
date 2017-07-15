using System;
using SQLite;

namespace JayCadSurveyXamarin.Persistence
{
    public interface ILocalFileHelper
    {
        string GetLocalFilePath(string fileName);
		
    }
}
