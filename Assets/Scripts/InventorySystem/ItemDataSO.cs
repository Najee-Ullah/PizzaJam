using UnityEngine;

[CreateAssetMenu(menuName ="Inventory Item")]
public class ItemDataSO : ScriptableObject
{
    public int itemId;
    public string itemName;
    public string itemDescription;
    public Sprite itemIcon;
    public GameObject itemPrefab;
}