;#define Debug
#define CompanyName         "NetworkDLS"
#define ApplicationName     "K-LOC Calculator"
#define ApplicationVersion  "1.0.0.0"

#ifdef Debug
#define CompileType         "Debug"
#define AgumentBuild        " Debug"
#else
#define CompileType         "Release"
#define AgumentBuild        ""
#endif

[Setup]
;-- Main Setup Information
 AppName                         = {#ApplicationName}
 AppVerName                      = {#ApplicationName} {#ApplicationVersion}
 AppCopyright                    = Copyright © 2012-2016 {#CompanyName}.
 DefaultDirName                  = {pf}\{#CompanyName}\{#ApplicationName}
 DefaultGroupName                = {#CompanyName}\{#ApplicationName}
 UninstallDisplayIcon            = {app}\K-LOC Calculator.exe
 PrivilegesRequired              = admin
 Uninstallable                   = Yes
 Compression                     = ZIP/9
 MinVersion                      = 0.0,6.0
 ArchitecturesInstallIn64BitMode = x64
 ArchitecturesAllowed            = x86 x64
 OutputBaseFilename              = {#ApplicationName} Client {#ApplicationVersion}{#AgumentBuild}
 
;-- Windows 2000+ Support Dialog
 AppPublisher    = {#CompanyName}
 AppPublisherURL = http://www.NetworkDLS.com/
 AppUpdatesURL   = http://www.NetworkDLS.com/
 AppVersion      = {#ApplicationVersion}

[Files]
 Source: "..\..\..\OpenSource\@Resources\Setup\License\EULA.txt";         DestDir: "{app}";             
 Source: "..\..\..\OpenSource\@AutoUpdate\Win32\Release\AutoUpdate.Exe";  DestDir: "{app}";             Flags: RestartReplace;
 Source: "AutoUpdate.xml";                                                DestDir: "{app}";             Flags: IgnoreVersion;

;----------------------------------------------------------(NETFramework)----
 Source: "_Dependencies\NDP451-KB2859818-Web.exe";                        DestDir: "{tmp}";             Flags: deleteafterinstall; Check: DotNetFrameworkIsNotInstalled
 
;----------------------------------------------------------(DIS.DynamicIPClientService)----
 Source: "..\bin\{#CompileType}\*.config";  Excludes: "*vshost*"; DestDir: "{app}";              Flags: IgnoreVersion;
 Source: "..\bin\{#CompileType}\*.exe";     Excludes: "*vshost*"; DestDir: "{app}";              Flags: IgnoreVersion;
;Source: "..\bin\{#CompileType}\*.dll";     Excludes: "*vshost*"; DestDir: "{app}";              Flags: IgnoreVersion;

#ifdef Debug
 Source: "..\DIS.DynamicIPClientService\bin\{#CompileType}\*.pdb";     Excludes: "*vshost*"; DestDir: "{app}";              Flags: IgnoreVersion;
#endif
;---------------------------------------------------------------------

[Icons]
 Name: "{group}\{#ApplicationName}"; Filename: "{app}\K-LOC Calculator.exe";
 Name: "{group}\Update {#ApplicationName}";    Filename: "{app}\AutoUpdate.Exe"; WorkingDir: "{app}";

;---------------------------------------------------------------------

[Run]
 Filename: "{tmp}\NDP451-KB2859818-Web"; StatusMsg: "Installing .NET framework..."; Check: DotNetFrameworkIsNotInstalled
 Filename: "{app}\K-LOC Calculator.Exe"; Description: "Launch K-LOC Calculator now?"; Flags: PostInstall NoWait shellexec;
 
[Code]
//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

function DotNetFrameworkIsNotInstalled: Boolean;
begin
  Result := not RegKeyExists(HKEY_LOCAL_MACHINE, 'SOFTWARE\Microsoft\.NETFramework\policy\v4.0');
end;

//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

function InitializeSetup(): Boolean;
begin
    if DotNetFrameworkIsNotInstalled() then begin
        MsgBox('{#ApplicationName} requires Microsoft .NET Framework 4.5.'#13#13
          'The installer will attempt to install it', mbInformation, MB_OK);
    end

    result := true;
end;

//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
