using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Package : MonoBehaviour
{
    private int uniqueID;
    [SerializeField] int size;
    // The amount of money you will get if you deliver this package.
    [SerializeField] int value;

    public int GetSize() {
        return size;
    }

    public int GetValue() {
        return value;
    }
}
