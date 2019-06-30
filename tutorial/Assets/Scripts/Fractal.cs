﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// TODO
// 1. yield and generics
// 2. MonoBehavior life cycle, debug
// 3. How the hierarchy affects position, scale and rotation.

public class Fractal : MonoBehaviour {
  public Mesh mesh;
  public Material material;
  public int maxDepth;
  public float childScale;

  private int depth;

  // Start is called before the first frame update
  void Start() {
    gameObject.AddComponent<MeshFilter>().mesh = mesh;
    gameObject.AddComponent<MeshRenderer>().material = material;

    if (depth < maxDepth) {
      new GameObject("Fractal Child").AddComponent<Fractal>().Initilaize(this);
    }
  }

  // Update is called once per frame
  void Update() {
      
  }

  void Initilaize(Fractal parent) {
    mesh = parent.mesh;
    material = parent.material;
    maxDepth = parent.maxDepth;
    depth = parent.depth + 1;
    childScale = parent.childScale;

    // build model hierarchy
    transform.parent = parent.transform;
    transform.localScale = Vector3.one * childScale;
    transform.localPosition = Vector3.up * (0.5f + 0.5f * childScale);
  }
}