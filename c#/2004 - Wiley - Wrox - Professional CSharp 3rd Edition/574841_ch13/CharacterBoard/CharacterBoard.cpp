char** allocateCharacterBoard(int xDimension, int yDimension)
{
  char** myArray = new char*[xDimension]; //  Allocate first dimension

  for (int i = 0; i < xDimension; i++) {
    myArray[i] = new char[yDimension];  //  Allocate ith sub-array
  }

  return myArray;
}

void releaseCharacterBoard(char** myArray, int xDimension)
{
  for (int i = 0; i < xDimension; i++) {
    delete[] myArray[i];    //  Delete ith sub-arraysubarray
  }

  delete[] myArray;           //  Delete first dimension
} 

int main(int argc, char** argv)
{
  char** board = allocateCharacterBoard(7, 13);
  releaseCharacterBoard(board, 7);
}
