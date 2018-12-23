// SortInterface - the SortInterface program demonstrates how
//                 the interface concept can be used to provide
//                 an enhanced degree of flexibility in factoring
//                 and implementing classes
using System;

namespace SortInterface
{

  // IDisplayable - an object that can convert itself into
  //                a displayable string format
  interface IDisplayable
  {
    // GetString - return a string representation of yourself
    string GetString();
  }

  class Program
  {
    public static void Main(string[] args)
    {
      // Sort students by grade...
      Console.WriteLine("Sorting the list of students");

      // get an unsorted array of students
      Student[] students = Student.CreateStudentList();

      // use the IComparable interface to sort the array
      IComparable[] comparableObjects = (IComparable[])students;
      Array.Sort(comparableObjects);

      // now the IDisplayable interface to display the results
      IDisplayable[] displayableObjects = (IDisplayable[])students;
      DisplayArray(displayableObjects);

      // Now sort an array of birds by name using
      // the same routines even though the class Bird and
      // Student have no common base class
      Console.WriteLine("\nSorting the list of birds");
      Bird[] birds = Bird.CreateBirdList();

      // notice that it's not really necessary to cast the
      // objects explicitly...
      Array.Sort(birds);
      DisplayArray(birds);
      
      // wait for user to acknowledge the results
      Console.WriteLine("Press Enter to terminate...");
      Console.Read();
    }

    // DisplayArray - display an array of objects that
    //                implement the IDisplayable interface
    public static void DisplayArray(IDisplayable[] displayables) 
    {
      int length = displayables.Length;
      for(int index = 0; index < length; index++)
      {
        IDisplayable displayable = displayables[index];
        Console.WriteLine("{0}", displayable.GetString());
      }
    }
  }

  // ----------- Students - sort students by grade -------
  // Student - description of a student with name and grade
  class Student : IComparable, IDisplayable
  {
    private string sName;
    private double dGrade = 0.0;
    
    // Constructor - initialize a new student object
    public Student(string sName, double dGrade)
    {
      this.sName = sName;
      this.dGrade = dGrade;
    }

    // CreateStudentList - to save space here, just create
    //             a fixed list of students
    static string[] sNames = 
           {"Homer", "Marge", "Bart", "Lisa", "Maggie"};
    static double[] dGrades = {0, 85, 50, 100, 30};
    public static Student[] CreateStudentList()
    {
      Student[] sArray = new Student[sNames.Length];
      for (int i = 0; i < sNames.Length; i++)
      {
        sArray[i] = new Student(sNames[i], dGrades[i]);
      }
      return sArray;
    }

    // access read-only methods
    public string Name
    {
      get { return sName; }
    }
    public double Grade
    {
      get { return dGrade; }
    }
    
    // implement the IComparable interface:
    // CompareTo - compare another object (in this case, Student
    //             objects) and decide which one comes after the 
    //             other in the sorted array
    public int CompareTo(object rightObject)
    {
      // compare the current Student (let's call her
      // 'left') against the other student (we'll call
      // her 'right') - generate an error if both
      // left and right are not Student objects
      Student leftStudent = this;
      if (!(rightObject is Student))
      {
        Console.WriteLine("Compare method passed a nonStudent");
        return 0;
      }
      Student rightStudent = (Student)rightObject;

      // now generate a -1, 0 or 1 based upon the
      // sort criteria (the student's grade)
      // (the Double class has a CompareTo() method
      // we could have used instead)
      if (rightStudent.Grade < leftStudent.Grade)
      {
        return -1;
      }
      if (rightStudent.Grade > leftStudent.Grade)
      {
        return 1;
      }
      return 0;
    }

    // implement the IDisplayable interface:
    // GetString - return a representation of the student
    public string GetString()
    {
      string sPadName = Name.PadRight(9);
      string s = String.Format("{0}: {1:N0}", sPadName, Grade);
      return s;
    }
  }

  // -----------Birds - sort birds by their names--------
  // Bird - just an array of bird names
  class Bird : IComparable, IDisplayable
  {
    private string sName;
    
    // Constructor - initialize a new Bird object
    public Bird(string sName)
    {
      this.sName = sName;
    }

    // CreateBirdList - return a list of birds to the caller;
    //                  use a canned list here to save time
    static string[] sBirdNames = 
       { "Oriole", "Hawk", "Robin", "Cardinal", 
         "Bluejay", "Finch", "Sparrow"};
    public static Bird[] CreateBirdList()
    {
      Bird[] birds = new Bird[sBirdNames.Length];
      for(int i = 0; i < birds.Length; i++)
      {
        birds[i] = new Bird(sBirdNames[i]);
      }
      return birds;
    }

    // access read-only methods
    public string Name
    {
      get { return sName; }
    }

    // implement the IComparable interface:
    // CompareTo - compare the birds by name; use the
    //             built-in String class compare method
    public int CompareTo(object rightObject)
    {
      // we'll compare the "current" bird to the
      // "right hand object" bird
      Bird leftBird = this;
      Bird rightBird = (Bird)rightObject;

      return String.Compare(leftBird.Name, rightBird.Name);
    }

    // implement the IDisplayable interface:
    // GetString - returns the name of the bird
    public string GetString()
    {
      return Name;
    }
  }
}
