set version=2.3
set batchBuilder="C:\Program Files\Tekla Structures\2023.0\bin\TeklaExtensionPackage.BatchBuilder.exe"
set tsepName=DDBIMDrawingNumberingTool
set folderName=DDBIMDrawingNumberingTool

set outdir=%userprofile%\desktop\tsep-output\%folderName%\tsep
set logDir=%userprofile%\desktop
mkdir "%outdir%"

set infile=%CD%\TsepDefinition.xml
set outfile=%outdir%\%tsepName%_Tekla2016-Tekla2024.tsep
%batchBuilder% -i "%infile%" -o "%outfile%" -l "%logDir%" -v "%version%" -a 
echo Exit Code is %errorlevel%

set infile=%CD%\TsepDefinition_2025plus.xml
set outfile=%outdir%\%tsepName%_Tekla2025_or_newer.tsep
%batchBuilder% -i "%infile%" -o "%outfile%" -l "%logDir%" -v "%version%" -a 
echo Exit Code is %errorlevel%

pause