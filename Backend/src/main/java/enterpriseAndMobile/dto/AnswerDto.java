package enterpriseAndMobile.dto;

public class AnswerDto {
    private String answerString;

    private int questionId;

    public AnswerDto(String answerString) {
        this.answerString = answerString;
    }

    public AnswerDto() {
    }

    public String getAnswerString() {
        return answerString;
    }

    public void setAnswerString(String answerString) {
        this.answerString = answerString;
    }

    public int getQuestionId() {
        return questionId;
    }

    public void setQuestionId(int questionId) {
        this.questionId = questionId;
    }
}
