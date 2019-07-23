using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sort
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] arr = new int[] { 45, 4, 90, 8, 1, 48, 32, 4 };

            Sort sort = new Sort();
            sort.QuickSort(arr, 0, arr.Count() - 1);

            foreach (var element in arr)
                Console.WriteLine(element + " ");

            Console.ReadKey();
        }

        

    }
}
