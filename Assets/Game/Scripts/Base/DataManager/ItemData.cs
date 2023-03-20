using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "ItemData", menuName = "Game/ItemData")]
public class ItemData : ScriptableObject {
    [SerializeField] private ItemID itemID;
    [SerializeField] private Sprite icon;
    [SerializeField] private string description;
    [SerializeField] private int number;
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
    public int Number => number;
}
