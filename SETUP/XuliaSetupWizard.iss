; Script generated by the Inno Setup Script Wizard.
; SEE THE DOCUMENTATION FOR DETAILS ON CREATING INNO SETUP SCRIPT FILES!

;-----------------------------------------------------------------------------
;#define PC_TRABAJO
#define PC_CASA
;-----------------------------------------------------------------------------

#define MyAppName "Xulia"
#define MyAppVersion "1.2.4"
#define MyAppPublisher "Antonio Losada González"
#define MyAppURL "https://www.novoser.org.br/xulia.html"
#define MyAppExeName "XULIA.exe"

#ifdef PC_TRABAJO
  ; Configuración PC Trabajo
  #define UnidadBase "F:"
  #define UnidadTomcat "F:"
  #define DirBase "\GOOGLEDRIVE\TONI\PROY\"
  #define UnidadBaseFicherosIntall "F:"
  #define DirBaseFicherosIntall "\INSTALL\NUEVA_INSTALL\Speech SDK\v11\setup"
#endif

#ifdef PC_CASA
  ; Configuración PC Casa
  #define UnidadBase "C:"
  #define DirSalida "\GOOGLE_DRIVE\GDRIVE\TONI\PROY\GITHUB\XULIA_SHARE"
  #define DirBase "\GOOGLE_DRIVE\GDRIVE\TONI\PROY\GITHUB\XULIA_SHARE\C_SHARP\AIMLGUI\AIMLGUI\bin\Debug"
  #define UnidadBaseFicherosIntall "D:"
  #define DirBaseFicherosIntall "\INSTALL\INSTALACION\Speech SDK\v11\setup"
#endif
[Setup]
; NOTE: The value of AppId uniquely identifies this application.
; Do not use the same AppId value in installers for other applications.
; (To generate a new GUID, click Tools | Generate GUID inside the IDE.)
AppId={{5A89B58C-E7E6-4971-859C-0266F4C9196D}
AppName={#MyAppName}
AppVersion={#MyAppVersion}
;AppVerName={#MyAppName} {#MyAppVersion}
AppPublisher={#MyAppPublisher}
AppPublisherURL={#MyAppURL}
AppSupportURL={#MyAppURL}
AppUpdatesURL={#MyAppURL}
DefaultDirName={pf}\{#MyAppName}
DefaultGroupName={#MyAppName}
InfoBeforeFile={#UnidadBase}{#DirBase}\InstrucionesPreInstall.txt
InfoAfterFile={#UnidadBase}{#DirBase}\InstrucionesPostInstall.txt
OutputDir={#UnidadBase}{#DirSalida}\SETUP
OutputBaseFilename=setupXulia
Compression=lzma
SolidCompression=yes

[Languages]
Name: "spanish"; MessagesFile: "compiler:Languages\Spanish.isl"

[Tasks]
Name: "desktopicon"; Description: "{cm:CreateDesktopIcon}"; GroupDescription: "{cm:AdditionalIcons}"; Flags: unchecked
Name: "quicklaunchicon"; Description: "{cm:CreateQuickLaunchIcon}"; GroupDescription: "{cm:AdditionalIcons}"; Flags: unchecked; OnlyBelowVersion: 0,6.1

[Files]
Source: "{#UnidadBase}{#DirBase}\*"; DestDir: "{app}"; Flags: ignoreversion
Source: "{#UnidadBaseFicherosIntall}{#DirBaseFicherosIntall}\*"; DestDir: "{app}\setup"; Flags: ignoreversion recursesubdirs createallsubdirs

Source: C:\WINDOWS\SysWOW64\MSWINSCK.OCX; DestDir: {syswow64}; Flags: regserver sharedfile; 
; NOTE: Don't use "Flags: ignoreversion" on any shared system files
Source: "{#UnidadBase}{#DirBase}\config\*"; DestDir: "{app}\config"; Flags: ignoreversion recursesubdirs createallsubdirs
Source: "{#UnidadBase}{#DirBase}\iconos\*"; DestDir: "{app}\iconos"; Flags: ignoreversion recursesubdirs createallsubdirs
Source: "{#UnidadBase}{#DirBase}\listas\*"; DestDir: "{app}\listas"; Flags: ignoreversion recursesubdirs createallsubdirs
Source: "{#UnidadBase}{#DirBase}\aiml\Xulia\*"; DestDir: "{app}\aiml\Xulia"; Flags: ignoreversion recursesubdirs createallsubdirs

[Icons]
Name: "{group}\{#MyAppName}"; Filename: "{app}\{#MyAppExeName}"
Name: "{commondesktop}\{#MyAppName}"; Filename: "{app}\{#MyAppExeName}"; Tasks: desktopicon
Name: "{userappdata}\Microsoft\Internet Explorer\Quick Launch\{#MyAppName}"; Filename: "{app}\{#MyAppExeName}"; Tasks: quicklaunchicon


[Run]
Filename: "{app}\{#MyAppExeName}"; Description: "{cm:LaunchProgram,{#StringChange(MyAppName, '&', '&&')}}"; Flags: nowait postinstall skipifsilent
Filename: "msiexec.exe"; Parameters: "/i ""{app}\setup\MSSpeech_SR_en-US_TELE.msi"""
Filename: "msiexec.exe"; Parameters: "/i ""{app}\setup\MSSpeech_SR_es-ES_TELE.msi"""
Filename: "msiexec.exe"; Parameters: "/i ""{app}\setup\MSSpeech_SR_pt-BR_TELE.msi"""
Filename: "msiexec.exe"; Parameters: "/i ""{app}\setup\MSSpeech_TTS_es-ES_Helena.msi"""
Filename: "msiexec.exe"; Parameters: "/i ""{app}\setup\MSSpeech_TTS_pt-BR_Heloisa.msi"""
Filename: "msiexec.exe"; Parameters: "/i ""{app}\setup\SpeechPlatformRuntime_x32.msi"""

[Registry]

[Dirs]
Name: C:\TMP; Permissions: everyone-full;
Name: C:\TMP\XULIA; Permissions: everyone-full;
Name: C:\TMP\XULIA\COPIAS; Permissions: everyone-full;
Name: {app}; Permissions: everyone-full;

