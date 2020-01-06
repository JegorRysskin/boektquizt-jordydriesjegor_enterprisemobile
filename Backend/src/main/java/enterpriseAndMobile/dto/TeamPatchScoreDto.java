package enterpriseAndMobile.dto;

public class TeamPatchScoreDto {
    private Double scores = 0.0;

    public TeamPatchScoreDto() {
    }

    public TeamPatchScoreDto(Double scores) {
        this.scores = scores;
    }

    public Double getScores() {
        return scores;
    }

    public void setScores(Double scores) {
        this.scores = scores;
    }
}
