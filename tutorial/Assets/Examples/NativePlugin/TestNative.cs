
using System.Runtime.InteropServices;
using UnityEngine;

public class TestNative : MonoBehaviour {
  
  [DllImport("native")]
  private static extern int sum(int a, int b);

  [DllImport("native")]
  private static extern int sub(int a, int b);

  [DllImport("native", CallingConvention = CallingConvention.FastCall)]
  private static extern int multiple(int a , int b);

  void Awake() {
    int ret = sum(1, 2);
    Debug.LogFormat("sum(1, 2) = {0}", ret);

    ret = sub(1, 2);
    Debug.LogFormat("sub(1, 2) = {0}", ret);

    ret = multiple(2, 4);
    Debug.LogFormat("multiple(2, 4) = {0}", ret);
  }
}
