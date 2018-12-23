/**
 * Logger.cpp
 *
 * Implementation of a singleton logger class.
 */
#include <string>
#include "Logger.h"

using namespace std;

const string Logger::kLogLevelDebug = "DEBUG";
const string Logger::kLogLevelInfo = "INFO";
const string Logger::kLogLevelError = "ERROR";

const char* const Logger::kLogFileName = "log.out";

bool Logger::sInitialized = false;
ofstream Logger::sOutputStream;

void Logger::log(const string& inMessage, const string& inLogLevel)
{
  if (!sInitialized) {
    init();
  }
  // print Print the message and flush the stream with endl.
  sOutputStream << inLogLevel << ": " << inMessage << endl;

}

void Logger::log(const vector<string>& inMessages, const string& inLogLevel)
{
  for (size_t i = 0; i < inMessages.size(); i++) {

    log(inMessages[i], inLogLevel);
  }
}

void Logger::teardown()
{
  if (sInitialized) {
    sOutputStream.close();
    sInitialized = false;
  }
}

void Logger::init()
{
  if (!sInitialized) {
    sOutputStream.open(kLogFileName, ios_base::app);
    if (!sOutputStream.good()) {
      cerr << "Unable to initialize the Logger!" << endl;
      return;
    }
    sInitialized = true;
  }
}

int main(int argc, char** argv)
{
  Logger::log("test message", Logger::kLogLevelDebug);

  vector<string> items;
  items.push_back("item1");
  items.push_back("item2");

  Logger::log(items, Logger::kLogLevelError);

  Logger::teardown();
}
