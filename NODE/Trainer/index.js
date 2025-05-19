const grpc = require('@grpc/grpc-js');
const protoLoader = require('@grpc/proto-loader');
const path = require('path');
const trainerService = require('./Services/TrainerService');

const PROTO_PATH = path.join(__dirname, '/Protos/trainer.proto');

// Cargar definición .proto
const packageDefinition = protoLoader.loadSync(PROTO_PATH, {
  keepCase: true,
  longs: String,
  enums: String,
  defaults: true,
  oneofs: true
});
const proto = grpc.loadPackageDefinition(packageDefinition).trainerpb;

// Crear servidor gRPC
const server = new grpc.Server();

// Registrar servicios
server.addService(proto.TrainerService.service, {
  GetTrainer: trainerService.GetTrainer,
  CreateTrainer: trainerService.CreateTrainer
});

// Iniciar servidor
const PORT = 5051;
server.bindAsync(`0.0.0.0:${PORT}`, grpc.ServerCredentials.createInsecure(), (err, port) => {
  if (err) {
    console.error('Failed to start server:', err);
    return;
  }
  console.log(`Trainer gRPC service running on port ${port}`);
  server.start();
});
