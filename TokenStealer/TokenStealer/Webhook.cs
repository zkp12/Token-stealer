using System;
using System.Net;
using System.Collections.Specialized;

public class Webhook : IDisposable
{
    private WebClient client;
    public string URL { get; set; }
    public string Name { get; set; }
    public string Pfp { get; set; }

    public Webhook()
    {
        client = new WebClient();
    }

    public void SendMsg(string msg)
    {
        //creates a collection of all the information that is needed for sending a message with it
        NameValueCollection collection = new NameValueCollection();

        collection.Add("username", Name);
        collection.Add("avatar_url", Pfp);
        collection.Add("content", msg);

        //upload the values, which will in this case send the message
        client.UploadValues(URL, collection);
    }

    public void Dispose()
    {
        client.Dispose();
    }
}
