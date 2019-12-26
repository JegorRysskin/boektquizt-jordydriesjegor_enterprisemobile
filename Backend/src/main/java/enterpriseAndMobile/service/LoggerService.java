package enterpriseAndMobile.service;

import enterpriseAndMobile.jmsQuee.MessageReceiver;
import org.springframework.stereotype.Service;

import javax.jms.JMSException;
import java.util.List;
import java.util.Optional;

@Service
public class LoggerService {
    private final MessageReceiver messageReceiver;

    public LoggerService(MessageReceiver messageReceiver) {
        this.messageReceiver = messageReceiver;
    }

    public Optional<List<String>> getAllLogMessages() {
        try {
            return Optional.of(messageReceiver.receiveAllMessages());
        } catch (JMSException e) {
            System.out.println(e.getErrorCode());
            return Optional.empty();
        }
    }
}
