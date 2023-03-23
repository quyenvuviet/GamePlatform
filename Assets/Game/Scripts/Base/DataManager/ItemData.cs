using UnityEngine;


public abstract class ItemData : ScriptableObject
{
    [SerializeField] private ItemID itemID;
    [SerializeField] private Sprite icon;
    [SerializeField] private GameObject prefab;
    [TextArea(15,20)]
    [SerializeField] private string description;
  

    public ItemID ItemID
    {
        get
        {
            return itemID;
        }
        set
        {
            itemID = value;
        }
    }

    public Sprite Icon => icon;
    public string Description => description;
    public GameObject Pefab => prefab;
 
}