using UnityEngine;
using UnityEngine.UI;

namespace Game.Scripts.player
{
    public class Healthbar : MonoBehaviour
    {
        [SerializeField] private Health playerhealth;
        [SerializeField] private Image totalhealthbar;
        [SerializeField] private Image currenthealthBar;

        private void Start()
        {
            totalhealthbar.fillAmount = playerhealth.currentHealth / 10;
        }

        // Update is called once per frame
        private void Update()
        {
            currenthealthBar.fillAmount = playerhealth.currentHealth / 10;
        }
    }
}