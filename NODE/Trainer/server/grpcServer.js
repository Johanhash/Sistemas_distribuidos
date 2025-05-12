const grpc = require('@grpc/grpc-js');
const protoLoader = require('@grpc/proto-loader');
const path = require('path');
const trainerService = require('./trainer.service');

const PROTO_PATH = path.join(__dirname, '../proto/trainer.proto');
const packageDef = protoLoader.loadSync(PROTO_PATH, {
  keepCase: true,
  enums: String,
  longs: String,
  defaults: true,
  oneofs: true,
});

const proto = grpc.loadPackageDefinition(packageDef);
const trainerpb = proto.trainerpb;

function startGrpcServer() {
  const server = new grpc.Server();
  server.addService(trainerpb.TrainerService.service, trainerService);
  const port = '0.0.0.0:5051';

  server.bindAsync(port, grpc.ServerCredentials.createInsecure(), () => {
    console.log(`GRPC server running on ${port}`);
    server.start();
  });
}

module.exports = startGrpcServer;
