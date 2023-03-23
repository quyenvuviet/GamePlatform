using Game.Scripts.Enemy;
using Spine.Unity;
using System.Collections;
using UnityEngine;

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

        [SerializeField]
        private SkeletonAnimation AnimationData;

        private string currentAnimation = " ";

        private Animator animator;

        private void Awake()
        {
            currentHealth = StartingHealth;
            spriteRenderer = GetComponent<SpriteRenderer>();
            animator = GetComponent<Animator>();
            AnimationData = GetComponent<SkeletonAnimation>();
        }

        private void Start()
        {
        }

        public void SetAnination(string name, bool loop = true)
        {
            if (gameObject.transform.tag == "enemy")
            {
                animator.SetTrigger(name);
                return;
            }
            else
            {
                if (name == this.currentAnimation)
                {
                    return;
                }
                if (AnimationData == null)
                {
                    return;
                }

                AnimationData.state.SetAnimation(0, name, loop);
                currentAnimation = name;
            }
        }

        private IEnumerator waitdie()
        {
            SetAnination("die");
            yield return new WaitForSeconds(2f);
            transform.gameObject.SetActive(false);
            EnemyPartrol.instance.hide();
        }

        public void TakeDame(float _dame)
        {
            currentHealth = Mathf.Clamp(currentHealth - _dame, 0, StartingHealth);
            if (currentHealth > 0)
            {
                SetAnination("idle");
                StartCoroutine(Invunerabilyty());
            }
            else
            {
                if (!dead)
                {
                    if (gameObject.transform.tag == "enemy")
                    {
                        StartCoroutine(waitdie());
                    }
                    else
                    {
                        SetAnination("die");
                        if (GetComponent<PlayerMove>() != null)
                        {
                            GetComponent<PlayerMove>().enabled = false;
                        }
                        if (GetComponentInParent<EnemyPartrol>() != null)
                        {
                            GetComponentInParent<EnemyPartrol>().enabled = false;
                            //  GetComponent<EnemyPartrol>().enabled = false;
                        }
                        if (GetComponent<MeleteEnemy>() != null)
                        {
                            GetComponent<MeleteEnemy>().enabled = false;
                        }
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
            for (int i = 0; i < numberOffLlashes; i++)
            {
                spriteRenderer.color = new Color(1, 0, 0, 0.5f);
                yield return new WaitForSeconds(iFramesDuration / (numberOffLlashes * 2));
                spriteRenderer.color = Color.white;
            }
            Physics2D.IgnoreLayerCollision(8, 9, false);
            yield return null;
        }
    }
}