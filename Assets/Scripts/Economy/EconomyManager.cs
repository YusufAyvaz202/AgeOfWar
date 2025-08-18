using System.Collections;
using UnityEngine;

namespace Economy
{
    public class EconomyManager : MonoBehaviour
    {
        [Header("Economy Settings")]
        public float _meatCount;
        public float _meatProductionPerTime;


        private IEnumerator IncreaseMeat()
        {
            yield return new WaitForSeconds(1f);
            _meatCount += _meatProductionPerTime;
        }
    }
}
