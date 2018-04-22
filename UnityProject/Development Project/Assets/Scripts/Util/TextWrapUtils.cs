using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;

public enum TextWrapMode
{
    IncludeLastWord,
    ExcludeLastWord
}

public class TextWrapUtils
{
    /// <summary>
    /// Wrap a string by inserting newline characters into the string after the specified limit
    /// based upon the wrap mode specified.
    /// </summary>
    /// <param name="rawText"></param>
    /// <param name="charactersPerLine"></param>
    /// <param name="wrapMode"></param>
    /// <returns></returns>
    public static string GetWrappedText(string rawText, int charactersPerLine, TextWrapMode wrapMode = TextWrapMode.IncludeLastWord)
    {
        char delimiter = ' ';
        string newLineChar = Environment.NewLine;
        string final = "";
        string line = "";

        string[] split = rawText.Split(delimiter);
        foreach(string str in split)
        {
            int result = line.Length + str.Length;
            if(result <= charactersPerLine) // Safe to use the string
            {
                line += str + delimiter;
            }
            else // The word exceeds the character limit
            {
                if(wrapMode == TextWrapMode.IncludeLastWord) // Should the last word be included?
                {
                    // Append the word and delimiter to the line
                    line += str + delimiter;

                    // Append the line onto the final string
                    final += line + newLineChar;

                    // Reset the line to be empty
                    line = "";
                }
                else // Should the last word be excluded?
                {
                    // Apply the line to the final string
                    final += line + newLineChar;

                    // Set the line to start with the word
                    line = str + delimiter;
                }
            }
        }

        // Apply the remainder of the final string
        final += line;

        // Trim the result string
        final = final.TrimEnd();

        return final;
    }
}