package enterpriseAndMobile.model;

import org.hibernate.annotations.GenericGenerator;

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

    public Quiz(String name, boolean enabled, List<Round> rounds) {
        this.name = name;
        this.enabled = enabled;
        this.rounds = rounds;
    }

    @Id
    @GeneratedValue(generator = "sequence-generator-quiz")
    @GenericGenerator(
            name = "sequence-generator-quiz",
            strategy = "org.hibernate.id.enhanced.SequenceStyleGenerator",
            parameters = {
                    @org.hibernate.annotations.Parameter(name = "sequence_name", value = "sequence-generator-quiz"),
                    @org.hibernate.annotations.Parameter(name = "initial_value", value = "1"),
                    @org.hibernate.annotations.Parameter(name = "increment_size", value = "1")
            })
    private int id;

    private String name;

    @OneToMany(cascade = CascadeType.ALL)
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
