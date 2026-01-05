namespace D2Bot;

public class WebResponse
{
	public string request;

	public string status;

	public string body;

	public WebResponse(string request, string status, string body)
	{
		this.request = request;
		this.status = status;
		this.body = body;
	}
}
