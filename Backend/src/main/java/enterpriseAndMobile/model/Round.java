package enterpriseAndMobile.model;

import javax.persistence.*;
import java.util.List;

@Entity
@Table(name = "round")
public class Round {

    @Id
    @GeneratedValue
    private int id;

    private boolean enabled;

    private String name;

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

    public boolean isEnabled() {
        return enabled;
    }

    public void setEnabled(boolean enabled) {
        this.enabled = enabled;
    }

    public String getName() { return name; }

    public void setName(String name) { this.name = name; }
}
