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

    public class ExampleReduction
    {
        public static void Main()
        {
            var client = new MongoClient(KitConfig.GetDbMongoConnectString());
            var db = client.GetDatabase("mydb");

            string source = "source100";
            string target = "result25";
            string exclude = "exclude25";
            ExampleReduction reduction = new ExampleReduction();
            var timeStart = DateTime.Now;
            var count = reduction.RunCommand(db, source, target, exclude).Result;
            /* var count = reduction.RunProgramming(db, source, target, exclude).Result; */
            var timeEnd = DateTime.Now;

            Console.WriteLine("total time take: " + (timeEnd - timeStart));
            Console.WriteLine("after reduction: " + count + " members.");
            Console.ReadKey();
        }

        private async Task<long> RunCommand(IMongoDatabase db, string source, string target, string exclude)
        {
            string reductionJsFunction = @"
                function(source, target, exclude) {
                    var sourceCol = db.getCollection(source);
                    sourceCol.copyTo(target);
                    var targetCol = db.getCollection(target);
                    targetCol.createIndex( { MemberId: 1 } );
            
                    var excludeCol = db.getCollection(exclude);
                    var ids = excludeCol.distinct('MemberId');
                                
                    var bulk = targetCol.initializeUnorderedBulkOp();
                    bulk.find({MemberId: {$in: ids}}).remove();
                    bulk.execute();
            
                    return targetCol.count();
                }";

            var result = await db.RunCommandAsync(new BsonDocumentCommand<BsonDocument>(
                new BsonDocument(
                    new Dictionary<string, object>
                        {
                            { "eval", reductionJsFunction },
                            { "args", new string[] { source, target, exclude } }
                        }))).ConfigureAwait(continueOnCapturedContext: false);

            return result.ElementAt(0).Value.ToInt64();
        }

        private async Task<long> RunProgramming(IMongoDatabase db, string source, string target, string exclude)
        {
            // 1. source copyTo target collection
            var sourceCol = db.GetCollection<DocTaMember>(source);
            var targetCol = db.GetCollection<DocTaMember>(target);
            var docs = await sourceCol.FindAsync(new BsonDocument()).ConfigureAwait(continueOnCapturedContext: false);
            await targetCol.InsertManyAsync(await docs.ToListAsync().ConfigureAwait(continueOnCapturedContext: false)).ConfigureAwait(continueOnCapturedContext: false);
            var index = await targetCol.Indexes.CreateOneAsync(Builders<DocTaMember>.IndexKeys.Ascending(_ => _.MemberId)).ConfigureAwait(continueOnCapturedContext: false);

            // 2. 找排除id
            var excludeCol = db.GetCollection<DocTaMember>(exclude);
            var ids = await excludeCol.DistinctAsync<string>("MemberId", new BsonDocument()).ConfigureAwait(continueOnCapturedContext: false);

            // 3. delete ids
            var filter = new BsonDocument("MemberId", new BsonDocument("$in", BsonValue.Create(await ids.ToListAsync().ConfigureAwait(continueOnCapturedContext: false))));
            /* var expectedRequest = new DeleteRequest(filter) { CorrelationId = 0, Limit = 0 };
            var operationResult = new BulkWriteOperationResult.Unacknowledged(9, new[] { expectedRequest });
            await Task.FromResult(operationResult); */
            await targetCol.DeleteManyAsync(filter, CancellationToken.None).ConfigureAwait(continueOnCapturedContext: false);

            var result = await targetCol.CountAsync(new BsonDocument()).ConfigureAwait(continueOnCapturedContext: false);

            return result;
        }
    }
}
