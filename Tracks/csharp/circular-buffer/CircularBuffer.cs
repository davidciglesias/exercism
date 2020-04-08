using System;

public class CircularBuffer<T>
{
    private class CircularBufferIndexes
    {
        private readonly int capacity;
        public CircularBufferIndexes(int capacity) => this.capacity = capacity;

        private uint ToCapacity(uint index) => Convert.ToUInt32(index % capacity);

        private uint reading = 0;
        public uint Reading { get => reading; private set => reading = ToCapacity(value); }

        private uint writing = 0;
        public uint Writing { get => writing; private set => writing = ToCapacity(value); }

        private uint Filled { get; set; } = 0;

        public bool IsFull() => Filled == capacity;
        public bool IsEmpty() => Filled == 0;

        public void OverwriteWhenFull() => Writing = Reading;

        public void DidRead()
        {
            Reading++;
            Filled--;
        }
        public void DidWrite()
        {
            Writing++;
            Filled++;
        }
    }

    private readonly CircularBufferIndexes indexes;

    private readonly T[] buffer;


    public CircularBuffer(int capacity)
    {
        indexes = new CircularBufferIndexes(capacity);
        buffer = new T[capacity];
    }

    public T Read()
    {
        if (indexes.IsEmpty()) throw new InvalidOperationException();
        T read = buffer[indexes.Reading];
        Clear();
        return read;
    }

    public void Write(T value)
    {
        if (indexes.IsFull()) throw new InvalidOperationException();
        buffer[indexes.Writing] = value;
        indexes.DidWrite();
    }

    public void Overwrite(T value)
    {
        if (indexes.IsFull())
        {
            indexes.OverwriteWhenFull();
            Clear();
        }
        Write(value);
    }

    public void Clear()
    {
        if (!indexes.IsEmpty())
        {
            buffer[indexes.Reading] = default;
            indexes.DidRead();
        }
    }
}