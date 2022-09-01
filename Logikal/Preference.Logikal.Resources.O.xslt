<?xml version="1.0" encoding="UTF-8"?>
<!--<xsl:stylesheet version="2.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:xs="http://www.w3.org/2001/XMLSchema" xmlns:fn="http://www.w3.org/2005/xpath-functions">
	<xsl:output method="xml" version="1.0" encoding="UTF-8" indent="yes" omit-xml-declaration="yes" />-->
<xsl:stylesheet version="2.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
	<xsl:output method="xml" version="1.0" omit-xml-declaration="yes" encoding="UTF-8" indent="yes"/>
	
	<xsl:param name="set" select="1"/>
		
		
		<xsl:template match="/">
			<Optimization>
				<xsl:apply-templates select="ProductionLot/ProductionSet[@productionSetNumber=$set]/Machines/Machine/Reference/Rod"/>
			</Optimization>
		</xsl:template>
		
		
	<xsl:template match="Rod">
		<ProfileBar>
		<xsl:attribute name="ArticleCode"><xsl:value-of select="parent::Reference/@materialReference"/></xsl:attribute>
		<xsl:attribute name="SawId"><xsl:value-of select="ancestor::Machine/@machineId"/></xsl:attribute>
		<xsl:attribute name="Length"><xsl:value-of select="@length"/></xsl:attribute>
		<xsl:attribute name="InnerColorType">0</xsl:attribute>
		<xsl:attribute name="OuterColorType">0</xsl:attribute>
		
		<xsl:for-each select="descendant::CutPiece/CutInstance">
			<ProfileCut>
				<xsl:attribute name="ProfileGUID"><xsl:value-of select="@parameter"/></xsl:attribute>
				<xsl:attribute name="Waggon"><xsl:value-of select="@container"/></xsl:attribute>
				<xsl:attribute name="Slot"><xsl:value-of select="@slot"/></xsl:attribute>
				<xsl:variable name="Barcode">
					<xsl:value-of select="format-number(ancestor::ProductionLot/@productionLotNumber,'0000')"/>
					<xsl:value-of select="format-number($set,'00')"/>
					<xsl:value-of select="format-number(ancestor::Machine/@machineId,'00')"/>
					<xsl:value-of select="format-number(@absoluteNumber,'0000')"/>
				</xsl:variable>
				<xsl:attribute name="Barcode"><xsl:value-of select="$Barcode"/></xsl:attribute>
			</ProfileCut>		
		</xsl:for-each>
		
		</ProfileBar>
	</xsl:template>
	
	
	
	
	
</xsl:stylesheet>
