#!/bin/bash
XSLT="../xslt/bin/Release/xslt.exe"
SRCDIR=../../src/$1
INPUT=${SRCDIR}/stringresource.xml

#Neutral
$XSLT -Dlang=en -s sr.xslt -i ${INPUT} -o ${SRCDIR}/strings.resx

#Japanese
$XSLT -Dlang=ja -s sr.xslt -i ${INPUT} -o ${SRCDIR}/strings_ja.resx
