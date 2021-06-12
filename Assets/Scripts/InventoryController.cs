using System.Linq;
using UnityEngine;

namespace KefirTest
{
    public class InventoryController : MonoBehaviour, IInventoryPresenter
    {
        [SerializeField] private int _leftSideMaxAmount = 10;
        [SerializeField] private int _rightSideMaxAmount = 10;

        [Space]
        [SerializeField] private InventoryVisualController _visualController;

        private IInventoryView _view;

        private string[] _itemNames;

        private InventoryModel _inventoryModel;

        public void MoveAction(InventorySide side, int slot)
        {
            if (side == InventorySide.Left)
            {
                if (slot >= _inventoryModel.LeftSideItems.Length) return;
                if (_inventoryModel.RightSideItems.Length == _rightSideMaxAmount) return;

                string itemName = _inventoryModel.LeftSideItems[slot];
                _inventoryModel.LeftSideItems = RemoveNameFromArray(_inventoryModel.LeftSideItems, slot);
                _inventoryModel.RightSideItems = AddNameToArray(_inventoryModel.RightSideItems, itemName);
            }
            else
            {
                if (slot >= _inventoryModel.RightSideItems.Length) return;
                if (_inventoryModel.LeftSideItems.Length == _leftSideMaxAmount) return;

                string itemName = _inventoryModel.RightSideItems[slot];
                _inventoryModel.RightSideItems = RemoveNameFromArray(_inventoryModel.RightSideItems, slot);
                _inventoryModel.LeftSideItems = AddNameToArray(_inventoryModel.LeftSideItems, itemName);
            }

            DrawInventory();
        }

        private string[] RemoveNameFromArray(string[] arr, int index)
        {
            string[] newArr = new string[arr.Length - 1];

            for (int i = 0; i < index; i++) newArr[i] = arr[i];
            for (int i = index + 1; i < arr.Length; i++) newArr[i - 1] = arr[i];

            return newArr;
        }

        private string[] AddNameToArray(string[] arr, string name)
        {
            string[] newArr = new string[arr.Length + 1];

            for (int i = 0; i < arr.Length; i++) newArr[i] = arr[i];
            newArr[arr.Length] = name;

            return newArr;
        }

        private void Awake()
        {
            _view = _visualController;

            _itemNames = Resources.LoadAll("Icons").Select(x => x.name).ToArray();

            _inventoryModel = new InventoryModel();
        }

        public void Start()
        {
            // For testing, this all can be technically loaded from the model but that wasn't part of my task :D
            int generatedLeftAmount = Random.Range(4, 8);
            int generatedRightAmount = Random.Range(2, 4);
            _inventoryModel.LeftSideItems = new string[generatedLeftAmount];
            _inventoryModel.RightSideItems = new string[generatedRightAmount];

            for (int i = 0; i < generatedLeftAmount; i++)
            {
                string itemName = _itemNames[Random.Range(0, _itemNames.Length)];
                _inventoryModel.LeftSideItems[i] = itemName;
            }

            for (int i = 0; i < generatedRightAmount; i++)
            {
                string itemName = _itemNames[Random.Range(0, _itemNames.Length)];
                _inventoryModel.RightSideItems[i] = itemName;
            }

            DrawInventory();
        }

        private void DrawInventory()
        {
            _view.ClearSlots();

            for (int i = 0; i < _inventoryModel.LeftSideItems.Length; i++)
            {
                _view.SetSlotInteractable(InventorySide.Left, i, true);
                _view.SetSlotItemSprite(InventorySide.Left, i, _inventoryModel.LeftSideItems[i]);
            }
            for (int i = 0; i < _inventoryModel.RightSideItems.Length; i++)
            {
                _view.SetSlotInteractable(InventorySide.Right, i, true);
                _view.SetSlotItemSprite(InventorySide.Right, i, _inventoryModel.RightSideItems[i]);
            }
        }
    }
}