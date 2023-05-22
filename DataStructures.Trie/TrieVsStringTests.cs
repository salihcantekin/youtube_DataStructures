using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Configs;
using Bogus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructures.Trie;
[GroupBenchmarksBy(BenchmarkLogicalGroupRule.ByCategory), CategoriesColumn, MemoryDiagnoser]
public class TrieVsStringTests
{
    private HashSet<string> badWords_HashSet = null;
    private List<string> badWords_List = null;
    private string[] badWords_Arr = null;
    private string[] fileContent = null;
    private string wholeText;

    private int badWordsCount = 0;

    private IEnumerable<string> badwords;

    private Trie trie;
    private Faker faker;

    [Params(100_000)]
    public int FileContentWordCount { get; set; }

    [Params(1000, 100_000)]
    public int BadWordsCount { get; set; }

    [IterationSetup]
    public void Setup()
    {
        faker = new Faker("en");

        fileContent = Enumerable.Repeat(1, FileContentWordCount)
            .Select(i => faker.Lorem.Word())
            .Distinct()
            .ToArray();

        badwords = Enumerable.Repeat(1, BadWordsCount)
            .Select(i => faker.Lorem.Word())
            .Distinct()
            .ToArray();

        wholeText = string.Join(" ", badwords);

        //badWords_HashSet = badwords.ToHashSet();
        badWords_List = badwords.ToList();
        badWords_Arr = badwords.ToArray();

        trie = new Trie();

        badWords_List.ForEach(i => trie.Add(i));

        badWordsCount = badWords_Arr.Length;
    }


    #region Contains Methods

    [Benchmark(Baseline = true), BenchmarkCategory("Contains")]
    public void HashSet_Contains()
    {
        foreach (var word in fileContent)
        {
            _ = badWords_HashSet.Contains(word);
        }
    }

    [Benchmark]
    [BenchmarkCategory("Contains")]
    public void List_Contains()
    {
        foreach (var word in fileContent)
        {
            _ = badWords_List.Contains(word);
        }
    }

    [Benchmark]
    [BenchmarkCategory("Contains")]
    public void Array_Contains()
    {
        foreach (var word in fileContent)
        {
            _ = badWords_Arr.Contains(word);
        }
    }

    [Benchmark]
    [BenchmarkCategory("Contains")]
    public void Trie_Contains()
    {
        foreach (var word in fileContent)
        {
            _ = trie.Contains(word);
        }
    }

    [Benchmark]
    [BenchmarkCategory("Contains")]
    public void String_Contains()
    {
        foreach (var word in fileContent)
        {
            _ = wholeText.Contains(word);
        }
    }

    #endregion

    #region Add Methods
    [Benchmark(Baseline = true), BenchmarkCategory("Add")]
    public void HashSet_Add()
    {
        var hashSet = new HashSet<string>(capacity: badWordsCount);

        foreach (var word in badwords)
        {
            hashSet.Add(word);
        }
    }

    [Benchmark]
    [BenchmarkCategory("Add")]
    public void List_Add()
    {
        var list = new List<string>(capacity: badWordsCount);

        foreach (var word in badwords)
        {
            list.Add(word);
        }
    }

    [Benchmark]
    [BenchmarkCategory("Add")]
    public void Array_Add()
    {
        var arr = new string[badWordsCount];

        for (int i = 0; i < badWordsCount; i++)
        {
            arr[i] = badwords.ElementAt(i);
        }
    }

    [Benchmark]
    [BenchmarkCategory("Add")]
    public void Trie_Add()
    {
        var trie = new Trie();

        foreach (var word in badwords)
        {
            trie.Add(word);
        }
    }

    #endregion
}
