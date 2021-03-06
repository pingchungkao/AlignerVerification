; Script generated by the HM NIS Edit Script Wizard.

; HM NIS Edit Wizard helper defines
!define PRODUCT_NAME "AlignerVerification"
!define PRODUCT_VERSION "1.0"
!define PRODUCT_PUBLISHER "My company, Inc."
!define PRODUCT_DIR_REGKEY "Software\Microsoft\Windows\CurrentVersion\App Paths\AlignerVerification.exe"
!define PRODUCT_UNINST_KEY "Software\Microsoft\Windows\CurrentVersion\Uninstall\${PRODUCT_NAME}"
!define PRODUCT_UNINST_ROOT_KEY "HKLM"

; MUI 1.67 compatible ------
!include "MUI.nsh"

; MUI Settings
!define MUI_ABORTWARNING
!define MUI_ICON "${NSISDIR}\Contrib\Graphics\Icons\modern-install.ico"
!define MUI_UNICON "${NSISDIR}\Contrib\Graphics\Icons\modern-uninstall.ico"

; Language Selection Dialog Settings
!define MUI_LANGDLL_REGISTRY_ROOT "${PRODUCT_UNINST_ROOT_KEY}"
!define MUI_LANGDLL_REGISTRY_KEY "${PRODUCT_UNINST_KEY}"
!define MUI_LANGDLL_REGISTRY_VALUENAME "NSIS:Language"

; Welcome page
!insertmacro MUI_PAGE_WELCOME
; Directory page
!insertmacro MUI_PAGE_DIRECTORY
; Instfiles page
!insertmacro MUI_PAGE_INSTFILES
; Finish page
!define MUI_FINISHPAGE_RUN "$INSTDIR\AlignerVerification.exe"
!insertmacro MUI_PAGE_FINISH

; Uninstaller pages
!insertmacro MUI_UNPAGE_INSTFILES

; Language files
!insertmacro MUI_LANGUAGE "English"
!insertmacro MUI_LANGUAGE "TradChinese"

; MUI end ------

Name "${PRODUCT_NAME} ${PRODUCT_VERSION}"
OutFile "Setup_V1.05.exe"
InstallDir "$PROGRAMFILES\AlignerVerification"
InstallDirRegKey HKLM "${PRODUCT_DIR_REGKEY}" ""
ShowInstDetails show
ShowUnInstDetails show

Function .onInit
  !insertmacro MUI_LANGDLL_DISPLAY
FunctionEnd

Section "MainSection" SEC01
  SetOutPath "$INSTDIR"
  SetOverwrite try
  File "AlignerVerification.exe"
  CreateDirectory "$SMPROGRAMS\AlignerVerification"
  CreateShortCut "$SMPROGRAMS\AlignerVerification\AlignerVerification.lnk" "$INSTDIR\AlignerVerification.exe"
  CreateShortCut "$DESKTOP\AlignerVerification.lnk" "$INSTDIR\AlignerVerification.exe"
  File "AlignerVerification.exe.config"
  File "AlignerVerification.pdb"
  File "CameraApi.dll"
  File "CameraDemo.exe"
  SetOutPath "$INSTDIR\de"
  File "de\ZedGraph.resources.dll"
  SetOutPath "$INSTDIR"
  File "Emgu.CV.UI.dll"
  File "Emgu.CV.UI.xml"
  File "Emgu.CV.World.dll"
  File "Emgu.CV.World.xml"
  SetOutPath "$INSTDIR\es"
  File "es\ZedGraph.resources.dll"
  SetOutPath "$INSTDIR\fr"
  File "fr\ZedGraph.resources.dll"
  SetOutPath "$INSTDIR\hu"
  File "hu\ZedGraph.resources.dll"
  SetOutPath "$INSTDIR\INI"
  File "INI\Config.ini"
  AccessControl::GrantOnFile \
  "$INSTDIR\INI" "(BU)" "GenericRead + GenericWrite"
  SetOutPath "$INSTDIR\it"
  File "it\ZedGraph.resources.dll"
  SetOutPath "$INSTDIR\ja"
  File "ja\ZedGraph.resources.dll"
  SetOutPath "$INSTDIR"
  File "log4net.dll"
  File "log4net.xml"
  File "MWArray.dll"
  File "MWArray.xml"
  SetOutPath "$INSTDIR\output\img0"
  File "output\img0\acc0.bmp"
  AccessControl::GrantOnFile \
  "$INSTDIR\output" "(BU)" "GenericRead + GenericWrite"
  SetOutPath "$INSTDIR\pt"
  File "pt\ZedGraph.resources.dll"
  SetOutPath "$INSTDIR\ru"
  File "ru\ZedGraph.resources.dll"
  SetOutPath "$INSTDIR\sk"
  File "sk\ZedGraph.resources.dll"
  SetOutPath "$INSTDIR\sv"
  File "sv\ZedGraph.resources.dll"
  SetOutPath "$INSTDIR\tr"
  File "tr\ZedGraph.resources.dll"
  SetOutPath "$INSTDIR\x64"
  File "x64\concrt140.dll"
  File "x64\cvextern.dll"
  File "x64\msvcp140.dll"
  File "x64\opencv_ffmpeg410_64.dll"
  File "x64\vcruntime140.dll"
  SetOutPath "$INSTDIR\x86"
  File "x86\concrt140.dll"
  File "x86\cvextern.dll"
  File "x86\msvcp140.dll"
  File "x86\opencv_ffmpeg410.dll"
  File "x86\vcruntime140.dll"
  SetOutPath "$INSTDIR"
  File "ZedGraph.dll"
  File "ZedGraph.xml"
  SetOutPath "$INSTDIR\zh-cn"
  File "zh-cn\ZedGraph.resources.dll"
  SetOutPath "$INSTDIR\zh-tw"
  File "zh-tw\ZedGraph.resources.dll"
SectionEnd

Section -AdditionalIcons
  SetOutPath $INSTDIR
  CreateShortCut "$SMPROGRAMS\AlignerVerification\Uninstall.lnk" "$INSTDIR\uninst.exe"
SectionEnd

Section -Post
  WriteUninstaller "$INSTDIR\uninst.exe"
  WriteRegStr HKLM "${PRODUCT_DIR_REGKEY}" "" "$INSTDIR\AlignerVerification.exe"
  WriteRegStr ${PRODUCT_UNINST_ROOT_KEY} "${PRODUCT_UNINST_KEY}" "DisplayName" "$(^Name)"
  WriteRegStr ${PRODUCT_UNINST_ROOT_KEY} "${PRODUCT_UNINST_KEY}" "UninstallString" "$INSTDIR\uninst.exe"
  WriteRegStr ${PRODUCT_UNINST_ROOT_KEY} "${PRODUCT_UNINST_KEY}" "DisplayIcon" "$INSTDIR\AlignerVerification.exe"
  WriteRegStr ${PRODUCT_UNINST_ROOT_KEY} "${PRODUCT_UNINST_KEY}" "DisplayVersion" "${PRODUCT_VERSION}"
  WriteRegStr ${PRODUCT_UNINST_ROOT_KEY} "${PRODUCT_UNINST_KEY}" "Publisher" "${PRODUCT_PUBLISHER}"
SectionEnd


Function un.onUninstSuccess
  HideWindow
  MessageBox MB_ICONINFORMATION|MB_OK "$(^Name) 已成功地從你的電腦移除。"
FunctionEnd

Function un.onInit
!insertmacro MUI_UNGETLANGUAGE
  MessageBox MB_ICONQUESTION|MB_YESNO|MB_DEFBUTTON2 "你確定要完全移除 $(^Name) ，其及所有的元件？" IDYES +2
  Abort
FunctionEnd

Section Uninstall
  Delete "$INSTDIR\uninst.exe"
  Delete "$INSTDIR\zh-tw\ZedGraph.resources.dll"
  Delete "$INSTDIR\zh-cn\ZedGraph.resources.dll"
  Delete "$INSTDIR\ZedGraph.xml"
  Delete "$INSTDIR\ZedGraph.dll"
  Delete "$INSTDIR\x86\vcruntime140.dll"
  Delete "$INSTDIR\x86\opencv_ffmpeg410.dll"
  Delete "$INSTDIR\x86\msvcp140.dll"
  Delete "$INSTDIR\x86\cvextern.dll"
  Delete "$INSTDIR\x86\concrt140.dll"
  Delete "$INSTDIR\x64\vcruntime140.dll"
  Delete "$INSTDIR\x64\opencv_ffmpeg410_64.dll"
  Delete "$INSTDIR\x64\msvcp140.dll"
  Delete "$INSTDIR\x64\cvextern.dll"
  Delete "$INSTDIR\x64\concrt140.dll"
  Delete "$INSTDIR\tr\ZedGraph.resources.dll"
  Delete "$INSTDIR\sv\ZedGraph.resources.dll"
  Delete "$INSTDIR\sk\ZedGraph.resources.dll"
  Delete "$INSTDIR\ru\ZedGraph.resources.dll"
  Delete "$INSTDIR\pt\ZedGraph.resources.dll"
  Delete "$INSTDIR\output\img0\acc0.bmp"
  Delete "$INSTDIR\MWArray.xml"
  Delete "$INSTDIR\MWArray.dll"
  Delete "$INSTDIR\log4net.xml"
  Delete "$INSTDIR\log4net.dll"
  Delete "$INSTDIR\ja\ZedGraph.resources.dll"
  Delete "$INSTDIR\it\ZedGraph.resources.dll"
  Delete "$INSTDIR\INI\Config.ini"
  Delete "$INSTDIR\hu\ZedGraph.resources.dll"
  Delete "$INSTDIR\fr\ZedGraph.resources.dll"
  Delete "$INSTDIR\es\ZedGraph.resources.dll"
  Delete "$INSTDIR\Emgu.CV.World.xml"
  Delete "$INSTDIR\Emgu.CV.World.dll"
  Delete "$INSTDIR\Emgu.CV.UI.xml"
  Delete "$INSTDIR\Emgu.CV.UI.dll"
  Delete "$INSTDIR\de\ZedGraph.resources.dll"
  Delete "$INSTDIR\CameraDemo.exe"
  Delete "$INSTDIR\CameraApi.dll"
  Delete "$INSTDIR\AlignerVerification.pdb"
  Delete "$INSTDIR\AlignerVerification.exe.config"
  Delete "$INSTDIR\AlignerVerification.exe"

  Delete "$SMPROGRAMS\AlignerVerification\Uninstall.lnk"
  Delete "$DESKTOP\AlignerVerification.lnk"
  Delete "$SMPROGRAMS\AlignerVerification\AlignerVerification.lnk"

  RMDir "$SMPROGRAMS\AlignerVerification"
  RMDir "$INSTDIR\zh-tw"
  RMDir "$INSTDIR\zh-cn"
  RMDir "$INSTDIR\x86"
  RMDir "$INSTDIR\x64"
  RMDir "$INSTDIR\tr"
  RMDir "$INSTDIR\sv"
  RMDir "$INSTDIR\sk"
  RMDir "$INSTDIR\ru"
  RMDir "$INSTDIR\pt"
  RMDir "$INSTDIR\output\img0"
  RMDir "$INSTDIR\ja"
  RMDir "$INSTDIR\it"
  RMDir "$INSTDIR\INI"
  RMDir "$INSTDIR\hu"
  RMDir "$INSTDIR\fr"
  RMDir "$INSTDIR\es"
  RMDir "$INSTDIR\de"
  RMDir "$INSTDIR"

  DeleteRegKey ${PRODUCT_UNINST_ROOT_KEY} "${PRODUCT_UNINST_KEY}"
  DeleteRegKey HKLM "${PRODUCT_DIR_REGKEY}"
  SetAutoClose true
SectionEnd