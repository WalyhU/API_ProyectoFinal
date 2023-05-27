using API_ProyectoFinal.Models;
using MongoDB.Driver;

namespace API_ProyectoFinal
{
    public class Database
    {
        public static string ConnectionString = "mongodb+srv://walter:yaaoHGkCa3L3ZpiI@proyectofinal.ubpuzum.mongodb.net/?retryWrites=true&w=majority";

        public static string DatabaseName = "ProyectoFinal";

        public static string CollectionName = "Tareas";

        // Metodo que retorna la conexion
        public static IMongoCollection<Tarea> GetConnection()
        {
            var client = new MongoClient(ConnectionString);
            var database = client.GetDatabase(DatabaseName);
            var collection = database.GetCollection<Tarea>(CollectionName);
            return collection;
        }
    }
}
