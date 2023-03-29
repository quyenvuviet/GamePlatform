using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Item : RaycastController, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerDownHandler, IDropHandler 
{
    // [SerializeField] private ItemID itemID;
    public bool dragOnSurfaces = true;

    public static Item instance;

    // private GameObject m_DraggingIcon;
    private RectTransform m_DraggingPlane;

    [SerializeField] private ItemData Items;
    [SerializeField] private int amount;
    [SerializeField] private Image imageItem;
    private CanvasGroup canvasGroup;
    public Canvas Canvas;
    private Vector3 beginMove;

    public Vector3 BeginMove
    {
        get { return beginMove; }
        set { beginMove = value; }
    }

    public override void Awake()
    {
        base.Awake();
        //  beginMove = transform.position;
    }

    /// <summary>
    /// bắt đầu cầm
    /// </summary>
    /// <param name="eventData"></param>
    public void OnBeginDrag(PointerEventData eventData)
    {
        var canvas = FindInParents<Canvas>(gameObject);
        if (canvas == null)
            return;

        transform.SetParent(canvas.transform, false);
        transform.SetAsLastSibling();

        if (dragOnSurfaces)
            m_DraggingPlane = transform as RectTransform;
        else
            m_DraggingPlane = canvas.transform as RectTransform;
        canvasGroup.blocksRaycasts = false;
        canvasGroup.alpha = .6f;

        SetDraggedPosition(eventData, ItemID.COIN);
    }

    public override void Start()
    {
        base.Start();
    //    imageItem.sprite = Items.uiDisPlay;
        canvasGroup = GetComponent<CanvasGroup>();
        beginMove = gameObject.transform.position;
    }

    /// <summary>
    /// update liên tục cái item
    /// </summary>
    /// <param name="eventData"></param>
    public void OnDrag(PointerEventData data)
    {
        if (transform != null)
            SetDraggedPosition(data, ItemID.COIN);
    }

    /// <summary>
    /// sau khi kết thúc thì làm gì
    /// </summary>
    /// <param name="eventData"></param>
    /// <exception cref="System.NotImplementedException"></exception>
    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.blocksRaycasts = true;
        canvasGroup.alpha = 1f;

        if (transform != null)
        {
            return;
            //  Destroy(transform);
        }
    }

    public ItemID ItemID => Items.itemID;
    public int Amount => amount;

    public ItemData ItemData
    {
        get
        {
            if (DataManager.Instance == null)
            {
                return null;
            }
            return DataManager.Instance.GetItemDataByID(Items.itemID);
        }
    }

    private void SetDraggedPosition(PointerEventData data, ItemID ItemID)
    {
        if (dragOnSurfaces && data.pointerEnter != null && data.pointerEnter.transform as RectTransform != null)
            m_DraggingPlane = data.pointerEnter.transform as RectTransform;

        var rt = transform.GetComponent<RectTransform>();
        Vector3 globalMousePos;
        if (RectTransformUtility.ScreenPointToWorldPointInRectangle(m_DraggingPlane, data.position, data.enterEventCamera, out globalMousePos))
        {
            /*if (ckeckButtonBox())
            {
            }*/
            //  m_DraggingPlane.anchoredPosition += data.delta / Canvas.scaleFactor;
            rt.position = globalMousePos;
            rt.rotation = m_DraggingPlane.rotation;
        }
    }

    public Item(ItemID itemID, int amount)
    {
        this.Items.itemID = itemID;
        this.amount = amount;
    }

    public void Add(int amout)
    {
        this.amount += amout;
    }

    //ckeck xem có phải cái box nếu n mà none thì cái nào cũng có thể hit được nếu không phải thì sẽ chả vễ chỗ cũ
    private bool ckeckButtonBox()
    {
        this.UpdateRaycastOrigins();

        for (int i = 0; i < horizontalRayCount; i++)
        {
            Vector2 rayOrigin = raycastOrigins.bottomLeft;
            rayOrigin += Vector2.up * (horizontalRaySpacing * i);
            RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.right, 0, collisionMask);

            Debug.DrawRay(rayOrigin, Vector2.right, Color.red);

            if (hit)
            {
                return true;
            }
        }

        return false;
    }

    public static Item operator *(Item a, int b)
    {
        int amoutN = a.amount * b;
        Item c = new Item(a.Items.itemID, amoutN);
        return c;
    }

    public static T FindInParents<T>(GameObject go) where T : Component
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

    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("onpionerdoewn");
    }

    public void OnDrop(PointerEventData eventData)
    {
        Debug.Log("OnDrop");
    }
}