using System;
using System.Collections.Generic;
using System.Linq;
using static System.Console;

namespace FrequencyCryptanalysis
{
	internal sealed class CharacterFrequencyAnalyzer
	{
		private string _text;

		public string Text
		{
			get => _text;
			private set => _text = value ?? throw new ArgumentNullException(nameof(value), "Text cannot be null");
		}

		public Dictionary<char, int> Frequencies { get; } = new();

		public CharacterFrequencyAnalyzer(string text)
		{
			Text = text;
		}

		private void CalculateCharacterFrequencies(string text)
		{
			foreach (var character in text)
			{
				if (Frequencies.TryGetValue(character, out int _))
				{
					Frequencies[character]++;
				}
				else
				{
					Frequencies.Add(character, 1);
				}
			}
		}

		public void GetCharacterFrequencies(int count)
		{
			CalculateCharacterFrequencies(Text);
			PrintCharacterFrequencies(count);
		}

		private void PrintCharacterFrequencies(int count)
		{
			WriteLine("Символ:\t Число вхождений");
			foreach (var character in Frequencies.OrderByDescending(x => x.Value).Take(count))
			{
				WriteLine("{0}\t {1}\t", character.Key, character.Value);
			}
		}
	}
}