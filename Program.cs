using MongoDB.Bson;
using MongoDB.Driver;
using System;

namespace Bus
{
  class Program
  {
    [Obsolete]
    static void Main(string[] args)
    {
      // To directly connect to a single MongoDB server
      // (this will not auto-discover the primary even if it's a member of a replica set)
      var client = new MongoClient();

      // Gets database Fred's bus DB
      var database = client.GetDatabase("fredsBusDB");

      // Gets collection Fred's bus collection
      var collection = database.GetCollection<BsonDocument>("fredsBusCollection");

      // Set up test BsonDocument to ensure database and collection are working. 
      // Run once and comment out.
      //
      //var testDocument = new BsonDocument
      //{
      //    { "name", "MongoDB" },
      //    { "type", "Database" },
      //    { "count", 1 },
      //    { "info", new BsonDocument
      //        {
      //            { "x", 203 },
      //            { "y", 102 }
      //        }}
      //};
      //collection.InsertOne(testDocument);

      //var testDocument2 = new BsonDocument
      //{
      //  { "name", "TestDocument" },
      //  { "type", "Document" },
      //  { "count", 2 },
      //  { "info", new BsonDocument
      //    {
      //      { "x", 36 },
      //      { "y", 63}
      //    }
      //  }
      //};
      //collection.InsertOne(testDocument2);

      // Find the document with a name equal to testDocument2.
      var equalsFilter = Builders<BsonDocument>.Filter.Eq("name", "TestDocument");
      var equalsDocument = collection.Find(equalsFilter).First();
      Console.WriteLine("After equals filter: " + equalsDocument + "\n");

      // Add new fields a and b to the test document but doesn't save the changes to the DB.
      equalsDocument = equalsDocument.Add("a", 2);
      equalsDocument = equalsDocument.Add("b", 4);
      Console.WriteLine("After adding fields a and b" + equalsDocument + "\n");

      // Add new field with fields but doesn't save the changes to the DB.
      equalsDocument = equalsDocument.Add("c", "{ z: 100 }");
      equalsDocument = equalsDocument.Add("d", "{ w: 120 }");
      Console.WriteLine("After adding fields c and d" + equalsDocument + "\n");

      // Add new fields a and b to the test document and does save the changes to the DB.
      collection.FindOneAndUpdate(equalsDocument);
      equalsDocument = equalsDocument.Add("b", 4);
      Console.WriteLine("After adding fields a and b" + equalsDocument + "\n");

      // Add new field with fields and does save the changes to the DB.
      equalsDocument = equalsDocument.Add("c", "{ z: 100 }");
      equalsDocument = equalsDocument.Add("d", "{ w: 120 }");


      // Find the document with a value greater than 50.
      //var greaterThanFilter = Builders<BsonDocument>.Filter.Gt("info", { "x", 50});
      //var greaterThanDocument = collection.Find(greaterThanFilter).First();
      //Console.WriteLine(greaterThanDocument);


    }
  }
}
