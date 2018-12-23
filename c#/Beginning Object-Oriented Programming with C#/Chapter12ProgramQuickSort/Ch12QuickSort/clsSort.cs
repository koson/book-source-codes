using System;
using System.Collections.Generic;
using System.Text;


class clsSort
{
    int[] data;

    public clsSort(int[] vals)
    {
        data = vals;    // Copies the rvalue
    }

    /*****
     * Purpose: This method sorts an array of integers values into
     *          ascending order via recursive calls to quickSort().
     *          
     * Parameter list:
     *  int first           the first value to sort
     *  int last            the last value to sort
     *  
     * Return value:
     *  void
     *****/
    public void quickSort(int first, int last)
    {
        int start;      // Index for left partition
        int end;        // Index for right partition

        start = first;  // Keep copy of first element of array...
        end = last;     // ...and the last one.

        if (last - first >= 1)   // Make sure there's enough data to sort
        {
            int pivot = data[first];    // Set partitions

            while (end > start) // While indexes don't match...
            {   // Do left partition
                while (data[start] <= pivot && start <= last && end > start)
                    start++;
                // Do right partition
                while (data[end] > pivot && end >= first && end >= start)
                    end--;
                if (end > start)    // If right partition index smaller...
                    swap(start, end); // ...do a swap
            }
            swap(first, end); // Swap last element with pivot
            quickSort(first, end - 1); // Sort around partitions
            quickSort(end + 1, last);
        }
        else
        {
            return;
        }

    }

    /*****
    * Purpose: This method performs the data swap for quickSort()
    *          
    * Parameter list:
    *  int[] data          the array to sort
    *  int pos1            the array index to first value
    *  int pos2            the array index to second value
    *  
    * Return value:
    *  void
    *****/
    public void swap(int pos1, int pos2)
    {
        int temp;

        temp = data[pos1];
        data[pos1] = data[pos2];
        data[pos2] = temp;
    }
}

