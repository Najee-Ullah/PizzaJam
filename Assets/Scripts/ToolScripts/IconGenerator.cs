using UnityEngine;
using UnityEditor;
using System.IO;

public class IconGenerator : MonoBehaviour
{
#if UNITY_EDITOR
    [Header("Icon Generation Settings")]
    public Camera renderCamera;
    public int iconResolution = 256;
    public Color backgroundColor = new Color(0, 0, 0,0);
    public string savePath = "Assets/GeneratedIcons/";

    public Sprite GenerateIconForPrefab(GameObject prefab, string iconName)
    {
        if (renderCamera == null)
        {
            Debug.LogError("Render camera not assigned!");
            return null;
        }

        // Create temporary object
        GameObject instance = Instantiate(prefab);
        instance.transform.position = Vector3.zero;

        // Set up camera
        renderCamera.clearFlags = CameraClearFlags.SolidColor;
        ////RenderSettings.skybox = null;
        renderCamera.backgroundColor = new Color(0, 0, 0, 0);

        // Create a temporary RenderTexture
        RenderTexture rt = new RenderTexture(iconResolution, iconResolution, 16, RenderTextureFormat.ARGB32);
        renderCamera.targetTexture = rt;
        renderCamera.Render();

        // Convert to Texture2D
        RenderTexture.active = rt;
        Texture2D tex = new Texture2D(iconResolution, iconResolution, TextureFormat.RGBA32, false);
        tex.ReadPixels(new Rect(0, 0, iconResolution, iconResolution), 0, 0);
        tex.Apply();

        // Save as PNG
        if (!Directory.Exists(savePath))
            Directory.CreateDirectory(savePath);

        string fullPath = Path.Combine(savePath, iconName + ".png");
        File.WriteAllBytes(fullPath, tex.EncodeToPNG());

        // Import as sprite
        AssetDatabase.Refresh();
        TextureImporter importer = (TextureImporter)AssetImporter.GetAtPath(fullPath);
        importer.textureType = TextureImporterType.Sprite;
        importer.spriteImportMode = SpriteImportMode.Single;
        importer.SaveAndReimport();

        // Load the sprite asset and return
        Sprite generatedSprite = AssetDatabase.LoadAssetAtPath<Sprite>(fullPath);

        // Cleanup
        renderCamera.targetTexture = null;
        RenderTexture.active = null;
        DestroyImmediate(rt);
        DestroyImmediate(instance);

        Debug.Log($"Icon saved at:{fullPath}");
        return generatedSprite;
    }
#endif

}
