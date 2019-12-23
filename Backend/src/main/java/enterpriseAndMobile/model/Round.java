package enterpriseAndMobile.model;

import javax.persistence.*;
import java.util.List;

@Entity
@Table(name = "round")
public class Round {

    @Id
    @GeneratedValue
    private int id;

    @OneToMany
    private List<Question> questions;

    public int getId() {
        return id;
    }

    public List<Question> getQuestions() {
        return questions;
    }

    public void setQuestions(List<Question> questions) {
        this.questions = questions;
    }
}
