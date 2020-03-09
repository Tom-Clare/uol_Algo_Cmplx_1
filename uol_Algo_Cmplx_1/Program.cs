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
		static Dictionary<int, ArrayDetails> array_sources = new Dictionary<int, ArrayDetails>()
		{
			{ 1, new ArrayDetails {Name="Net_1_256" , Step=10} },
			{ 2, new ArrayDetails {Name="Net_1_2048", Step=50} },
			{ 3, new ArrayDetails {Name="Net_2_256" , Step=10} },
			{ 4, new ArrayDetails {Name="Net_2_2048", Step=50} },
			{ 5, new ArrayDetails {Name="Net_3_256" , Step=10} },
			{ 6, new ArrayDetails {Name="Net_3_2048", Step=50} }
		};

		enum algorithm_types
		{
			search,
			sort
		}

		static void Main(string[] args)
		{

			List<int> target_details = getArray(); // Get the requested array details
			List<int> analyse_target_list = composeArray(target_details); // Create a List from values in the target array
			int[] analyse_target = analyse_target_list.ToArray(); // Cast List to array<int>, ready for analysing

			string sort_request = getAlgorithm(algorithm_types.sort); // Get name of requested algorithm

			// Need to duplicate arrays, as sorting twice on the same array will change the first variable, as arrays are reference types, not value types.
			int[] out_array_asc = new int[analyse_target.Length];
			int[] out_array_desc = new int[analyse_target.Length];
			analyse_target.CopyTo(out_array_asc, 0);
			analyse_target.CopyTo(out_array_desc, 0);

			int comparisons_asc = 0;  // new values to pass references to
			int comparisons_desc = 0;

			if (sort_request == "Bubble Sort")
			{
				out_array_asc = CustomSorting.bubbleSort(false, out_array_asc, ref comparisons_asc);
				out_array_desc = CustomSorting.bubbleSort(true, out_array_desc, ref comparisons_desc);
			}
			else if (sort_request == "Merge Sort")
			{
				out_array_asc = CustomSorting.mergeSort(false, out_array_asc, 0, out_array_asc.Length - 1, ref comparisons_asc);
				out_array_desc = CustomSorting.mergeSort(true, out_array_desc, 0, out_array_desc.Length - 1, ref comparisons_desc);
			}
			else if (sort_request == "Quick Sort")
			{
				out_array_asc = CustomSorting.quickSort(false, out_array_asc, 0, out_array_asc.Length - 1, ref comparisons_asc);
				out_array_desc = CustomSorting.quickSort(true, out_array_desc, 0, out_array_desc.Length - 1, ref comparisons_desc);
			}
			else if (sort_request == "Heap Sort")
			{
				out_array_asc = CustomSorting.heapSort(false, out_array_asc, out_array_asc.Length, ref comparisons_asc);
				out_array_desc = CustomSorting.heapSort(true, out_array_desc, out_array_desc.Length, ref comparisons_desc);
			}

			if (target_details.Count > 1) // If more than one array is being displayed
			{
				// No step value for multiple arrays, use default step value
				displayArray(out_array_asc, out_array_desc, comparisons_asc, comparisons_desc);
			}
			else
			{
				// Use step value as defined in array_sources object
				displayArray(out_array_asc, out_array_desc, comparisons_asc, comparisons_desc, array_sources[target_details[0]].Step);
			}

			int search_value = getSearchValue();
			string search_request = getAlgorithm(algorithm_types.search);

			int comparisons_search = 0;
			int[] value_index = new int[] { -1, -1 };
			if (search_request == "Linear Search")
			{
				value_index = CustomSearching.linearSearch(search_value, out_array_asc, ref comparisons_search);
			}

			displaySearchResults(search_value, value_index[0], out_array_asc, value_index[1], comparisons_search);
		}

		static List<int> composeArray(List<int> target_details)
		{
			string line;
			List<int> line_contents = new List<int>();

			Console.WriteLine("");

			for (int i = 0; i < target_details.Count; i++)
			{
				string filename = array_sources[target_details[i]].Name;

				try
				{
					StreamReader file = new StreamReader(AppDomain.CurrentDomain.BaseDirectory + @"\../../../data\" + filename + ".txt");  // locate and open file
				
					Console.WriteLine(filename + " selected");  // clarify with user

					while ((line = file.ReadLine()) != null)  // while not the end of the file
					{
						line_contents.Add(int.Parse(line));  // add as int
					}
				}
				catch (FileNotFoundException e)
				{
					Console.WriteLine("Target file could not be found. Please make sure it exists in the data directory of this project.");
					Environment.Exit(2); // 2 is file not found exception for windows standards
				}
			}

			return line_contents;
		}

		static List<int> getArray()
		{
			Console.WriteLine("Please type the number of one of the following arrays to be analysed.\nIf you want to merge arrays, seperate the numbers with a comma:");
			foreach (var file in array_sources) // Iterate through array names for selection
			{
				Console.WriteLine(file.Key + ") " + file.Value.Name);  // format and display
			}

			List<int> file_indexes = new List<int>();
			bool valid = false;
			int number = 0;
			string input = "";
			while (!valid)
			{
				Console.Write("Select array(s): ");
				input = Console.ReadLine();
				if (input.Contains(","))  // They would like to merge
				{
					string[] file_indexes_strings = input.Split(",");

					for (int i = 0; i < file_indexes_strings.Length; i++)
					{
						if (int.TryParse(file_indexes_strings[i], out number) && number > 0 && number <= array_sources.Count )  // if valid
						{
							file_indexes.Add(number);  // Add index of requested file to list
						}
						else
						{
							// invalid
							break;
						}
					}
					
					if (file_indexes_strings.Length == file_indexes.Count)
					{
						valid = true;
					}
				}
				else if (int.TryParse(input, out number) && number > 0 && number < 7) // we require an index, must be a number
				{
					file_indexes.Add(number);
					valid = true;
				}
				else
				{
					// Invalid input
					Console.WriteLine("Invalid input. Please enter an index or multiple indexes seperated by a comma (2,4) or (1,4,5).");
				}
			}
			return file_indexes; // return details of array as ArrayDetails object
		}

		private static string getAlgorithm (algorithm_types type)
		{
			Console.WriteLine($"\nWhich {type} algorithm would you like to use? Please enter the index or name as shown below:");

			Dictionary<int, string> available = new Dictionary<int, string>();
			if (type == algorithm_types.search)
			{
				available = CustomSearching.available_algorithms;
			}
			else if (type == algorithm_types.sort)
			{
				available = CustomSorting.available_algorithms;
			}

			foreach (KeyValuePair<int, string> algorithm in available) // Iterate through and display available algorithms for selection
			{
				Console.WriteLine(algorithm.Key + ") " + algorithm.Value);
			}

			string input = "";
			int num;

			Console.Write("Select algorithm: ");
			input = Console.ReadLine();
			// while index isn't in available_algorithms or input isn't a value in that dictionary...
			while (!(int.TryParse(input, out num) && available.ContainsKey(int.Parse(input))) && !available.ContainsValue(input))
			{
				// Invalid input
				Console.WriteLine("Invalid Input. Please try again.");
				Console.Write("Select algorithm: ");
				input = Console.ReadLine();
			}

			if (int.TryParse(input, out num)) // If their input was a number
			{
				Console.WriteLine($"\n{available[num]} selected.");
				return available[num]; // Get name of algorithm from index
			}

			Console.WriteLine($"\n{input} selected");
			return input; // In this case, they entered the name of the algorithm
		}

		private static int getSearchValue ()
		{
			string input = null;
			int search_value = 0;
			while (!int.TryParse(input, out search_value))
			{
				Console.WriteLine("Please enter an integer to search through the selected array:");
				input = Console.ReadLine();
			}

			return search_value;
		}

		public static void displayArray(int[] array_asc, int[] array_desc, int comparisons_asc, int comparisons_desc, int step_value = 25)
		{
			Console.WriteLine("\nAscending\tDescending");
			// iterate through and simply display the array
			for (int i = 0; i < array_asc.Length; i++)
			{
				if (i % step_value == 0)  // Only display every nth value as specified in ArrayDetails object or default parameter
				{
					Console.WriteLine(array_asc[i] + "\t\t" + array_desc[i]);
				}
			}

			Console.WriteLine("Ascending comparisons: " + comparisons_asc);
			Console.WriteLine("Descending comparisons: " + comparisons_desc);
		}

		public static void displaySearchResults (int search_value, int value_index, int[] haystack, int difference, int comparisons)
		{
			string found = "";
			string text_modifier = "";
			if (difference == 0)
			{
				found = "Yes";
				text_modifier = "Found";
			}
			else
			{
				found = "No";
				text_modifier = "Closest";
			}
			Console.WriteLine("Value found:\t\t" + found);
			Console.WriteLine("Search value:\t\t" + search_value);
			Console.WriteLine("Closest value:\t\t" + haystack[value_index]);
			Console.WriteLine($"{text_modifier} value index:\t" + value_index);
			Console.WriteLine("Comparisons made:\t" + comparisons);
		}
	}
}
