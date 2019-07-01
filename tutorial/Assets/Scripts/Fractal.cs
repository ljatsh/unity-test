using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// TODO
// 1. yield and generics, coroutine
// 2. MonoBehavior life cycle, debug
// 3. How the hierarchy affects position, scale and rotation.
// 4. Overdraw
// 5. relation between rotation and clockwise
// 6. dynamic batching
//    https://docs.unity3d.com/Manual/DrawCallBatching.html
//    https://docs.unity3d.com/Manual/FrameDebugger.html
//    https://docs.unity3d.com/Manual/GPUInstancing.html
// 7. Transform.Rotate mechanism

public class Fractal : MonoBehaviour {
  public Mesh[] meshes;
  public Material material;
  public int maxDepth;
  public float childScale;
  public float spawnProbability;
  public float maxRotationSpeed;
  public float maxTwist;

  private int depth;
  private Material[,] materials;

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

  private float rotationSpeed;

  // Start is called before the first frame update
  void Start() {
    if (materials == null)
      InitilaizeMaterials();

    rotationSpeed = Random.Range(-maxRotationSpeed, maxRotationSpeed);
    transform.Rotate(Random.Range(-maxTwist, maxTwist), 0f, 0f);

    gameObject.AddComponent<MeshFilter>().mesh = meshes[Random.Range(0, meshes.Length)];
    gameObject.AddComponent<MeshRenderer>().material = material;
    gameObject.GetComponent<MeshRenderer>().material = materials[depth, Random.Range(0, 2)];

    if (depth < maxDepth) {
      StartCoroutine(CreateChildren());
    }
  }

  // Update is called once per frame
  void Update() {
    transform.Rotate(0f, rotationSpeed * Time.deltaTime, 0f);
  }

  void Initilaize(Fractal parent, int childIndex) {
    meshes = parent.meshes;
    material = parent.material;
    maxDepth = parent.maxDepth;
    depth = parent.depth + 1;
    childScale = parent.childScale;
    spawnProbability = parent.spawnProbability;
    maxRotationSpeed = parent.maxRotationSpeed;
    maxTwist = parent.maxTwist;
    materials = parent.materials;

    // build model hierarchy
    transform.parent = parent.transform;
    transform.localScale = Vector3.one * childScale;
    transform.localPosition = childDirections[childIndex] * (0.5f + 0.5f * childScale);
    transform.localRotation = childOrientations[childIndex];
  }

  IEnumerator CreateChildren() {
    for (int i=0; i<childDirections.Length; i++) {
      if (Random.value < spawnProbability) {
        yield return new WaitForSeconds(0.5f);
        new GameObject("Fractal Child").AddComponent<Fractal>().Initilaize(this, i);
      }
    }
  }

  void InitilaizeMaterials() {
    materials = new Material[maxDepth + 1, 2];
    for (int i = 0; i <= maxDepth; i++) {
      float t = i / (maxDepth - 1f);
      t *= t;
      materials[i, 0] = new Material(material);
      materials[i, 0].color = Color.Lerp(Color.white, Color.yellow, t);
      materials[i, 1] = new Material(material);
      materials[i, 1].color = Color.Lerp(Color.white, Color.cyan, t);
    }
    materials[maxDepth, 0].color = Color.magenta;
    materials[maxDepth, 1].color = Color.red;
  }
}
