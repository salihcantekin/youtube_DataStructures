using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructures.Trie;
internal class Trie
{
    public bool IsEndOfWord { get; set; }

    public Dictionary<char, Trie> Children { get; set; } = new();

    public void Add(string word)
    {
        Trie current = this;
        foreach (var c in word)
        {
            if (!current.Children.TryGetValue(c, out var child))
            {
                child = new Trie();
                current.Children.Add(c, child);
            }

            current = child;
        }

        current.IsEndOfWord = true;
    }

    public bool Contains(string word)
    {
        Trie current = this;

        foreach (var c in word)
        {
            if (!current.Children.TryGetValue(c, out current))
            { 
                return false;
            }
        }

        return current.IsEndOfWord;
    }

    public void Print(int space = 0) => Print(this, space);

    public void Print(Trie trie, int space = 0)
    {
        trie ??= this;

        static void PrintSingleNode(char word, int space = 0, bool isEndOfWord = false)
        {
            if (space > 0)
                Console.Write(new string(' ', space));

            Console.ForegroundColor = space == 0 ? ConsoleColor.Red : ConsoleColor.White;

            Console.WriteLine($"-> {word}{(isEndOfWord ? "(*)" : "")}");
        }

        foreach (var entry in trie.Children)
        {
            PrintSingleNode(entry.Key, space, entry.Value.IsEndOfWord);
            Print(entry.Value, space + 3);
        }
    }
}
