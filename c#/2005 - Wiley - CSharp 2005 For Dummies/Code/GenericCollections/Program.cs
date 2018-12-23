// GenericCollections - demonstrate the generic collections
using System;
using System.Collections;
using System.Collections.Generic;
namespace GenericCollections
{
  public class Program
  {
    // demonstrate List<T> collection and many of its methods
    public static void Main(string[] args)
    {
      Console.WriteLine("List<T> greatly resembles ArrayList:");
      Console.WriteLine("\tArrayList aList = new ArrayList();");
      ArrayList aList = new ArrayList(); 
      // now List<T>: note angle brackets plus parentheses in 
      // List<T> declaration; T is a "type parameter"
      //
      // instantiate for string
      //
      Console.WriteLine("\tList<string> sList = new List<string>();");
      List<string> sList = new List<string>();
      Console.WriteLine("Add a string - OK");
      sList.Add("one");
      Console.WriteLine("Add an int - I  don't think so!");
      //sList.Add(3);         // compiler error here!
      Console.WriteLine("Add a Student - not to this string list!");
      Student aStudent = new Student("du Bois");
      //sList.Add(aStudent);  // compiler error here!
      //
      // instantiate for int
      //
      Console.WriteLine("Instantiate List<T> for ints:");
      List<int> intList = new List<int>();
      Console.WriteLine("Add some ints:");
      intList.Add(3);       // just fine; note, no boxing
      intList.Add(4);
      Console.WriteLine("Printing intList:");
      foreach(int i in intList)
      {
        Console.WriteLine("\tint i = " + i.ToString()); // no casting
      }
      //
      // instantiate for Student
      //
      Console.WriteLine("Instantiating List<T> for Student:");
      List<Student> studentList = new List<Student>();
      Console.WriteLine("Adding some students:");
      Student student1 = new Student("Vigil");
      Student student2 = new Student("Finch");
      studentList.Add(student1);
      studentList.Add(student2);
      // Add two more by adding a whole array to the list at once
      Student[] students = new Student[] 
                            { new Student("Mox"), new Student("Fox") };
      studentList.AddRange(students);
      Console.WriteLine("Num students in studentList = {0}", studentList.Count);
      Console.WriteLine("Search for Student2 ({0}) with IndexOf():", student2.Name);
      Console.WriteLine("\t" + student2.Name + " at " + studentList.IndexOf(student2));
      Console.WriteLine("Use indexer to pluck out student at item [3]: {0}", studentList[3].Name);
      Console.WriteLine("Search for student1 ({0}) with Contains():", student1.Name);
      if(studentList.Contains(student1))
      {
        Console.WriteLine("\t" + student1.Name + " contained in list");
      }
      Console.WriteLine("Display the unsorted list:");
      foreach (Student st in studentList)
      {
        Console.WriteLine("\t" + st.Name);
      }
      Console.WriteLine("Now display the sorted list:");
      studentList.Sort(); // assumes Student implements IComparable interface
      foreach (Student st in studentList)
      {
        Console.WriteLine("\t" + st.Name);
      }
      // BinarySearch() also assumes Student implements IComparable
      Console.WriteLine("Search for student2 with BinarySearch():");
      int student2Index = studentList.BinarySearch(student2);
      if( student2Index == 0) // a fast search, for sorted lists
      {
        Console.WriteLine("\tstudent2 ({0}) found: {1}", 
          student2.Name, studentList[student2Index].Name);
      }
      studentList.Insert(3, new Student("Ross"));
      Console.WriteLine("Removing {0}", studentList[3].Name);
      studentList.RemoveAt(3);  // deletes the element
      Console.WriteLine("Converting list to an array:");
      Student[] moreStudents = studentList.ToArray();
      Console.WriteLine("\tArray contents:");
      for (int i = 0; i < studentList.Count; i++)
      {
        Console.WriteLine("\t\t" + studentList[i].Name);
      }
      // wait for user to acknowledge the results
      Console.WriteLine("Press Enter to terminate...");
      Console.Read();
    }
  }
  public class Student : IComparable
  {
    private string sName;
    private double dGrade;
    public Student(string sName)
    {
      this.sName = sName;
    }
    public string Name
    {
      get { return sName; }
    }
    public double Grade
    {
      get { return dGrade; }
    }
    public int CompareTo(object rightObject)
    {
      Student leftStudent = this;
      if (!(rightObject is Student))
      {
        Console.WriteLine("CompareTo passed a nonstudent");
        return 0;
      }
      Student rightStudent = (Student)rightObject;
      if (String.Compare(leftStudent.Name, rightStudent.Name) > 0)
      {
        return 1;
      }
      if (String.Compare(leftStudent.Name, rightStudent.Name) < 0)
      {
        return -1;
      }
      return 0;
    }
  }
}
