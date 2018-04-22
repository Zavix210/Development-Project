using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

struct FeedbackEntry
{
    private DecisionResult result;
    private string title;
    private string feedback;
    private float timestamp;

    public DecisionResult Result { get { return result; } }
    public string Title { get { return title; } }
    public string Feedback { get { return feedback; } }
    public float Timestamp { get { return timestamp; } }

    public FeedbackEntry(DecisionResult result, string title, string feedback, float timestamp)
    {
        this.result = result;
        this.title = title;
        this.feedback = feedback;
        this.timestamp = timestamp;
    }

    public void WriteToFile(StreamWriter writer)
    {
        // Calculate the timestamp display string
        TimeSpan span = TimeSpan.FromSeconds(timestamp);
        string spanStr = string.Format("{0:D2}:{1:D2}:{2:D2}", span.Hours, span.Minutes, span.Seconds);

        writer.WriteLine("Title: " + title);
        writer.WriteLine("Result: " + result.ToString());
        writer.WriteLine("Feedback: " + feedback);
        writer.WriteLine("Time: " + spanStr);
    }
}

public class Feedback
{
    private List<FeedbackEntry> entries;

    public Feedback()
    {
        entries = new List<FeedbackEntry>();
    }

    public void LogEntry(DecisionResult result, string title, string feedback, float timestamp)
    {
        FeedbackEntry entry = new FeedbackEntry(result, title, feedback, timestamp);
        entries.Add(entry);
    }

    public void WriteFeedbackToFile(StreamWriter writer)
    {
        foreach(FeedbackEntry entry in entries)
        {
            entry.WriteToFile(writer);
        }
    }
}
