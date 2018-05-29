﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Hexen;
using UnityEngine;

namespace Assets.Scripts.UI
{
    class StartGameMenuBehaviour : MonoBehaviour
    {
        public void OnStartGameClicked()
        {
            GameManager.Instance.StartGame();
        }

        public void OnExitGameClicked()
        {
            GameManager.Instance.ExitGame();
        }
    }
}