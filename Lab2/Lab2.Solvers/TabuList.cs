using System.Collections;

namespace Lab2.Solvers;

public class TabuList<T> : IReadOnlyCollection<T>
{
    private readonly Queue<T> _queue;
    private int _tenure;

    public TabuList(int tenure)
    {
        _tenure = tenure;
        _queue = new Queue<T>(tenure);
    }

    public int Tenure
    {
        get => _tenure;
        set
        {
            while (_queue.Count > value)
                _queue.Dequeue();

            _tenure = value;
        }
    }

    public bool TryAdd(T item)
    {
        if (_queue.Contains(item))
            return false;

        if (_queue.Count == _tenure)
            _queue.Dequeue();

        _queue.Enqueue(item);
        return true;
    }

    public bool Contains(T item) => _queue.Contains(item);

    public IEnumerator<T> GetEnumerator() => _queue.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => ((IEnumerable) _queue).GetEnumerator();

    public int Count => _queue.Count;
}
