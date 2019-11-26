package enterpriseAndMobile.model;

import javax.persistence.*;
import java.util.List;

@Entity
@Table(name = "round")
public class Round {

    @Id
    @GeneratedValue
    private int id;

    @ManyToOne
    private Quiz quiz;

    @OneToMany(mappedBy = "round")
    private List<Question> questions;

    public int getId() {
        return id;
    }

    public Quiz getQuiz() {
        return quiz;
    }

    public void setQuiz(Quiz quiz) {
        this.quiz = quiz;
    }

    public List<Question> getQuestions() {
        return questions;
    }

    public void setQuestions(List<Question> questions) {
        this.questions = questions;
    }
}
