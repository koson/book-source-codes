using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Drawing;


namespace PieChartApp
{
   public class Slice
    {
        private string sliceName;
        private int sliceRange;
        private Color sliceColor;
        // countNoName is static to know in all instances of the 
        // slice class what is this count
        private static int countNoName = 0;

        private Slice()
        {
        }
        public Slice(string name, int range, Color color)
        {
            if (name == "")
            {
                sliceName = "Slice " + countNoName.ToString();
                countNoName++;
            }
            else
                sliceName = name;
            if (range < 0)
                range = 0;
            else
                sliceRange = range;
            sliceColor = color;

        }

        public string GetSliceName()
        {
            return sliceName;
        }
        public int GetSliceRange()
        {
            return sliceRange;
        }
        public Color GetSliceColor()
        {
            return sliceColor;
        }
        public void SetSliceName(string name)
        {
            if (name == "")
            {
                sliceName = "Slice " + countNoName.ToString();
                countNoName++;
            }
            else
                sliceName = name;
        }

        public void SetSliceRange(int range)
        {
            if (range < 0)
                range = 0;
            else
                sliceRange = range;
        }

        public void SetSliceColor(Color color)
        {
            sliceColor = color;
        }



    }
}
