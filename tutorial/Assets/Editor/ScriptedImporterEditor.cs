
using UnityEditor;
using UnityEditor.Experimental.AssetImporters;
using UnityEngine;

[CustomEditor(typeof(CubeImporter))]
public class CubeImporterEditor: ScriptedImporterEditor
{
  public override void OnInspectorGUI()
  {
    var colorShift = new GUIContent("Color Shift");
    //var prop = serializedObject.FindProperty("m_ColorShift");
    var prop = serializedObject.GetIterator();
    EditorGUILayout.PropertyField(prop, colorShift);
    base.ApplyRevertGUI();

    // SerializedProperty sp = serializedObject.GetIterator();
    // Debug.Log(sp.displayName);
  }
}
