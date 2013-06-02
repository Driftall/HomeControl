/*
 * To change this template, choose Tools | Templates
 * and open the template in the editor.
 */
package piclient;

import java.net.URL;
import java.util.ResourceBundle;
import javafx.event.ActionEvent;
import javafx.fxml.FXML;
import javafx.fxml.Initializable;
import javafx.scene.control.Label;

/**
 *
 * @author Blake
 */
public class DesignController implements Initializable {
    
    @FXML
    private Label label;
    
    @FXML
    private void handleButtonPresense(ActionEvent event) {
        System.out.println("Goodbye!");
    }
    
    @Override
    public void initialize(URL url, ResourceBundle rb) {
        // TODO
    }    
}
