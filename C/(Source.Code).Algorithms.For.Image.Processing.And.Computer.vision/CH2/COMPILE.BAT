gcc -o max max.c 
coff2exe -s c:\gnu\bin\go32.exe max
gcc -o maxg maxg.c
coff2exe -s c:\gnu\bin\go32.exe maxg         
gcc -o bindil bindil.c mlib.c 
coff2exe -s c:\gnu\bin\go32.exe bindil         
gcc -o binero binero.c mlib.c
coff2exe -s c:\gnu\bin\go32.exe binero        

