

// Data Structures => Trie

using BenchmarkDotNet.Running;
using DataStructures.Trie;

BenchmarkRunner.Run<TrieVsStringTests>();

return;

Trie trie = new();

trie.Add("ali");
trie.Add("alp");
trie.Add("alper");
trie.Add("salih");
trie.Add("cantekin");

trie.Print();

Console.ReadLine();
