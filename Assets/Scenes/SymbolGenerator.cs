using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class SymbolGenerator : MonoBehaviour
{
    [SerializeField] List<SymbolData> listOfSymbols = new List<SymbolData>();

    [SerializeField] Transform parentTransform;

    private void Start()
    {
        foreach (var symbol in listOfSymbols)
        {
            Image i = Instantiate(symbol.symbolImage, parentTransform.position, Quaternion.identity);
            i.transform.SetParent(parentTransform);
        }
    }
}
