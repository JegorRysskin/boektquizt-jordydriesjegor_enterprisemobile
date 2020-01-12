package enterpriseAndMobile.dto;

public class RoundPatchTeamIdDto {
    private int teamId;

    public RoundPatchTeamIdDto() {
    }

    public RoundPatchTeamIdDto(int teamId)
    {
        this.teamId = teamId;
    }

    public int getTeamId() { return teamId; }

    public void setTeamId(int teamId) { this.teamId = teamId; }
}
