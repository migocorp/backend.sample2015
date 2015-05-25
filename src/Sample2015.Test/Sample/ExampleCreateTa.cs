namespace Sample2015.Test.Sample
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;
    using MongoDB.Bson;
    using MongoDB.Driver;
    using Sample2015.Core.Helper;
    using Sample2015.Core.Model.Mongo;

    public class ExampleCreateTa
    {
        public static void Main()
        {
            var client = new MongoClient(KitConfig.GetDbMongoConnectString());
            var db = client.GetDatabase("mydb");

            ExampleCreateTa creation = new ExampleCreateTa();
            /* var sourceCount = creation.CreateTa(db, 1000000, "source100", null).Result;
            Console.WriteLine("sourceCol: " + sourceCount); */
            var excludeCount = creation.CreateTa(db, 1000000, "exclude25", 4).Result;
            Console.WriteLine("excludeCol: " + excludeCount);

            Console.ReadKey();
        }

        private async Task<long> CreateTa(IMongoDatabase db, int total, string col, int? separator)
        {
            var mongoCol = db.GetCollection<DocTaMember>(col);
            List<DocTaMember> lst = new List<DocTaMember>();
            for (int i = 1; i <= total; i++)
            {
                if (separator != null)
                {
                    if (i % (int)separator == 0)
                    {
                        DocTaMember ta = new DocTaMember()
                        {
                            CellPhone = "1234567890",
                            Gender = (i % 2).ToString(),
                            MemberId = i.ToString(),
                            Name = "王王王",
                            Area = "宜兴",
                            Address = "山东省济南市市中区玉函路60号8-3-202",
                            DateGeminiCreated = DateTime.Now
                        };
                        lst.Add(ta);
                    }
                }
                else
                {
                    DocTaMember ta = new DocTaMember()
                    {
                        CellPhone = "1234567890",
                        Gender = (i % 2).ToString(),
                        MemberId = i.ToString(),
                        Name = "王王王",
                        Area = "宜兴",
                        Address = "山东省济南市市中区玉函路60号8-3-202",
                        DateGeminiCreated = DateTime.Now
                    };
                    lst.Add(ta);
                }

                if (lst.Count != 0 && lst.Count % 10000 == 0)
                {
                    await mongoCol.InsertManyAsync(lst).ConfigureAwait(continueOnCapturedContext: false);
                    Console.WriteLine("insert " + lst.Count);
                    lst.Clear();
                }
            }

            var index = await mongoCol.Indexes.CreateOneAsync(Builders<DocTaMember>.IndexKeys.Ascending(_ => _.MemberId)).ConfigureAwait(continueOnCapturedContext: false);
            var result = await mongoCol.CountAsync(new BsonDocument()).ConfigureAwait(continueOnCapturedContext: false);

            return result;
        }
    }
}
