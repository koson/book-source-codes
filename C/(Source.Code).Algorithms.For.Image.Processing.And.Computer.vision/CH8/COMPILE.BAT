gcc -o baird baird.c lib.c -lm
coff2exe -s c:\gnu\bin\go32.exe baird     
del baird
gcc -o dists dists.c -lm
coff2exe -s c:\gnu\bin\go32.exe dists        
del dists
gcc -o hough hough.c lib.c -lm
coff2exe -s c:\gnu\bin\go32.exe hough        
del hough
gcc -o hskew hskew.c lib.c -lm
coff2exe -s c:\gnu\bin\go32.exe hskew        
del hskew
gcc -o kfill kfill.c lib.c -lm
coff2exe -s c:\gnu\bin\go32.exe kfill        
del kfill
gcc -o learn learn.c lib.c -lm
coff2exe -s c:\gnu\bin\go32.exe learn        
del learn
gcc -o learn2 learn2.c lib.c -lm
coff2exe -s c:\gnu\bin\go32.exe learn2       
del learn2
gcc -o learn3 learn3.c lib.c -lm
coff2exe -s c:\gnu\bin\go32.exe learn3       
del learn3
gcc -o ocr1 ocr1.c lib.c -lm
coff2exe -s c:\gnu\bin\go32.exe ocr1         
del ocr1
gcc -o ocr2 ocr2.c lib.c -lm
coff2exe -s c:\gnu\bin\go32.exe ocr2         
del ocr2
gcc -o ocr3 ocr3.c lib.c -lm
coff2exe -s c:\gnu\bin\go32.exe ocr3      
del ocr3
gcc -o segcost segcost.c lib.c -lm
coff2exe -s c:\gnu\bin\go32.exe segcost     
del segcost
gcc -o segpp segpro.c lib.c -lm
coff2exe -s c:\gnu\bin\go32.exe segpro       
del segpro
gcc -o sig sig.c lib.c -lm
coff2exe -s c:\gnu\bin\go32.exe sig          
del sig
gcc -o slhist slhist.c lib.c -lm
coff2exe -s c:\gnu\bin\go32.exe slhist       
del slhist
gcc -o vect vect.c lib.c -lm
coff2exe -s c:\gnu\bin\go32.exe vect         
del vect

