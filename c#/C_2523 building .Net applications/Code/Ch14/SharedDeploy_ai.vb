' Equivalent to the using
Imports SharedPhotoAlbum

Module VBPhotoAlbum

    ' Main entry point into the console application (make sure that this is  
    ' set in the project properties as the startup object).
    Sub Main()
        ' Creating instance of Component with the constuctor that initializes the properties.
        Dim spaTest As New SharedPhotoAlbum.Photo("vacation", "src_christmas_dec-1998_01.jpg", "Christmas in the Mountains")
        Console.Write(spaTest.GetFullDescription())
    End Sub

End Module
