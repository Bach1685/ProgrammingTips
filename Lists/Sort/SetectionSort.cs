using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sort
{
    partial class Sort
    {
        static public void SetectionSort(int[] arr)
        {
            int size = arr.Count();

            for (int i = 0; i < size - 1; i++)
            {
                int minIndex = i;

                for (int j = i + 1; j < size; j++)
                    if (arr[j] > arr[minIndex])
                        minIndex = j;

                if (minIndex != i)
                {
                    int temp = arr[i];
                    arr[i] = arr[minIndex];
                    arr[minIndex] = temp;
                }
            }
        }
    }
}
