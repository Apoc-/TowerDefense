﻿using Assets.Scripts.Systems.AttributeSystem;
using Assets.Scripts.Systems.GameSystem;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.Scripts.Systems.TowerSystem
{
    class TowerSelectionManager : MonoBehaviour
    {
        public Tower CurrentSelectedTower;
        private GameObject activeRangeIndicator;
        private Tower activeRangeIndicatorTower;

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

            if (activeRangeIndicator != null)
            {
                RecalculateRangeIndicatorRadius();
            }
        }

        private void SelectTower()
        {
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            
            LayerMask mask = LayerMask.GetMask("Towers");

            if (Physics.Raycast(ray, out hit))
            {
                var tower = hit.transform.gameObject.GetComponentInParent<Tower>();
                
                if (tower != null)
                {
                    CurrentSelectedTower = tower;
                    UpdateRangeIndicator(CurrentSelectedTower);

                    if (CurrentSelectedTower.IsPlaced)
                    {
                        GameManager.Instance.UIManager.InfoPopup.EnableTowerInfoPopup(CurrentSelectedTower);
                    }
                }
            }
        }

        private void UnselectTower()
        {
            CurrentSelectedTower = null;
            DestroyRangeIndicator();
            GameManager.Instance.UIManager.InfoPopup.DisableTowerInfoPopup();
        }

        public void UpdateRangeIndicator(Tower tower)
        {
            if (activeRangeIndicator != null && activeRangeIndicatorTower != tower)
            {
                Destroy(activeRangeIndicator);
                activeRangeIndicator = null;
            }

            if (activeRangeIndicator == null)
            {
                CreateRangeIndicator(tower);
            }
        }

        private void CreateRangeIndicator(Tower tower)
        {
            activeRangeIndicator = Instantiate(Resources.Load<GameObject>("Sfx/RangeIndicator"));
            activeRangeIndicator.transform.SetParent(tower.transform);
            activeRangeIndicator.transform.localPosition = Vector3.zero;

            activeRangeIndicatorTower = tower;

            var range = GetRangeIndicatorRange(tower);
            var particles = activeRangeIndicator.GetComponent<ParticleSystem>();
            var shape = particles.shape;
            shape.radius = range;
        }

        public void RecalculateRangeIndicatorRadius()
        {
            var range = GetRangeIndicatorRange(activeRangeIndicatorTower);

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

            activeRangeIndicator = null;
            activeRangeIndicatorTower = null;
        }

        private float GetRangeIndicatorRange(Tower tower)
        {
            var range = 0.0f;

            if (tower.HasAttribute(AttributeName.AttackRange))
            {
                range = tower.GetAttribute(AttributeName.AttackRange).Value;
            }
            else if (tower.HasAttribute(AttributeName.AuraRange))
            {
                range = tower.GetAttribute(AttributeName.AuraRange).Value;
            }

            return range;
        }
    }
}
