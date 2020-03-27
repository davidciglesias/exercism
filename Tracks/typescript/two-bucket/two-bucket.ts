export enum Bucket {
  One = "one",
  Two = "two"
}

type BucketDetail = { current: number; max: number };

export class TwoBucket {
  readonly goalBucket: Bucket;
  readonly otherBucket: number;
  private _moves: number;
  private readonly bucks: Map<Bucket, BucketDetail>;

  constructor(
    buckOne: number,
    buckTwo: number,
    goal: number,
    starterBuck: Bucket
  ) {
    this._moves = 0;
    this.bucks = new Map<Bucket, BucketDetail>([
      [Bucket.One, { current: 0, max: buckOne }],
      [Bucket.Two, { current: 0, max: buckTwo }]
    ]);
    const nonStarterBuck =
      (starterBuck === Bucket.One && Bucket.Two) || Bucket.One;
    do {
      if (this.isMax(nonStarterBuck)) {
        this.empty(nonStarterBuck);
      } else {
        this.fill(starterBuck);
      }
      this.move(starterBuck, nonStarterBuck);
    } while (this.get(starterBuck).current !== goal);

    this.goalBucket = starterBuck;
    this.otherBucket = this.get(nonStarterBuck).current;
  }

  private get = (bucket: Bucket) => this.bucks.get(bucket) as BucketDetail;

  private set = (bucket: Bucket, current: number) =>
    this.bucks.set(bucket, { ...this.get(bucket), current });

  private isMax = (bucket: Bucket) => {
    const bucketDetail = this.get(bucket);
    return bucketDetail.current === bucketDetail.max;
  };

  private fill = (bucket: Bucket) => {
    const currentBuck = this.get(bucket);
    this.set(bucket, currentBuck.max);
    this._moves++;
  };

  private move = (current: Bucket, other: Bucket) => {
    const currentBuck = this.get(current);
    const otherBuck = this.get(other);
    const result = currentBuck.current + otherBuck.current;
    const moved =
      result > otherBuck.max
        ? otherBuck.max - otherBuck.current
        : currentBuck.current;
    const remaining = currentBuck.current - moved;
    this.set(current, remaining);
    this.set(other, otherBuck.current + moved);
    this._moves++;
  };

  private empty = (bucket: Bucket) => {
    this.set(bucket, 0);
    this._moves++;
  };

  moves = (): number => this._moves;
}
