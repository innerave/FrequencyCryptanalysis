using System;
using System.IO;
using System.Security;
using System.Text;
using static System.Console;
using static System.IO.File;
using static System.Text.Encoding;

namespace FrequencyCryptanalysis
{
	internal static class FrequencyCryptanalysis
	{
		private static void Main(string[] args)
		{
			if (args.Length == 0)
			{
				WriteLine("Необходимо указать файл для анализа.");
				return;
			}
			var path = args[0];
			string text;
			try
			{
				text = ReadAllText(path, GetEncodingFromUser());
			}
			catch (FileNotFoundException)
			{
				WriteLine("Файл, заданный параметром, не найден.");
				return;
			}
			catch (DirectoryNotFoundException)
			{
				WriteLine("Указан недопустимый путь.");
				return;
			}
			catch (PathTooLongException)
			{
				WriteLine("Имя файла превышает максимальную длину, заданную в системе.");
				return;
			}
			catch (NotSupportedException)
			{
				WriteLine("Аргумент задан в недопустимом формате.");
				return;
			}
			catch (SecurityException)
			{
				WriteLine("Отсутствуют разрешения, необходимые для доступа к файлу.");
				return;
			}
			catch (IOException)
			{
				WriteLine("При открытии файла произошла ошибка ввода-вывода.");
				return;
			}
			var analyzer = new CharacterFrequencyAnalyzer(text);
			analyzer.GetCharacterFrequencies(10);
		}

		private static Encoding GetEncodingFromUser()
		{
			RegisterProvider(CodePagesEncodingProvider.Instance);
			WriteLine("Выберите кодировку файла:" +
				"\n[1] ASCII" +
				"\n[2] UTF-8" +
				"\n[3] Unicode" +
				"\n[4] UTF-32" +
				"\n[5] Windows 1252" +
				"\n[6] Windows 1251");
			return ReadKey(intercept: true).Key switch
			{
				ConsoleKey.D1 => ASCII,
				ConsoleKey.D2 => UTF8,
				ConsoleKey.D3 => Unicode,
				ConsoleKey.D4 => UTF32,
				ConsoleKey.D5 => GetEncoding(1252),
				ConsoleKey.D6 => GetEncoding(1251),
				_ => Default
			};
		}
	}
}