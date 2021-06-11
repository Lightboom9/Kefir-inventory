using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace KefirTest
{
    public class InventoryController : MonoBehaviour, IInventoryView
    {
        [SerializeField] private Transform _leftSideInventoryTransform;
        [SerializeField] private Transform _rightSideInventoryTransform;
        [SerializeField] private Button _moveButton;

        [Space] [SerializeField] private Sprite _slotSprite;
        [SerializeField] private Sprite _slotOverSprite;

        [Space] [SerializeField] private int _leftSideMaxAmount = 10;
        [SerializeField] private int _rightSideMaxAmount = 10;

        private Transform[] _leftSideSlots;
        private Transform[] _rightSideSlots;
        private Image[] _leftSideSlotImages;
        private Image[] _rightSideSlotImages;
        private Image[] _leftSideSlotItemImages;
        private Image[] _rightSideSlotItemImages;

        private Sprite[] _itemIcons;

        private int _leftAmount;
        private int _rightAmount;

        private InventorySide _selectedItemInventorySide;
        private int _selectedItemIndex = -1;

        private void Awake()
        {
            int leftCount = Mathf.Min(_leftSideInventoryTransform.childCount, _leftSideMaxAmount);
            _leftSideSlots = new Transform[leftCount];
            _leftSideSlotImages = new Image[leftCount];
            _leftSideSlotItemImages = new Image[leftCount];
            for (int i = 0; i < leftCount; i++)
            {
                _leftSideSlots[i] = _leftSideInventoryTransform.GetChild(i);
                _leftSideSlotImages[i] = _leftSideSlots[i].GetComponent<Image>();
                _leftSideSlotItemImages[i] = _leftSideSlots[i].GetChild(0).GetComponent<Image>();

                int slot = i;
                _leftSideSlots[i].GetComponent<Button>().onClick.AddListener(() =>
                {
                    SelectItem(InventorySide.Left, slot);
                });
            }

            int rightCount = Mathf.Min(_rightSideInventoryTransform.childCount, _rightSideMaxAmount);
            _rightSideSlots = new Transform[rightCount];
            _rightSideSlotImages = new Image[rightCount];
            _rightSideSlotItemImages = new Image[rightCount];
            for (int i = 0; i < rightCount; i++)
            {
                _rightSideSlots[i] = _rightSideInventoryTransform.GetChild(i);
                _rightSideSlotImages[i] = _rightSideSlots[i].GetComponent<Image>();
                _rightSideSlotItemImages[i] = _rightSideSlots[i].GetChild(0).GetComponent<Image>();

                int slot = i;
                _rightSideSlots[i].GetComponent<Button>().onClick.AddListener(() =>
                {
                    SelectItem(InventorySide.Right, slot);
                });
            }

            _itemIcons = Resources.LoadAll<Sprite>("Icons");

            int generatedLeftAmount = _leftAmount = Random.Range(4, 8);
            int generatedRightAmount = _rightAmount = Random.Range(2, 4);
            for (int i = 0; i < generatedLeftAmount; i++)
            {
                _leftSideSlotItemImages[i].color = Color.white;
                _leftSideSlotItemImages[i].sprite = _itemIcons[Random.Range(0, _itemIcons.Length)];
            }

            for (int i = 0; i < generatedRightAmount; i++)
            {
                _rightSideSlotItemImages[i].color = Color.white;
                _rightSideSlotItemImages[i].sprite = _itemIcons[Random.Range(0, _itemIcons.Length)];
            }
        }

        private void SelectItem(InventorySide side, int slot)
        {
            Debug.Log(side.ToString() + ", " + slot);

            if (_selectedItemIndex == slot)
            {
                if (side == InventorySide.Left) _leftSideSlotImages[slot].sprite = _slotSprite;
                else _rightSideSlotImages[slot].sprite = _slotSprite;

                _selectedItemIndex = -1;

                return;
            }

            if (_selectedItemIndex != -1)
            {
                if (side == InventorySide.Left) _leftSideSlotImages[_selectedItemIndex].sprite = _slotSprite;
                else _rightSideSlotImages[_selectedItemIndex].sprite = _slotSprite;
            }

            if (side == InventorySide.Left) _leftSideSlotImages[slot].sprite = _slotOverSprite;
            else _rightSideSlotImages[slot].sprite = _slotOverSprite;

            _selectedItemIndex = slot;
        }

        public void SwapSlots(int leftSlot, int rightSlots)
        {
            
        }

        public void SetSlotItemSprite(InventorySide side, int slot, Sprite sprite)
        {
            
        }
    }

    public enum InventorySide
    {
        Left, Right
    }
}
