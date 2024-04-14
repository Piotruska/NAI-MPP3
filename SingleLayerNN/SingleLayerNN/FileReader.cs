using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;

namespace SingleLayerNN;

public class FileReader
{
    private Dictionary<char, int> CreateLetterTemplate() //create template for vector of 26 dimentions
    {
        var letterTemplate = new Dictionary<char, int>();
        for (char c = 'a'; c <= 'z'; c++)
        {
            letterTemplate[c] = 0;
        }
        return letterTemplate;
    }

    public List<(double[], string)> ReadDataFile(string fileName)
    {
        string filePath = Path.Combine(Directory.GetCurrentDirectory(), fileName);
        var records = new List<(double[], string)>();
        var letterTemplate = CreateLetterTemplate(); 

        using (StreamReader reader = new StreamReader(filePath))
        {
            string line;
            while ((line = reader.ReadLine()) != null)
            {
                var processedData = ProcessLine(line, letterTemplate);
                records.Add(processedData);
            }
        }

        return records;
    }

    private (double[], string) ProcessLine(string line, Dictionary<char, int> template)
    {
        string[] parts = line.Split(new char[] { ',' }, 2);
        string language = parts[0].Trim(); //get lang name
        string content = parts.Length > 1 ? CleanInput(parts[1]) : ""; // removes unnessesary characters
        
        var letterCounts = new Dictionary<char, int>(template);// clone the template

        foreach (char ch in content) // add all the letters to the dictionary 
        {
            if (letterCounts.ContainsKey(ch))  
            {
                letterCounts[ch]++;
            }
        }
        //calculate the letter frequencies and sort it into a list a-z and return the touple
        double[] letterFrequencies = new double[26];
        int totalLetters = letterCounts.Sum(kvp => kvp.Value);
        for (int i = 0; i < 26; i++)
        {
            char letter = (char)('a' + i);
            letterFrequencies[i] = totalLetters > 0 ? (double)letterCounts[letter] / totalLetters : 0;
        }

        return (letterFrequencies, language);
    }

    private string CleanInput(string input)
    {
        return new string(input.ToLower().Where(c => c >= 'a' && c <= 'z').ToArray());
    }
}
