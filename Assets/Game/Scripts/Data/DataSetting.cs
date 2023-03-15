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
        public float Sound { get; set; } = 50;
        public float Music { get; set; } = 50;
        public int indexGraphic { get; set; } = 1;
        public int indexLengauge { get; set;} = 1;

        }
}
