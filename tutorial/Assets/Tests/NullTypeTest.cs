using System;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

// ref: https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/nullable-types/how-to-identify-a-nullable-type
namespace Tests
{
  // 1. Purpose: T? is used in the environment which value can be null
  // C# syntactic sugar:
  // a) T? is equivalent to new NullType<T> where T : struct. 
  public class NullTypeTest
  {
    [Test]
    public void TestNullType()
    {
      // Creation
      Nullable<int> age = null;
      Assert.That(age.HasValue, Is.False);
      Assert.That(0, Is.EqualTo(age.GetValueOrDefault()));
      Assert.That(1, Is.EqualTo(age.GetValueOrDefault(1)));
      Assert.Throws<InvalidOperationException>(()=>{ int v = age.Value; });

      Nullable<int> score = new Nullable<int>(3);
      Assert.That(score.HasValue, Is.True);
      Assert.That(3, Is.EqualTo(score.Value));
      Assert.That(3, Is.EqualTo(score.GetValueOrDefault()));

      // Comparision
      Assert.That(age.Equals(null), Is.True);
      Assert.That(age.Equals(score), Is.False);
      Assert.That(score.Equals(3), Is.True);
      Assert.That(score.Equals(null), Is.False);
    }

    [Test]
    public void TestSyntacticSugar()
    {
      int? age = null;
      Assert.That(age.HasValue, Is.False);
      Assert.That(age ?? 1, Is.EqualTo(1));
    }

    // TODO
    // 1. int? age can be assigned.
    // 2. boxing and unboxing
    // 3. operator overloading
  }
}
