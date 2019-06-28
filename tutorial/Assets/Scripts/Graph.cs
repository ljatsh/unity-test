using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// TODO
// 1. Cannot assign x,y,z of localPosition
// 2. Attribute

public class Graph : MonoBehaviour {
  public Transform sample;

  public Transform root;

  [Range(10, 100)]
  public Int32 resolution = 10;

  private const Single showRangeX = 6.0f;

  void Awake() {
    Single step = showRangeX / resolution;
    Vector3 scale = Vector3.one * step;

    Vector3 position;
    for (int i=0; i<resolution; i++) {
      var tf = Instantiate(sample, root);
      position = Vector3.right * (-showRangeX/2 + step * i);
      position.y = position.x * position.x;
      tf.localPosition = position;
      tf.localScale = scale;
    }
  }

  // Start is called before the first frame update
  void Start() {
      
  }

  // Update is called once per frame
  void Update() {
      
  }
}
