using UnityEngine;

namespace KefirTest
{
    public interface IInventoryView
    {
        void SetSlotItemSprite(InventorySide side, int slot, string spriteName);
        void SetSlotInteractable(InventorySide side, int slot, bool interactable);
        void ClearSlots();
    }
}