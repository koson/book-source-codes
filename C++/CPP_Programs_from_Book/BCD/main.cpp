
#include <iostream>
#include <ctime>

using namespace std;

typedef int T;

// the following macro causes the integer equivalent to be stored
// in the object; this makes it a lot easier to follow the value
// of the object in the debugger; set WATCH to null when not debugging
//#define WATCH(x) (x.toInt())
#define WATCH(x)

int max(int l, int r)
{
    return (l > r)? l : r;
}

int min(int l, int r)
{
    return (l < r)? l : r;
}

#define BLKSIZE 100

/**
 * BCD class implements a BCD number.
 * This can be any number of digits in length. The
 * maxdigits specifies how much room is currently available in
 * the object but the object autogrows (it does not autoshrink)
 *
 */
class BCD
{
  protected:
    int nDigits;/**< The number of digits in the number. */
    int nMaxDigits;/**< The number of digits that this object has room for. */
    int nValue;/**<  integer equivalent; only used when debugging.*/
    T* pDigits;/**< A pointer to the BCD digits. Store LSD first. */
    char* pCharRep; /**< used to store the character representation created by toString(). */

  public:
    /** \brief create a BCD object.
     *
     * \param 0 int n= the initial value of the object
     * \param BLKSIZE int nMax= the number of digits to allocate space for
     *
     */
    BCD(int n = 0, int nMax = BLKSIZE)
    {
        // start with BLKSIZE number of digits
        nMaxDigits = nMax;
        pDigits = new T[nMaxDigits];
        nValue = n;
        pCharRep = 0;

        // now store the int value passed into the digits
        for(nDigits = 1; nDigits < nMaxDigits; nDigits++)
        {
            // get the next rightmost digit and store it
            pDigits[nDigits-1] = (T)(n % 10);

            // now lop off that digit; quit if the
            // the result is zero
            n /= 10;
            if (n == 0)
            {
                break;
            }
        }
    }
    BCD(const BCD& r)
    {
        nMaxDigits = r.nMaxDigits;
        nDigits = r.nDigits;
        nValue = r.nValue;
        pDigits = new T[nMaxDigits];
        pCharRep = 0;

        // now copy the contents
        for(int i = 0; i < nDigits; i++)
        {
            pDigits[i] = r.pDigits[i];
        }
    }

   ~BCD()
    {
        delete pDigits;
        delete pCharRep;
    }

    // getSize
    int getSize() const { return nDigits;}
    int getMaxSize() const { return nMaxDigits; }

    // addDigit - add another digit to the BCD number
    void addDigit(T n)
    {
        // if the number of digits exceeds the maximum size, then
        // allocation a new block
        if (nDigits >= nMaxDigits)
        {
            nMaxDigits *= 2;
            T* pS = pDigits;
            pDigits = new T[nMaxDigits];
            for(int i = 0; i < nDigits; i++)
            {
                pDigits[i] = pS[i];
            }
            delete[] pS;
        }

        pDigits[nDigits] = n;
        nDigits++;

    }

    // toInt - calculate the integer value (if possible)
    int toInt()
    {
        // notice that nValue is a member of the class
        // (it's only used during debugging to watch the
        // value of the object)
        nValue = 0;
        for(int i = nDigits; i > 0; i--)
        {
            nValue *= 10;
            nValue += pDigits[i-1];
        }
        return nValue;
    }

    // toString - create an ASCII representation of BCD string
    char* toString()
    {
        // allocate space for the character representation
        if (pCharRep)
        {
            delete pCharRep;
        }
        pCharRep = new char[2*nDigits];

        const char* digitReps="0123456789";

        // remember that the BCD number is stored backwards (LSD first)
        // start at the rightmost digit (MSD) and work to the left
        int offset = 0;
        for (int i = nDigits; i; i--)
        {
            // convert that digit into a character and move the
            // ascii pointer to the right
            pCharRep[offset++] = digitReps[(int)pDigits[i - 1]];

            // put commas every 3 digits for numbers greater than 9999
            if ((i > 3) && (((i - 1) % 3) == 0))
            {
                pCharRep[offset++] = ',';
            }
        }

        // don't forget to null terminate the string pointer
        pCharRep[offset] = '\0';
        return pCharRep;
    }

    // trailingZeros - return the number of trailing zeros in the BCD number
    int trailingZeros()
    {
        // loop thru the BCD number up to the number of digits in the number
        int n;
        for (n = 0; n < nDigits; n++)
        {
            // stop on the first non-zero digit
            if (pDigits[n] != 0)
            {
                break;
            }
        }

        // return the number of digits that were zero
        return n;
    }

    BCD& operator=(const BCD& s)
    {
        if (pDigits)
        {
            delete pDigits;
        }

        nMaxDigits = s.nMaxDigits;
        nDigits = s.nDigits;
        nValue  = s.nValue;
        pDigits = new T[nMaxDigits];
        pCharRep = 0;

        for(int i = 0; i < nDigits; i++)
        {
            pDigits[i] = s.pDigits[i];
        }
        return *this;
    }

    bool isZero() const
    {
        return sumDigits() == 0;
    }

    // sumDigits - return the sum of the digits of the BCD number
    int sumDigits() const
    {
        int n = 0;
        for (int i = 0; i < nDigits; i++)
        {
            n += (int)pDigits[i];
        }
        return n;
    }

    // rationalize - reduce the number of digits in the number if
    //               the most significant digit is 0
    BCD& rationalize()
    {
        while(pDigits[nDigits-1] == 0 && nDigits > 1)
        {
            nDigits--;
        }
        return *this;
    }

    // operator[] - the l-value version allocates room for any extra digits,
    //              the r-value version assumes that digits that are off the end
    //              are zero without actually adding another digit
    T& operator[](int index)
    {
        while(index >= nDigits)
        {
            addDigit(0);
        }
        return pDigits[index];
    }
    T operator[](int index) const
    {
        if (index >= nDigits || index < 0)
        {
            return 0;
        }
        return pDigits[index];
    }
};

// operator+ - add two BCD numbers, the trick here is handling
//             the carry from one digit to the next
BCD operator+(const BCD& l, const BCD& r)
{
    // create a sum - initialize to 0 and set the
    //                max size equal to the max of l.maxsize and r.maxsize
    int maxDigits = max(l.getMaxSize(), r.getMaxSize());
    BCD sum(0, maxDigits);

    // add up to the number of digits in the lesser number
    // remembering to include the carry from the previous digit
    // sum
    T carry = 0;
    int m = max(l.getSize(), r.getSize());
    for(int i = 0; i < m; i++)
    {
        T s = l[i] + r[i] + carry;

        carry = 0;
        if (s > 10)
        {
            s -= 10;
            carry = 1;
        }
        sum[i] = s;
    }

    // return the result
    return sum;
}

// compareWShift - compare two BCD numbers (the right one with
//                 a possible left shift of 'shift' digits); return
//                 a 1 if l > r, a -1 if l < r and a 0 if l == r
//                 (we're only comparing over the same number of digits
//                 between l and r
int compareWShift(const BCD& l, const BCD& r, int shift = 0)
{
    int size = max(l.getSize(), r.getSize() + shift);

    for(int i = size - 1; i >= shift; i--)
    {
        int diff = l[i] - r[i - shift];
        if (diff > 0)
        {
            return 1;
        }
        if (diff < 0)
        {
            return -1;
        }
    }

    return 0;
}
bool operator>(const BCD& l, const BCD& r)
{
    return compareWShift(l, r) > 0;
}
bool operator<(const BCD& l, const BCD& r)
{
    return compareWShift(l, r) < 0;
}
bool operator==(const BCD& l, const BCD& r)
{
    return compareWShift(l, r) == 0;
}
bool operator>=(const BCD& l, const BCD& r)
{
    return compareWShift(l, r) >= 0;
}
bool operator<=(const BCD& l, const BCD& r)
{
    return compareWShift(l, r) <= 0;
}

//operator- - subtraction (of course)
//            return a zero if r > l
BCD subWShift(BCD& l, const BCD& r, int shift = 0)
{
    // if r (shifted) > l...
    if (compareWShift(l, r, shift) <= 0)
    {
        // ...then return a zero
        l = BCD(0);
        return l;
    }


    // ok - iterate thru the digits
    int m = l.getSize();
    for (int i = shift; i < m; i++)
    {
        // subtract the right from the left;
        // start at the shifted location
        T ld = l[i]; T rd = r[i - shift];
        l[i] = ld - rd;


        // if the result is negative then borrow the extra from the next higher
        // digit; keep doing this until we don't
        // need to borrow anymore
        for (int n = i; (l[n] < 0) && (n < (m-1)); n++)
        {
            l[n] += 10;
            l[n+1]--;
        }
    }
    WATCH(l);
    return l;
}

BCD operator-(const BCD& l, const BCD& r)
{
    BCD d(l);
    return subWShift(d, r);
}

// operator* - multiply two BCD numbers, this is tricky but follows
//             the way that humans multiply numbers
T remainders[100] =   {0, 1, 2, 3, 4, 5, 6, 7, 8, 9,
                       0, 1, 2, 3, 4, 5, 6, 7, 8, 9,
                       0, 1, 2, 3, 4, 5, 6, 7, 8, 9,
                       0, 1, 2, 3, 4, 5, 6, 7, 8, 9,
                       0, 1, 2, 3, 4, 5, 6, 7, 8, 9,
                       0, 1, 2, 3, 4, 5, 6, 7, 8, 9,
                       0, 1, 2, 3, 4, 5, 6, 7, 8, 9,
                       0, 1, 2, 3, 4, 5, 6, 7, 8, 9,
                       0, 1, 2, 3, 4, 5, 6, 7, 8, 9,
                       0, 1, 2, 3, 4, 5, 6, 7, 8, 9};
T modulas[100] =      {0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
                       1, 1, 1, 1, 1, 1, 1, 1, 1, 1,
                       2, 2, 2, 2, 2, 2, 2, 2, 2, 2,
                       3, 3, 3, 3, 3, 3, 3, 3, 3, 3,
                       4, 4, 4, 4, 4, 4, 4, 4, 4, 4,
                       5, 5, 5, 5, 5, 5, 5, 5, 5, 5,
                       6, 6, 6, 6, 6, 6, 6, 6, 6, 6,
                       7, 7, 7, 7, 7, 7, 7, 7, 7, 7,
                       8, 8, 8, 8, 8, 8, 8, 8, 8, 8,
                       9, 9, 9, 9, 9, 9, 9, 9, 9, 9};
BCD operator*(const BCD& l, const BCD& r)
{
    int lsize = l.getSize();
    int rsize = r.getSize();
    int maxSize = lsize + rsize;

    BCD product(0, maxSize);

    for(int i = 0; i < lsize; i++)
    {
        // we can skip zero digits (to save time)
        if (l[i] == 0)
        {
            continue;
        }

        for(int j = 0; j < rsize; j++)
        {
            if (r[j] == 0)
            {
                continue;
            }

            T p = l[i] * r[j];

            T carry = 0;
            for (int k = 0; (p > 0) || (carry != 0); k++)
            {
                int offset = i + j + k;
                T n = product[offset] + carry + remainders[(int)p];
                carry = 0;
                if (n >= 10)
                {
                    n -= 10;
                    carry = 1;
                }
                product[offset] = n;

                p = modulas[(int)p];
            }
        }
    }
    return product;
}

// division - this is integer division
BCD operator/(const BCD& n, const BCD& d)
{
    BCD q(0);
    // first - if d > n then the answer is zero
    if (d > n)
    {
        return q;
    }

    // figure the remainder
    BCD r(n);

    // now find out how far to left shift the denominator
    int s = 0;
    while (compareWShift(r, d, s + 1) >= 0)
    {
        s++;
    }

    // now start figuring digits in q;
    // start with denominator shifted left by s digits
    for(;s >= 0; s--)
    {
        // as long as remainder > denominator...
        while(compareWShift(r, d, s) >= 0)
        {
            // ...then subtract out another one
            r = subWShift(r, d, s);
            q[s]++;
            WATCH(q);
        }
    }

    return q;
}

// BCDFactorial - calculate factorial using BCD class
BCD BCDFactorial(int f)
{
    // start with a product of 1
    BCD product(1);

    // now multiply the product by the BCD equivalent of n
    // product = 100 * 99 * 98...
    int io = 2;
    for(int n = 2; n <= f; n++)
    {
        product = product * BCD(n);
        if (io++ >= 1000)
        {
            cout << n << " " << " " << product.getSize() << " " << product.getMaxSize() << endl;
            io = 1;
        }
    }
    return product;
}

// factorial - convential int factorial to compare with BCD
unsigned long factorial(unsigned long n)
{
    unsigned long f = 1;
    while(n > 1)
    {
        f*= n--;
    }
    return f;
}


int main()
{
    int n;
    for(;;)
    {
        cout << "Enter the number that you want to take factorial of\n(Enter 0 to quit)\n";
        cin  >> n;

        if (n <= 0)
        {
            break;
        }

        double d = clock();

        BCD f = BCDFactorial(n);
        cout << "The duration is " << clock() - d << "\n";
        cout << "The number of digits is " << f.getSize() << "\n";
        cout << "The number of trailing zeroes is " << f.trailingZeros() << "\n";

        cout << "BCDFactorial(" << n << ") is " << f.toString() << endl;
    }
    return 0;
}
