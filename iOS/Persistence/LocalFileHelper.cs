using System;
using System.IO;
using Xamarin.Forms;
using JayCadSurveyXamarin.Persistence;

[assembly: Dependency(typeof(JayCadSurveyXamarin.iOS.Persistence.LocalFileHelper))]
namespace JayCadSurveyXamarin.iOS.Persistence
{
    public class LocalFileHelper : ILocalFileHelper
    {
        public string GetLocalFilePath(string fileName)
        {
            string documentsFolder = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            string libraryFolder = Path.Combine(documentsFolder, "..", "Library", "Databases");

            if (!Directory.Exists((libraryFolder)))
            {
                Directory.CreateDirectory(libraryFolder);
            }

            return Path.Combine(libraryFolder, fileName);
        }
    }
}
