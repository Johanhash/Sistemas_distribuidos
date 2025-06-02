const buildTrainerService = require('./Services/TrainerService');
const repository = require('./Repositories/TrainerRepository');

const trainerService = buildTrainerService(repository);

server.addService(proto.TrainerService.service, trainerService);

