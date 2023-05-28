using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Engines;

namespace DataStructures.LinkedList;
[GroupBenchmarksBy(BenchmarkLogicalGroupRule.ByCategory), MemoryDiagnoser]
[SimpleJob(RunStrategy.ColdStart)]
public class CustomLinkedListBenchmarks_ADD
{
    private CustomLinkedList<int> linkedList;
    private List<int> list;
    private List<int> fixedLenList;
    private IEnumerable<int> numbers;

    [Params(100, 1000)]
    public int NumberCount { get; set; }

    [GlobalSetup]
    public void Setup()
    {
        numbers = Enumerable.Range(0, NumberCount);

        linkedList = new CustomLinkedList<int>();
        list = new List<int>();
        fixedLenList = new List<int>(numbers.Count());
    }


    #region AddFirst

    [BenchmarkCategory("Add_First"), Benchmark(Baseline = true)]
    public void LinkedList_AddFirst()
    {
        foreach (var number in numbers)
        {
            linkedList.AddFirst(number);
        }
    }

    [BenchmarkCategory("Add_First"), Benchmark]
    public void List_AddFirst()
    {
        foreach (var number in numbers)
        {
            list.Insert(0, number);
        }
    }

    [BenchmarkCategory("Add_First"), Benchmark]
    public void FixedLenList_AddFirst()
    {
        foreach (var number in numbers)
        {
            fixedLenList.Insert(0, number);
        }
    }

    #endregion

    #region AddMiddle

    [BenchmarkCategory("Add_Middle"), Benchmark(Baseline = true)]
    public void LinkedList_AddMiddle()
    {
        foreach (var number in numbers)
        {
            linkedList.AddMiddle(number);
        }
    }

    [BenchmarkCategory("Add_Middle"), Benchmark]
    public void List_AddMiddle()
    {
        foreach (var number in numbers)
        {
            var middleIndex = list.Count() / 2;
            list.Insert(middleIndex, number);
        }
    }

    [BenchmarkCategory("Add_Middle"), Benchmark]
    public void FixedLenList_AddMiddle()
    {
        foreach (var number in numbers)
        {
            var middleIndex = fixedLenList.Count() / 2;
            fixedLenList.Insert(middleIndex, number);
        }
    }

    #endregion

    #region AddLast

    [BenchmarkCategory("Add_Last"), Benchmark(Baseline = true)]
    public void LinkedList_AddLast()
    {
        foreach (var number in numbers)
        {
            linkedList.AddLast(number);
        }
    }

    [BenchmarkCategory("Add_Last"), Benchmark]
    public void List_AddLast()
    {
        foreach (var number in numbers)
        {
            list.Add(number);
        }
    }

    [BenchmarkCategory("Add_Last"), Benchmark]
    public void FixedLenList_AddLast()
    {
        foreach (var number in numbers)
        {
            fixedLenList.Add(number);
        }
    }

    #endregion

    #region AddLast Bulk

    [BenchmarkCategory("Add_LastBulk"), Benchmark(Baseline = true)]
    public void LinkedList_AddLastBulk()
    {
        linkedList.AddLast(numbers);
    }

    [BenchmarkCategory("Add_LastBulk"), Benchmark]
    public void List_AddLastBulk()
    {
        list.AddRange(numbers);
    }

    [BenchmarkCategory("Add_LastBulk"), Benchmark]
    public void FixedLenList_AddLastBulk()
    {
        fixedLenList.AddRange(numbers);
    }

    #endregion
}
