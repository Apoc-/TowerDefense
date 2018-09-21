﻿using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Definitions.Npcs;
using Assets.Scripts.Systems.AttributeSystem;
using Assets.Scripts.Systems.FactionSystem;
using Assets.Scripts.Systems.GameSystem;
using Assets.Scripts.Systems.TowerSystem;
using UnityEngine;

namespace Assets.Scripts.Definitions.Npcs
{
    public class GoblinUnderling : Npc
    {
        protected override void InitNpcData()
        {
            this.Name = "Goblin Underling";
            this.ModelPrefab = Resources.Load<GameObject>("Prefabs/Npcs/Goblins/GoblinUnderling");
            this.HealthBarOffset = 0.4f;

            Rarity = Rarities.Common;
            Faction = FactionNames.Goblins;
        }

        protected override void InitAttributes()
        {
            base.InitAttributes();

            AddAttribute(new Attribute(
                AttributeName.MaxHealth,
                GameSettings.BaselineNpcHp[Rarity],
                GameSettings.BaselineNpcHpInc[Rarity]));

            AddAttribute(new Attribute(AttributeName.MovementSpeed, 1.2f));
        }
    }
}
