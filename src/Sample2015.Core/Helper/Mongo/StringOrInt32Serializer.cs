namespace Sample2015.Core.Helper.Mongo
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using MongoDB.Bson;
    using MongoDB.Bson.Serialization;

    public sealed class StringOrInt32Serializer : IBsonSerializer
    {
        public Type ValueType
        {
            get { return typeof(string); }
        }

        public object Deserialize(BsonDeserializationContext context, BsonDeserializationArgs args)
        {
            var bsonReader = context.Reader;
            var bsonType = bsonReader.CurrentBsonType;
            switch (bsonType)
            {
                case BsonType.Null:
                    bsonReader.ReadNull();
                    return null;
                case BsonType.String:
                    return bsonReader.ReadString();
                case BsonType.Int32:
                    return bsonReader.ReadInt32().ToString(CultureInfo.InvariantCulture);
                default:
                    var message = string.Format("Cannot deserialize BsonString or BsonInt32 from BsonType {0}.", bsonType);
                    throw new BsonSerializationException(message);
            }
        }

        public void Serialize(BsonSerializationContext context, BsonSerializationArgs args, object value)
        {
            var bsonWriter = context.Writer;
            if (value != null)
            {
                bsonWriter.WriteString(value.ToString());
            }
            else
            {
                bsonWriter.WriteNull();
            }
        }
    }
}
