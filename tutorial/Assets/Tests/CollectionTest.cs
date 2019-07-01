using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

// TODO. Array in CLR
// TODO. foreach in CLR and foreach semantics requirement
// TODO. yield iterator in CLR
// TODO. NUnit strict equivalent constraint

namespace Tests {
  public class CollectionTest {
    // https://docs.microsoft.com/en-us/dotnet/api/system.collections.ienumerator?view=netcore-2.2
    // Enumerators can be used to read data from a collection, but they cannot be used to modify the collection
    [Test]
    public void TestEnumerator() {
      int[] v = {1, 2, 3};

      // TODO Why the following cast is invalid
      // (IEnumerator<int>)v.GetEnumerator()
      IEnumerator cursor = v.GetEnumerator();
      Assert.That(cursor.MoveNext(), Is.True);
      Assert.That(cursor.Current, Is.EqualTo(1));
      Assert.That(cursor.MoveNext(), Is.True);
      Assert.That(cursor.Current, Is.EqualTo(2));
      Assert.That(cursor.MoveNext(), Is.True);
      Assert.That(cursor.Current, Is.EqualTo(3));
      Assert.That(cursor.MoveNext(), Is.False);
    }

    // https://docs.microsoft.com/en-us/dotnet/api/system.collections.ienumerable?view=netcore-2.2
    // Have IEnumerable to forward traversal request to a new object make several concurrent traversals possible
    [Test]
    public void TestEnumerable() {
      int[] elements = {1, 2, 3};
      int[] results = new int[6];

      int index = 0;
      foreach (var v in elements) {
          if (v != 2) {
            foreach(var k in elements) {
              results[index++] = v * k;
            }
          }
      }

      Assert.That(results, Is.EquivalentTo(new int[]{1, 2, 3, 3, 6, 9}));
    }

    // Reasons to implement customized IEnumerable
    // * To support the foreach statement
    // * To interoperate with anything expecting a standard collection
    // * To meet the requirements of a more sophisticated collection interface
    // * To support collection initializers
    //
    // Methods to implement customized IEnumerable
    // * If the class is “wrapping” another collection, by returning the wrapped collec- tion’s enumerator
    // * Via an iterator using yield return
    // * By instantiating your own IEnumerator/IEnumerator<T> implementation
    [Test]
    public void TestEnumerableImplementationByIterator() {
      int[] results = new int[3];

      IEnumerator cursor = getTestEnumerator();
      int index = 0;
      while(cursor.MoveNext())
        results[index++] = (int)cursor.Current;

      Assert.That(results, Is.EquivalentTo(new int[]{1, 2, 3}));
    }

    // https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.icollection-1?view=netcore-2.2
    // https://docs.microsoft.com/en-us/dotnet/api/system.collections.icollection?view=netcore-2.2
    // * ICollection provides the ability to determine the size of a collection (Count), determine whether an item exists in the collection (Contains),
    //   copy the collection into an array (ToArray), and determine whether the collection is read-only (IsReadOnly)
    // * ICollection extends IEnumerable but ICollection<T> does not extends ICollection
    [Test]
    public void TestICollection() {
      ICollection<int> elements = new List<int>{1, 2, 3};

      Assert.That(elements.Count, Is.EqualTo(3));
      Assert.That(elements.IsReadOnly, Is.False);
      Assert.That(elements.Contains(3), Is.True);

      elements.Add(4);
      Assert.That(elements, Is.EquivalentTo(new int[]{1, 2, 3, 4}));
      elements.Remove(3);
      Assert.That(elements, Is.EquivalentTo(new int[]{1, 2, 4}));

      int[] results = new int[4];
      elements.CopyTo(results, 1);
      Assert.That(results, Is.EquivalentTo(new int[]{0, 1, 2, 4}));
    }

    // https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.ilist-1?view=netcore-2.2
    // https://docs.microsoft.com/en-us/dotnet/api/system.collections.ilist?view=netcore-2.2
    // * IList<T> is the standard interface for collections indexable by position. In addition to the functionality inherited from ICollection<T>
    //   and IEnumerable<T>, it provides the ability to read or write an element by position (via an indexer) and insert/remove by position
    [Test]
    public void TestIList() {
      IList<int> elements = new List<int>{1, 2, 3};

      Assert.That(elements.Count, Is.EqualTo(3));
      Assert.That(elements.IsReadOnly, Is.False);
      Assert.That(elements[1], Is.EqualTo(2));
      Assert.That(elements.IndexOf(3), Is.EqualTo(2));
      Assert.That(elements.IndexOf(4), Is.EqualTo(-1));

      elements.Insert(1, 4);
      Assert.That(elements, Is.EquivalentTo(new int[]{1, 4, 2, 3}));
      elements.RemoveAt(2);
      Assert.That(elements, Is.EquivalentTo(new int[]{1, 4, 3}));
    }

    private IEnumerator getTestEnumerator() {
      yield return 1;
      yield return 2;
      yield return 3;
    }
  }
}
