
using UnityEngine;

[CreateAssetMenu]
public class ShapeFactory : ScriptableObject {
  [SerializeField]
  Shape[] prefabs;
  [SerializeField]
  Material[] materials;

  public Shape getShape(int shapeId = 0, int materialId = 0) {
    Shape shape = Instantiate(prefabs[shapeId]);
    shape.ShapeId = shapeId;
    shape.setMaterial(materials[materialId], materialId);

    return shape;
  }

  public Shape getRandomShape() {
    return getShape(Random.Range(0, prefabs.Length), Random.Range(0, materials.Length));
  }
}
