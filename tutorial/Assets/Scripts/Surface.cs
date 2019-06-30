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
    MultiSine,
    Sine2D,
    MultiSine2D,
    Ripple,
    Cylinder,
    Sphere,
    Torous
  };

  public FunctionName currentFunction = FunctionName.Sine;

  private const Single showRange = 2.0f;
  private Transform[] points;

  private delegate Vector3 MappingFunction(float u, float v, float t);
  private MappingFunction[] functions = new MappingFunction[] {
    SineFunction,
    MultiSineFunction,
    Sine2DFunction,
    MultiSine2DFunction,
    RippleFunction,
    CylinderFunction,
    SphereFunction,
    TorusFunction
  };

  private static float pi = Mathf.PI;

  void Awake() {
    points = new Transform[resolution * resolution];

    Single step = showRange / resolution;
    Vector3 scale = Vector3.one * step;

    for (int i=0; i<resolution; i++) {
      for (int j=0; j<resolution; j++) {
        var cp = Instantiate(sample, root);
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
    var mapping = functions[(int)currentFunction];
    if (mapping == null)
      return;

    float step = showRange * 1.0f / resolution;
    float t = Time.time;
    for (int i=0; i<resolution; i++) {
      float u = -showRange * 0.5f + step * i;

      for (int j=0; j<resolution; j++) {
        float v = -showRange * 0.5f + step * j;
        points[i * resolution + j].localPosition = mapping(u, v, t);
      }
    }
  }

  static Vector3 SineFunction(float x, float z, float t) {
    return new Vector3(x, Mathf.Sin(pi * (x + t)), z);
  }

  static Vector3 MultiSineFunction(float x, float z, float t) {
    float y = Mathf.Sin(pi * (x + t));
    y += Mathf.Sin(2f * pi * (x + 2f * t)) / 2f;
    y *= 2f / 3f;
    return new Vector3(x, y, z);
  }

  static Vector3 Sine2DFunction(float x, float z, float t) {
    float y = Mathf.Sin(pi * (x + t));
    y += Mathf.Sin(x * (z + t));
    y *= 0.5f;
    return new Vector3(x, y, z);
  }

  static Vector3 MultiSine2DFunction(float x, float z, float t) {
    float y = 4f * Mathf.Sin(pi * (x + z + t * 0.5f));
    y += Mathf.Sin(pi * (x + t));
    y += Mathf.Sin(2f * pi * (z + 2f * t)) * 0.5f;
    y *= 1f / 5.5f;
    return new Vector3(x, y, z);
  }

  static Vector3 RippleFunction (float x, float z, float t) {
    float d = Mathf.Sqrt(x * x + z * z);
    float y = Mathf.Sin(pi * (4f * d - t));
    return new Vector3(x, y, z);
  }

  static Vector3 CylinderFunction(float u, float v, float t) {
    //float radius = 1f; // Cylinder
    // float radius = 1f + Mathf.Sin(6f * pi * u) * 0.2f; // Wobby Cyliner
    //float radius = 1f + Mathf.Sin(2f * pi * v) * 0.2f;
    float radius = 0.8f + Mathf.Sin(pi * (6f * u + 2f * v + t)) * 0.2f; // Twisting Cylinder
    return new Vector3(radius * Mathf.Sin(pi * u), v, radius * Mathf.Cos(pi * u));
  }

  static Vector3 SphereFunction(float u, float v, float t) {
    float r = Mathf.Cos(pi * 0.5f * v);
    return new Vector3(r * Mathf.Sin(pi * u), Mathf.Sin(pi * 0.5f * v), r * Mathf.Cos(pi * u));
  }

  static Vector3 TorusFunction(float u, float v, float t) {
    float r1 = 1f;
    float r2 = 0.5f;
    float s = r2 * Mathf.Cos(pi * v) + r1;

    return new Vector3(s * Mathf.Sin(pi * u), r2 * Mathf.Sin(pi * v), s * Mathf.Cos(pi * u));
  }
}
