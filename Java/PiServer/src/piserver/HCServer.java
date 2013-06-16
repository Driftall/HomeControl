/*
 * To change this template, choose Tools | Templates
 * and open the template in the editor.
 */
package piserver;

import Sockets.Server.*;
import java.util.ArrayList;
import java.util.List;

/**
 *
 * @author Blake
 */
public class HCServer extends Server implements ServerListeners {

    public HCServer() {
        this.addServerListener(this);
        //PiServer.
    }
    private List<HCServerListeners> listeners = new ArrayList<HCServerListeners>();

    public void addHCListener(HCServerListeners listener) {
        listeners.add(listener);
    }

    @Override
    public void onDataReceivedFromClient(String client, String data) {
        if (data.startsWith(ProtocolConstants.Protocol.getValue)) {
            //Value requested from client
            String setting = data.substring(3);
            ValueRequestedFromClient(client, setting);
        } else if (data.startsWith(ProtocolConstants.Protocol.setValue)) {
            //Setting sent from client
            String setting = data.substring(3);
            String value = setting.substring(4);
            SettingSentFromClient(client, setting, value);
        } else {
            //Message receieved from client
            MessageReceivedFromClient(client,data);
        }
        System.out.println("HCServer>" + client + ">" + data);
        System.out.println(this.getClients());
        //throw new UnsupportedOperationException("Not supported yet."); //To change body of generated methods, choose Tools | Templates.
    }

    public void SettingSentFromClient(String client, String setting, String value) {
        for (HCServerListeners listener : listeners) {
            listener.onSettingSentFromClient(client, setting, value);
        }
    }

    public void ValueRequestedFromClient(String client, String setting) {
        for (HCServerListeners listener : listeners) {
            listener.onValueRequestedFromClient(client, setting);
        }
    }

    public void MessageReceivedFromClient(String client, String message) {
        for (HCServerListeners listener : listeners) {
            listener.onMessageReceivedFromClient(client, message);
        }
    }
}
