using BenchmarkDotNet.Running;
using DataStructures.LinkedList;

// Data Structures => LinkedList


BenchmarkRunner.Run<CustomLinkedListBenchmarks_REMOVE>();

return;

CustomLinkedList<int> list = new();

list.AddFirst(1);
list.AddLast(2);
list.AddLast(new[] { 3, 4, 5 });
list.AddMiddle(0);

Print();


Console.ReadLine();






void Print()
{
    foreach (var item in list)
    {
        Console.WriteLine(item);
    }
}

