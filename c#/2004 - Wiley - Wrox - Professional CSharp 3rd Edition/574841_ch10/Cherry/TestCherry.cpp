// TestCherry.cpp

#include <iostream>

#include "BingCherry.h"
#include "BingCherryTree.h"

int main(int argc, char** argv)
{
  BingCherryTree theTree;
  Cherry* theCherry = theTree.pick();
  theCherry->printType();
  delete theCherry;
}

