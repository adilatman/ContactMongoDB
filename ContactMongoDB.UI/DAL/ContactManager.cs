using ContactMongoDB.UI.Models;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContactMongoDB.UI.DAL
{
    public class ContactManager
    {
        private readonly IMongoCollection<Contact> mongoCollection;

        public ContactManager(string mongoDbConnectionString, string dbName, string collectionName)
        {
            var client = new MongoClient(mongoDbConnectionString);
            var database = client.GetDatabase(dbName);
            mongoCollection = database.GetCollection<Contact>(collectionName);
        }
        public List<Contact> GetList()
        {
            return mongoCollection.Find(book => true).ToList();
        }
        public Contact GetById(string Id)
        {
            var docId = new ObjectId(Id);
            return mongoCollection.Find<Contact>(m => m.Id == docId).FirstOrDefault();
        }
        public Contact Create(Contact model)
        {
            mongoCollection.InsertOne(model);
            return model;
        }
        public void Update(string id, Contact model)
        {
            var docId = new ObjectId(id);
            mongoCollection.ReplaceOne(m => m.Id == docId, model);
        }
        public void Delete(string id)
        {
            var docId = new ObjectId(id);
            mongoCollection.DeleteOne(m=>m.Id==docId);
        }
    }
}
