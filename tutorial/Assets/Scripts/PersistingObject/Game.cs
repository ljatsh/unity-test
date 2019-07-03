using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// TODO. CSharp IO
// TODO. using and IDispose

public class Game : MonoBehaviour {
  public PersistableObject prefab;

  public KeyCode createObjectKey;
  public KeyCode newGameKey;
  public KeyCode saveGameKey;
  public KeyCode loadGameKey;

  private List<PersistableObject> objects;
  private string dataFile;

  void Awake() {
    objects = new List<PersistableObject>();
    dataFile = Path.Combine(Application.persistentDataPath, "data_PersistingObject");
  }

  // Start is called before the first frame update
  void Start() {
  }

  // Update is called once per frame
  void Update() {
    if (Input.GetKeyDown(createObjectKey))
      createObject();
    else if (Input.GetKeyDown(newGameKey))
      newGame();
    else if (Input.GetKeyDown(saveGameKey))
      saveGame();
    else if (Input.GetKeyDown(loadGameKey))
      loadGame();
  }

  void createObject() {
    PersistableObject spawn = Instantiate(prefab);
    spawn.transform.localPosition = Random.insideUnitSphere * 5f;
    spawn.transform.localScale = Vector3.one * Random.Range(0.1f, 1.0f);
    spawn.transform.rotation = Random.rotation;

    objects.Add(spawn);
  }

  void newGame() {
    foreach (var o in objects)
      Destroy(o.gameObject);
    objects.Clear();
  }

  void saveGame() {
    using (var writer = new BinaryWriter(File.Open(dataFile, FileMode.Create))) {
      var gameWriter = new GameDataWriter(writer);

      gameWriter.Write(objects.Count);
      foreach(var obj in objects) {
        gameWriter.Write(obj.transform.localPosition);
        gameWriter.Write(obj.transform.localScale);
        gameWriter.Write(obj.transform.rotation);
      }
    }

    Debug.LogFormat("save to {0}", dataFile);
  }

  // Ignore any IO error
  void loadGame() {
    newGame();

    using (var reader = new BinaryReader(File.Open(dataFile, FileMode.Open))) {
      var gameReader = new GameDataReader(reader);

      int count = gameReader.ReadInt();
      for (int i=0; i<count; i++) {
        PersistableObject spawn = Instantiate(prefab);
        spawn.transform.localPosition = gameReader.ReadVector3();
        spawn.transform.localScale = gameReader.ReadVector3();
        spawn.transform.rotation = gameReader.ReadQuaternion();

        objects.Add(spawn);
      }
    }
  }
}
