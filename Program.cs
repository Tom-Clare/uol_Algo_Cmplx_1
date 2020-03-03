using System;
using System.IO;
using System.Collections.Generic;

namespace uol_Algo_Cmplx_1
{
	class ArrayDetails
	{
		// Constructing metadata for assisting with analysation
		public string Name { get; set; }
		public int Step { get; set; }
	}

	class Program
	{
		static void Main(string[] args)
		{

			var files = new Dictionary<int, ArrayDetails>()
			{
				{ 1, new ArrayDetails {Name="Net_1_256" , Step=10} },
				{ 2, new ArrayDetails {Name="Net_1_2048", Step=50} },
				{ 3, new ArrayDetails {Name="Net_2_256" , Step=10} },
				{ 4, new ArrayDetails {Name="Net_2_2048", Step=50} },
				{ 5, new ArrayDetails {Name="Net_3_256" , Step=10} },
				{ 6, new ArrayDetails {Name="Net_3_2048", Step=50} }
			};

			string array_name = getArrayName(files);
			List<int> analyse_target_list = composeArray(array_name);
			int[] analyse_target = analyse_target_list.ToArray(); // Converted List to array, ready for analysing



			Console.ReadKey();
		}

		static List<int> composeArray(string filename)
		{
			string dir = Directory.GetCurrentDirectory();
			int counter = 0;
			string line;
			List<int> line_contents = new List<int>();

			StreamReader file = new StreamReader(AppDomain.CurrentDomain.BaseDirectory + @"\../../uol_Algo_Cmplx_1\data\" + filename + ".txt");

			Console.WriteLine(filename + " selected");

			while ((line = file.ReadLine()) != null)
			{
				line_contents.Add(int.Parse(line));
				counter++;
			}
			return line_contents;
		}

		static string getArrayName(Dictionary<int, ArrayDetails> files)
		{
			Console.WriteLine("Please type the number of one of the following arrays to be analysed:");
			foreach (var file in files)
			{
				Console.WriteLine(file.Key + ") " + file.Value.Name);
			}

			bool valid = false;
			int number = 0;
			string input = "";
			while (valid == false)
			{
				input = Console.ReadLine();
				if(int.TryParse(input, out number))
				{
					valid = true;
				}
				else
				{
					Console.WriteLine("Invalid input. Please enter a number only");
				}
			}
			return files[number].Name;
		}
	}
}
