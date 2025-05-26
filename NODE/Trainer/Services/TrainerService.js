const grpc = require('@grpc/grpc-js');
const { toResponse, toModelFromGrpcRequest } = require('../Mappers/TrainerMapper');

class TrainerService {
  constructor(repository) {
    this.repository = repository;
  }

  async GetTrainer(call, callback) {
    try {
      const id = call.request.id;
      const trainer = await this.repository.getById(id);

      if (!trainer) {
        return callback({
          code: grpc.status.NOT_FOUND,
          message: 'Trainer not found',
        });
      }

      return callback(null, toResponse(trainer));
    } catch (error) {
      console.error('Error in GetTrainer:', error);
      return callback({
        code: grpc.status.INTERNAL,
        message: 'Internal server error',
      });
    }
  }

  async CreateTrainer(call, callback) {
    const created = [];

    try {
      for await (const request of call) {
        const trainer = toModelFromGrpcRequest(request);

        const exists = await this.repository.getByName(trainer.name);
        if (exists.length > 0) continue;

        const newTrainer = await this.repository.create(trainer);
        created.push(toResponse(newTrainer));
      }

      return callback(null, {
        successCount: created.length,
        trainers: created,
      });
    } catch (error) {
      console.error('Error in CreateTrainer:', error);
      return callback({
        code: grpc.status.INTERNAL,
        message: 'Failed to create trainers',
      });
    }
  }

  async GetTrainersByName(call) {
    try {
      const name = call.request.name;

      if (!name || name.length <= 1) {
      const err = new Error('Name field is required');
      err.code = grpc.status.INVALID_ARGUMENT;
      return call.destroy(err);
    }


      const trainers = await this.repository.getByName(name);

      for (const trainer of trainers) {
        call.write(toResponse(trainer));
        await new Promise(resolve => setTimeout(resolve, 5000)); 
      }

      call.end();
    } catch (error) {
      console.error('Error in GetTrainersByName:', error);
      call.destroy(error);
    }
  }
}

module.exports = (repository) => new TrainerService(repository);