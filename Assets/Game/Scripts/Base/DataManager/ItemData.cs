using UnityEngine;
using System;


public abstract class ItemData : ScriptableObject
{
     public int Id;
     public ItemID itemID;
     public Sprite uiDisPlay;
    [TextArea(15,20)]
    public string description;
    public ItemBuff[] itemBuffs;
    public ItemObject CreateItem()
    {
        ItemObject newitem = new ItemObject(this);
        return newitem;
    }
 
}
[Serializable]
public class ItemObject{
    public int ID;
    public string Name;
    public ItemBuff[] itemBuffs;
    public ItemObject(ItemData item)
    {
        Name = item.name;
        ID = item.Id;
        itemBuffs = new ItemBuff[item.itemBuffs.Length];
        for(int i = 0; i < itemBuffs.Length; i++)
        {
            itemBuffs[i] = new ItemBuff(item.itemBuffs[i].min, item.itemBuffs[i].max);
        }
    }
}
[Serializable]
public class ItemBuff
{
    public Attributes attribute;
    public int vaule;
    public int min;
    public int max;
    public ItemBuff(int _max,int _min)
    {
        min = _min;
        max = _max;
    }
    public void GenerateVaule()
    {
        vaule = UnityEngine.Random.Range(min, max);
    }
}