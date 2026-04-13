echo on
cd "C:\"
set tsversion=2026.0
set extensionDir="C:\TeklaStructures\%tsversion%\Extensions\Installed\{DDBIM Drawing Numbering Tool}*"
set extensionDir2="C:\TeklaStructures\%tsversion%\Extensions\Installed\{Dev DDBIM Drawing Numbering Tool}*"
cd %extensionDir%
copy NUL "RemoveExtensionOnStartup"
cd %extensionDir2%
copy NUL "RemoveExtensionOnStartup"
cd "C:\"
"C:\TeklaStructures\%tsversion%\bin\TeklaExtensionPackage.TepAutoInstaller.exe" %tsversion% "C:\TeklaStructures\%tsversion%" 0
PAUSE
copy "%userprofile%\desktop\tsep-output\DDBIMDrawingNumberingTool\tsep\*.tsep" "C:\TeklaStructures\%tsversion%\Extensions\To be installed\"
"C:\TeklaStructures\%tsversion%\bin\TeklaExtensionPackage.TepAutoInstaller.exe" %tsversion% "C:\TeklaStructures\%tsversion%" 0
PAUSE