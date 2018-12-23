/**
 * Logger.h
 *
 * Definition of a true singleton logger class.
 */
#include <iostream>
#include <fstream>
#include <vector>

class Logger
{
 public:
  static const std::string kLogLevelDebug;
  static const std::string kLogLevelInfo;
  static const std::string kLogLevelError;

  // Returns a reference to the singleton Logger object
  static Logger& instance();

  // Logs a single message at the given log level
  void log(const std::string& inMessage, 
	   const std::string& inLogLevel);

  // Logs a vector of messages at the given log level
  void log(const std::vector<std::string>& inMessages, 
	   const std::string& inLogLevel);

 protected:
  // Static variable for the one-and-only instance  
  static Logger sInstance;

  // Constant for the file namename
  static const char* const kLogFileName;

  // Data member for the output stream
  std::ofstream mOutputStream;

 private:
  Logger();
  ~Logger();

};
