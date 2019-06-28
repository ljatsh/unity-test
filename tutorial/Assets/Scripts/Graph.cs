using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Graph : MonoBehaviour {
  public Transform sample;

  void Awake() {
    for (int i=1; i<=10; i++) {
      var tf = Instantiate(sample);
      tf.localPosition =  Vector3.right * i;
      tf.localScale = Vector3.one / 5.0f;
    }
  }

  // Start is called before the first frame update
  void Start() {
      
  }

  // Update is called once per frame
  void Update() {
      
  }
}
