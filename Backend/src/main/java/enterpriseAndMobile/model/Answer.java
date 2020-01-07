package enterpriseAndMobile.model;

import javax.persistence.*;

@Entity
@Table(name = "answer")
public class Answer {
    @Id
    @GeneratedValue
    private int id;

    private String answerString;

    @OneToOne
    private Question question;

    public int getId() {
        return id;
    }

    public String getAnswerString() {
        return answerString;
    }

    public void setAnswerString(String answer) {
        this.answerString = answer;
    }

    public void setId(int id) {
        this.id = id;
    }

    public Question getQuestion() {
        return question;
    }

    public void setQuestion(Question question) {
        this.question = question;
    }
}
