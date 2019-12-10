package enterpriseAndMobile.scenarioTest;

import enterpriseAndMobile.QuizApplication;
import enterpriseAndMobile.model.Quiz;
import org.junit.jupiter.api.Assertions;
import org.junit.jupiter.api.Test;
import org.junit.jupiter.api.extension.ExtendWith;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.boot.test.context.SpringBootTest;
import org.springframework.boot.test.web.client.TestRestTemplate;
import org.springframework.http.ResponseEntity;
import org.springframework.test.context.junit.jupiter.SpringExtension;


@SpringBootTest(classes = QuizApplication.class,
        webEnvironment = SpringBootTest.WebEnvironment.RANDOM_PORT)
@ExtendWith(SpringExtension.class)
public class QuizScenarioTest {

    @Autowired
    private TestRestTemplate restTemplate;

    private int port = 8080;

    @Test
    public void fullTest() throws Exception {
        Quiz quiz = new Quiz("testData", true);
        ResponseEntity<String> responseEntity = this.restTemplate
                .postForEntity("http://localhost:" + port + "/quiz", quiz, String.class);
        Assertions.assertEquals(201, responseEntity.getStatusCodeValue());

        Assertions.assertEquals("testData",
                this.restTemplate
                        .getForObject("http://localhost:" + port + "/quiz/1" , Quiz.class)
                        .getName());
    }
}
