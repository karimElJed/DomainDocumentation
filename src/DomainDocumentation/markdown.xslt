<?xml version="1.0"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">

    <xsl:output method="text"/>

    <xsl:template match="/">
        <xsl:apply-templates select="member/summary" />
    </xsl:template>

    <xsl:template match="summary">
        
        <xsl:value-of select="normalize-space(.)" /> 
<!--        todo: see-Tags are deleted-->
    </xsl:template>

</xsl:stylesheet>