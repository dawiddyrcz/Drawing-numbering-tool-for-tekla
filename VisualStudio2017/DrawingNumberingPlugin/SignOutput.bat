echo on
signtool sign /n "Dawid Dyrcz" /t http://time.certum.pl/ /fd sha256 /v "bin\Debug\DrawingNumberingApp.exe" "bin\Debug\DrawingNumberingPlugin.dll"
PAUSE