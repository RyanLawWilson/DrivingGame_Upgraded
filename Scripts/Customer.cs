using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Customer : MonoBehaviour
{
    [SerializeField] string customerName;
    [SerializeField] List<int> acceptablePackageSizes;

    // This is to show the correct sprite according to the sprite renderer that I created.
    
    public List<int> GetAcceptablePackageSizesList() {
        return acceptablePackageSizes;
    }
}
