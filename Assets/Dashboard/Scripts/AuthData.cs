[System.Serializable]
public class AuthData
{
    public string username;
    public string password;
    public string grant_type;

    public AuthData(string username, string password, string grant_type)
    {
        this.username = username;
        this.password = password;
        this.grant_type = grant_type;
    }
}