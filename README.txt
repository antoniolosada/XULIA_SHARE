Proyecto principal: \XULIA_SHARE\C_SHARP\XULIA\XULIA.sln

Para solucionar los errores en las referencias a AIMLBot, añadir referencia:
\XULIA_SHARE\C_SHARP\AIMLBot\AIMLBot.dll


Problemas con:
    using Windows.Media.SpeechRecognition;
    using Windows.ApplicationModel.Resources.Core;
Instalar los paquetes de Windows UWP (Windows Kits).
Buscar el lugar de instalación y
Añadir referencia Windows Universal UWP AGregar referencias

\Windows Kits\10\References\10.0.17763.0\Windows.Foundation.FoundationContract\3.0.0.0\Windows.Foundation.FoundationContract.winmd

\Windows Kits\10\References\10.0.17763.0\Windows.Foundation.UniversalApiContract\7.0.0.0\Windows.Foundation.UniversalApiContract.winmd

