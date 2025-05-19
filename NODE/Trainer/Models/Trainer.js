const MedalType = {
  Unknown: 0,
  Gold: 1,
  Silver: 2,
  Bronze: 3
};

class Medal {
  constructor(region, type = MedalType.Unknown) {
    this.region = region;
    this.type = type;
  }
}

class Trainer {
  constructor({ id, name, age, birthdate, medals, createdAt }) {
    this.id = id;
    this.name = name;
    this.age = age;
    this.birthdate = birthdate instanceof Date ? birthdate : new Date(birthdate);
    this.medals = (medals || []).map(m => new Medal(m.region, m.type));
    this.createdAt = createdAt instanceof Date ? createdAt : new Date(createdAt);
  }
}

module.exports = {
  Trainer,
  Medal,
  MedalType
};
