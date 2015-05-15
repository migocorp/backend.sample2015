namespace Sample2015.Core.Helper.Mongo
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using MongoDB.Bson;
    using MongoDB.Bson.Serialization;
    using MongoDB.Bson.Serialization.Serializers;

    public class Sample2015ArrayOfObjectsToListSerializer : IBsonArraySerializer
    {
        public Type ValueType
        {
            get { return typeof(List<object>); }
        }

        public bool TryGetItemSerializationInfo(out BsonSerializationInfo serializationInfo)
        {
            serializationInfo = null;
            return false;
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
                case BsonType.Array:
                    break;
                default:
                    var message = string.Format("Cannot deserialize from BsonType {0}.", bsonType);
                    throw new BsonSerializationException(message);
            }

            var ser = new ArraySerializer<object>();
            var items = (object[])ser.Deserialize(context, args);
            var dataList = new List<object>();
            foreach (var item in items)
            {
                dataList.Add(item);
            }

            return dataList;
        }

        public void Serialize(BsonSerializationContext context, BsonSerializationArgs args, object value)
        {
            var bsonWriter = context.Writer;
            if (value != null)
            {
            }
            else
            {
                bsonWriter.WriteNull();
            }
        }
    }
}
