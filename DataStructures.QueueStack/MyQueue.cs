using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructures.QueueStack;
public class MyQueue<T>
{
    private const int DEFAULT_SIZE = 50;
    private T[] elements;
    private int head = 0;
    private int tail = - 1;
    private int count = 0;

    // 0, 2, 3, 0, 0
    public MyQueue(int initialSize = DEFAULT_SIZE)
    {
        elements = new T[initialSize];
    }

    public void Enqueue(T item)
    {
        if (count == elements.Length)
        {
            Extend();
        }

        tail++;
        elements[tail] = item;
        count++;
    }

    public T Dequeue()
    {
        ThrowIfEmpty();

        T item = elements[head];
        elements[head] = default;
        head++;
        count--;

        if (count > 0 && count == elements.Length / 4)
        {
            Shrink();
        }

        return item;
    }

    private void ThrowIfEmpty()
    {
        if (IsEmpty())
        {
            throw new InvalidOperationException("Queue is empty");
        }
    }

    public bool IsEmpty()
    {
        return count == 0;
    }

    private void Extend()
    {
        /*
            OLD -> 1, 2, 3          // Count = 3, head = 0, tail = 2
            NEW -> 1, 2, 3, 0, 0, 0 // Count = 3, head = 0, tail = 2
        */

        Array.Resize(ref elements, elements.Length * 2);
        head = 0;
        tail = count - 1;
    }

    public void Shrink()
    {
        /*
            OLD -> 0, 0, 0, 1, 2, 3 // count = 3, head = 2, tail = 5
            NEW -> 1, 2, 3          // count = 3, head = 0, tail = 2
        */

        int capacity = elements.Length / 2;
        var newArray = new T[capacity];

        Array.Copy(elements, head, newArray, 0, count);
        elements = newArray;

        head = 0;
        tail = count - 1;
    }
}
