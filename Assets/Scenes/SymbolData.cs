using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "NewSymbol", menuName = "Slot Machine/Symbol Data")]
public class SymbolData : ScriptableObject
{
    public string symbolName;
    public Image symbolImage;
    public Color symbolColor;

    private void OnEnable()
    {
        symbolName = this.name;
        symbolImage.color = symbolColor;
    }
}