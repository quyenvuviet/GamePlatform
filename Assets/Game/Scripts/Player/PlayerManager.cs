using Spine.Unity;
using UnityEngine;

/// <summary>
/// Quản lý Player
/// </summary>
public class PlayerManager : MonoBehaviour
{

    public InventoryObject Inventorys;
    private void OnTriggerExit2D(Collider2D collision)
    {
        var Item = collision.GetComponent<GroudItem>();
        if (Item)
        {
            Inventorys.AddItem(new ItemObject(Item.item), 1);
            Destroy(collision.gameObject);
        }
    }
    private void OnCollisionExit2D(Collision2D other)
    {
        var Item = other.transform.GetComponent<GroudItem>();
        if (Item)
        {
            Inventorys.AddItem(new ItemObject(Item.item), 1);
            Destroy(other.transform.gameObject);
        }
    }
    private void OnApplicationQuit()
    {
        Inventorys.Container.Items = new InventorySlot[6];
    }
}
