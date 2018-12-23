Imports PhotoAlbum

Module VBPhotoAlbum

    Sub Main()

        Dim oPA As New PhotoAlbum.Photo("vacation", "src_christmas_dec-1998_01.jpg", "Christmas in the Mountains")

        Console.Write(oPA.GetFullDescription())

    End Sub

End Module
