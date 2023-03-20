using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InVentory : MonoBehaviour
{
    public Item _item;
    public List<Item> listItems;
    public Button buttonClose;

    private void Awake()
    {
        _item = GetComponent<Item>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
