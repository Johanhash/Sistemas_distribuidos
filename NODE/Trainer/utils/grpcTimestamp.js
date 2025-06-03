// utils/grpcTimestamp.js
const { Timestamp } = require('google-protobuf/google/protobuf/timestamp_pb');

/**
 * Convierte un objeto Date válido a Timestamp de Protobuf
 */
function toGrpcTimestamp(date) {
  const timestamp = new Timestamp();
  timestamp.setSeconds(Math.floor(date.getTime() / 1000));
  timestamp.setNanos((date.getTime() % 1000) * 1e6);
  return timestamp;
}

/**
 * Versión segura que retorna undefined si el valor no es una Date válida
 */
function safeToGrpcTimestamp(date) {
  if (!(date instanceof Date) || isNaN(date)) return undefined;
  return toGrpcTimestamp(date);
}

module.exports = {
  toGrpcTimestamp,
  safeToGrpcTimestamp
};
