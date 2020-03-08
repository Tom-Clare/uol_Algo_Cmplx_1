using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace uol_Algo_Cmplx_1
{
    class CustomSorting
    {
        public static Dictionary<int, string> available_algorithms = new Dictionary<int, string>
        {
            {1, "Bubble Sort" },
            {2, "Merge Sort" },
            {3, "Quick Sort" },
            {4, "Heap Sort" }
        };

        public static int[] bubbleSort (int[] target)
        {
            int length = target.Length;

            for (int i = 0; i < length; i++)
            {
                for (int j = 0; j < length - i - 1; j++)
                {
                    if (target[j] > target[j + 1])
                    {
                        int temp = target[j];
                        target[j] = target[j + 1];
                        target[j + 1] = temp;
                    }
                }
            }

            return target;
        }

        public static int[] mergeSort (int[] target, int left, int right)
        {
            if (left < right) // recursive until they are the same index
            {
                int middle = (left + right) / 2; // Calculate where we split the array

                mergeSort(target, left, middle); // Break down first half further
                mergeSort(target, middle + 1, right); // Break down second half further

                merge(target, left, middle, right); // Merge and sort the two halves
            }

            return target; // Return results
        }

        private static int[] merge (int[] target, int left, int middle, int right)
        {
            int[] left_array = new int[middle - left + 1]; // Create left half of array
            int[] right_array = new int[right - middle]; // Create right half of array

            Array.Copy(target, left, left_array, 0, middle - left + 1); // Copy in values. We now have the arrays to work with and to amalgamate into one array.
            Array.Copy(target, middle + 1, right_array, 0, right - middle);

            int i = 0; // Initialise array indexes
            int j = 0;

            for (int k = left; k < right + 1; k++) // Using k as a counter
            {
                if (i == left_array.Length) // if we've gone through all values in the left array...
                {
                    target[k] = right_array[j]; // ...add values from the right array
                    j++;
                }
                else if (j == right_array.Length) // likewise if we've exhausted the right array...
                {
                    target[k] = left_array[i]; //...add from the left
                    i++;
                }
                else if (left_array[i] <= right_array[j]) // Compare and add smallest
                {
                    target[k] = left_array[i];
                    i++;
                }
                else
                {
                    target[k] = right_array[j]; // Alternate outcome of the comparison
                    j++;
                }
            }

            return target;
        }

        public static int[] quickSort (int[] target, int left, int right)
        {
            if (left < right)  // if there are at least two values in our range...
            {
                int pivot = sortAndGetPivot(target, left, right);  // start swapping values about the pivot point and return a new pivot
                if (pivot > 1) quickSort(target, left, pivot - 1);  // again about the *left* side of the new pivot point
                if (pivot + 1 < right) quickSort(target, pivot + 1, right);  // again about the *right* side of the new pivot point
            }

            return target;  // feed back sorted array
        }

        private static int sortAndGetPivot (int[] target, int left, int right)
        {
            int pivot = target[left];  // select pivot point

            while (true)
            {

                while (target[left] < pivot) // need an element on the left that's larger than pivot
                {
                    left++;
                }
                while (target[right] > pivot) // and an element on the right that's smaller than pivot
                {
                    right--;
                }
                if (left < right)  // value of left is higher than value of right, so if their indexes aren't in the right order...
                {
                    if (target[left] == target[right]) // ...and if they are the same value
                    {
                        return right; // return right element as new pivot point
                    }

                    int temp_left = target[left];   // otherwise swap left and right points
                    target[left] = target[right];
                    target[right] = temp_left;      // and loop again
                }
                else
                {
                    return right;  // the left and right values are in the correct order, meaning this pivot element is sorted. Return new pivot point.
                }
            }
        }
    }
}
