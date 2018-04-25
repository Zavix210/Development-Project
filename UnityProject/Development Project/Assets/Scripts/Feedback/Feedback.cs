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
    private float remainingTime;

    public DecisionResult Result { get { return result; } }
    public string Title { get { return title; } }
    public string Feedback { get { return feedback; } }
    public float Timestamp { get { return timestamp; } }
    public float RemainingTime { get { return remainingTime; } }

    public FeedbackEntry(DecisionResult result, string title, string feedback, float timestamp, float remainingTime)
    {
        this.result = result;
        this.title = title;
        this.feedback = feedback;
        this.timestamp = timestamp;
        this.remainingTime = remainingTime;
    }

    public void WriteToFile(StreamWriter writer)
    {
        // Calculate the timestamp display string
        TimeSpan span = TimeSpan.FromSeconds(timestamp);
        string spanStr = string.Format("{0:D2}:{1:D2}:{2:D2}", span.Hours, span.Minutes, span.Seconds);

        // Calculate the remaining time display string
        span = TimeSpan.FromSeconds(remainingTime);
        string spanStr1 = string.Format("{0:D2}:{1:D2}:{2:D2}", span.Hours, span.Minutes, span.Seconds);

        // Output the data to the file
        writer.WriteLine("Title: " + title);
        writer.WriteLine("Result: " + result.ToString());
        writer.WriteLine("Feedback: " + feedback);
        writer.WriteLine("Time: " + spanStr);
        writer.WriteLine("Remaining Time: " + spanStr1);
    }
}

public class Feedback
{
    private List<FeedbackEntry> entries;

    public Feedback()
    {
        entries = new List<FeedbackEntry>();
    }

    public void LogEntry(DecisionResult result, string title, string feedback, float timestamp, float remainingTime)
    {
        FeedbackEntry entry = new FeedbackEntry(result, title, feedback, timestamp, remainingTime);
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
