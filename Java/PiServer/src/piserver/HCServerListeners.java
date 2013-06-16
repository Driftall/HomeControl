/*
 * To change this template, choose Tools | Templates
 * and open the template in the editor.
 */
package piserver;

/**
 *
 * @author Blake
 */
public interface HCServerListeners 
{
    public void onSettingSentFromClient(String client, String setting, String value);
    public void onValueRequestedFromClient(String client, String setting);
    public void onMessageReceivedFromClient(String client, String message);
}