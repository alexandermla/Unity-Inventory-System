using UnityEngine;
using UnityEngine.InputSystem; 
using UnityEngine.EventSystems; 
using UnityEngine.UI;          
using System.Collections.Generic; 

public class InventoryTester : MonoBehaviour
{
    [SerializeField] private ItemData potionData;
    [SerializeField] private ItemData swordData;

    private InventorySystem inventory;
    private PlayerControls playerControls;

    private GraphicRaycaster graphicRaycaster;
    private PointerEventData pointerEventData;
    private EventSystem eventSystem;

    [SerializeField] private GameObject inventoryUI; // Referencia al UI del inventario

    private void Awake()
    {
        inventory = GetComponent<InventorySystem>();
        inventory.InitializeInventory(8);
        playerControls = new PlayerControls();

        playerControls.Gameplay.AddItem1.performed += AddItem1_Performed;
        playerControls.Gameplay.AddItem2.performed += AddItem2_Performed;
        playerControls.Gameplay.ResetSlot.performed += ResetSlot_Performed;

        Canvas canvas = FindFirstObjectByType<Canvas>(); 
        if (canvas != null)
        {
            graphicRaycaster = canvas.GetComponent<GraphicRaycaster>();
        }
        eventSystem = FindFirstObjectByType<EventSystem>();
    }

    private void OnEnable()
    {
        playerControls.Gameplay.Enable();
    }

    private void OnDisable()
    {
        playerControls.Gameplay.Disable();
    }

    //Este método se ejecutará SOLO cuando se pulse la tecla 1.
    private void AddItem1_Performed(InputAction.CallbackContext context)
    {
        Debug.Log("Intentando añadir Poción...");
        inventory.AddToInventory(potionData, 1);
    }

    //Este método se ejecutará SOLO cuando se pulse la tecla 2.
    private void AddItem2_Performed(InputAction.CallbackContext context)
    {
        Debug.Log("Intentando añadir Espada...");
        inventory.AddToInventory(swordData, 1);
    }

    //Este método se ejecutará SOLO cuando se pulse la tecla Q.
    private void ResetSlot_Performed(InputAction.CallbackContext context)
    {
        //1. Creamos un evento de puntero y le damos la posición actual del ratón.
        pointerEventData = new PointerEventData(eventSystem);
        pointerEventData.position = Mouse.current.position.ReadValue();

        //2. Creamos una lista para guardar los resultados del "rayo".
        List<RaycastResult> results = new List<RaycastResult>();

        //3. Lanzamos el rayo desde el GraphicRaycaster.
        graphicRaycaster.Raycast(pointerEventData, results);

        //4. Recorremos todos los objetos de UI que hemos golpeado.
        foreach (RaycastResult result in results)
        {
            //Intentamos obtener el componente de nuestra ranura visual.
            InventorySlot_UI slotUI = result.gameObject.GetComponent<InventorySlot_UI>();

            //5. Si lo encontramos
            if (slotUI != null)
            {
                Debug.Log($"Ranura encontrada en el índice: {slotUI.SlotIndex}");
                //Le decimos al sistema de inventario que la limpie.
                inventory.ResetSlot(slotUI.SlotIndex);
                
                //Salimos del bucle una vez que hemos encontrado la ranura.
                break; 
            }
        }
    }
}