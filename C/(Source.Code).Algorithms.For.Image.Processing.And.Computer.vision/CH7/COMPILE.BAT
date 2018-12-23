gcc -o recur recur.c -lm
coff2exe -s c:\gnu\bin\go32.exe recur
gcc -o bfwt bfwt.c  -lm
coff2exe -s c:\gnu\bin\go32.exe bfwt        
gcc -o bfwtr bfwtr.c  -lm
coff2exe -s c:\gnu\bin\go32.exe bfwtr          
gcc -o daub2d daub2d.c lib.c -lm
coff2exe -s c:\gnu\bin\go32.exe daub2d      
gcc -o dfilter dfilter.c lib.c -lm
coff2exe -s c:\gnu\bin\go32.exe dfilter       
gcc -o dthresh dthresh.c lib.c -lm
coff2exe -s c:\gnu\bin\go32.exe dthresh      
gcc -o fwt1d fwt1d.c  -lm
coff2exe -s c:\gnu\bin\go32.exe fwt1d        
gcc -o fwt2d fwt2d.c lib.c -lm
coff2exe -s c:\gnu\bin\go32.exe fwt2d       
gcc -o showdaub showdaub.c lib.c -lm
coff2exe -s c:\gnu\bin\go32.exe showdaub     
gcc -o unwalsh unwalsh.c lib.c -lm
coff2exe -s c:\gnu\bin\go32.exe unwalsh      
gcc -o walsh2d walsh2d.c lib.c -lm
coff2exe -s c:\gnu\bin\go32.exe walsh2d      
gcc -o wthresh wthresh.c lib.c -lm
coff2exe -s c:\gnu\bin\go32.exe wthresh      

