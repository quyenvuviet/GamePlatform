using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Game.Scripts.Enemy
{
    /// <summary>
    ///  M?i tên b?n
    /// </summary>
    public class ArrawTrap : MonoBehaviour
    {
        [SerializeField] private float atttackCooldowm;
        [SerializeField] private GameObject ArrawPos;
        [SerializeField] private GameObject[] ArrawHolder;
        private float coolDownTime;
        private void Attack()
        {
            coolDownTime = 0;
            ArrawHolder[FindArrawTrap()].transform.position = ArrawPos.transform.position;
            ArrawHolder[FindArrawTrap()].GetComponent<EnemyProjectile>().ActiveProjectile();
        }
        private void Update()
        {
            coolDownTime += Time.deltaTime;
            if (coolDownTime >= atttackCooldowm)
            {
                Attack();
            }
        }
        private int FindArrawTrap()
        {
            for (int i = 0; i < ArrawHolder.Length; i++)
            {
                if (ArrawHolder[i].activeInHierarchy)
                {
                    return i;
                }
            }
            return 0;
        }
    }
}
