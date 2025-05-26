const grpc = require('@grpc/grpc-js');
const protoLoader = require('@grpc/proto-loader');
const path = require('path');
const buildTrainerService = require('../Services/TrainerService');
const repository = require('../Repositories/TrainerRepository');

const packageDefinition = protoLoader.loadSync(
  path.join(__dirname, '../Protos/trainer.proto'),
  {
    keepCase: true,
    longs: String,
    enums: String,
    defaults: true,
    oneofs: true,
  }
);

const trainerProto = grpc.loadPackageDefinition(packageDefinition).trainerpb;

function startGrpcServer() {
  const server = new grpc.Server();
  const trainerService = buildTrainerService(repository);

  server.addService(trainerProto.TrainerService.service, trainerService);

  server.bindAsync('0.0.0.0:5051', grpc.ServerCredentials.createInsecure(), () => {
    server.start();
    console.log('Trainer gRPC service running on port 5051');
  });
}

module.exports = { startGrpcServer };
