package enterpriseAndMobile.model;

import javax.persistence.*;
import java.util.List;

@Entity
@Table(name = "quiz")
public class Quiz {
    public Quiz() {
    }

    public Quiz(String name) {
        this.name = name;
    }

    public Quiz(String name, boolean enabled) {
        this.name = name;
        this.enabled = enabled;
    }

    @Id
    @GeneratedValue
    private int id;

    private String name;

    @OneToMany(mappedBy = "quiz")
    private List<Round> rounds;

    private boolean enabled;

    public int getId() {
        return id;
    }

    public String getName() {
        return name;
    }

    public void setName(String name) {
        this.name = name;
    }

    public List<Round> getRounds() {
        return rounds;
    }

    public void setRounds(List<Round> rounds) {
        this.rounds = rounds;
    }

    public boolean isEnabled() {
        return enabled;
    }

    public void setEnabled(boolean enabled) {
        this.enabled = enabled;
    }
}
