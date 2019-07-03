using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// TODO. CSharp IO
// TODO. using and IDispose

public class PersistingObjectGame : Game {
  public PersistableObject prefab;

  private List<PersistableObject> objects;
  private string dataFile;

  void Awake() {
    objects = new List<PersistableObject>();
    dataFile = Path.Combine(Application.persistentDataPath, "data_PersistingObject");
  }

  protected override void createObject() {
    PersistableObject spawn = Instantiate(prefab);
    spawn.transform.localPosition = Random.insideUnitSphere * 5f;
    spawn.transform.localScale = Vector3.one * Random.Range(0.1f, 1.0f);
    spawn.transform.rotation = Random.rotation;

    objects.Add(spawn);
  }

  protected override void newGame() {
    foreach (var o in objects)
      Destroy(o.gameObject);
    objects.Clear();
  }

  protected override void saveGame() {
    using (var writer = new BinaryWriter(File.Open(dataFile, FileMode.Create))) {
      var gameWriter = new GameDataWriter(writer);

      gameWriter.Write(objects.Count);
      foreach(var obj in objects) {
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
      for (int i=0; i<count; i++) {
        PersistableObject spawn = Instantiate(prefab);
        spawn.Load(gameReader);

        objects.Add(spawn);
      }
    }
  }
}
