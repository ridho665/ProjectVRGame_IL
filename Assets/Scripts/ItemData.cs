using UnityEngine;

[CreateAssetMenu(fileName = "NewItem", menuName = "Shop/Item")]
public class ItemData : ScriptableObject
{
    public string itemName;
    public GameObject itemPrefab;
    public float price;
    public string category;  
    public float styleScore; 
}
