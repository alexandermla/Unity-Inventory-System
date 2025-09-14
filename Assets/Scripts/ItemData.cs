using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
public class ItemData : ScriptableObject
{
    [Header("Info")]
    public string itemName;
    [TextArea(4, 10)]
    public string description;
    public Sprite icon;

    [Header("Stacking")]
    public bool canStack;
    public int maxStackSize;
}