#include <jni.h>
#include "HelloCpp.h"
#include <iostream>

void Java_HelloCpp_callCpp(JNIEnv* env, jobject javaobj)
{
  std::cout << "Hello from C++!" << std::endl;
}
