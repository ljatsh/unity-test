
using UnityEngine;

[CreateAssetMenu]
public class ShapeFactory : ScriptableObject {
  [SerializeField]
  Shape[] prefabs;

  public Shape getShape(int shapeId) {
    Shape shape = Instantiate(prefabs[shapeId]);
    shape.ShapeId = shapeId;

    return shape;
  }

  public Shape getRandomShape() {
    return getShape(Random.Range(0, prefabs.Length));
  }
}
