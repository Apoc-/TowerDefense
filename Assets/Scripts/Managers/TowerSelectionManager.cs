﻿using UnityEngine;
using UnityEngine.EventSystems;

namespace Hexen
{
    class TowerSelectionManager : MonoBehaviour
    {
        private Tower selectedTower;
        private GameObject activeRangeIndicator;

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Mouse0) && !EventSystem.current.IsPointerOverGameObject())
            {
                SelectTower();
            }

            if (Input.GetKeyDown(KeyCode.Mouse1))
            {
                UnselectTower();
            }

            DebugInputHandler();

            if (selectedTower != null && activeRangeIndicator != null)
            {
                if (activeRangeIndicator != null)
                {
                    RecalculateRangeIndicatorRadius();
                }
                
                GameManager.Instance.UIManager.InfoPanel.DisplayTowerInformation(selectedTower);
            }
        }

        private void DebugInputHandler()
        {
            if (selectedTower != null)
            {
                if (Input.GetKeyDown(KeyCode.KeypadPlus))
                {

                }
            }
        }

        private void SelectTower()
        {
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            
            LayerMask mask = LayerMask.GetMask("Towers");

            if (Physics.Raycast(ray, out hit))
            {
                var tower = hit.transform.gameObject.GetComponent<Tower>();

                if (tower != null)
                {
                    selectedTower = tower;
                    DisplayRangeIndicator(tower);
                }
            }
        }

        private void UnselectTower()
        {
            selectedTower = null;
            DestroyRangeIndicator();
            GameManager.Instance.UIManager.InfoPanel.ClearInformation();
        }

        public void DisplayRangeIndicator(Tower tower)
        {
            if (activeRangeIndicator != null)
            {
                Destroy(activeRangeIndicator);
            }

            activeRangeIndicator = Instantiate(Resources.Load<GameObject>("Prefabs/Entities/RangeIndicator"));
            activeRangeIndicator.transform.SetParent(tower.transform);
            activeRangeIndicator.transform.localPosition = Vector3.zero;

            var range = tower.AttackRange.Value;
            var particles = activeRangeIndicator.GetComponent<ParticleSystem>();
            var shape = particles.shape;
            shape.radius = range;
        }

        public void RecalculateRangeIndicatorRadius()
        {
            var range = selectedTower.AttackRange.Value;
            

            var particles = activeRangeIndicator.GetComponent<ParticleSystem>();
            var shape = particles.shape;
            var radius = shape.radius;

            if (Mathf.Abs(radius - range) > 0.0001f)
            {
                shape.radius = range;
            }
        }

        public void DestroyRangeIndicator()
        {
            if (activeRangeIndicator != null)
            {
                Destroy(activeRangeIndicator);
            }
        }
    }
}
