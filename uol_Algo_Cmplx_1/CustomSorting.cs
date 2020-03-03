using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace uol_Algo_Cmplx_1
{
    class CustomSorting
    {
        public Dictionary<int, string> available_algorithms = new Dictionary<int, string>
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
            // currently not working. Go through with pen and paper, see what's wrong.
            if (left < right && target.Length > 1)
            {
                int middle = (left + right) / 2;

                mergeSort(target, left, middle);
                mergeSort(target, middle, right);

                merge(target, left, middle + 1, right);
            }

            return target;
        }

        private static int[] merge (int[] target, int left, int middle, int right)
        {
            int[] left_array = new int[middle - left + 1];
            int[] right_array = new int[right - middle];

            Array.Copy(target, left, left_array, 0, middle - left + 1);
            Array.Copy(target, middle + 1, right_array, 0, right - middle + 1);

            int i = 0;
            int j = 0;

            for (int k = left; k < right + 1; k++)
            {
                if (i == left_array.Length)
                {
                    target[k] = right_array[j];
                    j++;
                }
                else if (j == right_array.Length)
                {
                    target[k] = left_array[i];
                    i++;
                }
                else if (left_array[i] <= right_array[j])
                {
                    target[k] = left_array[i];
                    i++;
                }
                else
                {
                    target[k] = right_array[j];
                    j++;
                }
            }

            return target;
        }
    }
}
