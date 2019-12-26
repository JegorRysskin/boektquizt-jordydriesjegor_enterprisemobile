package enterpriseAndMobile.jmsQuee;


import javax.jms.Connection;
import javax.jms.ConnectionFactory;
import javax.jms.Destination;
import javax.jms.JMSException;
import javax.jms.MessageProducer;
import javax.jms.Session;
import javax.jms.TextMessage;

import org.apache.activemq.ActiveMQConnection;
import org.apache.activemq.ActiveMQConnectionFactory;
import org.springframework.stereotype.Component;

import static enterpriseAndMobile.model.Constants.QUEUE;

@Component
public class MessageSender {

    private static String url = ActiveMQConnection.DEFAULT_BROKER_URL;

    private static String subject = QUEUE;

    public void sendMessage(String givenMessage) throws JMSException {
        ConnectionFactory connectionFactory = new ActiveMQConnectionFactory(url);
        Connection connection = connectionFactory.createConnection();
        connection.start();

        Session session = connection.createSession(false,
                Session.AUTO_ACKNOWLEDGE);

        Destination destination = session.createQueue(subject);

        MessageProducer producer = session.createProducer(destination);

        TextMessage message = session.createTextMessage(givenMessage);

        producer.send(message);

        connection.close();
    }
}