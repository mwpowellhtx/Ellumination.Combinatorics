@echo off

setlocal

rem We do not publish the API key as part of the script itself.
if "%my_nuget_api_key%" == "" (
    echo You are prohibited from making these sorts of changes.
    goto :end
)

rem Default list delimiter is Comma (,).
:redefine_delim
if "%delim%" == "" (
    set delim=,
)
rem Redefine the delimiter when a Dot (.) is discovered.
rem Anticipates potentially accepting Delimiter as a command line arg.
if "%delim%" == "." (
    set delim=
    goto :redefine_delim
)

rem Go ahead and pre-seed the Projects up front.
:set_projects
set projects=
rem Setup All Projects
set all_projects=Ellumination.Combinatorics.Combinatorials
set all_projects=%all_projects%%delim%Ellumination.Combinatorics.Permutations
rem Setup Combinatorials
set combinatorials_projects=Ellumination.Combinatorics.Combinatorials
rem Setup Permutations
set permutations_projects=Ellumination.Combinatorics.Permutations

:parse_args

:set_pause
if "%1" == "--pause" (
    set should_pause=1
    goto :next_arg
)
if "%1" == "--no-pause" (
    set should_pause=0
    goto :next_arg
)

rem Done parsing the args.
if "%1" == "" (
    goto :end_args
)

:set_dry_run
if "%1" == "--dry" (
    set dry=true
    goto :next_arg
)
if "%1" == "--dry-run" (
    set dry=true
    goto :next_arg
)

:set_config
if "%1" == "--config" (
    set config=%2
    shift
    goto :next_arg
)

:set_config
if "%1" == "--drive" (
    set drive_letter=%2
    shift
    goto :next_arg
)
if "%1" == "--drive-letter" (
    set drive_letter=%2
    shift
    goto :next_arg
)

:set_publish_local
if "%1" == "--local" (
    set function=local
    goto :next_arg
)

:set_publish_nuget
if "%1" == "--nuget" (
    set function=nuget
    goto :next_arg
)

:add_comb_projects
if "%1" == "--combinatorials" (
    if "%projects%" == "" (
        set projects=%combinatorials_projects%
    ) else (
        set projects=%projects%%delim%%combinatorials_projects%
    )
	goto :next_arg
)
if "%1" == "--comb" (
    if "%projects%" == "" (
        set projects=%combinatorials_projects%
    ) else (
        set projects=%projects%%delim%%combinatorials_projects%
    )
	goto :next_arg
)

:add_perm_projects
if "%1" == "--permutations" (
    if "%projects%" == "" (
        set projects=%permutations_projects%
    ) else (
        set projects=%projects%%delim%%permutations_projects%
    )
	goto :next_arg
)
if "%1" == "--perm" (
    if "%projects%" == "" (
        set projects=%permutations_projects%
    ) else (
        set projects=%projects%%delim%%permutations_projects%
    )
	goto :next_arg
)

:add_all_projects
if "%1" == "--all" (
    rem Prepare to publish All Projects.
    set projects=%all_projects%
    goto :next_arg
)

:add_project
if "%1" == "--project" (
    rem Add a Project to the Projects list.
    if "%projects%" == "" (
        set projects=%2
    ) else (
        set projects=%projects%%delim%%2
    )
    shift
    goto :next_arg
)

:next_arg

shift

goto :parse_args

:end_args

:verify_args

:verify_function
if "%function%" == "" (
    set function=local
)

:verify_projects
if "%projects%" == "" (
    rem In which case, there is nothing else to do.
    echo Nothing to process.
    goto :end
)

:verify_config
if "%config%" == "" (
    rem Assumes Release Configuration when not otherwise specified.
    set config=Release
)

:publish_projects

:set_vars
if "%should_pause%" == "" set should_pause=1
if "%drive_letter%" == "" set drive_letter=F:

set xcopy_exe=xcopy.exe
set xcopy_opts=/y /f
set xcopy_dest_dir=%drive_letter%\Dev\NuGet\local\packages
rem Expecting NuGet to be found in the System Path.
set nuget_exe=NuGet.exe
set nuget_push_verbosity=detailed
set nuget_push_source=https://api.nuget.org/v3/index.json

set nuget_push_opts=%nuget_push_opts% %my_nuget_api_key%
set nuget_push_opts=%nuget_push_opts% -Verbosity %nuget_push_verbosity%
set nuget_push_opts=%nuget_push_opts% -NonInteractive
set nuget_push_opts=%nuget_push_opts% -Source %nuget_push_source%

rem Do the main areas here.
pushd ..\..

if not "%projects%" == "" (
    echo Processing '%config%' configuration for '%projects%' ...
)
:next_project
if not "%projects%" == "" (
    for /f "tokens=1* delims=%delim%" %%p in ("%projects%") do (
        if "%function%" == "nuget" call :publish_nuget %%p
        if "%function%" == "local" call :publish_local %%p
        set projects=%%q
        goto :next_project
    )
)

popd

goto :end

:publish_local
for %%f in ("%1\bin\%config%\%1.*.nupkg") do (
    if "%dry%" == "true" (
        echo Dry run: %xcopy_exe% %%f %xcopy_dest_dir% %xcopy_opts%
    ) else (
        echo Running: %xcopy_exe% %%f %xcopy_dest_dir% %xcopy_opts%
        %xcopy_exe% %%f %xcopy_dest_dir% %xcopy_opts%
    )
)
exit /b

:publish_nuget
for %%f in ("%1\bin\%config%\%1.*.nupkg") do (
    if "%dry%" == "true" (
        echo Dry run: %nuget_exe% push "%%f"%nuget_push_opts%
    ) else (
        echo Running: %nuget_exe% push "%%f"%nuget_push_opts%
        %nuget_exe% push "%%f"%nuget_push_opts%
    )
)
exit /b

:end

endlocal

if "%should_pause%" == "1" pause

:fini
