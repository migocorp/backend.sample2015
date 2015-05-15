[module: System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1402:FileMayOnlyContainASingleClass", Justification = "Reviewed")]

namespace Sample2015.Core.Model.Mongo.Pandora
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using MongoDB.Bson;
    using MongoDB.Bson.Serialization.Attributes;
    using Sample2015.Core.Helper;
    using Sample2015.Core.Helper.Mongo;

    public class PandoRptKpiBasic
    {
        public ObjectId Id { get; set; }

        public string ShopID { get; set; }

        public string StoreID { get; set; }

        public string PeriodType { get; set; }

        [BsonElement("CalDate")]
        [BsonSerializer(typeof(Sample2015DateTimeNullableSerializer))]
        public DateTime? RptDate { get; set; }

        [BsonElement("PeriodRecords")]
        public RptPandoKpiBasicRecords Records { get; set; }

        public override string ToString()
        {
            return string.Format("RptPandoKpiBasic({0}) StoreID:{1}", KitStr.FormatDate(this.RptDate), this.StoreID);
        }
    }

    public class RptPandoKpiBasicRecords
    {
        [BsonElement("ValidChangeNumber")]
        [BsonSerializer(typeof(Sample2015ArrayOfObjectsToListSerializer))]
        public List<object> MemberCounts { get; set; }

        [BsonSerializer(typeof(Sample2015ArrayOfObjectsToListSerializer))]
        public List<object> ChurnRate { get; set; }

        [BsonSerializer(typeof(Sample2015ArrayOfObjectsToListSerializer))]
        public List<object> AddedRate { get; set; }

        [BsonSerializer(typeof(Sample2015ArrayOfObjectsToListSerializer))]
        public List<object> ConversionRate { get; set; }

        [BsonSerializer(typeof(Sample2015ArrayOfObjectsToListSerializer))]
        public List<object> Active { get; set; }

        [BsonElement("ARPU")]
        [BsonSerializer(typeof(Sample2015ArrayOfObjectsToListSerializer))]
        public List<object> Arpu { get; set; }

        [BsonSerializer(typeof(Sample2015ArrayOfObjectsToListSerializer))]
        public List<object> WakeUpRate { get; set; }
    }
}
