using UnityEditor;
using System.IO;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public static class MaterialSaverEditor 
{
    [MenuItem("CONTEXT/Material/Save Material Texture As A New Instance")]
	public static void SaveMeshNewInstanceItem(MenuCommand menuCommand)
	{
		//MapGeneration mapGeneration = GameObject.FindObjectOfType<MapGeneration>();
		Material mr = menuCommand.context as Material;
		Texture2D texture = mr.mainTexture as Texture2D;
		SaveTexture(texture);
		//SaveMaterial(mr, mr.name);
	}

	public static void SaveMaterial(Material material, string name)
	{
		string path = EditorUtility.SaveFilePanel("Save Separate Material Asset", "Assets/", name, "mat");
		if (string.IsNullOrEmpty(path)) return;

		path = FileUtil.GetProjectRelativePath(path);

		Material materialToSave = Object.Instantiate(material);
		materialToSave.mainTexture = material.mainTexture;
		//MapData mapData = map.GenerateMapData(Vector2.zero);
		//materialToSave.SetTexture("placeholder",TextureGenerator.TextureFromColorMap(mapData.colorMap, MapGeneration.mapChunkSize, MapGeneration.mapChunkSize));

		

		AssetDatabase.CreateAsset(materialToSave, path);

		//materialToSave = (Material)AssetDatabase.LoadAssetAtPath(path, typeof(Material));


		AssetDatabase.SaveAssets();
	}

	public static void SaveTexture(Texture2D texture)
	{
		byte[] bytes = texture.EncodeToPNG();
		var dirPath = Application.dataPath + "/RenderOutput";
		if (!System.IO.Directory.Exists(dirPath))
		{
			System.IO.Directory.CreateDirectory(dirPath);
		}
		System.IO.File.WriteAllBytes(dirPath + "/R_" + Random.Range(0, 100000) + ".png", bytes);
		Debug.Log(bytes.Length / 1024 + "Kb was saved as: " + dirPath);
#if UNITY_EDITOR
		UnityEditor.AssetDatabase.Refresh();
#endif
	}
}
