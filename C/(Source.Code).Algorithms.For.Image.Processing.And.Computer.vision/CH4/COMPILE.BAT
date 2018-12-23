gcc -o fast fast.c lib.c -lm
coff2exe -s c:\gnu\bin\go32.exe fast
gcc -o text text.c lib.c -lm
coff2exe -s c:\gnu\bin\go32.exe text

