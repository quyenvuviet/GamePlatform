using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Game.Scripts.Data
{
   
  //  [CreateAssetMenu(fileName = "ItemDataSetting", menuName = "Game/Setting")]
    [Serializable]
    public class DataSetting 
    {
        public float Sound { get; set; }
        public float Music { get; set; }
        public int indexGraphic { get; set; }
        public int indexLengauge { get; set; }
    }
}
