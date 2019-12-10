package enterpriseAndMobile.model;

import javax.persistence.*;

@Entity
@Table(name = "answer")
public class Answer {
    @Id
    @GeneratedValue
    private int id;

    private String answer;

    @ManyToOne
    private Team team;

    public int getId() {
        return id;
    }

    public String getAnswer() {
        return answer;
    }

    public void setAnswer(String answer) {
        this.answer = answer;
    }

    public void setId(int id) { this.id = id; }

    public Team getTeam() { return team; }

    public void setTeam(Team team) { this.team = team; }
}
