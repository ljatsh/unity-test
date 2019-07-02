using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FpsCounter : MonoBehaviour {
  public int FPSAverage { get; private set; }
  public int FPSHighest { get; private set; }
  public int FPSLowest { get; private set; }

  public int frameRange;
  
  private int[] fpsBuffer;
  private int fpsBufferIndex;

  void Awake() {
    FPSLowest = 100;
    fpsBuffer = new int[frameRange];
  }

  // Update is called once per frame
  void Update()
  {
    int fps = (int)(1f / Time.unscaledDeltaTime);

    fpsBuffer[fpsBufferIndex++] = (int)(1f / Time.unscaledDeltaTime);
    if (fpsBufferIndex >= frameRange) {
      fpsBufferIndex = 0;
    }

    int sum = 0;
    int highest = 0;
    int lowest = Int32.MaxValue;
    foreach (var v in fpsBuffer) {
      sum += v;

      if (fps > highest) {
        highest = fps;
      }
      if (fps < lowest) {
        lowest = fps;
      }
    }
    FPSAverage = sum / frameRange;
    FPSHighest = highest;
    FPSLowest = lowest;
  }
}
