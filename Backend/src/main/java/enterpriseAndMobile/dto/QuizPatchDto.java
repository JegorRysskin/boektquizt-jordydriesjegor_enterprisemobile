package enterpriseAndMobile.dto;

public class QuizPatchDto {
    private boolean enabled;
    private String name;

    public QuizPatchDto() {
    }

    public QuizPatchDto(String name, boolean enabled) {
        this.name = name;
        this.enabled = enabled;
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
}
