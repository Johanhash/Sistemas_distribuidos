const { Trainer, Medal, MedalType } = require('../Models/Trainer');
const TrainerDocument = require('../Infrastructure/Documents/TrainerDocument');
const { MedalDocument } = require('../Infrastructure/Documents/MedalDocument');
const { toGrpcTimestamp } = require('../utils/grpcTimestamp');


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


function toResponse(trainer) {
  return {
    id: trainer.id,
    name: trainer.name,
    age: trainer.age,
    birthdate: toGrpcTimestamp(trainer.birthdate),
    createdAt: toGrpcTimestamp(trainer.createdAt),
    medals: trainer.medals.map(m => ({
      region: m.region,
      type: m.type
    }))
  };
}


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
