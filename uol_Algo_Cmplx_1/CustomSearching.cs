using System;
using System.Collections.Generic;
using System.Text;

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

    }
}
