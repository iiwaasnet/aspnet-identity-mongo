namespace IntegrationTests
{
    using MongoDB.Bson;
    using MongoDB.Bson.Serialization.Attributes;

    [BsonIgnoreExtraElements]
    public class IndexDescription
    {
        [BsonElement("v")]
        public int Version { get; set; }

        [BsonElement("key")]
        public BsonDocument Key { get; set; }

        [BsonElement("unique")]
        public bool Unique { get; set; }
    }
}