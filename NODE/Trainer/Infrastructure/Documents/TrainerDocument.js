const { MedalDocument } = require('./MedalDocument');

class TrainerDocument {
  constructor({ id, name, age, birthdate, createdAt, medals }) {
    this._id = id; // Mongo ID
    this.name = name;
    this.age = age;
    this.birthdate = birthdate instanceof Date ? birthdate : new Date(birthdate);
    this.created_at = createdAt instanceof Date ? createdAt : new Date(createdAt);
    this.medals = (medals || []).map(m => new MedalDocument(m.region, m.type));
  }
}

module.exports = TrainerDocument;

