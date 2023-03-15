using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Game.Scripts.Data
{
   
    [CreateAssetMenu(fileName = "ItemDataSetting", menuName = "Game/Setting")]
    [Serializable]
    public class DataSetting : ScriptableObject
    {
        public float Sound = 10;
        public float Music = 10;
        public int indexGraphic = 1;
        public int indexLengauge = 1;
    }
}
