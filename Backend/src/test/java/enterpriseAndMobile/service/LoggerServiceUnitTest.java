package enterpriseAndMobile.service;

import enterpriseAndMobile.jmsQuee.MessageReceiver;
import org.junit.jupiter.api.Assertions;
import org.junit.jupiter.api.Test;
import org.junit.jupiter.api.extension.ExtendWith;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.boot.test.context.SpringBootTest;
import org.springframework.boot.test.mock.mockito.MockBean;
import org.springframework.test.context.junit.jupiter.SpringExtension;

import javax.jms.JMSException;
import java.util.ArrayList;
import java.util.List;

import static org.mockito.BDDMockito.given;

@ExtendWith(SpringExtension.class)
@SpringBootTest(classes = LoggerService.class)
public class LoggerServiceUnitTest {

    @MockBean
    private MessageReceiver messageReceiver;

    @Autowired
    private LoggerService loggerService;

    @Test
    public void getAllMessages_FromLoggerService() throws JMSException {
        List<String> messages = new ArrayList<>();

        messages.add("test");
        messages.add("test2");

        given(messageReceiver.receiveAllMessages()).willReturn(messages);
        Assertions.assertEquals(2, loggerService.getAllLogMessages().get().size());
    }
}
