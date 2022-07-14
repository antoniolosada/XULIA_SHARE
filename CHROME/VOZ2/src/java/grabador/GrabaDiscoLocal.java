package grabador;

import java.io.BufferedReader;
import java.io.FileNotFoundException;
import java.io.FileReader;
import java.io.IOException;
import java.io.FileWriter;
import java.io.IOException;
import java.io.PrintWriter;
import java.text.SimpleDateFormat;
import java.util.Date;
import javax.servlet.ServletException;
import javax.servlet.http.HttpServlet;
import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;

public class GrabaDiscoLocal extends HttpServlet {

    private static final long serialVersionUID = 98432167664343490L;
    private static final SimpleDateFormat sdf = new SimpleDateFormat("yyyy/MM/dd HH:mm:ss");
    private static final String archivo = "C:\\tmp\\recovoz.txt";
    private static final String ArchivoComandos = "C:\\tmp\\comandos.txt";

    @Override
    protected void doGet(HttpServletRequest request, HttpServletResponse response) throws ServletException, IOException {
        response.setContentType("text/html;charset=UTF-8");
        PrintWriter out = response.getWriter();
        try{            
            out.println("Non se admite método GET na chamada.");
        }finally{
            out.close();
        }    
    }
    
    @Override
    protected void doPost(HttpServletRequest request, HttpServletResponse response) throws ServletException {
        String tiempo = sdf.format(new Date());
        String res;
        String respuesta = "";
        String frase = request.getParameter("frase");
        PrintWriter out;
        
        if((frase == null) || (frase.length() == 0))
            res = "";
        else
            //res = tiempo + "·" + frase;
            res = frase + "·";
        
        try
        {
            if (!res.equals(""))
            {
                FileWriter fwriter = new FileWriter(archivo,true);
                PrintWriter pwriter = new PrintWriter(fwriter);
                pwriter.println(res);        
                fwriter.close(); 
            }
            
            //Leemos el archivo de comandos
            String cadena;
            FileReader f = new FileReader(ArchivoComandos);
            BufferedReader b = new BufferedReader(f);
            if((cadena = b.readLine())!=null) 
                respuesta = cadena;
            else
                respuesta = "";
            b.close();        

            try
            {           
                response.setContentType("text/html;charset=UTF-8");
                out = response.getWriter();
                out.println(respuesta);
                out.close();
            }
            catch(IOException ioe)
            {
            }
        } 
        catch(IOException ioe)
        {
        }
    }
}
