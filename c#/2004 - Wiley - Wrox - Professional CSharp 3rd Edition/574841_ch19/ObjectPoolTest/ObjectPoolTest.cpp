// export LD_LIBRARY_PATH=/usr/local/lib
// g++ -I/usr/local/include/cppunit -L/usr/local/lib -lcppunit -ldl *.cpp

//#include "ObjectPool.h"
#include "ObjectPool.cpp"
#include "Serial.h"
#include "ObjectPoolTest.h"
#include <cppunit/TestAssert.h>
#include <cppunit/TestCaller.h>
#include <cppunit/TestResult.h>
#include <cppunit/ui/text/TestRunner.h>
#include <iostream>
#include <set>

void ObjectPoolTest::setUp()
{
}

void ObjectPoolTest::tearDown()
{
}

void ObjectPoolTest::testCreation()
{
  ObjectPool<Serial> myPool;
}

void ObjectPoolTest::testInvalidChunkSize()
{
  bool caughtException = false;

  try {
    ObjectPool<Serial> myPool(0);
  } catch (const invalid_argument& ex) {
    // ok. we were expecting an exception
    caughtException = true;
  }

  CPPUNIT_ASSERT(caughtException);
}

void ObjectPoolTest::testAcquire()
{
  ObjectPool<Serial> myPool;

  Serial& serial = myPool.acquireObject();

  CPPUNIT_ASSERT(serial.getSerialNumber() >= 0);
}


void ObjectPoolTest::testExclusivity()
{
  const int poolSize = 5;
  ObjectPool<Serial> myPool(poolSize);
  set<int> seenSerials;

  for (int i = 0; i < poolSize; i++) {
    Serial& nextSerial = myPool.acquireObject();

    // assert that this number hasn't been seen before
    CPPUNIT_ASSERT(seenSerials.find(nextSerial.getSerialNumber()) == 
		   seenSerials.end());

    // add this number to the set
    seenSerials.insert(nextSerial.getSerialNumber());
  }
}

void ObjectPoolTest::testRelease()
{
  const int poolSize = 5;
  ObjectPool<Serial> myPool(poolSize);

  Serial& originalSerial = myPool.acquireObject();

  int originalSerialNumber = originalSerial.getSerialNumber();

  // return the original object to the pool
  myPool.releaseObject(originalSerial);

  // now make sure that the original object is recycled before
  // a new chunk is created
  bool wasRecycled = false;
  for (int i = 0; i < poolSize; i++) {
    Serial& nextSerial = myPool.acquireObject();
    if (nextSerial.getSerialNumber() == originalSerialNumber) {
      wasRecycled = true;
      break;
    }
  }

  CPPUNIT_ASSERT(wasRecycled);
}

void ObjectPoolTest::testSimple()
{
  CPPUNIT_ASSERT(1 != 0);
}

CppUnit::Test* ObjectPoolTest::suite()
{
  CppUnit::TestSuite *suiteOfTests = new CppUnit::TestSuite( "ObjectPoolTest" );
  suiteOfTests->addTest( new CppUnit::TestCaller<ObjectPoolTest>( 
								    "testSimple", 
								    &ObjectPoolTest::testSimple ) );

  suiteOfTests->addTest( new CppUnit::TestCaller<ObjectPoolTest>( 
								    "testCreation", 
								    &ObjectPoolTest::testCreation ) );

  suiteOfTests->addTest( new CppUnit::TestCaller<ObjectPoolTest>( 
								    "testInvalidChunkSize", 
								    &ObjectPoolTest::testInvalidChunkSize ) );

  suiteOfTests->addTest( new CppUnit::TestCaller<ObjectPoolTest>( 
								    "testAcquire", 
								    &ObjectPoolTest::testAcquire ) );

  suiteOfTests->addTest( new CppUnit::TestCaller<ObjectPoolTest>( 
								    "testExclusivity", 
								    &ObjectPoolTest::testExclusivity ) );

  suiteOfTests->addTest( new CppUnit::TestCaller<ObjectPoolTest>( 
								    "testRelease", 
								    &ObjectPoolTest::testRelease ) );
  return suiteOfTests;
}

int main()
{
  CppUnit::TextUi::TestRunner runner;
  runner.addTest( ObjectPoolTest::suite() );
  runner.run();
  return 0;
}
