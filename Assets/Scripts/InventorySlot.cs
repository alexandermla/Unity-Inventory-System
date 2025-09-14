using UnityEngine;

[System.Serializable]
public class InventorySlot
{
    [SerializeField] private ItemData itemData;
    [SerializeField] private int stackSize;

    //Propiedades públicas para acceder a los datos de forma controlada.
    //Otros scripts podrán LEER estos datos, pero solo la propia clase podrá MODIFICARLOS.
    public ItemData ItemData => itemData;
    public int StackSize => stackSize;

    public InventorySlot(ItemData source, int amount)
    {
        itemData = source;
        stackSize = amount;
    }

    public InventorySlot()
    {
        ClearSlot();
    }

    public void ClearSlot()
    {
        itemData = null;
        stackSize = 0;
    }

    public void AddToStack(int amount)
    {
        stackSize += amount;
    }
}