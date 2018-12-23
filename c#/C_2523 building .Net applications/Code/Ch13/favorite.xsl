<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
	<xsl:template match="/">
		<root>
			<xsl:apply-templates />
		</root>
	</xsl:template>
	<xsl:template match="photolibrary">
		<photolibrary>
			<xsl:apply-templates select="photo" />
		</photolibrary>
	</xsl:template>
	<xsl:template match="photo">
		<photo>
			<xsl:attribute name="genre">
				<xsl:value-of select="@genre" />
			</xsl:attribute>
			<title>
				<xsl:value-of select="title" />
			</title>
			<xsl:text></xsl:text>
		</photo>
	</xsl:template>
</xsl:stylesheet>
