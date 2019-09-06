
using UnityEditor;
using UnityEditor.Experimental.AssetImporters;
using UnityEngine;

[CustomEditor(typeof(CubeImporter))]
public class CubeImporterEditor: ScriptedImporterEditor
{
  public override void OnInspectorGUI()
  {
    Debug.Log("OnInspectorGUI was called");
    Debug.Assert(false);

    var colorShift = new GUIContent("Color Shift");
    var prop = serializedObject.FindProperty("m_ColorShift");
    EditorGUILayout.PropertyField(prop, colorShift);
    base.ApplyRevertGUI();
  }
}
