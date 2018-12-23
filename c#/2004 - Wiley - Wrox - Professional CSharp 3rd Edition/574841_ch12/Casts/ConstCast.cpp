void g(char* str)
{
  // function body omitted for brevity
}

void f(const char* str)
{
  // function body omitted for brevity
  g(const_cast<char*>(str));
  // function body omitted for brevity
}

int main(int argc, char** argv)
{
  char str[100];
  f(str);

  return (0);
}
