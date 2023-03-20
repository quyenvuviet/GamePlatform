using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using DG.Tweening;

public class Item :MonoBehaviour, IBeginDragHandler,IDragHandler, IEndDragHandler 
{
    [SerializeField] private ItemID itemID;
    [SerializeField] private int amount;
   
    /// <summary>
    /// bắt đầu cầm 
    /// </summary>
    /// <param name="eventData"></param>
    public void OnBeginDrag(PointerEventData eventData)
    {
        UpdatePOS();
    }
    /// <summary>
    /// update liên tục cái item
    /// </summary>
    /// <param name="eventData"></param>
    public void OnDrag(PointerEventData eventData)
    {
        UpdatePOS();
    }
    /// <summary>
    /// sau khi kết thúc thì làm gì 
    /// </summary>
    /// <param name="eventData"></param>
    /// <exception cref="System.NotImplementedException"></exception>
    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("endondrag00");
    }
    public ItemID ItemID => itemID;
    public int Amount => amount;

    public ItemData ItemData
    {
        get
        {
            if (DataManager.Instance == null)
            {
                return null;
            }
            return DataManager.Instance.GetItemDataByID(itemID);
        }
    }

    public Item(ItemID itemID, int amount)
    {
        this.itemID = itemID;
        this.amount = amount;
    }

    public void Add(int amout)
    {
        this.amount += amout;
    }
    /// <summary>
    /// Item đi theo con chuột
    /// </summary>
    private void UpdatePOS()
    {
       var mouse = Input.mousePosition;
        transform.position = mouse;
    }



    public static Item operator *(Item a, int b)
    {
        int amoutN = a.amount * b;
        Item c = new Item(a.itemID, amoutN);
        return c;
    }
}
