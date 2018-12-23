using System;
using System.Collections.Generic;


public class clsQuickSort<T> where T : IComparable
{
    // ================= instance member ====================
    T[] data;

    // ================= constructor ====================
    public clsQuickSort(T[] values)
    {
        data = values;    // copy rvalue of unsorted data
    }
    // ================= Property methods ====================

    // ================= Helper methods ====================

    /*****
    * Purpose: This method gets the initial pivot point for the sort and then
    *          calls itself recursively until all values have been set.
    *          
    * Parameter list:
    *  int first            index of first element of partition
    *  int last            index of last element of partition
    *  
    * Return value:
    *  void
    *****/
    private void doSort(int first, int last)
    {
        if (first == last)
        {
            return;   // Done
        }
        else
        {
            int pivot = getPivotPoint(first, last);

            if (pivot > first)
                doSort(first, pivot - 1);
            if (pivot < last)
                doSort(pivot + 1, last);
        }
    }

    /*****
    * Purpose: This method sets the pivot point for the sort.
    *          
    * Parameter list:
    *  int start          index to start of partition
    *  int end            index to end of partition
    *  
    * Return value:
    *  void
    *****/
    private int getPivotPoint(int first, int last)
    {
        int pivot = first;

        int start = first;  // Keep copies
        int end = last;

        if (last - first >= 1)
        {
            while (end > start)
            {
                while (data[pivot].CompareTo(data[start]) >= 0 &&
                                     start <= last && end > start)
                    start++;
                while (data[pivot].CompareTo(data[end]) <= 0 &&
                                     end >= first && end >= start)
                    end--;
                if (end > start)
                    swap(start, end);
            }
            swap(first, end);
            doSort(first, end - 1);
        }
        return end;
    }


    /*****
    * Purpose: This method performs the data swap for quickSort()
    *          
    * Parameter list:
    *  int pos1            index to first value to swap
    *  int pos2            index to second value to swap
    *  
    * Return value:
    *  void
    *****/
    private void swap(int pos1, int pos2)
    {
        T temp;

        temp = data[pos1];
        data[pos1] = data[pos2];
        data[pos2] = temp;
    }


    // ================= General methods ====================
    /*****
    * Purpose: This is the user interface entry point for the Quicksort
    *          
    * Parameter list:
    *  n/a
    *  
    * Return value:
    *  void
    *
    * CAUTION: This routine assumes constructor is passed unsort data
    *****/
    public void Sort()
    {
        int len = data.Length;
        if (len < 2)      // Enough to sort?
            return;
        doSort(0, data.Length - 1);
    }
}
