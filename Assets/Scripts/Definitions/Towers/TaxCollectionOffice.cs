﻿using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Definitions.Projectiles;
using Assets.Scripts.Systems.AttributeSystem;
using Assets.Scripts.Systems.FactionSystem;
using Assets.Scripts.Systems.GameSystem;
using Assets.Scripts.Systems.TowerSystem;
using UnityEngine;
using Attribute = Assets.Scripts.Systems.AttributeSystem.Attribute;
using AttributeName = Assets.Scripts.Systems.AttributeSystem.AttributeName;
using Random = System.Random;

namespace Assets.Scripts.Definitions.Towers
{
    class TaxCollectionOffice : Tower
    {
        private List<Tower> towers = new List<Tower>();
        private int checkTickInterval = 10;
        private int checkTick = 0;

        public override void InitTower()
        {
            Name = "Tax Collection Office";
            Description =
                "Progression Tax: Whenever a nearby tower levels up, the player gets gold and the Collector gets xp equal to the level.";
            GoldCost = 100;
            Icon = Resources.Load<Sprite>("UI/Icons/Towers/Dwarfs/Office");
            Model = Resources.Load<GameObject>("Prefabs/TowerModels/ArrowTower");

            ProjectileType = typeof(TaxCollectionOfficeProjectile);
            ProjectileModel = Resources.Load<GameObject>("Prefabs/ProjectileModels/Arrow");

            WeaponHeight = 0.4f;

            Faction = FactionNames.Dwarfs;
            Rarity = Rarities.Rare;
        }

        protected override void InitAttributes()
        {
            base.InitAttributes();

            AddAttribute(new Attribute(AttributeName.AttackRange, 1.5f, 0.0f));
            AddAttribute(new Attribute(AttributeName.AttackDamage, 3f));
            AddAttribute(new Attribute(AttributeName.AttackSpeed, 1.0f));
            AddAttribute(new Attribute(AttributeName.AuraRange, 1.5f, 0.04f));
        }

        protected override void DoUpdate()
        {
            base.DoUpdate();

            checkTick += 1;
            if (checkTick >= checkTickInterval)
            {
                checkTick = 0;
                RefreshNearbyTowers();
            }
        }

        private void RefreshNearbyTowers()
        {
            UnsubscribeTowerEvents();

            var radius = Attributes[AttributeName.AuraRange].Value;
            var colliders = GetCollidersInRadius(radius);
            towers = colliders
                .Select(e => e.GetComponentInParent<Tower>())
                .Where(e => e != null)
                .ToList();

            towers.ForEach(tower => { tower.OnLevelUp += HandleTowerLevelUp; });
        }

        private void HandleTowerLevelUp(int level)
        {
            GameManager.Instance.Player.IncreaseGold(level);
            this.GiveXP(level);
        }

        public override void Remove()
        {
            UnsubscribeTowerEvents();

            base.Remove();
        }

        private void UnsubscribeTowerEvents()
        {
            towers.ForEach(tower => { tower.OnLevelUp -= HandleTowerLevelUp; });
        }
    }
}
