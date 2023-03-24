using UnityEngine;
using System;


public abstract class ItemData : ScriptableObject
{
     public int Id;
     public ItemID itemID;
     public Sprite uiDisPlay;
    [TextArea(15,20)]
    public string description;
 
 
}
[Serializable]
public class ItemObject{
    public int ID;
    public string Name;
    public ItemObject(ItemData item)
    {
        Name = item.name;
        ID = item.Id;
    }
}