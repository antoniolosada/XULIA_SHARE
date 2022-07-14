<%@page contentType="text/html" pageEncoding="UTF-8"%>
<!DOCTYPE html>
<!-- saved from url=(0087)http://www.desarrollolibre.net/public/download/speechRecognition/speechRecognition.html -->
<html>
    <head>
        <meta http-equiv="Content-Type" content="text/html; charset=UTF-8">
        <link rel="stylesheet"  media="screen" type="text/css"  href="css/estilos.css">
        <title>RECVOZ.GOOGLE.DESACTIVO</title>       
        <script type="text/javascript">
            
            // FUNCIONES AJAX  
            /*
            function tratamientoVOZ2(frase){
                var req = new XMLHttpRequest();
                req.open("POST", "GrabaDiscoLocal", false);
                req.setRequestHeader("Content-Type", "application/x-www-form-urlencoded");
                req.send("frase=" + frase);
                var tmp;
                if ((req.readyState === 4) && (req.status === 200)) {
                    tmp =  req.responseText;
                }
                document.getElementById("logs").value += tmp;    
            }    
            */
                var IDIOMA = "";
                var CODIGO_IDIOMA = "";
                var ARRANQUE_AUTOMATICO = 'N';
                // Leer los datos GET de nuestra pagina y devolver un array asociativo (Nombre de la variable GET => Valor de la variable).
                function getUrlVars()
                {
                    var vars = [], hash;
                    var hashes = window.location.href.slice(window.location.href.indexOf('?') + 1).split('&');
                    for(var i = 0; i < hashes.length; i++)
                    {
                        hash = hashes[i].split('=');
                        vars.push(hash[0]);
                        vars[hash[0]] = hash[1];
                    }
                    return vars;
                }                
                function tratamientoVOZ(frase)
                {
                    var req = new XMLHttpRequest();                
                    req.open("POST", "GrabaDiscoLocal", true);
                    req.setRequestHeader("Content-Type", "application/x-www-form-urlencoded");

                    var tmp;
                    req.onreadystatechange = function(){                    
                    if ((req.readyState === 4) && (req.status === 200)) {
                        tmp =  req.responseText;
                    }
                    if (tmp !== undefined)
                    {
                        var REANUDAR = 'REANUDAR.'+IDIOMA;
                        if ((tmp.substring(0,5) === "PARAR") && (recognizing === true))
                        {
                            document.getElementById("logs").value += tmp;
                            recognizing = false;
                            recognition.stop();
                            document.getElementById("procesar").innerHTML = "Escuchar";
                            document.title = 'RECVOZ.GOOGLE.'+IDIOMA+'.DESACTIVO';
                            
                            var myVar = setInterval(function(){ tratamientoVOZ("") }, 1000);
                        }
                        else if ((tmp.substring(0,REANUDAR.length) === REANUDAR))
                        {
                            if (recognizing === false)
                            {
                                document.getElementById("logs").value += tmp;
                                clearInterval(myVar);
                                recognition.start();
                                recognizing = true;
                                document.getElementById("procesar").innerHTML = "Detener";
                                document.title = 'RECVOZ.GOOGLE.'+IDIOMA+'.ACTIVO';
                            }
                        }
                        else if ((tmp.substring(0,8) === 'REANUDAR'))
                        {
                            //He recuperado información de reanudación de otro idioma
                            //Paramos el reconocimiento del idioma actual
                            if (recognizing === true)
                            {
                                recognizing = false;
                                recognition.stop();
                                document.getElementById("procesar").innerHTML = "Escuchar";
                                document.title = 'RECVOZ.GOOGLE.'+IDIOMA+'.DESACTIVO';
                            }
                        }
                    }
                };

                req.send("frase=" + escape(frase));
            }              
                                   
            // CONFIGURACIÓN DEL SOPORTE DE RECONOCIMIENTO DE VOZ
            var recolector = "";
            var recognition;
            var recognizing = false;
            if (!('webkitSpeechRecognition' in window)) {
                alert("¡API no soportada!");
            }else{
                recognition = new webkitSpeechRecognition();
                recognition.lang = CODIGO_IDIOMA;
                //recognition.lang = "pt-BR";
                recognition.continuous = true;
                recognition.interimResults = true;

                recognition.onstart = function(){
                recognizing = true;
                console.log("Comenzando a escuchar");
                document.getElementById("logs").value += "Comenzando a escuchar... \n";
                document.title = 'RECVOZ.GOOGLE.'+IDIOMA+'.ACTIVO';
                };
            }
            
            recognition.onresult = function(event) {
                for (var i = event.resultIndex; i < event.results.length; i++) {
                    if(event.results[i].isFinal){
                        recolector = event.results[i][0].transcript;
                        tratamientoVOZ(recolector);
                        document.getElementById("texto").value += recolector;
                    }
                }
            };
            recognition.onerror = function(event) {                
                /*for (var i = event.resultIndex; i < event.results.length; i++) {
                    if(event.results[i].isFinal){                        
                        document.getElementById("logs").value += event.results[i][0].transcript;
                    }
                } */                
            };
            recognition.onend = function() {
                if (ARRANQUE_AUTOMATICO === 'S')
                {
                    if (recognizing === true)
                        recognition.start();
                }
                else
                {
                    recognizing = false;
                    document.getElementById("procesar").innerHTML = "Activar";
                    console.log("Terminó de escuchar, llegó a su fin");
                    document.getElementById("logs").value += "Terminó de escuchar, llegó a su fin.\n";
                    document.title = 'RECVOZ.GOOGLE.'+IDIOMA+'.DESACTIVO';
                }
            };
            function procesar() {
                if (recognizing === false) 
                {
                    recognition.start();
                    recognizing = true;
                    document.getElementById("procesar").innerHTML = "Detener";
                    document.title = 'RECVOZ.GOOGLE.'+IDIOMA+'.ACTIVO';
                } 
                else 
                {
                    recognition.stop();
                    recognizing = false;
                    document.getElementById("procesar").innerHTML = "Escuchar";
                    document.title = 'RECVOZ.GOOGLE.'+IDIOMA+'.DESACTIVO';
                }
            }
            function CargaInicial()
            {
                // Cogemos los valores pasados por get
                var par = getUrlVars();
                IDIOMA = getUrlVars()["idioma"];
                if(IDIOMA !== undefined)
                {
                    CODIGO_IDIOMA = getUrlVars()["codigo"];
                    ARRANQUE_AUTOMATICO = getUrlVars()["auto"];
                }
                else
                {
                    IDIOMA = 'ESPANOL';
                    CODIGO_IDIOMA = 'es-ES';
                    ARRANQUE_AUTOMATICO = 'N'
                }
                document.title = 'RECVOZ.GOOGLE.'+IDIOMA+'.DESACTIVO';
                var myVar = setInterval(function(){ tratamientoVOZ("") }, 1000);
                recognizing = false;
            }
         </script>           
        
    </head>
    <body onload="CargaInicial()">
	<header>
            <h1>La API de Reconocimiento de Voz en JavaScript: speechRecognition()</h1>
	</header>
	<section>
            <div class="example">
                <textarea id="texto"></textarea>
                <textarea id="logs"></textarea>
                <button onclick="procesar()" id="procesar">Activar</button>
            </div>
	</section>

     
    </body>
</html>