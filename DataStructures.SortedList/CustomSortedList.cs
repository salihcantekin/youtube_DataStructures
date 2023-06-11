using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructures.SortedList;
public class CustomSortedList<TKey, TValue>:
    IEnumerable<KeyValuePair<TKey, TValue>>
{
    /*
        KEYS ->   0,     1,     4,     5,     0,    0
        VALUES -> "Zero, "One", "Four", "Five" null, null
     */

    private const int DEFAULT_CAPACITY = 100;
    private TKey[] keys;
    private TValue[] values;
    private int count;

    public CustomSortedList() : this(DEFAULT_CAPACITY) { }

    public CustomSortedList(int capacity)
    {
        keys = new TKey[capacity];
        values = new TValue[capacity];
    }

    public void Add(TKey key, TValue value)
    {
        if (count == keys.Length)
        {
            throw new InvalidOperationException("SortedList capacity exceeded");
        }

        var i = Array.BinarySearch(keys, 0, count, key);

        if (i > 0)
            throw new ArgumentException("An element with the same key already exists in the SortedList");

        int insertIndex = ~i;

        Array.Copy(keys, insertIndex, keys, insertIndex + 1, count - insertIndex); ;
        Array.Copy(values, insertIndex, values, insertIndex + 1, count - insertIndex); ;

        keys[insertIndex] = key;
        values[insertIndex] = value;
        count++;
    }

    public bool Remove(TKey key)
    {
        int i = Array.BinarySearch(keys, 0, count, key);
        if (i < 0)
            return false;

        Array.Copy(keys, i + 1, keys, i, count - i - 1);
        Array.Copy(values, i + 1, values, i, count - i - 1);
        count--;

        return true;
    }

    public bool ContainsKey(TKey key)
    {
        return Array.BinarySearch(keys, 0, count, key) >= 0;
    }

    public int IndexofValue(TValue value)
    {
        return Array.IndexOf(values, value, 0, count);
    }

    public bool TryGetValue(TKey key, out TValue value)
    {
        int i = Array.BinarySearch(keys, 0, count, key);
        if (i < 0)
        {
            value = default;
            return false;
        }

        value = values[i];
        return true;
    }

    public void Clear()
    {
        Array.Clear(keys);
        Array.Clear(values);
        count = 0;
    }

    public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
    {
        for (int i = 0; i < count; i++)
        {
            yield return new KeyValuePair<TKey, TValue>(keys[i], values[i]);
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public int Count => count;
}
