package enterpriseAndMobile.model;

import org.hibernate.annotations.GenericGenerator;

import javax.persistence.*;

@Entity
@Table(name = "answer")
public class Answer {
    @Id
    @GeneratedValue(generator = "sequence-generator-answer")
    @GenericGenerator(
            name = "sequence-generator-answer",
            strategy = "org.hibernate.id.enhanced.SequenceStyleGenerator",
            parameters = {
                    @org.hibernate.annotations.Parameter(name = "sequence_name", value = "answer_sequence"),
                    @org.hibernate.annotations.Parameter(name = "initial_value", value = "1"),
                    @org.hibernate.annotations.Parameter(name = "increment_size", value = "1")
            })
    private int id;

    private String answerString;

    @OneToOne
    private Question question;

    public Answer() {
    }

    public Answer(String answerString) {
        this.answerString = answerString;
    }

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
