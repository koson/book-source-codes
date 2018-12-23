#include <list>
#include <vector>
#include <string>
#include <algorithm>
#include <iterator>
#include <fstream>
#include <iostream>
#include <sstream>
using namespace std;

//
// classLists is a vector of lists, one for each course. The lists
// contain the students enrolled in those courses. They are not sorted.
//
// droppedStudents is a list of students who failed to pay their
// tuition, so are dropped from their courses.
//
// The function returns a list of every enrolled (non-dropped) student in
// all the courses
//
list<string>
getTotalEnrollment(const vector<list<string> >& classLists,
		   const list<string>& droppedStudents)
{
  list<string> allStudents;

  // Concatenate all the course lists onto the master list
  for (size_t i = 0; i < classLists.size(); ++i) {
    allStudents.insert(allStudents.end(), classLists[i].begin(),
		       classLists[i].end());
  }

  // Sort the master list
  allStudents.sort();

  // Remove duplicate student names (those who are in multiple courses).
  allStudents.unique();

  //
  // Remove students who are on the dropped list.
  // Iterate through the drop list, calling remove on the
  // master list for each student in the dropped list.
  //
  for (list<string>::const_iterator it = droppedStudents.begin();
       it != droppedStudents.end(); ++it) {
    allStudents.remove(*it);
  }

  // done!
  return (allStudents);
}

void readStudentList(list<string> &students, ifstream &istr)
{
  string name;
  while (getline(istr, name)) {
    cout << "Read name " << name << endl;
    students.push_back(name);
  }
}

void readCourseLists(vector<list<string> > &lists)
{
  for(int i = 1; ; i++) {
    ostringstream ostr;
    ostr << "course" << i << ".txt";

    ifstream istr(ostr.str().c_str());
    if (!istr) {
      cout << "Failed to open " << ostr.str() << endl;
      break;
    }
    list<string> newList;
    readStudentList(newList, istr);
    lists.push_back(newList);
  }
}

void readDroppedStudents(list<string> &students)
{
  ifstream istr("dropped.txt");
  readStudentList(students, istr);
}

int main(int argc, char **argv)
{
  vector<list<string> > courseLists;
  list<string> droppedStudents;

  readCourseLists(courseLists);
  readDroppedStudents(droppedStudents);

  list<string> finalList = getTotalEnrollment(courseLists, droppedStudents);

  copy(finalList.begin(), finalList.end(), ostream_iterator<string>(cout, "\n"));
}
