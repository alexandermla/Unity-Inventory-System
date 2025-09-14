using System.Collections.Generic;
using UnityEngine;

public class InventoryPanel_UI : MonoBehaviour
{
    [SerializeField] private InventorySystem inventorySystem; //la referencia a nuestra lógica
    [SerializeField] private GameObject slotPrefab; //el prefab de la ranura visual

    private List<InventorySlot_UI> slotUIs = new List<InventorySlot_UI>();

    private void Start()
    {
        InitializeInventoryUI();
    }

    //suscribirse al evento cuando el objeto se activa
    private void OnEnable()
    {
        inventorySystem.OnInventoryChanged += RedrawInventory;
    }

    //Desuscribirse al evento cuando el objeto se desactiva
    private void OnDisable()
    {
        inventorySystem.OnInventoryChanged -= RedrawInventory;
    }

    private void InitializeInventoryUI()
    {
        for (int i = 0; i < inventorySystem.InventorySlots.Count; i++)
        {
            //Instanciamos el prefab de la ranura.
            GameObject newSlot = Instantiate(slotPrefab, transform);
            
            //Obtenemos una referencia al script de la ranura UI.
            InventorySlot_UI uiSlot = newSlot.GetComponent<InventorySlot_UI>();
            
            //le pasamos las referencias que necesita: el sistema de inventario y su propio índice.
            uiSlot.Initialize(inventorySystem, i);
            
            //Añadimos la ranura a nuestra lista para futuras actualizaciones.
            slotUIs.Add(uiSlot);
        }
        
        //Redibujamos el inventario para mostrar el estado inicial.
        RedrawInventory();
    }
    
    //este método se llamará automáticamente cada vez que el inventario cambie.
    private void RedrawInventory()
    {
        //Recorremos todas las ranuras de la UI y los datos correspondientes.
        for (int i = 0; i < slotUIs.Count; i++)
        {
            //Le pedimos a cada ranura visual que se actualice con su nuevo dato.
            slotUIs[i].UpdateSlot(inventorySystem.InventorySlots[i]);
        }
    }
    
}