using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InVentory : MonoBehaviour
{
    public Item _item;
    private List<Item> listItems;
    public ButtonItem ButtonItems;
    public Button buttonClose;
    public int countBoxItem;
    public GameObject clone;

    private void Awake()
    {
        _item = GetComponent<Item>();
    }
    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < 13; i++)
        {
          // 
            var isss= Instantiate(ButtonItems, clone.transform);
            ButtonItems.gameObject.SetActive(false);
            isss.transform.gameObject.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
