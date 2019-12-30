package enterpriseAndMobile.dto;

import enterpriseAndMobile.model.Round;

import java.util.List;

public class QuizDto {

    private String name;

    private boolean enabled;

    private List<Round> rounds;

    public QuizDto() {
    }

    public QuizDto(String name, boolean enabled) {
        this.name = name;
        this.enabled = enabled;
    }

    public QuizDto(String name, boolean enabled, List<Round> rounds) {
        this.name = name;
        this.enabled = enabled;
        this.rounds = rounds;
    }

    public String getName() {
        return name;
    }

    public void setName(String name) {
        this.name = name;
    }

    public boolean isEnabled() {
        return enabled;
    }

    public void setEnabled(boolean enabled) {
        this.enabled = enabled;
    }

    public List<Round> getRounds() { return rounds; }

    public void setRounds(List<Round> rounds) { this.rounds = rounds;
    }
}
