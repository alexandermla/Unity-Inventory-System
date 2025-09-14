using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class InventorySlot_UI : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IDropHandler
{
    [SerializeField] private Image itemIcon;
    [SerializeField] private TMP_Text stackSizeText;

    //referencias que la ranura necesita para funcionar
    private InventorySystem inventorySystem;
    private int slotIndex;
    public int SlotIndex => slotIndex;

    //El icono "fantasma" que sigue al ratón
    private static Image dragItemIcon;
    
    //Método para que el panel nos dé las referencias necesarias al crearnos
    public void Initialize(InventorySystem invSystem, int index)
    {
        inventorySystem = invSystem;
        slotIndex = index;

        //Buscamos el icono fantasma la primera vez
        if (dragItemIcon == null)
        {
            dragItemIcon = GameObject.Find("DragItemIcon").GetComponent<Image>();
            dragItemIcon.enabled = false; // Aseguramos que empieza desactivado
        }
    }

    public void UpdateSlot(InventorySlot slotData)
    {
        //Actualizamos la UI según los datos de la ranura
        if (slotData.ItemData == null)
        {
            itemIcon.enabled = false;
            stackSizeText.text = "";
        }
        else
        {
            itemIcon.enabled = true;
            itemIcon.sprite = slotData.ItemData.icon;

            if (slotData.StackSize > 1 && slotData.ItemData.canStack)
            {
                stackSizeText.text = slotData.StackSize.ToString();
            }
            else
            {
                stackSizeText.text = "";
            }
        }
    }

    // --- Implementación de las Interfaces de Drag & Drop ---

    public void OnBeginDrag(PointerEventData eventData)
    {
        //Si la ranura no está vacía
        if (inventorySystem.InventorySlots[slotIndex].ItemData != null)
        {
            //Activamos el icono fantasma, le ponemos el sprite y lo movemos a la posición del ratón
            dragItemIcon.enabled = true;
            dragItemIcon.sprite = inventorySystem.InventorySlots[slotIndex].ItemData.icon;
            dragItemIcon.transform.position = eventData.position;

            //Hacemos que el icono original sea semitransparente para dar feedback
            itemIcon.color = new Color(1, 1, 1, 0.5f);
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        //Mientras arrastramos, el icono fantasma sigue al ratón
        if (dragItemIcon.enabled)
        {
            dragItemIcon.transform.position = eventData.position;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        //Al soltar, desactivamos el icono fantasma y restauramos el icono original
        dragItemIcon.enabled = false;
        itemIcon.color = Color.white;
    }

    public void OnDrop(PointerEventData eventData)
    {
        //Obtenemos la ranura desde la que se originó el drag
        InventorySlot_UI sourceSlotUI = eventData.pointerDrag.GetComponent<InventorySlot_UI>();

        //si es una ranura válida (no estamos soltando algo que no sea una ranura)
        if (sourceSlotUI != null)
        {
            //Le pedimos al sistema de inventario que intercambie los datos
            inventorySystem.SwapSlots(sourceSlotUI.slotIndex, this.slotIndex);
        }
    }
}