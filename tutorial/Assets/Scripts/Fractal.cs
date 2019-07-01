using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// TODO
// 1. yield and generics, coroutine
// 2. MonoBehavior life cycle, debug
// 3. How the hierarchy affects position, scale and rotation.
// 4. Overdraw
// 5. relation between rotation and clockwise

public class Fractal : MonoBehaviour {
  public Mesh mesh;
  public Material material;
  public int maxDepth;
  public float childScale;

  private int depth;

  private static Vector3[] childDirections = {
    Vector3.up,
    Vector3.right,
    Vector3.left,
    Vector3.forward,
    Vector3.back
  };

  private static Quaternion[] childOrientations = {
    Quaternion.identity,
    Quaternion.Euler(0f, 0f, -90f),
    Quaternion.Euler(0f, 0f, 90f),
    Quaternion.Euler(90f, 0f, 0f),
    Quaternion.Euler(-90f, 0f, 0f)
  };

  // Start is called before the first frame update
  void Start() {
    gameObject.AddComponent<MeshFilter>().mesh = mesh;
    gameObject.AddComponent<MeshRenderer>().material = material;

    if (depth < maxDepth) {
      StartCoroutine(CreateChildren());
    }
  }

  // Update is called once per frame
  void Update() {
      
  }

  void Initilaize(Fractal parent, int childIndex) {
    mesh = parent.mesh;
    material = parent.material;
    maxDepth = parent.maxDepth;
    depth = parent.depth + 1;
    childScale = parent.childScale;

    // build model hierarchy
    transform.parent = parent.transform;
    transform.localScale = Vector3.one * childScale;
    transform.localPosition = childDirections[childIndex] * (0.5f + 0.5f * childScale);
    transform.localRotation = childOrientations[childIndex];
  }

  IEnumerator CreateChildren() {
    for (int i=0; i<childDirections.Length; i++) {
      yield return new WaitForSeconds(0.5f);
      new GameObject("Fractal Child").AddComponent<Fractal>().Initilaize(this, i);
    }
  }
}
