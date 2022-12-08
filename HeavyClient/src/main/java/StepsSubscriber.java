package main.java;

import main.java.soap.ws.client.*;
import org.apache.activemq.*;
import org.json.*;


import javax.xml.bind.JAXBElement;
import javax.xml.namespace.QName;




public class StepsSubscriber implements
     javax.jms.MessageListener      // to handle message subscriptions
{
    private static final String DEFAULT_BROKER_NAME = "tcp://localhost:61616";
    private static final long   MESSAGE_LIFESPAN = 1800000; //30 minutes

    private javax.jms.Connection connection = null;
    private javax.jms.Session subSession = null;
    private Home home;
    private Double[] currentLocation;

    public StepsSubscriber(String username, String queueName, Double[] currentLocation, Home home)
    {
        javax.jms.MessageConsumer subscriber = null;
        javax.jms.Topic topic = null;

        //Set external dependency
        this.currentLocation = currentLocation;
        this.home = home;
        
        //Create a connection:
        try{
            javax.jms.ConnectionFactory factory;
            factory = new ActiveMQConnectionFactory(DEFAULT_BROKER_NAME);
            connection = factory.createConnection ();
            connection.setClientID(username);
            subSession = connection.createSession(false,javax.jms.Session.AUTO_ACKNOWLEDGE);
        }
        catch (javax.jms.JMSException jmse){
            System.err.println ("Error: Cannot connect to Broker - " + DEFAULT_BROKER_NAME);
            jmse.printStackTrace();
            System.exit(1);
        }

        //Create Publisher and Durable Subscriber:
        try{

            topic = subSession.createTopic(queueName);
            subscriber = subSession.createDurableSubscriber(topic, username);
            subscriber.setMessageListener(this);
            connection.start();
        }
        catch (javax.jms.JMSException jmse){
            System.out.println("Error: connection not started.");
            jmse.printStackTrace();
            exit();
        }
    }

    /** Message Handler**/
    public void onMessage( javax.jms.Message aMessage)
    {
        try
        {
            // Cast the message as a text message.
            javax.jms.TextMessage textMessage = (javax.jms.TextMessage) aMessage;
            System.out.println("I got a juicy message " + textMessage.getText());

            JSONObject json = new JSONObject(textMessage.getText());

            Step step = new Step();

            JAXBElement<String> instruction = new JAXBElement<String>(new QName("instruction"), String.class, json.get("instruction").toString());
            JAXBElement<String> maneuver = new JAXBElement<String>(new QName("maneuver"), String.class, json.get("maneuver").toString());
            
            String longitude = maneuver.getValue().split(",")[1].split("]")[0];
            String latitude = maneuver.getValue().split(",")[0].split("\\[")[1];
            
           
            currentLocation[0] = Double.parseDouble(longitude);
            currentLocation[1] = Double.parseDouble(latitude);
            
            home.getStepLbl().setText(instruction.getValue());
            // This handler reads a single String from the
            // message and prints it to the standard output.
            try
            {
                String string = textMessage.getText();
                System.out.println( string );
            }
            catch (javax.jms.JMSException jmse)
            {
                jmse.printStackTrace();
            }
        }catch(Exception ex){
            System.out.println("Error " + ex.getMessage());
        }
    }

    /** Cleanup resources cleanly and exit. */
    private void exit()
    {
        try
        {
            connection.close();
        }
        catch (javax.jms.JMSException jmse)
        {
            jmse.printStackTrace();
        }

        System.exit(0);
    }

}
