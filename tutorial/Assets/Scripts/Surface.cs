using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Surface : MonoBehaviour {
  public Transform sample;

  public Transform root;

  [Range(10, 100)]
  public Int32 resolution = 10;

  public enum FunctionName {
    Sine,
    MultiSine
  };

  public FunctionName currentFunction = FunctionName.Sine;

  private const Single showRangeX = 6.0f;
  private Transform[] points;

  private delegate float MappingFunction(float x, float z, float t);
  private MappingFunction[] functions = new MappingFunction[] {
    SineFunction,
    MultiSineFunction
  };

  void Awake() {
    points = new Transform[resolution * resolution];

    Single step = showRangeX / resolution;
    Vector3 scale = Vector3.one * step;

    Vector3 position;
    float z;
    for (int i=0; i<resolution; i++) {
      z = -showRangeX/2 + step * i;

      for (int j=0; j<resolution; j++) {
        var cp = Instantiate(sample, root);
        position = Vector3.right * (-showRangeX/2 + step * j);
        position.z = z;
        cp.localPosition = position;
        cp.localScale = scale;

        points[i * resolution + j] = cp;
      }
    }
  }

  // Start is called before the first frame update
  void Start() {
  }

  // Update is called once per frame
  void Update() {
    Vector3 point;
    var mapping = functions[(int)currentFunction];
    if (mapping == null)
      return;

    foreach (var cp in points) {
      point = cp.localPosition;
      point.y = mapping(point.x, point.z, Time.time);
      cp.localPosition = point;
    }
  }

  static float SineFunction (float x, float z, float t) {
    return Mathf.Sin(Mathf.PI * (x + t));
  }

  static float MultiSineFunction (float x, float z, float t) {
    float y = Mathf.Sin(Mathf.PI * (x + t));
    y += Mathf.Sin(2f * Mathf.PI * (x + 2f * t)) / 2f;
    y *= 2f / 3f;
    return y;
  }
}
