using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sort
{
    partial class Sort
    {
        Methods methods = new Methods();

        public void QuickSort(int[] arr, int low, int high)
        {
            if (low < high)
            {
                int p = Partition(arr, low, high);
                QuickSort(arr, low, p - 1);
                QuickSort(arr, p + 1, high);
            }
        }

        private int Partition(int[] arr, int low, int high)
        {
            int pivot = arr[high];
            int j = low;

            for (int i = low; i < high; i++)
            {
                if (arr[i] <= pivot)
                {
                    methods.Swap(ref arr[i], ref arr[j]);
                    j++;
                }
            }
            methods.Swap(ref arr[j], ref arr[high]);

            return j;
        }
    }
}
