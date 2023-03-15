using UnityEngine;

namespace Game.Scripts.Enemy
{
    public class SpikeHead : EnemyDamge
    {
        [Header("SpikedHead Attrite")]
        [SerializeField] private float speed;

        [SerializeField] private float range;
        [SerializeField] private LayerMask playerLayer;
        private Vector3 destination;

        [SerializeField]
        private float CkeckDelay;

        private float ckecktime;
        private bool attacking;
        private Vector3[] direction = new Vector3[4];

        private void Update()
        {
            if (attacking)
            {
                transform.Translate(destination * Time.deltaTime * speed);
            }
            else
            {
                this.ckecktime += Time.deltaTime;
                if (ckecktime > CkeckDelay)
                {
                    CkeckForPlayer();
                }
            }
        }

        private void OnEnable()
        {
            this.Stop();
        }

        private void Stop()
        {
            destination = transform.position;
            attacking = false;
        }

        private void CkeckForPlayer()
        {
            this.CalulateDiretions();
            for (int i = 0; i < direction.Length; i++)
            {
                Debug.DrawRay(transform.position, direction[i], Color.red);
                RaycastHit2D raycastHit2D = Physics2D.Raycast(transform.position, direction[i], range, playerLayer);
                if (raycastHit2D.collider != null && !attacking)
                {
                    attacking = true;
                    destination = direction[i];
                    ckecktime = 0;
                }
            }
        }

        private void CalulateDiretions()
        {
            direction[0] = transform.right * range;// ben phai
            direction[1] = -transform.right * range;// ben trai
            direction[2] = transform.up * range;// ben tren
            direction[3] = -transform.up * range;// ben duoi
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            base.OnTriggerEnter2D(collision);
            this.Stop();
        }
    }
}