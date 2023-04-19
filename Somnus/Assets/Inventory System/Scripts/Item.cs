using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "ScriptableObject/Item")]
public class Item : ScriptableObject
{
    public int ID = 0;
    public string Name = "Item";
    public Sprite Icon;
    public bool Used = false;
}
