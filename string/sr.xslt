<?xml version="1.0" encoding="utf-8"?>

<xsl:stylesheet xmlns:xsl="http://www.w3.org/1999/XSL/Transform" version="1.0">
<xsl:output method="xml" version="1.0" encoding="utf-8" indent="yes"/>
<xsl:param name="lang"/>

<xsl:template match="/">
<root>
	<resheader name="ResMimeType">
		<value>text/microsoft-resx</value>
	</resheader>
	<resheader name="Version">
		<value>1.0.0.0</value>
	</resheader>
	<resheader name="Reader">
		<value>System.Resources.ResXResourceReader, System.Windows.Forms, Version=1.0.3102.0, Culture=neutral, PublicKeyToken=b77a5c561934e089</value>
	</resheader>
	<resheader name="Writer">
		<value>System.Resources.ResXResourceWriter, System.Windows.Forms, Version=1.0.3102.0, Culture=neutral, PublicKeyToken=b77a5c561934e089</value>
	</resheader>

  <xsl:apply-templates select="string-resource/*"/>
</root>
</xsl:template>

<xsl:template match="record">
<data>
<xsl:attribute name="name"><xsl:value-of select="@name"/></xsl:attribute>
<value><xsl:value-of select="*[name()=$lang]"/></value>
</data>
</xsl:template>

</xsl:stylesheet>
