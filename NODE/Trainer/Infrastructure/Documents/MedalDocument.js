const MedalType = {
  Unknown: 0,
  Gold: 1,
  Silver: 2,
  Bronze: 3
};

class MedalDocument {
  constructor(region, type = MedalType.Unknown) {
    this.region = region;
    this.type = type;
  }
}

module.exports = { MedalDocument, MedalType };

