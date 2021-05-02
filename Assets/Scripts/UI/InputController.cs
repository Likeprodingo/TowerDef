using System;
using System.Collections.Generic;
using Types;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class InputController : MonoBehaviour
    {
        public static event Action<TowerType> Pressed = delegate {  };
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Pressed.Invoke(TowerType.Empty);
            }
            else
            {
                if (Input.GetKeyDown(KeyCode.Alpha1))
                {
                    Pressed.Invoke(TowerType.GunTower);
                }
                if (Input.GetKeyDown(KeyCode.Alpha2))
                {
                    Pressed.Invoke(TowerType.FireTower);
                }
                if (Input.GetKeyDown(KeyCode.Alpha3))
                {
                    Pressed.Invoke(TowerType.LightningPower);
                }
            }
        }
    }
}