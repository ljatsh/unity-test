
using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using NUnit.Framework;

// Tips:
// 1. Meta Character
//    a) \w = [\d_a-zA-Z]
//    b) \s 仅仅指代空格，并不包括\t
//    c) . 仅仅排除\n

// References:
// 1. https://docs.microsoft.com/en-us/dotnet/standard/base-types/regular-expression-language-quick-reference

// TODO:
// 1. \b{ name }

namespace Tests
{
  public class RegexTest
  {
    [Test]
    public void TestMetaCharacter()
    {
      UnityEngine.Debug.Log("meta character .");
      UnityEngine.Debug.Log(GetMatchesChars(new Regex(@".")));
      UnityEngine.Debug.Log("meta character \\w");
      UnityEngine.Debug.Log(GetMatchesChars(new Regex(@"\w")));
      UnityEngine.Debug.Log("meta character \\s");
      UnityEngine.Debug.Log(GetMatchesChars(new Regex(@"\s")));
      UnityEngine.Debug.Log("meta character \\d");
      UnityEngine.Debug.Log(GetMatchesChars(new Regex(@"\d")));

      Assert.That(true, Is.True);
    }

    [Test]
    public void TestMatch()
    {
      Regex pattern = new Regex(@"\b[at]\w+");
      Match match = pattern.Match("The threaded application ate up the thread pool as it executed.");
      // threaded application ate the thread as
      Assert.That(match.Success, Is.True);
      Assert.That(match.Value, Is.EqualTo("threaded"));
      match = match.NextMatch();
      Assert.That(match.Success, Is.True);
      Assert.That(match.Value, Is.EqualTo("application"));

      List<string> rets = new List<string>();
      foreach (Match m in pattern.Matches("The threaded application ate up the thread pool as it executed."))
      {
        rets.Add(m.Value);
      }
      Assert.That(rets, Is.EquivalentTo(new string[]{"threaded", "application", "ate", "the", "thread", "as"}));
    }

    [Test]
    public void TestCapture()
    {
      string input = "Yes.Hello. This dog is very friendly.";
      //string pattern = @"((\w+)[\s.])+";
      string pattern = @"(\w+\s*)+\.";
      foreach (Match match in Regex.Matches(input, pattern))
      {
         UnityEngine.Debug.LogFormat("Match: {0}", match.Value);
        //  for (int groupCtr = 0; groupCtr < match.Groups.Count; groupCtr++)
        //  {
        //     Group group = match.Groups[groupCtr];
        //     UnityEngine.Debug.LogFormat("   Group {0}: {1}", groupCtr, group.Value);
        //     for (int captureCtr = 0; captureCtr < group.Captures.Count; captureCtr++)
        //        UnityEngine.Debug.LogFormat("      Capture {0}: {1}", captureCtr, 
        //                          group.Captures[captureCtr].Value);
        //  }                      
      }

      // TODO
    }

    private string GetMatchesChars(Regex pattern)
    {
      StringBuilder sb = new StringBuilder();
      char c;
      string s;
      for (int i=0; i<128; i++)
      {
        c = (char)i;
        if (!Char.IsControl(c))
        {
          s = new String(c, 1);
          if (pattern.IsMatch(s))
            sb.AppendFormat("{0}({1}); ", Regex.Escape(s), i);
        }
      }

      return sb.ToString();
    }
  }
}
