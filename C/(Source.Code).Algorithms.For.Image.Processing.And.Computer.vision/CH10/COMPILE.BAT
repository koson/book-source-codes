gcc -o gapoly gapoly.c -lm 
coff2exe -s c:\gnu\bin\go32.exe gapoly
gcc -o ts tsset.c ipcgen.c lib.c -lm
coff2exe -s c:\gnu\bin\go32.exe ts
gcc -o ip ipset.c ipcgen.c lib.c -lm
coff2exe -s c:\gnu\bin\go32.exe ip
gcc -o gauset gauset.c fpcgen.c lib.c -lm
coff2exe -s c:\gnu\bin\go32.exe gauset
gcc -o conset conset.c fpcgen.c lib.c -lm
coff2exe -s c:\gnu\bin\go32.exe conset
gcc -o drawcon drawcon.c lib.c -lm
coff2exe -s c:\gnu\bin\go32.exe drawcon
gcc -o geoset geoset.c lib.c -lm
coff2exe -s c:\gnu\bin\go32.exe geoset

