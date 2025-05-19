const { toResponse, toModelFromGrpcRequest } = require('../Mappers/TrainerMapper');
const repository = require('../Repositories/TrainerRepository');
const grpc = require('@grpc/grpc-js');

async function GetTrainer(call, callback) {
  try {
    const id = call.request.id;
    const trainer = await repository.getById(id);
    if (!trainer) {
      return callback({
        code: grpc.status.NOT_FOUND,
        message: "Trainer not found"
      });
    }
    return callback(null, toResponse(trainer));
  } catch (error) {
    console.error("Error in GetTrainer:", error);
    return callback({
      code: grpc.status.INTERNAL,
      message: "Internal server error"
    });
  }
}

async function CreateTrainer(call, callback) {
  const created = [];

  try {
    for await (const request of call) {
      const trainer = toModelFromGrpcRequest(request);

      const exists = await repository.getByName(trainer.name);
      if (exists.length > 0) continue;

      const newTrainer = await repository.create(trainer);
      created.push(toResponse(newTrainer));
    }

    return callback(null, {
      successCount: created.length,
      trainers: created
    });
  } catch (error) {
    console.error("Error in CreateTrainer:", error);
    return callback({
      code: grpc.status.INTERNAL,
      message: "Failed to create trainers"
    });
  }
}

module.exports = {
  GetTrainer,
  CreateTrainer
};
