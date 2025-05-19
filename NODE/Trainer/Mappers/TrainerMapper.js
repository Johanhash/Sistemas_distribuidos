const { Trainer, Medal, MedalType } = require('../Models/Trainer');
const TrainerDocument = require('../Infrastructure/Documents/TrainerDocument');
const { MedalDocument } = require('../Infrastructure/Documents/MedalDocument');
const { Timestamp } = require('google-protobuf/google/protobuf/timestamp_pb.js');

// Convierte un documento de Mongo a un modelo de dominio
function toModelFromDocument(doc) {
  if (!doc) return null;
  return new Trainer({
    id: doc._id?.toString(),
    name: doc.name,
    age: doc.age,
    birthdate: doc.birthdate,
    createdAt: doc.created_at,
    medals: doc.medals.map(m => new Medal(m.region, m.type))
  });
}

// Convierte un modelo a un documento para guardar en Mongo
function toDocumentFromModel(trainer) {
  return new TrainerDocument({
    id: trainer.id,
    name: trainer.name,
    age: trainer.age,
    birthdate: trainer.birthdate,
    createdAt: trainer.createdAt,
    medals: trainer.medals.map(m => new MedalDocument(m.region, m.type))
  });
}

// Convierte un modelo a una respuesta gRPC (TrainerResponse)
function toResponse(trainer) {
  const birthTs = Timestamp.fromDate(trainer.birthdate);
  const createdTs = Timestamp.fromDate(trainer.createdAt);

  return {
    id: trainer.id,
    name: trainer.name,
    age: trainer.age,
    birthdate: birthTs,
    createdAt: createdTs,
    medals: trainer.medals.map(m => ({
      region: m.region,
      type: m.type
    }))
  };
}

// Convierte un CreateTrainerRequest a modelo de dominio
function toModelFromGrpcRequest(request) {
  const birthdate = new Date(request.birthdate.seconds * 1000);
  return new Trainer({
    name: request.name,
    age: request.age,
    birthdate,
    createdAt: new Date(),
    medals: request.medals.map(m => new Medal(m.region, m.type))
  });
}

module.exports = {
  toModelFromDocument,
  toDocumentFromModel,
  toResponse,
  toModelFromGrpcRequest
};
