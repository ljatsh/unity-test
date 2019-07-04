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

  public int MaterialId { get; private set; }

  private int shapeId = int.MinValue;

  public void SetMaterial(Material material, int materialId) {
    GetComponent<MeshRenderer>().material = material;
    MaterialId = materialId;
  }

  private Color color;

  public void SetColor(Color color) {
    GetComponent<MeshRenderer>().material.color = color;
    this.color = color;
  }

  public override void Save(GameDataWriter writer) {
    base.Save(writer);
    writer.Write(color);
  }

  public override void Load(GameDataReader reader) {
    base.Load(reader);
    SetColor(reader.ReadColor());
  }
}
