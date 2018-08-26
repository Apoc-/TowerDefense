﻿using Assets.Scripts.Definitions.Projectiles;
using Assets.Scripts.Systems.AttributeSystem;
using Assets.Scripts.Systems.FactionSystem;
using Assets.Scripts.Systems.TowerSystem;
using UnityEngine;
using Attribute = Assets.Scripts.Systems.AttributeSystem.Attribute;

namespace Assets.Scripts.Definitions.Towers
{
    class ArrowTower : Tower
    {
        public override void InitTower()
        {
            Name = "Arrow Tower";
            Description = "A tower that shoots arrows";
            GoldCost = 10;            
            Icon = Resources.Load<Sprite>("UI/Icons/Towers/Humans/Arrow");
            Model = Resources.Load<GameObject>("Prefabs/TowerModels/ArrowTower");

            ProjectileType = typeof(ArrowProjectile);
            ProjectileModel = Resources.Load<GameObject>("Prefabs/ProjectileModels/Arrow");

            WeaponHeight = 0.4f;

            Faction = FactionNames.Humans;
            Rarity = TowerRarities.Common;
        }

        protected override void InitAttributes()
        {
            base.InitAttributes();

            AddAttribute(new Attribute(AttributeName.AttackRange, 2.5f, 0.04f, LevelIncrementType.Percentage));
            AddAttribute(new Attribute(AttributeName.AttackDamage, 2f, 0.04f, LevelIncrementType.Percentage));
            AddAttribute(new Attribute(AttributeName.AttackSpeed, 1.5f, 0.04f, LevelIncrementType.Percentage));
        }
    }
}