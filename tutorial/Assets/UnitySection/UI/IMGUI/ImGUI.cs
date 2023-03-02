
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImGUI : MonoBehaviour {

  private readonly string[] _menu = {"File", "Edit", "Help"};
  private int _menuSelection = 0;

  void OnGUI() {

    _menuSelection = GUI.SelectionGrid(new Rect(0, 0, 100, 30), _menuSelection, _menu, 3);
  }
}
