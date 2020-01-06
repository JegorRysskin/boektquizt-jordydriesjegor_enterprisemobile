package enterpriseAndMobile.model;


import javax.persistence.*;
import java.util.List;

@Entity
@Table(name = "question")
public class Question {

    @Id
    @GeneratedValue
    private int id;

    private String questionString;

    @ElementCollection
    private List<String> correctAnswerToQuestion;

    public int getId() {
        return id;
    }

    public String getQuestion() {
        return questionString;
    }

    public void setQuestion(String question) {
        this.questionString = question;
    }

    public List<String> getCorrectAnswerToQuestion() {
        return correctAnswerToQuestion;
    }

    public void setCorrectAnswerToQuestion(List<String> correctAnswerToQuestion) {
        this.correctAnswerToQuestion = correctAnswerToQuestion;
    }

}
