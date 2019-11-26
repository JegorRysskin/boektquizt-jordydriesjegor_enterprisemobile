package enterpriseAndMobile.model;


import javax.persistence.*;
import java.util.List;

@Entity
@Table(name = "question")
public class Question {

    @Id
    @GeneratedValue
    private int id;

    @ManyToOne
    private Round round;

    @OneToMany(mappedBy = "question")
    private List<Answer> answers;

    public int getId() {
        return id;
    }

    public Round getRound() {
        return round;
    }

    public void setRound(Round round) {
        this.round = round;
    }

    public List<Answer> getAnswers() {
        return answers;
    }

    public void setAnswers(List<Answer> answers) {
        this.answers = answers;
    }
}
