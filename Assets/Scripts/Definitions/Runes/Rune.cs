﻿using System.Collections.Generic;
using Assets.Scripts.Systems.AttributeSystem;
using Assets.Scripts.Systems.TowerSystem;
using UnityEngine;

namespace Assets.Scripts.Definitions.Runes
{
    public abstract class Rune : MonoBehaviour, AttributeEffectSource
    {
        private List<Tower> affectedTowers = new List<Tower>();
        private List<AttributeEffect> attributeEffects = new List<AttributeEffect>();        

        public virtual void InitRuneData()
        {

        }

        protected void AddAttributeEffect(AttributeEffect effect)
        {
            attributeEffects.Add(effect);
        }

        public void ApplyRune(Tower tower)
        {
            attributeEffects.ForEach(effect =>
            {
                var attr = effect.AffectedAttributeName;
                tower.GetAttribute(attr).AddAttributeEffect(effect);

                affectedTowers.Add(tower);
            });
        }

        public void RemoveRune()
        {
            affectedTowers.ForEach(tower =>
            {
                tower.Attributes.RemoveAttributeEffectsFromSource(this);
            });
        }
    }
}