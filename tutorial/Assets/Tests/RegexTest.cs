
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
// 2. Captures
//    a) capture can be used as backreference (pattern)\1
//    b) capture can be named and can be referenced by tag or by number

// References:
// 1. https://docs.microsoft.com/en-us/dotnet/standard/base-types/regular-expression-language-quick-reference

// TODO:
// 1. \b{ name }

namespace CSharp {
  public class RegexTest {
    [Test]
    public void TestMetaCharacter() {
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
    public void TestMatch() {
      Regex pattern = new Regex(@"\b[at]\w+");
      Match match = pattern.Match("The threaded application ate up the thread pool as it executed.");
      // threaded application ate the thread as
      Assert.That(match.Success, Is.True);
      Assert.That(match.Value, Is.EqualTo("threaded"));
      match = match.NextMatch();
      Assert.That(match.Success, Is.True);
      Assert.That(match.Value, Is.EqualTo("application"));

      List<string> rets = new List<string>();
      foreach (Match m in pattern.Matches("The threaded application ate up the thread pool as it executed.")) {
        rets.Add(m.Value);
      }
      Assert.That(rets, Is.EquivalentTo(new string[]{"threaded", "application", "ate", "the", "thread", "as"}));
      // TODO use LINQ to do a convinent test
    }

    [Test]
    public void TestCaptureBackReference() {
      string input = "He said that that was the The correct answer.";
      MatchCollection c = Regex.Matches(input, @"(\w+)\s\1", RegexOptions.IgnoreCase);
      Assert.That(c.Count, Is.EqualTo(2));
      Assert.That(c[0].Value, Is.EqualTo("that that"));
      Assert.That(c[1].Value, Is.EqualTo("the The"));
    }

    [Test]
    public void TestCaptureName() {
      string input = "He said that that was the The correct answer.";
      MatchCollection c = Regex.Matches(input, @"(?<duplicate>\w+)\s\k<duplicate>\W(?<next>\w+)", RegexOptions.IgnoreCase);
      Assert.That(c.Count, Is.EqualTo(2));

      Match m = c[0];
      Assert.That(m.Value, Is.EqualTo("that that was"));
      GroupCollection gc = m.Groups;
      Assert.That(gc["duplicate"].Value, Is.EqualTo("that"));
      Assert.That(gc["next"].Value, Is.EqualTo("was"));

      m = c[1];
      Assert.That(m.Value, Is.EqualTo("the The correct"));
      gc = m.Groups;
      Assert.That(gc["duplicate"].Value, Is.EqualTo("the"));
      Assert.That(gc["next"].Value, Is.EqualTo("correct"));
    }

    [Test]
    public void TestCaptureTODO() {
      string input = "Yes.Hello. This dog is very friendly.";
      string pattern = @"(\w+\s*)+\.";
      foreach (Match match in Regex.Matches(input, pattern)) {


         UnityEngine.Debug.LogFormat("Match: {0}", match.Value);
         for (int groupCtr = 0; groupCtr < match.Groups.Count; groupCtr++) {
            Group group = match.Groups[groupCtr];
            UnityEngine.Debug.LogFormat("   Group {0}: {1}", groupCtr, group.Value);
            // for (int captureCtr = 0; captureCtr < group.Captures.Count; captureCtr++)
            //    UnityEngine.Debug.LogFormat("      Capture {0}: {1}", captureCtr, 
            //                      group.Captures[captureCtr].Value);
         }                      
      }

      MatchCollection c = Regex.Matches(input, pattern);
      Assert.That(c.Count, Is.EqualTo(3));

      // Match 1
      Match m = c[0];
      Assert.That(m.Value, Is.EqualTo("Yes."));

        // Group
        GroupCollection gc = m.Groups;
        Assert.That(gc.Count, Is.EqualTo(2));

      // Match 2

      // Match 3
    }

    private string GetMatchesChars(Regex pattern) {
      StringBuilder sb = new StringBuilder();
      char c;
      string s;
      for (int i=0; i<128; i++) {
        c = (char)i;
        if (!Char.IsControl(c)) {
          s = new String(c, 1);
          if (pattern.IsMatch(s))
            sb.AppendFormat("{0}({1}); ", Regex.Escape(s), i);
        }
      }

      return sb.ToString();
    }
  }
}
