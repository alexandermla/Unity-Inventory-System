# Modular Inventory System - Portfolio Project

A robust and reusable inventory system for Unity, built from scratch with a focus on decoupled data architecture and advanced UI interaction.

**[➡️ TRY THE DEMO HERE! ⬅️](https://alexandermla.itch.io/inventory-system-porfolio)**


### Description

This project demonstrates my ability to design and program complex, data-centric game systems. The goal was to create an inventory system that was efficient, scalable, and easy to expand with new types of objects, following industry best practices.

### Features Implemented
* **ScriptableObjects-based architecture:** Objects are independent data assets, allowing designers to create hundreds of new objects without touching a single line of code.
* **Robust Inventory Logic:** Handles stacking of objects across multiple slots and intelligently searches for available space, even when there are multiple stacks of the same object.
* **Dynamic, Prefab-Based UI:** The user interface is automatically generated based on inventory size, using `Grid Layout Groups` for perfect positioning.
* **Professional Drag and Drop System:** Implemented using Unity's event interfaces (`IBeginDragHandler`, `IDragHandler`, `IEndDragHandler`, `IDropHandler`) for precise and efficient interaction.
* **Event-Driven Communication:** Logic (`InventorySystem`) and presentation (`InventoryPanel_UI`) are completely decoupled thanks to the use of C# events (`Action`), allowing the system to be easy to maintain and expand.
* **Precise UI Detection:** Uses Unity's `GraphicRaycaster` for functionalities such as “remove object under cursor,” ensuring robust interaction independent of UI layout.

### Challenges and Lessons Learned

A key challenge was ensuring the accuracy of Drag and Drop. The solution involved a deep understanding of Unity's UI Raycast Target system to ensure that mouse events were captured by the correct elements. This experience taught me the importance of hierarchy and detailed configuration of UI components to achieve a polished and frustration-free user experience.

### Tools Used

* **Engine:** Unity 6000.2.1f1*
* **Language:** C#
* **Version Control:** Git and GitHub
