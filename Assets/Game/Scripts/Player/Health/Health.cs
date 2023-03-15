using UnityEngine;
using System.Collections;
using Game.Scripts.Enemy;

namespace Game.Scripts.player
{
    public class Health : MonoBehaviour
    {
        [Header("Health")]
        [SerializeField] private float StartingHealth;
        public float currentHealth { get; set; }
        private bool dead;
        [Header("iFrames")]
        [SerializeField] private float iFramesDuration;
        [SerializeField] private float numberOffLlashes;
        private SpriteRenderer spriteRenderer;
        private PlayerMove player2d;

      

        private void Awake()
        {
          
            currentHealth = StartingHealth;
            spriteRenderer = GetComponent<SpriteRenderer>();
            player2d = GetComponent<PlayerMove>();
        }
        private void Start()
        {
            
        }
        public void TakeDame(float _dame)
        {
            currentHealth = Mathf.Clamp(currentHealth - _dame, 0, StartingHealth);
            if (currentHealth > 0)
            {
                player2d.SetAnination("idle"); 
                StartCoroutine(Invunerabilyty());
            }
            else
            {
                if (!dead)
                {
                    player2d.SetAnination("die");
                    if (GetComponent<PlayerMove>() != null)
                    {
                        GetComponent<PlayerMove>().enabled = false;

                    }
                    if (GetComponentInParent <EnemyPartrol> () != null)
                    {
                        GetComponentInParent<EnemyPartrol>().enabled = false;
                      //  GetComponent<EnemyPartrol>().enabled = false;
                     }
                    if (GetComponent<MeleteEnemy>() != null)
                    {
                        GetComponent<MeleteEnemy>().enabled = false;
                    }
                    
                    dead = true;
                    //todo Game Over
                }
            }
        }
        public void AddHeath(float _values)
        {
            currentHealth = Mathf.Clamp(currentHealth + _values, 0, StartingHealth);

        }
       private IEnumerator Invunerabilyty()
        {
            
            Physics2D.IgnoreLayerCollision(8, 9, true);
            for(int i = 0; i < numberOffLlashes; i++)
            {
                spriteRenderer.color = new Color(1, 0, 0, 0.5f);
                yield return new WaitForSeconds(iFramesDuration/(numberOffLlashes*2));
                spriteRenderer.color = Color.white;
            }
            Physics2D.IgnoreLayerCollision(8, 9, false);
            yield return null;
        }

    }
}