using System.IO;
using Newtonsoft.Json;

public class Settings
{
    public string PathToWatch { get; set; }
    public string ToFilter { get; set; }
    public string ToEmailAddress { get; set; }
    public string ToEmailName { get; set; }
    public int TimerInterval { get; set; }

    public static Settings Load(string path)
    {
        using (StreamReader r = new StreamReader(path))
        {
            string json = r.ReadToEnd();
            return JsonConvert.DeserializeObject<Settings>(json);
        }
    }
}
