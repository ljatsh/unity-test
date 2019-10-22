using System;
using NUnit.Framework;
using UnityEngine;
using Moq;

using Object = System.Object;

// TODO.
// 1. GetHashCode together with HashTable
// 2. Object Initializer

// Tips:
// 1. == and != is compiler behavior. It is converted to identify comparision by default,
//    unless you do not override the correspoinding operators.(refer operator overload in CLR for more information)
// 2. Override equality semantics:
//    * Override GetHashCode() and Equals()
//    * (Optionally) overload != and ==
//    * (Optionally) implement IEquatable<T>
// 3. Rules to override GetHashCode()
//    * It must return the same value on two objects for which Equals returns true(hence, GetHashCode and Equals are overridden together).
//    * It must not throw exceptions.
//    * It must return the same value if called repeatedly on the same object (unless the object has changed).
//    Tuple<>::GetHashCode is a conveninent way to implement GetHashCode of types with many fields (https://docs.microsoft.com/en-us/dotnet/api/system.object.gethashcode?view=netcore-2.2#System_Object_GetHashCode)



namespace CSharp
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

    // .method public hidebysig instance void TestIEquatable() cil managed
    // {
    //   // Code size 75
    //   .maxstack 3
    //   .locals init(Tests.SemanticsEqualityTest/InfoA V_0, [mscorlib]System.Object V_1, Tests.SemanticsEqualityTest/InfoA V_2)
    //   IL_0000: nop
    //   IL_0001: newobj instance void Tests.SemanticsEqualityTest/InfoA::.ctor()
    //   IL_0006: dup
    //   IL_0007: ldc.i4.s 10
    //   IL_0009: callvirt instance void Tests.SemanticsEqualityTest/InfoA::set_Age(int32)
    //   IL_000e: nop
    //   IL_000f: stloc.0
    //   IL_0010: newobj instance void Tests.SemanticsEqualityTest/InfoA::.ctor()
    //   IL_0015: dup
    //   IL_0016: ldc.i4.s 10
    //   IL_0018: callvirt instance void Tests.SemanticsEqualityTest/InfoA::set_Age(int32)
    //   IL_001d: nop
    //   IL_001e: stloc.1
    //   IL_001f: ldloc.1
    //   IL_0020: castclass Tests.SemanticsEqualityTest/InfoA
    //   IL_0025: stloc.2
    //   IL_0026: ldloc.0
    //   IL_0027: ldloc.1
    //   IL_0028: callvirt instance boolean [mscorlib]System.Object::Equals([mscorlib]System.Object)
    //   IL_002d: call [nunit.framework]NUnit.Framework.Constraints.TrueConstraint [nunit.framework]NUnit.Framework.Is::get_True()
    //   IL_0032: call void [nunit.framework]NUnit.Framework.Assert::That(mvar, [nunit.framework]NUnit.Framework.Constraints.IResolveConstraint)
    //   IL_0037: nop
    //   IL_0038: ldloc.0
    //   IL_0039: ldloc.2
    //   IL_003a: callvirt instance boolean Tests.SemanticsEqualityTest/InfoA::Equals(Tests.SemanticsEqualityTest/InfoA)
    //   IL_003f: call [nunit.framework]NUnit.Framework.Constraints.TrueConstraint [nunit.framework]NUnit.Framework.Is::get_True()
    //   IL_0044: call void [nunit.framework]NUnit.Framework.Assert::That(mvar, [nunit.framework]NUnit.Framework.Constraints.IResolveConstraint)
    //   IL_0049: nop
    //   IL_004a: ret
    // } // End of method System.Void Tests.SemanticsEqualityTest::TestIEquatable()
    [Test]
    [Ignore("This is just used to demostrate static binding")]
    public void TestIEquatable() {
      var o1 = new InfoA {Age = 10};
      Object o2 = new InfoA {Age = 10};
      var o3 = (InfoA)o2;

      Assert.That(o1.Equals(o2), Is.True);
      Assert.That(o1.Equals(o3), Is.True);
    }

    // .method public hidebysig instance void TestEqualityOperatorOverload() cil managed
    // {
    //   // Code size 65
    //   .maxstack 3
    //   .locals init(Tests.SemanticsEqualityTest/InfoA V_0, Tests.SemanticsEqualityTest/InfoA V_1)
    //   IL_0000: nop
    //   IL_0001: newobj instance void Tests.SemanticsEqualityTest/InfoA::.ctor()
    //   IL_0006: dup
    //   IL_0007: ldc.i4.s 10
    //   IL_0009: callvirt instance void Tests.SemanticsEqualityTest/InfoA::set_Age(int32)
    //   IL_000e: nop
    //   IL_000f: stloc.0
    //   IL_0010: newobj instance void Tests.SemanticsEqualityTest/InfoA::.ctor()
    //   IL_0015: dup
    //   IL_0016: ldc.i4.s 10
    //   IL_0018: callvirt instance void Tests.SemanticsEqualityTest/InfoA::set_Age(int32)
    //   IL_001d: nop
    //   IL_001e: stloc.1
    //   IL_001f: ldloc.0
    //   IL_0020: ldloc.1
    //   IL_0021: ceq
    //   IL_0023: call [nunit.framework]NUnit.Framework.Constraints.FalseConstraint [nunit.framework]NUnit.Framework.Is::get_False()
    //   IL_0028: call void [nunit.framework]NUnit.Framework.Assert::That(mvar, [nunit.framework]NUnit.Framework.Constraints.IResolveConstraint)
    //   IL_002d: nop
    //   IL_002e: ldloc.0
    //   IL_002f: ldloc.1
    //   IL_0030: ceq
    //   IL_0032: ldc.i4.0
    //   IL_0033: ceq
    //   IL_0035: call [nunit.framework]NUnit.Framework.Constraints.TrueConstraint [nunit.framework]NUnit.Framework.Is::get_True()
    //   IL_003a: call void [nunit.framework]NUnit.Framework.Assert::That(mvar, [nunit.framework]NUnit.Framework.Constraints.IResolveConstraint)
    //   IL_003f: nop
    //   IL_0040: ret
    // } // End of method System.Void Tests.SemanticsEqualityTest::TestEqualityOperatorOverload()
    [Test]
    public void TestEqualityOperatorOverload() {
      var o1 = new InfoA {Age = 10};
      var o2 = new InfoA {Age = 10};

      Assert.That(o1 == o2, Is.False);
      Assert.That(o1 != o2, Is.True);

      // Wont't compile
      // var o3 = new InfoB(new DateTime(2019, 3, 12), 10);
      // var o4 = new InfoB(new DateTime(2019, 3, 12), 10);
      // Assert.That(o3 == o4, Is.True);
    }
  }
}
