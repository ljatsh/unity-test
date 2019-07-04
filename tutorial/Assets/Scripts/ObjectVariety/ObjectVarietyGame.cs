
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// TODO. HSVColor Model
// TODO. Static Intialization and why Unity disabled this feature
// TODO. Enable GPU Instancing

public class ObjectVarietyGame : Game
{
  public ShapeFactory factory;

  private List<Shape> shapes;
  private string dataFile;

  void Awake() {
    shapes = new List<Shape>();
    dataFile = Path.Combine(Application.persistentDataPath, "data_ObjectVarient");
  }

  protected override void createObject() {
    Shape spawn = factory.getRandomShape();

    spawn.transform.localPosition = Random.insideUnitSphere * 5f;
    spawn.transform.localScale = Vector3.one * Random.Range(0.1f, 1.0f);
    spawn.transform.rotation = Random.rotation;
    spawn.SetColor(Random.ColorHSV(0f, 1f, 0.5f, 1f, 0.25f, 1f, 1f, 1f));

    shapes.Add(spawn);
  }

  protected override void newGame() {
    foreach (var o in shapes)
      Destroy(o.gameObject);
    shapes.Clear();
  }

  protected override void saveGame() {
    using (var writer = new BinaryWriter(File.Open(dataFile, FileMode.Create))) {
      var gameWriter = new GameDataWriter(writer);

      gameWriter.Write(shapes.Count);
      foreach(var obj in shapes) {
        gameWriter.Write(obj.ShapeId);
        gameWriter.Write(obj.MaterialId);
        obj.Save(gameWriter);
      }
    }
  }

  // Ignore any IO error
  protected override void loadGame() {
    newGame();

    using (var reader = new BinaryReader(File.Open(dataFile, FileMode.Open))) {
      var gameReader = new GameDataReader(reader);

      int count = gameReader.ReadInt();
      int shapeId, materialId;
      for (int i=0; i<count; i++) {
        shapeId = gameReader.ReadInt();
        materialId = gameReader.ReadInt();
        Shape spawn = factory.getShape(shapeId, materialId);
        spawn.Load(gameReader);

        shapes.Add(spawn);
      }
    }
  }
}
