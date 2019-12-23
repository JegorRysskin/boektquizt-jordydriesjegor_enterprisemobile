package enterpriseAndMobile.jmsQuee;

import javax.jms.Connection;
import javax.jms.ConnectionFactory;
import javax.jms.Destination;
import javax.jms.JMSException;
import javax.jms.Message;
import javax.jms.MessageConsumer;
import javax.jms.Session;
import javax.jms.TextMessage;

import org.apache.activemq.ActiveMQConnection;
import org.apache.activemq.ActiveMQConnectionFactory;
import org.springframework.stereotype.Component;

import java.util.List;

import static enterpriseAndMobile.model.Constants.QUEUE;

@Component
public class MessageReceiver {

    private static String url = ActiveMQConnection.DEFAULT_BROKER_URL;

    private static String subject = QUEUE;

    private List<String> messages;

    public List<String> receiveAllMessages() throws JMSException {
        ConnectionFactory connectionFactory = new ActiveMQConnectionFactory(url);
        Connection connection = connectionFactory.createConnection();
        connection.start();

        Session session = connection.createSession(false,
                Session.AUTO_ACKNOWLEDGE);

        Destination destination = session.createQueue(subject);

        MessageConsumer consumer = session.createConsumer(destination);

        Message message = consumer.receive();
        while (message != null) {
            if (message instanceof TextMessage) {
                TextMessage textMessage = (TextMessage) message;
                messages.add(textMessage.getText());
            }
            message = consumer.receive();
        }
        connection.close();
        return messages;
    }
}