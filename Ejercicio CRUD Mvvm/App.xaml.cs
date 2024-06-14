using Ejercicio_CRUD_Mvvm.Data;
using System.IO;

namespace Ejercicio_CRUD_Mvvm
{
    public partial class App : Application
    {
        private static Database database;

        public static Database Database
        {
            get
            {
                if (database == null)
                {
                    string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "EjercicioCRUDMvvm.db3");
                    database = new Database(dbPath);
                }
                return database;
            }
        }
        public App()
        {
            InitializeComponent();

            MainPage = new MainPage();
        }
    }
}
