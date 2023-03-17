using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Game.Scripts.Data
{

    //  [CreateAssetMenu(fileName = "ItemDataSetting", menuName = "Game/Setting")]
    public class DataSetting
    {
        public float Sound;
        public float Music;
        public int indexGraphic;
        public int indexLengauge;
        public void SetSound(float sound)
        {
           this.Sound = sound;
        }
        public void SetMusicd(float Music)
        {
            this.Music = Music;
        }

    }
}
