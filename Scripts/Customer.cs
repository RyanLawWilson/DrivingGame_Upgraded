using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Customer : MonoBehaviour
{
    [SerializeField] string customerName;
    [SerializeField] List<int> acceptablePackageSizes;

    public List<int> GetAcceptablePackageSizesList() {
        return acceptablePackageSizes;
    }
}
