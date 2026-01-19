using System.Collections.Generic;
using UnityEngine;

public class CraftingSystem : MonoBehaviour
{
    // List of Craftable Items
    public Dictionary<string, Recipe> recipes = new Dictionary<string, Recipe>();

    void Start()
    {
        InitializeRecipes();
    }

    void InitializeRecipes()
    {
        // Add recipes to the crafting system
        recipes.Add("WoodenSword", new Recipe(new List<string> { "Wood", "Wood" }, 1));
        // Add more recipes as needed
    }

    public bool CraftItem(string itemName)
    {
        if (recipes.ContainsKey(itemName))
        {
            Recipe recipe = recipes[itemName];

            // Check if the player has enough materials
            if (HasRequiredMaterials(recipe))
            {
                ConsumeMaterials(recipe);
                Debug.Log("Crafted: " + itemName);
                return true;
            }
            else
            {
                Debug.Log("Not enough materials to craft: " + itemName);
            }
        }

        return false;
    }

    private bool HasRequiredMaterials(Recipe recipe)
    {
        // Check player's inventory for required materials
        // This is a placeholder. Implement your inventory check logic here.
        return true;
    }

    private void ConsumeMaterials(Recipe recipe)
    {
        // Remove the required materials from the inventory
        // This is a placeholder. Implement your inventory consumption logic here.
    }
}

[System.Serializable]
public class Recipe
{
    public List<string> requiredItems;
    public int outputAmount;

    public Recipe(List<string> requiredItems, int outputAmount)
    {
        this.requiredItems = requiredItems;
        this.outputAmount = outputAmount;
    }
}