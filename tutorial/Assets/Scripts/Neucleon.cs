using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Neucleon : MonoBehaviour {
  public float attaractionForce;

  private Rigidbody body;

  void Awake() {
    body = gameObject.GetComponent<Rigidbody>();
  }

  // Start is called before the first frame update
  void Start() {
    
  }

  // Update is called once per frame
  void Update(){
      
  }

  void FixedUpdate() {
    body.AddForce(transform.localPosition * -attaractionForce);
  }
}
