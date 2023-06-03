using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructures.QueueStack;
internal class MyStack<T>
{
    private const int DEFAULT_SIZE = 50;
    private T[] elements;
    private int top = -1;

    public MyStack(int initalSize = DEFAULT_SIZE)
    {
        elements = new T[initalSize];
    }

    public void Push(T item)
    {
        if (top == elements.Length - 1)
        {
            Extend();
        }

        top++;
        elements[top] = item;
    }

    public T Pop()
    {
        T item = elements[top];
        elements[top--] = default;

        if (top > 0 && top == elements.Length / 4)
        {
            Shink();
        }

        return item;
    }

    private void Extend()
    {
        // 1, 2
        // 1, 2, 0, 0
        var newArray = new T[elements.Length * 2];
        Array.Copy(elements, newArray, elements.Length);
        elements = newArray;
    }

    private void Shink()
    {
        // 1, 2, 0, 0
        // 1, 2

        var newArray = new T[elements.Length / 2];
        Array.Copy(elements, 0, newArray, 0, top + 1);
        elements = newArray;
    }
}
