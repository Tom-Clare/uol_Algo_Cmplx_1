using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace uol_Algo_Cmplx_1
{
    class CustomSearching
    {
        public static Dictionary<int, string> available_algorithms = new Dictionary<int, string>
        {
            {1, "Linear Search" },
            {2, "Binary Search" }
        };

        public static int[] linearSearch(int needle, int[] haystack, ref int comparisons)
        {
            if (haystack.Length == 0) return new int[] { -1, -1 };

            int difference = Math.Abs(haystack[0] - needle); // Absolute difference (as a positive integer)
            int closest_value = 0;  // This won't ever be returned as the value defined here
            for (int i = 0; i < haystack.Length; i++)  // Iterate through array in linear fashion
            {
                comparisons++; // New comparisons are being made
                if (haystack[i] == needle)  // if value is found
                {
                    return new int[] { i, difference };
                }
                else if (Math.Abs(haystack[i] - needle) < difference) // If a closer value is found...
                {
                    difference = Math.Abs(haystack[i] - needle); // Absolute difference (as a positive integer)
                    closest_value = i;  // Store difference and index
                }
                comparisons++; // New comparison was made if we didn't return in the first if statement
            }
            return new int[] { closest_value, difference };
        }

        public static int[] binarySearch (int needle, int[] haystack, int index, ref int comparisons)
        {
            int difference = Math.Abs(haystack[0] - needle); // Absolute difference (as a positive integer)
            int closest_value = 0; // Overwitten or correct, no matter what happens
            if (haystack.Length > 1 && haystack[index] != needle)
            {
                if (Math.Abs(haystack[index] - needle) < difference) // If a closer value is found...
                {
                    difference = Math.Abs(haystack[index] - needle); // Absolute difference (as a positive integer)
                    closest_value = index;  // Store difference and index
                }

                if (needle > haystack[index])  // if needle is higher than current index
                {
                    int[] new_haystack = haystack.Skip(index).Take((haystack.Length - 1) - index).ToArray();
                    return binarySearch(needle, new_haystack, new_haystack.Length / 2, ref comparisons); // Search in upper half
                }
                else if (needle < haystack[index])  // if needle is lower than current index
                {
                    int[] new_haystack = haystack.Take(haystack.Length / 2).ToArray();
                    return binarySearch(needle, new_haystack, new_haystack.Length / 2, ref comparisons); // search in lower half
                }
                else
                {
                    // value found!
                    return new int[] { index, 0 };
                }
            }

            return new int[] { closest_value, difference };  // Not working
        }
    }
}
