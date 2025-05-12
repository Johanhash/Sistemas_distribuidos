const { v4: uuidv4 } = require('uuid');

// Convertir `Date` a formato de `google.protobuf.Timestamp`
function toTimestamp(date = new Date()) {
  const seconds = Math.floor(date.getTime() / 1000);
  const nanos = (date.getTime() % 1000) * 1e6;
  return { seconds, nanos };
}

function GetTrainer(call, callback) {
  const response = {
    id: uuidv4(),
    name: "Johan",
    age: 22,
    birthdate: toTimestamp(),
    createdAt: toTimestamp(),
    medals: [
      { region: "MX", type: "GOLD" },   
      { region: "JPN", type: "SILVER" },
      { region: "USA", type: "BRONZE" }
    ]
  };

  callback(null, response);
}

module.exports = {
  GetTrainer
};
