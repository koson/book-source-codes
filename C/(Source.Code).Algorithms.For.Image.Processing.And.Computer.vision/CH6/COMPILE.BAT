gcc -o makepsf makepsf.c lib.c -lm
coff2exe -s c:\gnu\bin\go32.exe makepsf  
gcc -o align align.c lib.c -lm
coff2exe -s c:\gnu\bin\go32.exe align
gcc -o blur blur.c lib.c fftlib.c -lm    
coff2exe -s c:\gnu\bin\go32.exe blur     
gcc -o hiemph hiemph.c fftlib.c  lib.c -lm      
coff2exe -s c:\gnu\bin\go32.exe hiemph     
gcc -o hipass hipass.c fftlib.c  lib.c -lm      
coff2exe -s c:\gnu\bin\go32.exe hipass     
gcc -o homo homo.c  fftlib.c   lib.c -lm      
coff2exe -s c:\gnu\bin\go32.exe homo     
gcc -o inverse inverse.c fftlib.c lib.c -lm      
coff2exe -s c:\gnu\bin\go32.exe inverse     
gcc -o lopass lopass.c fftlib.c  lib.c -lm      
coff2exe -s c:\gnu\bin\go32.exe lopass     
gcc -o mblur mblur.c lib.c -lm      
coff2exe -s c:\gnu\bin\go32.exe mblur     
gcc -o motion motion.c  lib.c -lm      
coff2exe -s c:\gnu\bin\go32.exe motion     
gcc -o notch notch.c  fftlib.c  lib.c -lm      
coff2exe -s c:\gnu\bin\go32.exe notch     
gcc -o shuffle shuffle.c  lib.c -lm      
coff2exe -s c:\gnu\bin\go32.exe shuffle     
gcc -o slow1 slow1.c -lm      
coff2exe -s c:\gnu\bin\go32.exe slow1     
gcc -o slow2 slow2.c -lm      
coff2exe -s c:\gnu\bin\go32.exe slow2     
gcc -o slow3 slow3.c -lm      
coff2exe -s c:\gnu\bin\go32.exe slow3     
gcc -o slow4 slow4.c -lm      
coff2exe -s c:\gnu\bin\go32.exe slow4     
gcc -o snoise snoise.c fftlib.c  lib.c -lm      
coff2exe -s c:\gnu\bin\go32.exe snoise     
gcc -o snr snr.c  fftlib.c lib.c -lm      
coff2exe -s c:\gnu\bin\go32.exe snr     
gcc -o transfor fftlib.c transfor.c lib.c -lm      
coff2exe -s c:\gnu\bin\go32.exe transfor     
gcc -o wiener wiener.c fftlib.c lib.c -lm      
coff2exe -s c:\gnu\bin\go32.exe wiener     
