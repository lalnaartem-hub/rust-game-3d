using UnityEngine;
using System.Collections.Generic;

namespace GameSystems {
    public class Inventory : MonoBehaviour {
        public class InventorySlot {
            public string itemId;
            public int quantity;
            public ItemData itemData;

            public InventorySlot(string id, int qty, ItemData data) {
                itemId = id;
                quantity = qty;
                itemData = data;
            }
        }

        [SerializeField] private int maxSlots = 30;
        private List<InventorySlot> slots = new List<InventorySlot>();
        private int totalWeight = 0;
        private int maxWeight = 100;

        private void Awake() {
            InitializeInventory();
        }

        private void InitializeInventory() {
            for (int i = 0; i < maxSlots; i++) {
                slots.Add(null);
            }
        }

        public bool AddItem(ItemData itemData, int quantity = 1) {
            for (int i = 0; i < slots.Count; i++) {
                if (slots[i] != null && slots[i].itemId == itemData.id) {
                    if (slots[i].itemData.stackable) {
                        int newWeight = totalWeight + (itemData.weight * quantity);
                        if (newWeight <= maxWeight) {
                            slots[i].quantity += quantity;
                            totalWeight = newWeight;
                            return true;
                        }
                        return false;
                    }
                }
            }

            for (int i = 0; i < slots.Count; i++) {
                if (slots[i] == null) {
                    int newWeight = totalWeight + (itemData.weight * quantity);
                    if (newWeight <= maxWeight) {
                        slots[i] = new InventorySlot(itemData.id, quantity, itemData);
                        totalWeight = newWeight;
                        return true;
                    }
                    return false;
                }
            }
            return false;
        }

        public bool RemoveItem(string itemId, int quantity = 1) {
            for (int i = 0; i < slots.Count; i++) {
                if (slots[i] != null && slots[i].itemId == itemId) {
                    slots[i].quantity -= quantity;
                    totalWeight -= slots[i].itemData.weight * quantity;
                    if (slots[i].quantity <= 0) {
                        slots[i] = null;
                    }
                    return true;
                }
            }
            return false;
        }

        public int GetItemQuantity(string itemId) {
            for (int i = 0; i < slots.Count; i++) {
                if (slots[i] != null && slots[i].itemId == itemId) {
                    return slots[i].quantity;
                }
            }
            return 0;
        }

        public void ClearInventory() {
            slots.Clear();
            totalWeight = 0;
            InitializeInventory();
        }

        public float GetWeightPercentage() {
            return (float)totalWeight / maxWeight;
        }

        public bool IsFull() {
            return totalWeight >= maxWeight;
        }

        public List<InventorySlot> GetSlots() => slots;

        public int TotalWeight => totalWeight;
        public int MaxWeight => maxWeight;

        public int UsedSlots {
            get {
                int count = 0;
                foreach (var slot in slots) {
                    if (slot != null) count++;
                }
                return count;
            }
        }
    }

    [CreateAssetMenu(fileName = "Item_", menuName = "Game/Item")]
    public class ItemData : ScriptableObject {
        public string id;
        public string displayName;
        public string description;
        public int weight;
        public bool stackable;
        public int maxStack;
        public Sprite icon;
        public GameObject worldModel;
        public ItemRarity rarity;
    }

    public enum ItemRarity {
        Common,
        Uncommon,
        Rare,
        Epic,
        Legendary
    }
}