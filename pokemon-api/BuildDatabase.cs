using pokemon_api.DAOs;
using System.Data.Common;

namespace pokemon_api
{
    public class BuildDatabase
    {
        private readonly string DBConnection;
        private readonly string FilePath;

        public BuildDatabase(string connection, string filepath)
        {
            DBConnection = connection;
            FilePath = filepath;
        }

        public bool CheckForData()
        {
            FileDao fileDao = new FileDao(DBConnection, FilePath);
            return fileDao.HasData();
        }

        public void AddData()
        {
            FileDao fileDao = new FileDao(DBConnection, FilePath);
            fileDao.AddPokemonData();
        }
    }
}
