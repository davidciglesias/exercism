using System;
using System.Collections.Generic;

public enum Bucket
{
    One,
    Two
}

public class TwoBucketResult
{
    public int Moves { get; set; }
    public Bucket GoalBucket { get; set; }
    public int OtherBucket { get; set; }
}

public class TwoBucket
{
    public Dictionary<Bucket, (int max, int current)> buckets = new Dictionary<Bucket, (int max, int current)>();
    public Bucket currentBucket;
    public int moves = 0;

    private Bucket OtherBucket(Bucket current) => current == Bucket.One ? Bucket.Two : Bucket.One;

    public TwoBucket(int bucketOne, int bucketTwo, Bucket startBucket)
    {
        buckets.Add(Bucket.One, (bucketOne, 0));
        buckets.Add(Bucket.Two, (bucketTwo, 0));
        currentBucket = startBucket;
    }

    private void Fill(Bucket bucket)
    {
        moves++;
        buckets[bucket] = (buckets[bucket].max, buckets[bucket].max);
    }

    private void Empty(Bucket bucket)
    {
        moves++;
        buckets[bucket] = (buckets[bucket].max, 0);
    }

    private void PassFrom(Bucket bucket)
    {
        var (_, current) = buckets[bucket];
        if (current != 0)
        {
            moves++;
            Bucket other = OtherBucket(bucket);
            var (otherMax, otherCurrent) = buckets[other];
            int passed = Math.Min(current, otherMax - otherCurrent);
            buckets[other] = (otherMax, otherCurrent + passed);
            buckets[bucket] = (buckets[bucket].max, buckets[bucket].current - passed);
        }
    }

    private void EmptyIfFull(Bucket bucket)
    {
        var (max, current) = buckets[bucket];
        if (max == current)
        {
            Empty(bucket);
        }
    }

    public TwoBucketResult Measure(int goal)
    {
        var otherBucket = OtherBucket(currentBucket);

        if (goal > buckets[currentBucket].max && goal > buckets[otherBucket].max) throw new ArgumentException("Sorry too many liters for these Buckets!");
        if (buckets[OtherBucket(currentBucket)].max == goal) return new TwoBucketResult { GoalBucket = OtherBucket(currentBucket), Moves = 2, OtherBucket = buckets[currentBucket].max };
        if (buckets[currentBucket].max == goal) return new TwoBucketResult { GoalBucket = currentBucket, Moves = 1, OtherBucket = buckets[OtherBucket(currentBucket)].current };

        do
        {
            Fill(currentBucket);
            do
            {
                PassFrom(currentBucket);
                if (buckets[otherBucket].current == goal) return new TwoBucketResult { GoalBucket = otherBucket, Moves = moves, OtherBucket = buckets[currentBucket].current };
                if (buckets[currentBucket].current == goal) return new TwoBucketResult { GoalBucket = currentBucket, Moves = moves, OtherBucket = buckets[otherBucket].current };
                EmptyIfFull(otherBucket);
            } while (buckets[currentBucket].current != 0);
        } while (!(buckets[currentBucket].current == 0 && buckets[otherBucket].current == 0));

        throw new ArgumentException("Sorry but it's impossible!");
    }
}
