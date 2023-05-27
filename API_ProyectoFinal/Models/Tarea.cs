using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace API_ProyectoFinal.Models
{
    public class Tarea
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        [BsonElement ("nombre")]
        public string Nombre { get; set; }

        [BsonElement("carnet")]
        public string Carnet { get; set; }
        [BsonElement("resumen")]
        public string Resumen { get; set; }

        [BsonElement("nota")]
        public string Nota { get; set; }

        [BsonElement("tema")]
        public string? Tema { get; set; }

        [BsonElement("categoria")]
        public string? Categoria { get; set; }
    }
}
