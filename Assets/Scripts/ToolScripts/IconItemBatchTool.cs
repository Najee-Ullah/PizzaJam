using UnityEditor;
using UnityEngine;

public class IconItemBatchTool : MonoBehaviour
{
    [MenuItem("Tools/Generate Item Icons")]
    public static void GenerateAllIcons()
    {
        IconGenerator generator = Object.FindFirstObjectByType<IconGenerator>();

        if (generator == null)
        {
            Debug.LogError("No IconGenerator found in scene!");
            return;
        }

        string[] prefabPaths = AssetDatabase.FindAssets("t:ItemDataSO", new[] { "Assets/InventoryItemSOs" });
        foreach (string guid in prefabPaths)
        {
            string path = AssetDatabase.GUIDToAssetPath(guid);
            ItemDataSO itemSO = AssetDatabase.LoadAssetAtPath<ItemDataSO>(path);
            Sprite itemIcon = generator.GenerateIconForPrefab(itemSO.itemPrefab, itemSO.itemPrefab.name);
            itemSO.itemIcon = itemIcon;
            EditorUtility.SetDirty(itemSO);

        }
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();

        Debug.Log("Icons Generation Complete");
    }
}
