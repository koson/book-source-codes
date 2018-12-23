#include <algorithm>
#include <functional>
#include <map>
#include <list>
#include <iostream>
#include <utility>
#include <string>
using namespace std;

void printString(const string& str)
{
  cout << " {" << str << "}";
}

void printCounty(const pair<const string, list<string> >& county)
{
  cout << county.first << ":";
  for_each(county.second.begin(), county.second.end(), &printString);
  cout << endl;
}

//
// NameInList
//
// Functor to check if a string is in a list of strings (supplied
// at construction time).
//
class NameInList : public unary_function<string, bool>
{
public:
  NameInList(const list<string>& names) : mNames(names) {}
  bool operator() (const string& val);

protected:
  const list<string>& mNames;
};

//
// function-call operator for NameInList functor
//
// Returns true if it can find name in mNames, false otherwise.
// Uses find() algorithm.
//
bool NameInList::operator() (const string& name)
{
  return (find(mNames.begin(), mNames.end(), name) != mNames.end());
}


//
// RemoveNames
//
// Functor class that takes a string/list<string> pair and removes
// any strings from the list that are found in a list of names
// (supplied in the constructor).
//
class RemoveNames : public unary_function<pair<const string, list<string> >,
    void>
{
public:
  RemoveNames(const list<string>& names) : mNames(names) {}
  void operator() (pair<const string, list<string> >& val);

protected:
  const list<string>& mNames; 
};



//
// Function-call operator for RemoveNames functor.
//
// Uses remove_if() followed by erase to actually delete the names
// from the list.
//
// Names are removed if they are in our list of mNames. Use the NameInList
// functor to check if the name is in the list.
//
void RemoveNames::operator() (pair<const string, list<string> >& val)
{
  list<string>::iterator it = remove_if(val.second.begin(), val.second.end(),
					NameInList(mNames));
  val.second.erase(it, val.second.end());
} 

//
// getDuplicates()
//
// Returns a list of all names that appear in more than one list in
// the map.
//
// The implementation generates one large list of all the names from
// all the lists in the map, sorts it, then finds all duplicates
// in the sorted list with adjacent_find().
//
list<string> getDuplicates(const map<string, list<string> >& voters)
{
  list<string> allNames, duplicates;

  // Collect all the names from all the lists into one big list
  map<string, list<string> >::const_iterator it;
  for(it = voters.begin(); it != voters.end(); ++it) {
    allNames.insert(allNames.end(), it->second.begin(), it->second.end());
  }

  // sort the list -- use the list version, not the general algorithm,
  // because the list version is faster
  allNames.sort();

  //
  // Now that it's sorted, all duplicate names will be next to each other.
  // Use adjacent_find() to find instances of two or more identical names
  // next to each other.
  //
  // Loop until adjacent_find returns the end iterator.
  //
  list<string>::iterator lit;
  for (lit = allNames.begin(); lit != allNames.end(); ++lit) {
    lit = adjacent_find(lit, allNames.end());
    if (lit == allNames.end()) {
      break;
    }
    duplicates.push_back(*lit);
  }

  //
  // If someone was on more than two voter lists, he or she will
  // show up more than once in the duplicates list. Sort the list
  // and remove duplicates with unique.
  //
  // Use the list versions because they are faster than the generic versions.
  //
  duplicates.sort();
  duplicates.unique();

  return (duplicates);
}


//
// auditVoterRolls
//
// Expects a map of string/list<string> pairs keyed on county names
// and containing lists of all the registered voters in those counties.
//
// Removes from each list any name on the convictedFelons list and
// any name that is found on any other list.
//
void auditVoterRolls(map<string, list<string> >& votersByCounty,
		     const list<string>& convictedFelons)
{
  // get all the duplicate names
  list<string> duplicates = getDuplicates(votersByCounty);

  // combine the duplicates and convicted felons -- we want
  // to remove names on both lists from all voter rolls
  duplicates.insert(duplicates.end(), convictedFelons.begin(),
		    convictedFelons.end());

  // If there were any duplicates, remove them.
  // Use the list versions of sort and unique instead of the generic
  // algorithms, because the list versions are more efficient.
  duplicates.sort();
  duplicates.unique();

  // Now remove all the names we need to remove
  for_each(votersByCounty.begin(), votersByCounty.end(),
	   RemoveNames(duplicates));
}

int main(int argc, char** argv)
{
  map<string, list<string> > voters;
  list<string> nameList, felons;
  nameList.push_back("Amy Aardvark");
  nameList.push_back("Bob Buffalo");
  nameList.push_back("Charles Cat");
  nameList.push_back("Dwayne Dog");

  voters.insert(make_pair("Orange", nameList));

  nameList.clear();
  nameList.push_back("Elizabeth Elephant");
  nameList.push_back("Fred Flamingo");
  nameList.push_back("Amy Aardvark");

  voters.insert(make_pair("Los Angeles", nameList));

  nameList.clear();
  nameList.push_back("George Goose");
  nameList.push_back("Heidi Hen");
  nameList.push_back("Fred Flamingo");

  voters.insert(make_pair("San Diego", nameList));

  felons.push_back("Bob Buffalo");
  felons.push_back("Charles Cat");

  for_each(voters.begin(), voters.end(), &printCounty);
  cout << endl;
  auditVoterRolls(voters, felons);
  for_each(voters.begin(), voters.end(), &printCounty);

  return (0);
}
