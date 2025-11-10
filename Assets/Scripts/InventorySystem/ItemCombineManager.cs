using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCombineManager : MonoBehaviour
{

    [SerializeField] private List<CombineRecipeSO> combineRecipeSOs = new List<CombineRecipeSO>();

    public static ItemCombineManager Instance;

    private ItemDataSO firstItemSelected;

    private void Awake()
    {
        if(Instance!= null)
        {
            Destroy(Instance);
        }
        Instance = this;
    }

    public bool IsActive()
    {
        return firstItemSelected != null;
    }

    public void ResetCombine()
    {
        firstItemSelected = null;
    }

    public ItemDataSO GetFirstItem()
    {
        return firstItemSelected;
    }

    public void CombineStart(ItemDataSO firstItem)
    {
        ResetCombine();
        firstItemSelected = firstItem;
        Debug.Log("Combine Start");
    }

    public ItemDataSO CombineEnd(ItemDataSO item)
    {
        Debug.Log("Combine End");
        if (firstItemSelected != null && item != firstItemSelected)
        {
            return TryCombine(firstItemSelected, item);
        }
        return item;
    }

    public ItemDataSO TryCombine(ItemDataSO itemData1, ItemDataSO itemData2)
    {
        bool validRecipeA = false;
        bool validRecipeB = false;
        foreach (var combineRecipe in combineRecipeSOs)
        {
            validRecipeA = (combineRecipe.itemDataA == itemData1 && combineRecipe.itemDataB == itemData2);
            validRecipeB = (combineRecipe.itemDataA == itemData2 && combineRecipe.itemDataB == itemData1);
            if (validRecipeA || validRecipeB)
            {
                ResetCombine();
                return combineRecipe.itemDataC;
            }
        }
        ResetCombine();
        return itemData2;
    }
    
}