class X {};
class Y {};

int main(int argc, char** argv)
{
  int i = 3;

  X x;
  Y y;

  X* xp;
  Y* yp;

  // Need reinterpret cast to perform pointer conversion from unrelated classes
  // static_cast doesn't work
  xp = reinterpret_cast<X*>(yp);

  // Need reinterpret_cast to go from pointer to int and from int to pointer
  i = reinterpret_cast<int>(xp);
  xp = reinterpret_cast<X*>(i);

  // Need reinterpret cast to perform reference conversion from unrelated
  // classes --  static_cast doesn't work
  X& xr = x;
  Y& yr = reinterpret_cast<Y&>(x);

  return (0);
}
