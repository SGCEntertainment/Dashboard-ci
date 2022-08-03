using System;
using System.Collections.Generic;

[Serializable]
public class Config
{
    public string projectId;
    public string environmentId;
    public string type;
    public List<Value> value;
    public string id;
    public string version;
    public string createdAt;
    public string updatedAt;
}
