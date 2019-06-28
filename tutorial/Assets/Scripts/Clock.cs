using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clock : MonoBehaviour {
  public Transform hourArm;
  public Transform minuteArm;
  public Transform secondArm;

  public Boolean isUpdateContinous;

  // Start is called before the first frame update
  void Start() {
    
  }

  // Update is called once per frame
  void Update()
  {
    if (isUpdateContinous)
      updateContinous();
    else
      updateDiscrete();
  }

  void updateDiscrete() {
    DateTime now = DateTime.Now;
    hourArm.rotation = Quaternion.Euler(0f, (now.Hour + now.Minute / 60.0f) * 30.0f, 0f);
    minuteArm.rotation = Quaternion.Euler(0f, now.Minute * 6.0f, 0f);
    secondArm.rotation = Quaternion.Euler(0f, now.Second * 6.0f, 0f);
  }

  void updateContinous() {
    TimeSpan span = DateTime.Now.TimeOfDay;
    hourArm.rotation = Quaternion.Euler(0f, (float)(span.TotalHours * 30.0f), 0f);
    minuteArm.rotation = Quaternion.Euler(0f, (float)(span.TotalMinutes * 6.0f), 0f);
    secondArm.rotation = Quaternion.Euler(0f, (float)(span.TotalSeconds * 6.0f), 0f);
  }
}
