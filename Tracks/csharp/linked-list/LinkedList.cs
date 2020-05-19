public class Node<T>
{
    public Node<T> Previous;
    public Node<T> Next;
    public readonly T Value;

    public Node(T value = default) => Value = value;
    public Node(T value, Node<T> previous, Node<T> next): this(value) => (Previous, Next) = (previous, next);

    public void Append(T value) => Next = Next.Previous = new Node<T>(value, this, Next);

    public T Retrieve()
    {
        Previous.Next = Next;
        Next.Previous = Previous;
        Next = Previous= null;
        return Value;
    }
}

public class Deque<T>
{
    private readonly Node<T> last = new Node<T>();

    public Deque() => last.Next = last.Previous = last;

    public void Push(T value) => last.Append(value);

    public T Pop() => last.Next.Retrieve();

    public void Unshift(T value) => last.Previous.Append(value);

    public T Shift() => last.Previous.Retrieve();
}