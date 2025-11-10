using System.Collections;
using UnityEngine;

[CreateAssetMenu(menuName = "Inventory Item Combine Recipe")]
public class CombineRecipeSO : ScriptableObject
{
    [Header("Required Items")]
    public ItemDataSO itemDataA;
    public ItemDataSO itemDataB;

    [Header("Resulting Item")]
    public ItemDataSO itemDataC;
}