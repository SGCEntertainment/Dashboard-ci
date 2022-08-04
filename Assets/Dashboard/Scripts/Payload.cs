using System.Collections.Generic;

[System.Serializable]
public class Payload
{
    public string type;
    public List<Value> value;

    public Payload(string type, List<Value> value)
    {
        this.type = type;
        this.value = value;
    }
}
