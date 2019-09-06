
using System.Text;
using UnityEngine;
using UnityEditor;

public class AssetImporter
{
  [MenuItem("AssetDatabase/ImportExample")]
  static void ImportExample()
  {
    Debug.Log("ImportExample");
    AssetDatabase.ImportAsset("Assets/Texture/test.jpg", ImportAssetOptions.Default);
    Object obj = AssetDatabase.LoadAssetAtPath("Assets/Texture/test.jpg", typeof(Texture2D));
    Debug.Log(obj);
    Texture2D tex = obj as Texture2D;
    Debug.Log(tex);
  }

  [MenuItem("AssetDatabase/Dump")]
  static void Dump()
  {
    string[] rets = AssetDatabase.FindAssets("");
    StringBuilder sb = new StringBuilder();
    foreach(string guid in rets)
    {
      sb.Clear();

      string assetPath = AssetDatabase.GUIDToAssetPath(guid);
      sb.AppendFormat("{0} ---> {1}", guid, assetPath); sb.AppendLine();

      string[] dependencies = AssetDatabase.GetDependencies(assetPath, true);
      if (dependencies.Length > 0)
      {
        sb.AppendFormat("Dependencies({0}):", dependencies.Length); sb.AppendLine();
        foreach (string p in dependencies)
        {
          sb.AppendLine(p);
        }
      }

      System.Type type = AssetDatabase.GetMainAssetTypeAtPath(assetPath);
      sb.AppendFormat("MainType {0}", type.ToString()); sb.AppendLine();

      Debug.Log(sb.ToString());
    }
  }

  [MenuItem ("AssetDatabase/FileOperationsExample")]
  static void Example ()
  {
    string ret;
    
    // Create
    Material material = new Material (Shader.Find("Specular"));
    AssetDatabase.CreateAsset(material, "Assets/MyMaterial.mat");
    if(AssetDatabase.Contains(material))
        Debug.Log("Material asset created");
    
    // Rename
    ret = AssetDatabase.RenameAsset("Assets/MyMaterial.mat", "MyMaterialNew");
    if(ret == "")
        Debug.Log("Material asset renamed to MyMaterialNew");
    else
        Debug.Log(ret);
    
    // Create a Folder
    ret = AssetDatabase.CreateFolder("Assets", "NewFolder");
    if(AssetDatabase.GUIDToAssetPath(ret) != "")
        Debug.Log("Folder asset created");
    else
        Debug.Log("Couldn't find the GUID for the path");
    
    // Move
    ret = AssetDatabase.MoveAsset(AssetDatabase.GetAssetPath(material), "Assets/NewFolder/MyMaterialNew.mat");
    if(ret == "")
        Debug.Log("Material asset moved to NewFolder/MyMaterialNew.mat");
    else
        Debug.Log(ret);
    
    // Copy
    if(AssetDatabase.CopyAsset(AssetDatabase.GetAssetPath(material), "Assets/MyMaterialNew.mat"))
        Debug.Log("Material asset copied as Assets/MyMaterialNew.mat");
    else
        Debug.Log("Couldn't copy the material");
    // Manually refresh the Database to inform of a change
    AssetDatabase.Refresh();
    Material MaterialCopy = AssetDatabase.LoadAssetAtPath("Assets/MyMaterialNew.mat", typeof(Material)) as Material;
    
    // Move to Trash
    if(AssetDatabase.MoveAssetToTrash(AssetDatabase.GetAssetPath(MaterialCopy)))
        Debug.Log("MaterialCopy asset moved to trash");
    
    // Delete
    if(AssetDatabase.DeleteAsset(AssetDatabase.GetAssetPath(material)))
        Debug.Log("Material asset deleted");
    if(AssetDatabase.DeleteAsset("Assets/NewFolder"))
        Debug.Log("NewFolder deleted");
    
    // Refresh the AssetDatabase after all the changes
    AssetDatabase.Refresh();
  }
}
