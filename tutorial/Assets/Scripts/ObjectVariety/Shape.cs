using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Shape : PersistableObject {
  public int ShapeId {
    get { return shapeId; }
    set {
      if (shapeId == int.MinValue && value != int.MinValue) {
        shapeId = value;
      }
      else {
        Debug.LogError("changes of shape id is not allowed.");
      }
    }
  }

  private int shapeId = int.MinValue;
}
