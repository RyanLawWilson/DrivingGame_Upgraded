using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    [SerializeField] List<InventorySlot> inventorySlots;
    [SerializeField] bool isFull = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }





    public void PickUpPackage(GameObject packageObject) {
        // If we are not full...
        if (!isFull) {
            Package package = packageObject.GetComponent<Package>();
            // if the object is a package...
            if (package != null) {
                // Do we have the room to pick this package up?
                int openSlots = inventorySlots.Where(slot => slot.GetCurrentPackage() == null).Count();
                InventorySlot openSlot = inventorySlots.Where(slot => slot.GetCurrentPackage() == null).FirstOrDefault();
                if (openSlot == null) {
                    isFull = true;
                } else {
                    openSlot.InsertPackage(package);
                    if (openSlots - 1 <= 0) {   // Replace the  - 1 with  - package.GetSize()
                        isFull = true;
                    }
                }
            }
        }
    }

    public bool GetIsFull() {
        return isFull;
    }

    public void SetIsFull(bool full) {
        isFull = full;
    }

    public List<InventorySlot> GetInventorySlots() {
        return inventorySlots;
    }

    public void ReorganizeInventory() {
        for (int n = 0; n < inventorySlots.Count; n++) {
            for (int i = inventorySlots.Count - 1; i > n; i--) {
                // If the previous slot is empty, move Package into that slot.
                if (inventorySlots[i].GetCurrentPackage() != null && inventorySlots[i-1].GetCurrentPackage() == null) {
                    inventorySlots[i-1].SetCurrentPackage(inventorySlots[i].GetCurrentPackage());
                    inventorySlots[i].SetCurrentPackage(null);
                }
            }
        }
    }
}
