using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(FpsCounter))]
public class FpsDisplay : MonoBehaviour {
  [System.Serializable]
  private struct FpsColor {
    public Color color;
    public int minimumFps;
  }

  public Text averageFpsLabel;
  public Text highestFpsLabel;
  public Text lowestFpsLabel;
  [SerializeField]
  private FpsColor[] fpsColors;

  private FpsCounter counter;

  void Awake() {
    counter = GetComponent<FpsCounter>();
  }

  // Update is called once per frame
  void Update() {
    Display(averageFpsLabel, counter.FPSAverage);
    Display(highestFpsLabel, counter.FPSHighest);
    Display(lowestFpsLabel, counter.FPSLowest);
  }

  void Display(Text label, int fps) {
    label.text = Mathf.Clamp(fps, 0, 99).ToString();
    foreach(var v in fpsColors) {
      if (fps >= v.minimumFps) {
        label.color = v.color;
        break;
      }
    }
  }
}
