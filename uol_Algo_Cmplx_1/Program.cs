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

			ArrayDetails target_details = getArray(files);
			List<int> analyse_target_list = composeArray(target_details.Name);
			int[] analyse_target = analyse_target_list.ToArray(); // Converted List to array, ready for analysing

			Console.WriteLine("Which algorithm would you like to use?");
			bool valid = false;
			int[] out_array = new int[0];
			while (!valid)
			{
				string request = Console.ReadLine();
				if (request == "1" || request == "bubbleSort")
				{
					out_array = CustomSorting.bubbleSort(analyse_target);
					valid = true;
				}
				else if (request == "2" || request == "mergeSort")
				{
					int[] test = new int[] { 1, 2, 5, 7, 2, 53, 3 };
					out_array = CustomSorting.mergeSort(test, 0, test.Length - 1);
					valid = true;
				}
				else if (request == "3" || request == "quickSort")
				{
					//out_array = CustomSorting.quickSort(analyse_target);
					valid = true;
				}
				else if (request == "4" || request == "heapSort")
				{
					//out_array = CustomSorting.heapSort(analyse_target);
					valid = true;
				}
				else if (!valid)
				{
					Console.WriteLine("Invalid input. Please enter an index or name from the list.");
				}
			}

			displayArray(out_array);

		}

		static List<int> composeArray(string filename)
		{
			int counter = 0;
			string line;
			List<int> line_contents = new List<int>();

			StreamReader file = new StreamReader(AppDomain.CurrentDomain.BaseDirectory + @"\../../../data\" + filename + ".txt");

			Console.WriteLine(filename + " selected");

			while ((line = file.ReadLine()) != null)
			{
				line_contents.Add(int.Parse(line));
				counter++;
			}
			return line_contents;
		}

		static ArrayDetails getArray(Dictionary<int, ArrayDetails> files)
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
				if (int.TryParse(input, out number))
				{
					valid = true;
				}
				else
				{
					Console.WriteLine("Invalid input. Please enter a number only");
				}
			}
			return files[number];
		}

		public static void displayArray(int[] array)
		{
			for (int i = 0; i < array.Length; i++)
			{
				Console.WriteLine(array[i]);
			}
		}
	}
}
