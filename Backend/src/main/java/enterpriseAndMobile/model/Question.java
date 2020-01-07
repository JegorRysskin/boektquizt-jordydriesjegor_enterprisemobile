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

    @OneToMany(cascade = CascadeType.ALL)
    private List<CorrectAnswer> correctAnswerToQuestion;

    public int getId() {
        return id;
    }

    public String getQuestionString() {
        return questionString;
    }

    public void setQuestionString(String question) {
        this.questionString = question;
    }

    public List<CorrectAnswer> getCorrectAnswerToQuestion() {
        return correctAnswerToQuestion;
    }

    public void setCorrectAnswerToQuestion(List<CorrectAnswer> correctAnswerToQuestion) {
        this.correctAnswerToQuestion = correctAnswerToQuestion;
    }

}
