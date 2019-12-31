package enterpriseAndMobile.dto;

import enterpriseAndMobile.model.Question;

import java.util.List;

public class RoundPatchDto {
    private boolean enabled;

    private String name;

    private List<Question> questions;

    public RoundPatchDto(String name) {
        this.name = name;
    }

    public RoundPatchDto() {
    }

    public boolean isEnabled() {
        return enabled;
    }

    public void setEnabled(boolean enabled) {
        this.enabled = enabled;
    }

    public String getName() {
        return name;
    }

    public void setName(String name) {
        this.name = name;
    }

    public List<Question> getQuestions() {
        return questions;
    }

    public void setQuestions(List<Question> questions) {
        this.questions = questions;
    }
}
