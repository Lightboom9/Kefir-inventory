using UnityEngine;

namespace KefirTest
{
    public interface IInventoryView
    {
        void SwapSlots(int leftSlot, int rightSlot);
        void SetSlotItemSprite(InventorySide side, int slot, Sprite sprite);
    }
}