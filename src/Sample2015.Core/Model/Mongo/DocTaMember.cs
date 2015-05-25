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

    public class DocTaMember
    {
        public DocTaMember()
        {
            this.DateGeminiCreated = DateTime.Now;
            this.ValidEmail = StatusValid.Unknown;
            this.ValidCellphone = StatusValid.Unknown;
            this.ValidWechat = StatusValid.Unknown;
        }

        public enum StatusValid : int
        {
            Okay = 0,
            Invalid = -1,
            Empty = -2,
            Unknown = -99
        }

        public ObjectId Id { get; set; }

        public string Location { get; set; }

        public string CellPhone { get; set; }

        [BsonSerializer(typeof(Sample2015DateTimeNullableSerializer))]
        public DateTime? Birthday { get; set; }

        public string Wechat { get; set; }

        public string Uid { get; set; }

        public string Phone { get; set; }

        [BsonSerializer(typeof(StringOrInt32Serializer))]
        public string Gender { get; set; }

        public string GmnGender
        {
            get { return this.GetGenderDisplay(this.Gender); }
        }

        public string Email { get; set; }

        public string MemberId { get; set; }

        public string Name { get; set; }

        public string Area { get; set; }

        public string Address { get; set; }

        public int GmnValidEmail
        {
            get { return (int)this.ValidEmail; }
            set { this.ValidEmail = FromInt(value); }
        }

        public int GmnValidWechat
        {
            get { return (int)this.ValidWechat; }
            set { this.ValidWechat = FromInt(value); }
        }

        public int GmnValidCellphone
        {
            get { return (int)this.ValidCellphone; }
            set { this.ValidCellphone = FromInt(value); }
        }

        [BsonSerializer(typeof(Sample2015DateTimeNullableSerializer))]
        public DateTime? DateCreated { get; set; }

        [BsonSerializer(typeof(Sample2015DateTimeNullableSerializer))]
        public DateTime? DateModified { get; set; }

        [BsonSerializer(typeof(Sample2015DateTimeNullableSerializer))]
        public DateTime? DateGeminiCreated { get; set; }

        [BsonSerializer(typeof(Sample2015DateTimeNullableSerializer))]
        public DateTime? DateGeminiModified { get; set; }

        [BsonIgnore]
        public StatusValid ValidEmail { get; set; }

        [BsonIgnore]
        public StatusValid ValidWechat { get; set; }

        [BsonIgnore]
        public StatusValid ValidCellphone { get; set; }

        public static StatusValid FromInt(int i)
        {
            StatusValid s = StatusValid.Unknown;
            switch (i)
            {
                case (int)StatusValid.Okay:
                    s = StatusValid.Okay;
                    break;
                case (int)StatusValid.Invalid:
                    s = StatusValid.Invalid;
                    break;
                case (int)StatusValid.Empty:
                    s = StatusValid.Empty;
                    break;
            }

            return s;
        }

        public override string ToString()
        {
            return string.Format("TaMember({0}) memberId:{1}", this.Id, this.MemberId);
        }

        private string GetGenderDisplay(string gender)
        {
            if (!string.IsNullOrEmpty(gender))
            {
                if (gender.Equals("0"))
                {
                    return "女";
                }
                else if (gender.Equals("1"))
                {
                    return "男";
                }
                else
                {
                    return "未定義";
                }
            }

            return gender;
        }
    }
}
