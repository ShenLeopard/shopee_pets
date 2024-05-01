public class ApiResponseModel
{
    public Candidate[] Candidates { get; set; }
    public Promptfeedback PromptFeedback { get; set; }

    public class Candidate
    {
        public Content Content { get; set; }
        public string FinishReason { get; set; }
        public int Index { get; set; }
        public Safetyrating[] SafetyRatings { get; set; }
    }

    public class Content
    {
        public Part[] Parts { get; set; }
        public string Role { get; set; }
    }

    public class Part
    {
        public string Text { get; set; }
    }

    public class Safetyrating
    {
        public string Category { get; set; }
        public string Probability { get; set; }
    }

    public class Promptfeedback
    {
        public Safetyrating[] SafetyRatings { get; set; }
    }
}
