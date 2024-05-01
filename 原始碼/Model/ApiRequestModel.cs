public class ApiRequestModel
{
    public Content[] contents { get; set; }
}

public class Content
{
    public object[] parts { get; set; }
}

public class TextPart
{
    public string text { get; set; }
}

public class InlineDataPart
{
    public InlineData inlineData { get; set; }
}

public class InlineData
{
    public string mimeType { get; set; }
    public string data { get; set; }
}

