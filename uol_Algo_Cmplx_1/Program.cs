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

			ArrayDetails target_details = getArray(files); // Get the requested array details
			List<int> analyse_target_list = composeArray(target_details.Name); // Create a List from values in the target array
			int[] analyse_target = analyse_target_list.ToArray(); // Cast List to array<int>, ready for analysing

			string request = getSortingAlgorithm(); // Get name of requested algorithm
			int[] out_array = new int[0];

			analyse_target = new int[] { 1, 5, 4, 9, 3 };
			if (request == "Bubble Sort")
			{
				out_array = CustomSorting.bubbleSort(analyse_target);
			}
			else if (request == "Merge Sort")
			{
				out_array = CustomSorting.mergeSort(analyse_target, 0, analyse_target.Length - 1);
			}
			else if (request == "Quick Sort")
			{
				//out_array = CustomSorting.quickSort(analyse_target);
			}
			else if (request == "Heap Sort")
			{
				//out_array = CustomSorting.heapSort(analyse_target);
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
			foreach (var file in files) // Iterate through array names for selection
			{
				Console.WriteLine(file.Key + ") " + file.Value.Name);
			}

			bool valid = false;
			int number = 0;
			string input = "";
			while (!valid)
			{
				input = Console.ReadLine();
				if (int.TryParse(input, out number)) // we require an index
				{
					valid = true;
				}
				else
				{
					// Invalid input
					Console.WriteLine("Invalid input. Please enter a number only");
				}
			}
			return files[number]; // return details of array as ArrayDetails object
		}

		private static string getSortingAlgorithm ()
		{
			Console.WriteLine("Which algorithm would you like to use? Please enter the index or name as shown below:");

			foreach (KeyValuePair<int, string> algorithm in CustomSorting.available_algorithms) // Iterate through and display available algorithms for selection
			{
				Console.WriteLine(algorithm.Key + ") " + algorithm.Value);
			}

			string input = "";
			int num;

			input = Console.ReadLine();
			// while index isn't in available_algorithms or input isn't a value in that dictionary...
			while (!(int.TryParse(input, out num) && CustomSorting.available_algorithms.ContainsKey(int.Parse(input))) && !CustomSorting.available_algorithms.ContainsValue(input))
			{
				// Invalid input
				Console.WriteLine("Invalid Input. Please try again.");
				input = Console.ReadLine();
			}

			if (int.TryParse(input, out num)) // If their input was a number
			{
				return CustomSorting.available_algorithms[num]; // Get name of algorithm from index
			}
			return input; // In this case, they entered the name of the algorithm
		}

		public static void displayArray(int[] array)
		{
			// iterate through and simply display the array
			for (int i = 0; i < array.Length; i++)
			{
				Console.WriteLine(array[i]);
			}
		}
	}
}
