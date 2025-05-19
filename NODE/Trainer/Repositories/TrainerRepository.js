const { ObjectId } = require('mongodb');
const { toModelFromDocument, toDocumentFromModel } = require('../Mappers/TrainerMapper');
const { connectionString, databaseName, trainerCollectionName } = require('../Infrastructure/MongoDBSettings');
const { MongoClient } = require('mongodb');

let collection;

async function init() {
  if (!collection) {
    const client = await MongoClient.connect(connectionString);
    const db = client.db(databaseName);
    collection = db.collection(trainerCollectionName);
  }
}

// GET by ID
async function getById(id) {
  await init();
  const doc = await collection.findOne({ _id: new ObjectId(id) });
  return toModelFromDocument(doc);
}

// CREATE
async function create(trainer) {
  await init();
  const doc = toDocumentFromModel(trainer);
  await collection.insertOne(doc);
  return toModelFromDocument(doc);
}

// GET by Name (contains)
async function getByName(name) {
  await init();
  const regex = new RegExp(name, 'i');
  const docs = await collection.find({ name: { $regex: regex } }).toArray();
  return docs.map(toModelFromDocument);
}

module.exports = {
  getById,
  create,
  getByName
};
