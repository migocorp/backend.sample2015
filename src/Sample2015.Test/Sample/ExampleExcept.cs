namespace Sample2015.Test.Sample
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using MongoDB.Bson;
    using MongoDB.Driver;
    using Sample2015.Core.Helper;
    using Sample2015.Core.Model.EF;
    using Sample2015.Core.Model.Mongo;

    public class ExampleExcept
    {
        public static void Main()
        {
            List<FakeTa> fakes = new List<FakeTa>();
            for (int i = 1; i <= 250000; i++)
            {
                fakes.Add(new FakeTa()
                    {
                        MemberId = (i * 4).ToString(),
                        CellPhone = (i * 4).ToString(),
                        Email = (i * 4).ToString() + "@yahoo.com",
                        Wechat = (i * 4).ToString()
                    });
            }

            var client = new MongoClient(KitConfig.GetDbMongoConnectString());
            var db = client.GetDatabase("mydb");
            ExampleExcept except = new ExampleExcept();
            string source = "source100";
            string target = "send25";
            string field = "Wechat";
            var timeStart = DateTime.Now;
            var count = except.RunProgramming(db, source, target, field, fakes.Select(f => f.CellPhone)).Result;
            var timeEnd = DateTime.Now;

            Console.WriteLine("total time take: " + (timeEnd - timeStart));
            Console.WriteLine("after except: " + count + " members.");
            Console.ReadKey();
        }

        private async Task<long> RunProgramming(IMongoDatabase db, string source, string target, string field, IEnumerable<string> ids)
        {
            var sourceCol = db.GetCollection<DocTaMember>(source);
            var targetCol = db.GetCollection<DocTaMember>(target);

            var filter = new BsonDocument(field, new BsonDocument("$nin", BsonValue.Create(ids)));
            var docs = await sourceCol.FindAsync(filter).ConfigureAwait(continueOnCapturedContext: false);
            await targetCol.InsertManyAsync(await docs.ToListAsync().ConfigureAwait(continueOnCapturedContext: false)).ConfigureAwait(continueOnCapturedContext: false);
            var index = await targetCol.Indexes.CreateOneAsync(Builders<DocTaMember>.IndexKeys.Combine(
                Builders<DocTaMember>.IndexKeys.Ascending(_ => _.MemberId),
                Builders<DocTaMember>.IndexKeys.Ascending(_ => _.CellPhone),
                Builders<DocTaMember>.IndexKeys.Ascending(_ => _.Email),
                Builders<DocTaMember>.IndexKeys.Ascending(_ => _.Wechat))).ConfigureAwait(continueOnCapturedContext: false);

            var result = await targetCol.CountAsync(new BsonDocument()).ConfigureAwait(continueOnCapturedContext: false);

            return result;
        }
    }
}
