
gcc -o bestzs bestzs.c lib.c -lm
coff2exe -s /gnu/bin/go32.exe  bestzs        
gcc -o contourh contourh.c lib.c -lm
coff2exe -s /gnu/bin/go32.exe contourh     
gcc -o mat mat.c lib.c -lm
coff2exe -s /gnu/bin/go32.exe mat          
gcc -o thnstent thnstent.c lib.c -lm
coff2exe -s /gnu/bin/go32.exe thnstent     
gcc -o thnstpp thnstpp.c lib.c -lm
coff2exe -s /gnu/bin/go32.exe thnstpp      
gcc -o thnz thnz.c lib.c -lm
coff2exe -s /gnu/bin/go32.exe thnz         
gcc -o thnzh thnzh.c lib.c -lm
coff2exe -s /gnu/bin/go32.exe thnzh        
gcc -o dist dist.c lib.c -lm
coff2exe -s /gnu/bin/go32.exe dist  
gcc -o lap lap.c lib.c -lm
coff2exe -s /gnu/bin/go32.exe lap  
