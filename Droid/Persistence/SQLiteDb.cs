using System;
using System.IO;
using SQLite;
using Xamarin.Forms;
using JayCadSurveyXamarin.Persistence;

[assembly: Dependency(typeof(JayCadSurveyXamarin.Droid.Persistence.SQLiteDb))]

namespace JayCadSurveyXamarin.Droid.Persistence
{
	public class SQLiteDb : ISQLiteDb
	{
		public SQLiteAsyncConnection GetConnection()
		{
			var documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
			var path = Path.Combine(documentsPath, "MySQLite.db3");

			return new SQLiteAsyncConnection(path);
		}
	}
}