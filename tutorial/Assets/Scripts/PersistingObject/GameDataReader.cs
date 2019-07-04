using System.IO;
using UnityEngine;

public class GameDataReader {
  BinaryReader reader;

  public GameDataReader(BinaryReader reader) {
    this.reader = reader;
  }

  public int ReadInt() {
    return reader.ReadInt32();
  }

  public float ReadFloat() {
    return reader.ReadSingle();
  }

  public Quaternion ReadQuaternion() {
    return new Quaternion(reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle());
  }

  public Vector3 ReadVector3() {
    Vector3 value;
    value.x = reader.ReadSingle();
    value.y = reader.ReadSingle();
    value.z = reader.ReadSingle();

    return value;
  }

  public Color ReadColor() {
    return new Color(reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle());
  }
}
