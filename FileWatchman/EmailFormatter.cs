using System.Collections.Generic;
using System.Text;
using System;

public class EmailFormatter
{
    public static string FormatChanges(string rawLog)
    {
        StringBuilder formattedLog = new StringBuilder();
        string[] logEntries = rawLog.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
        HashSet<string> loggedEntries = new HashSet<string>();

        formattedLog.AppendLine("<table border='1' style='border-collapse: collapse; width: 100%;'>");
        formattedLog.AppendLine("<tr><th>Date/Time</th><th>File Path</th><th>Change Type</th><th>Size (bytes)</th></tr>");

        foreach (string entry in logEntries)
        {
            string formattedEntry = FormatEntry(entry);
            if (!loggedEntries.Contains(formattedEntry))
            {
                loggedEntries.Add(formattedEntry);
                formattedLog.AppendLine(formattedEntry);
            }
        }

        formattedLog.AppendLine("</table>");
        return formattedLog.ToString();
    }

    private static string FormatEntry(string entry)
    {
        string[] parts = entry.Split(new[] { ": " }, 2, StringSplitOptions.None);
        if (parts.Length == 2)
        {
            string timestamp = parts[0];
            string changeDetails = parts[1];

            int wasIndex = changeDetails.IndexOf(" was ");
            if (wasIndex > -1)
            {
                string filePath = changeDetails.Substring(0, wasIndex);
                string changeType = changeDetails.Substring(wasIndex + 5);

                string sizeText = "";
                int sizeIndex = changeDetails.IndexOf(" Size: ");
                if (sizeIndex > -1)
                {
                    sizeText = changeDetails.Substring(sizeIndex + 7);
                    changeType = changeDetails.Substring(wasIndex + 5, sizeIndex - (wasIndex + 5));
                }

                return $"<tr><td>{timestamp}</td><td>{filePath}</td><td>{changeType}</td><td>{sizeText}</td></tr>";
            }
            return $"<tr><td>{timestamp}</td><td colspan='3'>{changeDetails}</td></tr>";
        }
        return entry;
    }
}