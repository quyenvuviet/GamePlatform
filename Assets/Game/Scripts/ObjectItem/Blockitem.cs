using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

///To Do state coin , nam , thuoc , dan\
[Serializable]
public enum ItemGame
{
    /// <summary>
    /// không có gì
    /// </summary>
    NONE,

    /// <summary>
    ///  Ti?n
    /// </summary>
    COIN,

    /// <summary>
    /// N?m
    /// </summary>
    MUSHROOM,

    /// <summary>
    ///  thu?c
    /// </summary>
    MEDICINE,

    /// <summary>
    ///  ??n
    /// </summary>
    BULLET,
}

public enum BlockType
{
    DESTROYBLOCK,
    BONUS,
}

public class Blockitem : MonoBehaviour
{
    /// <summary>
    ///  t?c ?? nh?y c?a kh?i
    /// </summary>
    [SerializeField]
    private float speedBlock;

    /// <summary>
    ///  kh?i có th? ??y lên hay không
    /// </summary>
    [SerializeField]
    private float maxPush;

    private Vector2 startPos;
    [SerializeField] private List<GameObject> objectItemGame;
    [SerializeField] private ItemGame itemGame;
    [SerializeField] private int numberHit;
    [SerializeField] private BlockType typeblock;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "CkeckCollision" && collision.contacts[0].normal.y > 0)
        {
            //Nh?y kh?i lên
            if (this.numberHit <= 0)
            {
                return;
            }
            if (BlockType.DESTROYBLOCK == this.typeblock)
            {
                transform.DOMoveY(transform.position.y + maxPush, 0.2f).SetEase(Ease.Linear).OnComplete(() =>
                {
                    transform.DOMoveY(transform.position.y - maxPush, 0.2f).SetEase(Ease.Linear);
                });
            }
            this.Pushblock();
        }
    }

    public void Hit()
    {
        if (this.numberHit < 0)
        {
            return;
        }
        else
        {
            Pushblock();
        }
    }

    private void Pushblock()
    {
        this.StartCoroutine(this.IEPust());
        this.StartCoroutine(this.IEBonusitem());
    }

    private IEnumerator IEPust()
    {
        transform.DOMoveY(transform.position.y + maxPush, 0.2f).SetEase(Ease.Linear).OnComplete(() =>
        {
            transform.DOMoveY(transform.position.y - maxPush, 0.2f).SetEase(Ease.Linear);
        });
        yield return null;
    }

    private IEnumerator IEBonusitem()
    {
        numberHit--;
        switch (itemGame)
        {
            case ItemGame.NONE:
                Instantiate(objectItemGame[0], new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity);
                break;

            case ItemGame.COIN:
                var block = Instantiate(objectItemGame[1], transform.position + Vector3.up, Quaternion.identity, transform);
                block.transform.DOMoveY(transform.position.y + 1.5f, 0.25f).OnComplete(() =>
                {
                    block.transform.DOMoveY(transform.position.y, 0.5f);
                    block.SetActive(false);
                    //+Coin
                   
                });
                if (numberHit == 0)
                {
                    Instantiate(objectItemGame[0], transform.position, Quaternion.identity);
                }
                break;

            case ItemGame.MUSHROOM:
                Instantiate(objectItemGame[2], transform.position + Vector3.up, Quaternion.identity);
                break;

            case ItemGame.BULLET:
                Instantiate(objectItemGame[3], transform.position + Vector3.up, Quaternion.identity);
                break;

            case ItemGame.MEDICINE:
                Instantiate(objectItemGame[4], transform.position + Vector3.up, Quaternion.identity);
                break;

            default:
                break;
        }
        yield return null;
    }

    //ckeck block bang raycat
    private void CkeckUp()
    {
        /// ben tren block co enemy thi thi se chet khi nguoi choi nhay len
    }
}