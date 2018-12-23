<?xml version="1.0" encoding="UTF-8"?>
<xsl:stylesheet xmlns:xsl="http://www.w3.org/1999/XSL/Transform" version="1.0">
<xsl:param name='min_pos'>0</xsl:param>
<xsl:param name='max_pos'>0</xsl:param>


<xsl:template match="/">

<xsl:for-each select="//Book[position() &lt;= $max_pos and position() &gt;= $min_pos]">
  <xsl:apply-templates select="Title"/>
</xsl:for-each>

</xsl:template>
</xsl:stylesheet>

