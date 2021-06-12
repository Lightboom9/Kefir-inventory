using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace KefirTest
{
    public class InventoryVisualController : MonoBehaviour, IInventoryView
    {
        [SerializeField] private Transform _leftSideInventoryTransform;
        [SerializeField] private Transform _rightSideInventoryTransform;
        [SerializeField] private Button _moveButton;

        [Space] [SerializeField] private Sprite _slotSprite;
        [SerializeField] private Sprite _slotOverSprite;    

        [Space] [SerializeField] private int _leftSideMaxAmount = 10;
        [SerializeField] private int _rightSideMaxAmount = 10;

        [Space]
        [SerializeField] private InventoryController _inventoryController;

        private IInventoryPresenter _presenter;

        private Transform[] _leftSideSlots;
        private Transform[] _rightSideSlots;
        private Image[] _leftSideSlotImages;
        private Image[] _rightSideSlotImages;
        private Button[] _leftSideSlotButtons;
        private Button[] _rightSideSlotButtons;
        private Image[] _leftSideSlotItemImages;
        private Image[] _rightSideSlotItemImages;

        private Sprite[] _itemIcons;

        private InventorySide _selectedItemInventorySide;
        private int _selectedItemIndex = -1;

        private void Awake()
        {
            _presenter = _inventoryController;
            _moveButton.onClick.AddListener(OnMoveButtonClick);

            int leftCount = Mathf.Min(_leftSideInventoryTransform.childCount, _leftSideMaxAmount);
            _leftSideSlots = new Transform[leftCount];
            _leftSideSlotImages = new Image[leftCount];
            _leftSideSlotButtons = new Button[leftCount];
            _leftSideSlotItemImages = new Image[leftCount];
            for (int i = 0; i < leftCount; i++)
            {
                _leftSideSlots[i] = _leftSideInventoryTransform.GetChild(i);
                _leftSideSlotImages[i] = _leftSideSlots[i].GetComponent<Image>();
                _leftSideSlotButtons[i] = _leftSideSlots[i].GetComponent<Button>();
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
            _rightSideSlotButtons = new Button[rightCount];
            _rightSideSlotItemImages = new Image[rightCount];
            for (int i = 0; i < rightCount; i++)
            {
                _rightSideSlots[i] = _rightSideInventoryTransform.GetChild(i);
                _rightSideSlotImages[i] = _rightSideSlots[i].GetComponent<Image>();
                _rightSideSlotButtons[i] = _rightSideSlots[i].GetComponent<Button>();
                _rightSideSlotItemImages[i] = _rightSideSlots[i].GetChild(0).GetComponent<Image>();

                int slot = i;
                _rightSideSlots[i].GetComponent<Button>().onClick.AddListener(() =>
                {
                    SelectItem(InventorySide.Right, slot);
                });
            }

            _itemIcons = Resources.LoadAll<Sprite>("Icons");
            
        }

        private void OnMoveButtonClick()
        {
            if (_selectedItemIndex == -1) return;

            _presenter.MoveAction(_selectedItemInventorySide, _selectedItemIndex);
        }

        private void SelectItem(InventorySide side, int slot)
        {
            if (_selectedItemIndex == slot)
            {
                if (side == InventorySide.Left) _leftSideSlotImages[slot].sprite = _slotSprite;
                else _rightSideSlotImages[slot].sprite = _slotSprite;

                _selectedItemIndex = -1;

                return;
            }

            if (_selectedItemIndex != -1) RemoveVisualSelection(_selectedItemInventorySide, _selectedItemIndex);

            if (side == InventorySide.Left) _leftSideSlotImages[slot].sprite = _slotOverSprite;
            else _rightSideSlotImages[slot].sprite = _slotOverSprite;

            _selectedItemInventorySide = side;
            _selectedItemIndex = slot;
        }

        private void RemoveVisualSelection(InventorySide side, int slot)
        {
            if (side == InventorySide.Left) _leftSideSlotImages[_selectedItemIndex].sprite = _slotSprite;
            else _rightSideSlotImages[_selectedItemIndex].sprite = _slotSprite;
        }

        public void SetSlotItemSprite(InventorySide side, int slot, string spriteName)
        {
            if (string.IsNullOrEmpty(spriteName))
            {
                if (side == InventorySide.Left)
                {
                    _leftSideSlotItemImages[slot].color = new Color(1, 1, 1, 0);
                }
                else
                {
                    _rightSideSlotItemImages[slot].color = new Color(1, 1, 1, 0);
                }

                return;
            }

            Sprite sprite = _itemIcons.First(x => x.name == spriteName);
            if (side == InventorySide.Left)
            {
                _leftSideSlotItemImages[slot].color = Color.white;
                _leftSideSlotItemImages[slot].sprite = sprite;
            }
            else
            {
                _rightSideSlotItemImages[slot].color = Color.white;
                _rightSideSlotItemImages[slot].sprite = sprite;
            }
        }

        public void SetSlotInteractable(InventorySide side, int slot, bool interactable)
        {
            if (side == InventorySide.Left) _leftSideSlotButtons[slot].interactable = interactable;
            else _rightSideSlotButtons[slot].interactable = interactable;
        }

        public void ClearSlots()
        {
            for (int i = 0; i < _leftSideSlots.Length; i++)
            {
                _leftSideSlotItemImages[i].color = new Color(1, 1, 1, 0);
                _leftSideSlotImages[i].sprite = _slotSprite;
                _leftSideSlotButtons[i].interactable = false;
            }
            for (int i = 0; i < _rightSideSlots.Length; i++)
            {
                _rightSideSlotItemImages[i].color = new Color(1, 1, 1, 0);
                _rightSideSlotImages[i].sprite = _slotSprite;
                _rightSideSlotButtons[i].interactable = false;
            }
        }
    }

    public enum InventorySide
    {
        Left, Right
    }
}
