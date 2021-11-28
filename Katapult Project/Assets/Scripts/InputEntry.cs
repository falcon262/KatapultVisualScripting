using System;

[Serializable]
public class InputEntry
{
    public string username;
    public string password;

    public InputEntry(string username, string password)
    {
        this.username = username;
        this.password = password;
    }
}
