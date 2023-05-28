using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Engines;

namespace DataStructures.LinkedList;
[GroupBenchmarksBy(BenchmarkLogicalGroupRule.ByCategory), MemoryDiagnoser]
[SimpleJob(RunStrategy.ColdStart, iterationCount: 50)]
public class CustomLinkedListBenchmarks_REMOVE
{
    private CustomLinkedList<int> linkedList;
    private List<int> list;
    private List<int> fixedLenList;
    private IEnumerable<int> numbers;
    private int middleValue;

    [Params(100, 1000)]
    public int NumberCount { get; set; }

    [GlobalSetup]
    public void Setup()
    {
        numbers = Enumerable.Range(0, NumberCount);

        linkedList = new CustomLinkedList<int>();
        list = new List<int>();
        fixedLenList = new List<int>(numbers.Count());

        linkedList.AddLast(numbers);
        list.AddRange(numbers);
        fixedLenList.AddRange(numbers);

        middleValue = numbers.Count() / 2;
    }

    #region RemoveFirst

    [BenchmarkCategory("Remove_First"), Benchmark(Baseline = true)]
    public void LinkedList_RemoveFirst()
    {
        linkedList.RemoveFirst();
    }

    [BenchmarkCategory("Remove_First"), Benchmark]
    public void List_RemoveFirst()
    {
        list.RemoveAt(0);
    }

    [BenchmarkCategory("Remove_First"), Benchmark]
    public void FixedLenList_RemoveFirst()
    {
        fixedLenList.RemoveAt(0);
    }

    #endregion

    #region RemoveLast

    [BenchmarkCategory("Remove_Last"), Benchmark(Baseline = true)]
    public void LinkedList_RemoveLast()
    {
        linkedList.RemoveLast();
    }

    [BenchmarkCategory("Remove_Last"), Benchmark]
    public void List_RemoveLast()
    {
        list.RemoveAt(list.Count - 1);
    }

    [BenchmarkCategory("Remove_Last"), Benchmark]
    public void FixedLenList_RemoveLast()
    {
        fixedLenList.RemoveAt(fixedLenList.Count - 1);
    }

    #endregion

    #region RemoveWithValue

    [BenchmarkCategory("Remove_Middle"), Benchmark(Baseline = true)]

    public void LinkedList_RemoveWithValue()
    {
        linkedList.RemoveWithValue(middleValue);
    }

    [BenchmarkCategory("Remove_Middle"), Benchmark]
    public void List_RemoveWithValue()
    {
        list.Remove(middleValue);
    }

    [BenchmarkCategory("Remove_Middle"), Benchmark]
    public void FixedLenList_RemoveWithValue()
    {
        fixedLenList.Remove(middleValue);
    }

    #endregion
}
