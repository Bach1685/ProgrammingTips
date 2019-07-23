using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sort
{
    class Sort
    {
        public void InsertionSort(int[] arr)
        {
            int j;
            int size = arr.Count();
            for (int i = 1; i < size; i++)
            {
                int key = arr[i];
                j = i - 1;

                while (j > -1 && arr[j] > key)
                {
                    arr[j + 1] = arr[j];
                    j = j - 1;
                }
                arr[j + 1] = key;
            }
        }
    }
}
