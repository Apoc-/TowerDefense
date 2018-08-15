﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Hexen.AbilitySystem;
using Hexen;
using Hexen.GameData.Towers;
using UnityEngine;

namespace Hexen.GameData.Towers
{
    class HasteAuraTower : AuraTower
    {
        public override void InitTowerData()
        {
            base.InitTowerData();

            Name = "Haste Aura Tower";
            Description = "A tower that deals very low damage, but increases the attackspeed of towers in its range.";
            GoldCost = 100;
            Projectile = Resources.Load<Projectile>("Prefabs/Projectiles/Arrow");
            Icon = Resources.Load<Sprite>("UI/Icons/DefaultTowerIcon");
            Model = Resources.Load<GameObject>("Prefabs/TowerModels/HasteAuraTowerModel");
            AuraEffect = new AuraEffect(new AttributeEffect(0.1f, AttributeName.AttackSpeed, AttributeEffectType.PercentMul, this));

            WeaponHeight = 1;

            AddAttribute(new Attribute(AttributeName.AttackRange, 1.5f, 0.04f, LevelIncrementType.Percentage));
            AddAttribute(new Attribute(AttributeName.AttackDamage, 0.5f, 0.04f, LevelIncrementType.Percentage));
            AddAttribute(new Attribute(AttributeName.AttackSpeed, 1.5f, 0.04f, LevelIncrementType.Percentage));
        }
    }
}