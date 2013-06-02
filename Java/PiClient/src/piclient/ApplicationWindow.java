/*
 * To change this template, choose Tools | Templates
 * and open the template in the editor.
 */
package piclient;

import javafx.application.Application;
import javafx.event.ActionEvent;
import javafx.event.EventHandler;
import javafx.scene.Scene;
import javafx.scene.control.Button;
import javafx.scene.control.Label;
import javafx.scene.layout.StackPane;
import javafx.stage.Stage;

/**
 * Source: http://docs.oracle.com/javafx/
 * @author Blake
 */
public class ApplicationWindow extends Application {
    
    @Override
    public void start(Stage primaryStage) {
        final Label mediaPlaying = new Label("No media is currently playing");
        final Label time = new Label("01:40 AM");
        final Label date = new Label("01/06/2013");
        Button btn = new Button();
        btn.setText("Media");
        btn.setOnAction(new EventHandler<ActionEvent>() {
            @Override
            public void handle(ActionEvent event) {
                mediaPlaying.setText("HTPC: 2 Fast 2 Furious (5:23/1:24:04)");
                System.out.println("Lights Toggled");
            }
        });
        
        StackPane root = new StackPane();
        root.getChildren().add(time);
        root.getChildren().add(date);
        //root.getChildren().add(mediaPlaying);
        //root.getChildren().add(btn);
        
        Scene scene = new Scene(root, 300, 250);
        
        primaryStage.setTitle("Home Control Suite");
        primaryStage.setScene(scene);
        primaryStage.show();
    }

    /**
     * The main() method is ignored in correctly deployed JavaFX application.
     * main() serves only as fallback in case the application can not be
     * launched through deployment artifacts, e.g., in IDEs with limited FX
     * support. NetBeans ignores main().
     *
     * @param args the command line arguments
     */
    public static void main(String[] args) {
        launch(args);
    }
}
