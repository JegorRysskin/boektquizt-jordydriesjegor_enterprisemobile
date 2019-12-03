package enterpriseAndMobile.model;

import javax.persistence.*;
import java.util.List;

@Entity
@Table(name = "team")
public class Team {
    public Team() {
    }

    public Team(String name) {
        this.name = name;
    }

    @Id
    @GeneratedValue
    private int id;

    private String name;

    @OneToMany(mappedBy = "team")
    private List<Answer> answers;

    private boolean enabled;

    public int getId() { return id; }

    public void setId(int id) { this.id = id; }

    public String getName() { return name; }

    public void setName(String name) { this.name = name; }

    public List<Answer> getAnswers() { return answers; }

    public void setAnswers(List<Answer> answers) { this.answers = answers; }

    public boolean isEnabled() { return enabled; }

    public void setEnabled(boolean enabled) { this.enabled = enabled; }
}
