echo on
copy NUL "C:\ProgramData\Trimble\Tekla Structures\2019.1\Extensions\Installed\{DDBIM Drawing Numbering Tool}{2.2}{71687cf2-ef12-4bd5-91f5-ba2cf1d3d667}\RemoveExtensionOnStartup"
"C:\Program Files\Tekla Structures\2019.1\nt\bin\TeklaExtensionPackage.TepAutoInstaller.exe" 2019.1 "C:\ProgramData\Trimble\Tekla Structures\2019.1" 0
copy "%userprofile%\Desktop\*.tsep" "C:\ProgramData\Trimble\Tekla Structures\2019.1\Extensions\To be installed\"
"C:\Program Files\Tekla Structures\2019.1\nt\bin\TeklaExtensionPackage.TepAutoInstaller.exe" 2019.1 "C:\ProgramData\Trimble\Tekla Structures\2019.1" 0
PAUSE
