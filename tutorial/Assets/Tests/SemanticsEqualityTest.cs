using System;
using NUnit.Framework;
using UnityEngine;
using Moq;

using Object = System.Object;

// TODO.
// 1. GetHashCode
// 2. Object Initializer

// Tips:
// 1. == and != is compiler behavior. It is converted to identify comparision by default,
//    unless you do not override the correspoinding operators.

namespace Tests
{
  public class SemanticsEqualityTest {
    public class InfoA : IEquatable<InfoA> {
      public int Age { get; set; }

      public override bool Equals(object obj) {
        Debug.Log("Equals with obj wal called");
        if (obj == null || !this.GetType().Equals(obj.GetType()))
          return false;

        InfoA o = (InfoA)obj;

        return Equals(o);
      }

      public bool Equals(InfoA obj) {
        Debug.Log("Equatable Equals was called.");
        return Age == obj.Age;
      }

      public override int GetHashCode() {
        return Age;
      }
    }

    public struct InfoB {
      public DateTime               Birthday;
      public InfoA                  OtherInfo;

      public InfoB(DateTime birthday, int age) {
        Birthday = birthday;
        OtherInfo = new InfoA {Age = age};
      }
    }

    // Object.Equal
    // https://docs.microsoft.com/en-us/dotnet/api/system.object.equals?view=netframework-4.8#System_Object_Equals_System_Object_
    // Purpose -- Perform equality check. However, its default implementation is identity comparision.
    [Test]
    public void TestObjectEqual() {
      var o1 = new InfoA {Age = 1};
      var o2 = new InfoA {Age = 2};
      var o3 = new InfoA {Age = 2};
      Assert.That(o1.Equals(o2), Is.False);
      Assert.That(o2.Equals(o3), Is.True);
    }

    // ValueType.Equals
    // https://docs.microsoft.com/en-us/dotnet/api/system.valuetype.equals?view=netframework-4.8
    // If none of the fields of the current instance and obj are reference types, the Equals method performs a byte-by-byte
    // comparison of the two objects in memory. Otherwise, it uses reflection to compare the corresponding fields of obj and this instance.
    [Test]
    public void TestValueTypeEquals() {
      InfoB s1 = new InfoB(new DateTime(2019, 3, 12), 10);
      InfoB s2 = new InfoB(new DateTime(2019, 3, 12), 11);

      Assert.That(s1.Equals(s2), Is.False);

      var mock = new Mock<InfoA>();
      mock.Setup(m => m.Equals((Object)s2.OtherInfo)).Returns(true);
      s1.OtherInfo = mock.Object;

      Assert.That(s1.Equals(s2), Is.True);
      mock.Verify(m => m.Equals((Object)s2.OtherInfo), Times.Once);
    }

    // static Object.ReferenceEquals
    // https://docs.microsoft.com/en-us/dotnet/api/system.object.referenceequals?view=netframework-4.8#System_Object_ReferenceEquals_System_Object_System_Object_
    // Purpose: compare object identity
    [Test]
    public void TestObjectReferenceEquals() {
      Assert.That(Object.ReferenceEquals(null, null), Is.True);
      var suffix = "_";
      var o1 = "TestString" + suffix;
      var o2 = "TestString" + suffix;
      var o3 = o1;
      Assert.That(Object.ReferenceEquals(null, o1), Is.False);
      Assert.That(Object.ReferenceEquals(o1, o2), Is.False);
      Assert.That(Object.ReferenceEquals(o1, o3), Is.True);

      // Object.ReferenceEquals(valueType1, valueType2) always returns false
      int age = 34;
      Assert.That(Object.ReferenceEquals(age, age), Is.False);

      // When comparing strings, the interned string is compared if the string is interned.
      var s1 = "TestString";
      var s2 = "TestString";
      Assert.That(String.IsInterned(s1), Is.Not.Null);
      Assert.That(String.IsInterned(s2), Is.Not.Null);
      Assert.That(Object.ReferenceEquals(s1, s2), Is.True);
    }

    // static Object.Equals
    // https://docs.microsoft.com/en-us/dotnet/api/system.object.equals?view=netframework-4.8#System_Object_Equals_System_Object_System_Object_
    // Purpose - helper method to call a.Equals(b) safely
    [Test]
    public void TestObjectStaticEquals() {
      var mock = new Mock<InfoA>();
      Object o = new InfoA();

      mock.Setup(m => m.Equals(o)).Returns(false);

      Assert.That(Object.Equals(null, mock.Object), Is.False);
      mock.Verify(m => m.Equals(o), Times.Never);

      Assert.That(Object.Equals(mock.Object, o), Is.False);
      mock.Verify(m => m.Equals(o), Times.Once);
    }
  }
}
