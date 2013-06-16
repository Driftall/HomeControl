/*
 * To change this template, choose Tools | Templates
 * and open the template in the editor.
 */
package piserver;
import Sockets.Server.*;
import java.io.IOException;
/**
 *
 * @author Blake
 */
public class PiServer {

    /**
     * @param args the command line arguments
     */
    public static void main(String[] args) throws IOException 
    {
        System.out.println("JarPi - Home Control Suite - Pi Server");
        HCServer server = new HCServer();
        server.Listen(9999);
        //server.
        // TODO code application logic here
    }
}
