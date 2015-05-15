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

    public class Sample2015DateTimeSerializer : DateTimeSerializer
    {
        public override void Serialize(BsonSerializationContext context, BsonSerializationArgs args, DateTime value)
        {
            var dt = (DateTime)value;
            var utcValue = new DateTime(dt.Ticks, DateTimeKind.Utc);
            base.Serialize(context, args, utcValue);
        }

        public override DateTime Deserialize(BsonDeserializationContext context, BsonDeserializationArgs args)
        {
            var bsonReader = context.Reader;
            var bsonType = bsonReader.CurrentBsonType;
            switch (bsonType)
            {
                case BsonType.DateTime:
                    var obj = base.Deserialize(context, args);
                    var dt = (DateTime)obj;
                    return new DateTime(dt.Ticks, DateTimeKind.Unspecified);
                default:
                    var message = string.Format("Sample2015DateTimeSerializer needs a DateTime not {0}.", bsonType);
                    throw new BsonSerializationException(message);
            }
        }
    }
}
