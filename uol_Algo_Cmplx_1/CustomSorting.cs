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

        public static int[] bubbleSort (bool reverse, int[] target, ref int comparisons)
        {
            int length = target.Length;

            for (int i = 0; i < length; i++)
            {
                for (int j = 0; j < length - i - 1; j++)
                {

                    comparisons++;
                    if (!reverse && target[j] > target[j + 1])
                    {
                        int temp = target[j];
                        target[j] = target[j + 1];
                        target[j + 1] = temp;
                    }
                    else
                    {
                        comparisons++;
                        if (reverse && target[j] < target[j + 1])
                        {
                            int temp = target[j + 1];
                            target[j + 1] = target[j];
                            target[j] = temp;
                        }
                    }
                }
            }

            return target;
        }

        public static int[] mergeSort (bool reverse, int[] target, int left, int right, ref int comparisons)
        {
            comparisons++;
            if (left < right) // recursive until they are the same index
            {
                int middle = (left + right) / 2; // Calculate where we split the array

                mergeSort(reverse, target, left, middle, ref comparisons); // Break down first half further
                mergeSort(reverse, target, middle + 1, right, ref comparisons); // Break down second half further

                merge(reverse, target, left, middle, right, ref comparisons); // Merge and sort the two halves
            }

            return target;
        }

        private static void merge (bool reverse, int[] target, int left, int middle, int right, ref int comparisons)
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

                    // Only one comparison made this iteration
                    comparisons++;
                }
                else if (j == right_array.Length) // likewise if we've exhausted the right array...
                {
                    target[k] = left_array[i]; //...add from the left
                    i++;

                    // Two comparisons this iteration
                    comparisons++;
                    comparisons++;
                }
                else if ((!reverse && left_array[i] <= right_array[j]) || (reverse && left_array[i] >= right_array[j])) // Compare and add smallest
                {
                    target[k] = left_array[i];
                    i++;

                    // Three comparisons this iteration
                    comparisons++;
                    comparisons++;
                    comparisons++;
                }
                else
                {
                    target[k] = right_array[j]; // Alternate outcome of the comparison
                    j++;

                    // Still three comparisons this iteration, as this is an else statement
                    comparisons++;
                    comparisons++;
                    comparisons++;
                }
            }
        }

        public static int[] quickSort(bool reverse, int[] target, int left, int right, ref int comparisons)
        {
            comparisons++;
            if (left < right)  // if there are at least two values in our range...
            {
                int pivot = sortAndGetPivot(reverse, target, left, right, ref comparisons);  // start swapping values about the pivot point and return a new pivot
                if (pivot > 1) quickSort(reverse, target, left, pivot - 1, ref comparisons);  // again about the *left* side of the new pivot point
                if (pivot + 1 < right) quickSort(reverse, target, pivot + 1, right, ref comparisons);  // again about the *right* side of the new pivot point
            }

            return target;
        }

        private static int sortAndGetPivot (bool reverse, int[] target, int left, int right, ref int comparisons)
        {
            int pivot = target[left];  // select pivot point

            while (true)
            {

                while ((!reverse && target[left] < pivot) || (reverse && target[left] > pivot)) // need an element on the left that's larger than pivot
                {
                    comparisons++;
                    left++;
                }
                while ((!reverse && target[right] > pivot) || (reverse && target[right] < pivot)) // and an element on the right that's smaller than pivot
                {
                    comparisons++;
                    right--;
                }

                comparisons++; // Couting below if statement
                if (left < right)  // value of left is higher than value of right, so if their indexes aren't in the right order...
                {

                    comparisons++;  // Count comparison even if it doesn't pass
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
                    // No extra comparisons made here
                    return right;  // the left and right values are in the correct order, meaning this pivot element is sorted. Return new pivot point.
                }
            }
        }

        public static int[] heapSort(bool reverse, int[] target, int length, ref int comparisons)
        {
            for (int i = length / 2 - 1; i >= 0; i--)  // count down from half of target length
                heapify(reverse, target, length, i, ref comparisons);  // create heap by calling heapify() for all non-leaf elements
            for (int i = length - 1; i >= 0; i--)  // Excludes root element (largest element)
            {
                int temp = target[0];  // replace root element with rightmost element
                target[0] = target[i];
                target[i] = temp;
                heapify(reverse, target, i, 0, ref comparisons);  // and re-create heap with i as root
            }

            return target;
        }

        private static void heapify(bool reverse, int[] target, int length, int root, ref int comparisons)
        {
            int largest = root;
            int left = 2 * root + 1;
            int right = 2 * root + 2;
            comparisons++;
            if ((!reverse && left < length && target[left] > target[largest]) || (reverse && left < length && target[left] < target[largest]))  // larger/smaller element found
                largest = left;
            comparisons++;
            if ((!reverse && right < length && target[right] > target[largest]) || (reverse && right < length && target[right] < target[largest]))  // larger/smaller element found
                largest = right;
            comparisons++;
            if (largest != root)
            {
                int swap = target[root];  // swap tree root with larger element
                target[root] = target[largest];
                target[largest] = swap;
                heapify(reverse, target, length, largest, ref comparisons);  // re-create heap with new root
            }
        }
    }
}
