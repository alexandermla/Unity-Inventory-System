using System.Collections.Generic;
using UnityEngine;
using System;

public class InventorySystem : MonoBehaviour
{
    public event Action OnInventoryChanged;

    //Una lista de nuestras ranuras. Será visible y editable en el Inspector.
    [SerializeField] private List<InventorySlot> inventorySlots;

    //Propiedades para exponer la lista de forma segura (solo lectura).
    public List<InventorySlot> InventorySlots => inventorySlots;

    //Método para inicializar el inventario con un tamaño determinado.
    public void InitializeInventory(int size)
    {
        inventorySlots = new List<InventorySlot>(size);
        for (int i = 0; i < size; i++)
        {
            inventorySlots.Add(new InventorySlot()); // Añadimos ranuras vacías
        }
    }

    //El método principal para añadir objetos al inventario.
    public bool AddToInventory(ItemData itemToAdd, int amount)
    {
        //1. Creamos una bool para saber si la operación ha tenido éxito.
        bool itemAdded = false;

        //Lógica para apilar objetos
        if (itemToAdd.canStack)
        {
            //Buscamos si ya existe una ranura con ese objeto.
            InventorySlot slotToStackTo = FindSlot(itemToAdd);
            if (slotToStackTo != null && slotToStackTo.StackSize < itemToAdd.maxStackSize)
            {
                slotToStackTo.AddToStack(amount);
                itemAdded = true; //Marcamos que hemos tenido éxito.
            }
        }

        //Si no hemos apilado el objeto (porque no podía o no había dónde)...
        if (!itemAdded)
        {
            //buscamos una ranura vacía.
            InventorySlot emptySlot = FindEmptySlot();
            if (emptySlot != null)
            {
                //Creamos una nueva instancia de InventorySlot en la ranura vacía.
                int slotIndex = inventorySlots.IndexOf(emptySlot);
                inventorySlots[slotIndex] = new InventorySlot(itemToAdd, amount);
                itemAdded = true; // Marcamos que hemos tenido éxito.
            }
        }

        //2. Al final, si la operación ha tenido éxito en cualquiera de los casos...
        if (itemAdded)
        {
            //3. ...lanzamos el evento para notificar a la UI (y a quien esté escuchando).
            OnInventoryChanged?.Invoke();
            return true; //Devolvemos 'true' para confirmar que se añadió.
        }
        else
        {
            return false;
        }
    }

    //Método para encontrar una ranura que ya contenga el mismo objeto.
    private InventorySlot FindSlot(ItemData itemToFind)
    {
        foreach (var slot in inventorySlots)
        {
            if (slot.ItemData == itemToFind && slot.StackSize < itemToFind.maxStackSize)
            {
                return slot;
            }
        }
        return null;
    }

    //Método para encontrar la primera ranura vacía.
    private InventorySlot FindEmptySlot()
    {
        foreach (var slot in inventorySlots)
        {
            if (slot.ItemData == null)
            {
                return slot;
            }
        }
        return null;
    }

    public void SwapSlots(int indexA, int indexB)
    {
        InventorySlot tempSlot = inventorySlots[indexA];

        inventorySlots[indexA] = inventorySlots[indexB];

        inventorySlots[indexB] = tempSlot;

        OnInventoryChanged?.Invoke();
    }

    public void ResetSlot(int index)
    {
        //Verificamos que el índice es válido para evitar errores.
        if (index < 0 || index >= inventorySlots.Count)
        {
            return;
        }

        //Limpiamos los datos de la ranura.
        inventorySlots[index].ClearSlot();

        //Notificamos a la UI que algo ha cambiado para que se redibuje.
        OnInventoryChanged?.Invoke();
    }
}