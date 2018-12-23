<?xml version="1.0" encoding="UTF-8"?>
<xsl:stylesheet
      xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
      version="1.0">
   <xsl:template match="/">
      <html>
         <body>
            <h1>Customer Listing</h1>
            <table border="1">
               <tr>
                  <th>Name</th>
                  <th>Age</th>
                  <th>Favorite Color</th>
               </tr>
               <xsl:for-each select="Customers/Customer">
                  <tr>
                     <td>
                        <xsl:value-of select="Name" />
                     </td>
                     <td>
                        <xsl:value-of select="Age" />
                     </td>
                     <td>
                        <xsl:value-of
                           select="FavoriteColor" />
                     </td>
                  </tr>
               </xsl:for-each>
            </table>
         </body>
      </html>
   </xsl:template>
</xsl:stylesheet>
