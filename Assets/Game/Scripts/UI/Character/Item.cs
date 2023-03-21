using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using DG.Tweening;

public class Item :MonoBehaviour, IBeginDragHandler,IDragHandler, IEndDragHandler 
{
    // [SerializeField] private ItemID itemID;
    public bool dragOnSurfaces = true;
   // private GameObject m_DraggingIcon;
    private RectTransform m_DraggingPlane;
    [SerializeField] private ItemData Items;
    [SerializeField] private int amount;
    [SerializeField] private Image imageItem;

    //Lưu vị trí ban đầu của n 
   
    /// <summary>
    /// bắt đầu cầm 
    /// </summary>
    /// <param name="eventData"></param>
    public void OnBeginDrag(PointerEventData eventData)
    {
        var canvas = FindInParents<Canvas>(gameObject);
        if (canvas == null)
            return;
        // We have clicked something that can be dragged.
        // What we want to do is create an icon for this.
        // m_DraggingIcon = new GameObject("icon");

        transform.SetParent(canvas.transform, false);
        transform.SetAsLastSibling();

       // var image = m_DraggingIcon.AddComponent<Image>();

        //image.sprite = GetComponent<Image>().sprite;
        // image.SetNativeSize();

        if (dragOnSurfaces)
            m_DraggingPlane = transform as RectTransform;
        else
            m_DraggingPlane = canvas.transform as RectTransform;

        SetDraggedPosition(eventData);
    }
    private void Start()
    {
        imageItem.sprite = Items.Icon;
       
    }
    /// <summary>
    /// update liên tục cái item
    /// </summary>
    /// <param name="eventData"></param>
    public void OnDrag(PointerEventData data)
    {
        if (transform != null)
            SetDraggedPosition(data);
    }
    /// <summary>
    /// sau khi kết thúc thì làm gì 
    /// </summary>
    /// <param name="eventData"></param>
    /// <exception cref="System.NotImplementedException"></exception>
    public void OnEndDrag(PointerEventData eventData)
    {
        if (transform != null)
        {

            return;
          //  Destroy(transform);
        }
        
    }
    public ItemID ItemID => Items.ItemID;
    public int Amount => amount;

    public ItemData ItemData
    {
        get
        {
            if (DataManager.Instance == null)
            {
                return null;
            }
            return DataManager.Instance.GetItemDataByID(Items.ItemID);
        }
    }
    private void SetDraggedPosition(PointerEventData data)
    {
        if (dragOnSurfaces && data.pointerEnter != null && data.pointerEnter.transform as RectTransform != null)
            m_DraggingPlane = data.pointerEnter.transform as RectTransform;

        var rt = transform.GetComponent<RectTransform>();
        Vector3 globalMousePos;
        if (RectTransformUtility.ScreenPointToWorldPointInRectangle(m_DraggingPlane, data.position, data.pressEventCamera, out globalMousePos))
        {
            rt.position = globalMousePos;
            rt.rotation = m_DraggingPlane.rotation;
        }
    }
    public Item(ItemID itemID, int amount)
    {
        this.Items.ItemID = itemID;
        this.amount = amount;
    }
   

    public void Add(int amout)
    {
        this.amount += amout;
    }



    public static Item operator *(Item a, int b)
    {
        int amoutN = a.amount * b;
        Item c = new Item(a.Items.ItemID, amoutN);
        return c;
    }
    static public T FindInParents<T>(GameObject go) where T : Component
    {
        if (go == null) return null;
        var comp = go.GetComponent<T>();

        if (comp != null)
            return comp;

        Transform t = go.transform.parent;
        while (t != null && comp == null)
        {
            comp = t.gameObject.GetComponent<T>();
            t = t.parent;
        }
        return comp;
    }
/*    private RaycastHit2D CastRay()
    {
        Vector3  worldMousePOsFar = Camera.main.ViewportToScreenPoint()
        Physics2D.Raycast()
    }*/
}
