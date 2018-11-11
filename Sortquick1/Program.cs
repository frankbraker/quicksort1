using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Quicksort
//
// This algorithm doesn't use ANY array shift operations, which seem to appear in the descriptions inconsistently.
// Because an array shift is more expensive than swap, and because mergesort works fine with just swap, I don't
// see any reason quicksort needs array shift.
//
// It might just be all the explanations I've seen are sloppy.  But that's pretty hard to believe.
//
//  
namespace Sortquick1
{

    class Arr       // the Arr class!  Which is short for Array!  And it has a quick sort as part of it.
    {
        public static void swap(ref int[] ar, int x, int y)
        {
        //    Console.Write("swap: ar[" + x + "]=" + ar[x] + " with ar[" + y + "]=" + ar[y] + "\n");
            int tmp = ar[x];
            ar[x] = ar[y];
            ar[y] = tmp;
        }
        public static void quicksort(ref int[] array)
        {
            quicksort(ref array, 0, array.Length-1);
        }

        public static bool updateEarliestGreaterThanPivotIdx(ref int[] array, ref int earliestGreaterThanPivotIdx, int pivot, int limitIdx)
        {
            bool success = true;
            while (array[earliestGreaterThanPivotIdx] <= pivot && (earliestGreaterThanPivotIdx < limitIdx))          // keep bumping until it's something big and then we'll play with target
            {
                earliestGreaterThanPivotIdx++;
            }
            if (earliestGreaterThanPivotIdx == limitIdx)
                success = false;
            return success;
        }
        public static bool updateEarliestLessThanPivotIdx(ref int[] array, ref int earliestLessThanPivotIdx, int pivot, int limitIdx)
        {
            bool success = true;
            while (array[earliestLessThanPivotIdx] >= pivot && (earliestLessThanPivotIdx < limitIdx))          // keep bumping until it's something big and then we'll play with target
            {
                earliestLessThanPivotIdx++;
            }
            if (earliestLessThanPivotIdx == limitIdx)
                success = false;
            return success;
        }
        public static void MaxToX(ref int x, int max)
        {
            x = x < max ? max : x;
        }
        public static void showArray(int[] array, int originIdx, int limitIdx)
        {
            Console.Write("showArray: originIdx=" + originIdx + " limitIdx=" + limitIdx + "\n");
            for (int i=originIdx; i<= limitIdx; i++)
            {
                int j = array[i];
                Console.Write(j + ",");
            }
            Console.Write("\n");
        }

        public static void quicksort(ref int[] array, int originIdx, int limitIdx)
        {
            if (limitIdx - originIdx < 1)
                return;
            // Legend:
            //
            // originIdx (the first element in the array that will be sorted - THIS IS NEVER TOUCHED)
            // limitIdx (last element in the array that will be sorted)
            //
            // pivotIdx (whatever is the pivot element - usually this is originIdx+limit)
            //
            // earliestGreaterThanPivotIdx (originIdx + walking offset - this stops for first higher-than-pivot and waits to be swapped)
            // targetIdx (this is the first lower-than-pivot-after-earliestGreaterThanPivotIdx - it wants to be swapped with what is at earliestGreaterThanPivotIdx)
            int pivotIdx = limitIdx;
            int pivot = array[pivotIdx];

            int targetIdx = originIdx;                          // starts as originIdx and then walks
            //Console.Write("quicksort is called with originIdx=" + originIdx + " targetIdx=" + targetIdx + " limitIdx=" + limitIdx + " pivotIdx=" + pivotIdx + "\n");

            int earliestGreaterThanPivotIdx = originIdx;

            while (updateEarliestGreaterThanPivotIdx(ref array, ref earliestGreaterThanPivotIdx, pivot, limitIdx) )
            {
                MaxToX(ref targetIdx, earliestGreaterThanPivotIdx + 1);
                if (!updateEarliestLessThanPivotIdx(ref array, ref targetIdx, pivot, limitIdx))
                {
                    // all swapping is finished, so do a final swap of the pivot (which is at the end of the array, with the earliestGreatThanPivotIdx
            //        Console.WriteLine(" earliestGreaterThanPivotIdx=" + earliestGreaterThanPivotIdx + ", limitIdx=" + limitIdx + "\n");
            //        Console.WriteLine(" and pivotIdx=" + pivotIdx + "\n");
                    if ( earliestGreaterThanPivotIdx<limitIdx)
                    {
                        swap(ref array, pivotIdx, earliestGreaterThanPivotIdx);
                        pivotIdx = earliestGreaterThanPivotIdx;
                    }
                    break;
                }
            //    Console.WriteLine(" pre-swap: earliestGreaterThanPivotIdx="+ earliestGreaterThanPivotIdx +" targetIdx=" + targetIdx + "\n");
                swap(ref array, earliestGreaterThanPivotIdx, targetIdx);
                //showArray(array, originIdx, limitIdx);
            }

            // now split the array into 2 halves (under pivot and over pivot) and call myself for each half until we're done
            //System.Threading.Thread.Sleep(100);
            //showArray(array, originIdx, limitIdx);
            //Console.WriteLine("1st half will call quicksort with " + " originIdx=" + originIdx + ", limitIdx=" + pivotIdx);
            quicksort(ref array, originIdx, pivotIdx-1);    // start from wherever we started and go to the current pivot
            //showArray(array, originIdx, limitIdx);
            //System.Threading.Thread.Sleep(100);
            //Console.WriteLine("2nd half will call quicksort with " + " originIdx=" + (pivotIdx + 1) + ", limitIdx=" + limitIdx);
            quicksort(ref array, pivotIdx, limitIdx);
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            int[] a = { 1, 5, 3, 4, 2, 9, 1, 13, 8, 9 };
        /*    int Min = 0;
            int Max = 1000;

            // this declares an integer array with 5 elements
            // and initializes all of them to their default value
            // which is zero
            int[] a = new int[9999];

            Random randNum = new Random();
            for (int i = 0; i < a.Length; i++)
            {
                a[i] = randNum.Next(Min, Max);
            }
            */


            Console.WriteLine("Show original array:");
            Arr.showArray(a, 0, a.Length - 1);

            Arr.quicksort(ref a);
            Console.WriteLine("\nShow sorted array:");
            Arr.showArray(a, 0, a.Length - 1);
        }
    }
}
