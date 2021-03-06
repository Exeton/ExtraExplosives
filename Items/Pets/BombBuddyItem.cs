using Terraria.ModLoader;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameInput;
using Terraria.Graphics.Shaders;
using Terraria.Graphics.Effects;
using Terraria.ID;
using Terraria.Localization;
using Microsoft.Xna.Framework.Graphics;
using System.IO;
using Microsoft.Xna.Framework.Input;
using Terraria.UI;
using static Terraria.ModLoader.ModContent;
using ExtraExplosives.Projectiles;
using ExtraExplosives.Pets;
using ExtraExplosives.Buffs;

namespace ExtraExplosives.Items.Pets
{
    public class BombBuddyItem : ModItem
    {

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Bomb Buddy");
            Tooltip.SetDefault("A likly companian?");
        }

        public override void SetDefaults()
        {
            item.CloneDefaults(ItemID.Carrot);
            item.shoot = ModContent.ProjectileType<BombBuddy>();
            item.buffType = ModContent.BuffType<BombBuddyBuff>();
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.Cobweb, 15);
            recipe.AddIngredient(ItemID.Wood, 1);
            recipe.anyWood = true;
            recipe.AddTile(TileID.WorkBenches);
            recipe.SetResult(this);
            recipe.AddRecipe();

        }

        public override void UseStyle(Player player)
        {
            if (player.whoAmI == Main.myPlayer && player.itemTime == 0)
            {
                player.AddBuff(item.buffType, 3600, true);
            }
        }
    }

}