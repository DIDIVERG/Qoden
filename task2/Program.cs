using System;
using System.Collections.Generic;
using System.Linq;

class Program
{
    static void Main(string[] args)
    {
        string text = Console.ReadLine();
        string[] words = text.Split(' ');
        Dictionary<string, int> wordCounts = new Dictionary<string, int>();
        foreach (string word in words)
        {
            if (wordCounts.ContainsKey(word))
                wordCounts[word]++;
            else
                wordCounts[word] = 1;
        }
        var sortedWords = wordCounts.OrderByDescending(x => x.Value);
        int maxCount = sortedWords.First().Value;
        int maxLength = sortedWords.Max(i => i.Key.Length);
        foreach (var wordCount in sortedWords.OrderBy(x => x.Value))
        {
            string word = wordCount.Key;
            int count = wordCount.Value;
            string alignedWord = word;
            int barWidth = 10;
            int barLength = (int)Math.Round((double)count / maxCount * barWidth);
            string bars = new string('.', barLength).PadRight(barWidth);
            int underscoreLenght = maxLength - wordCount.Key.Length;
            string underscores = new string('_', underscoreLenght);
            Console.WriteLine($"{underscores}{alignedWord} {bars}");
        }
    }
}