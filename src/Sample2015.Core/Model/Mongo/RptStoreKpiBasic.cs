namespace Sample2015.Core.Model.Mongo
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using MongoDB.Bson;
    using MongoDB.Bson.Serialization.Attributes;
    using Sample2015.Core.Helper.Mongo;

    public class RptStoreKpiBasic
    {
        public RptStoreKpiBasic()
        {
            this.DateCreated = DateTime.Now;
        }

        public ObjectId Id { get; set; }

        [BsonSerializer(typeof(Sample2015DateTimeSerializer))]
        public DateTime DateReport { get; set; }

        public string Period { get; set; }

        [BsonSerializer(typeof(Sample2015DateTimeSerializer))]
        public DateTime DateCreated { get; set; }

        public int? MemberCountN0 { get; set; }

        public int? MemberCountE0 { get; set; }

        public int? MemberCountS1 { get; set; }

        public int? MemberCountS2 { get; set; }

        public int? MemberCountS3 { get; set; }
    }
}
