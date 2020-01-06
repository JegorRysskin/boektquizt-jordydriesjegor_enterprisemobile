package enterpriseAndMobile.dto;

import enterpriseAndMobile.model.Answer;

public class TeamPatchAnswersDto {
    private Answer answers;

    public TeamPatchAnswersDto() {
    }

    public TeamPatchAnswersDto(Answer answers) {
        this.answers = answers;
    }

    public Answer getAnswers() {
        return answers;
    }

    public void setAnswers(Answer answers) {
        this.answers = answers;
    }
}
