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
        }

        private void DebugInputHandler()
        {
            if (selectedTower != null)
            {
                if (Input.GetKeyDown(KeyCode.KeypadPlus))
                {
                    selectedTower.AttackRange.AddAttributeEffect(new AttributeEffect(1.0f, AttributeEffectType.Flat));
                }

                if (Input.GetKeyDown(KeyCode.KeypadMultiply))
                {
                    selectedTower.AttackRange.AddAttributeEffect(new AttributeEffect(0.10f, AttributeEffectType.PercentAdd));
                    selectedTower.AttackRange.AddAttributeEffect(new AttributeEffect(0.2f, AttributeEffectType.PercentAdd));
                    selectedTower.AttackRange.AddAttributeEffect(new AttributeEffect(0.5f, AttributeEffectType.PercentMul));
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

            var range = tower.HeightDependantAttackRange();
            
            var particles = activeRangeIndicator.GetComponent<ParticleSystem>();
            var shape = particles.shape;
            shape.radius = range;
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