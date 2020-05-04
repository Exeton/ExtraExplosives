﻿using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ExtraExplosives.Tiles;

namespace ExtraExplosives.Projectiles
{
    public class NukeProjectile : ModProjectile
    {
        Mod CalamityMod = ModLoader.GetMod("CalamityMod");
        Mod ThoriumMod = ModLoader.GetMod("ThoriumMod");

        internal static bool CanBreakWalls;

        private Vector2 Dest;

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("GiganticExplosive");
            //Tooltip.SetDefault("Your one stop shop for all your turretaria needs.");
        }

        public override string Texture => "ExtraExplosives/Projectiles/BulletBoomProjectile";

        public override void SetDefaults()
        {
            projectile.tileCollide = false; //checks to see if the projectile can go through tiles
            projectile.width = 44;   //This defines the hitbox width
            projectile.height = 44;    //This defines the hitbox height
            projectile.aiStyle = 16;  //How the projectile works, 16 is the aistyle Used for: Grenades, Dynamite, Bombs, Sticky Bomb.
            projectile.friendly = true; //Tells the game whether it is friendly to players/friendly npcs or not
            projectile.penetrate = -1; //Tells the game how many enemies it can hit before being destroyed
            projectile.timeLeft = 10; //The amount of time the projectile is alive for
            projectile.scale = 1.5f;

            Dest = projectile.position;

            projectile.position.Y = 10;
        }

        public override void PostAI()
        {
            if (projectile.position.Y < Main.worldSurface * 0.35)
            {
                projectile.tileCollide = true;
            }
        }

        public override void Kill(int timeLeft)
        {

            Vector2 position = projectile.Center;
            Main.PlaySound(SoundID.Item14, (int)position.X, (int)position.Y);
            int radius = 150;     //this is the explosion radius, the highter is the value the bigger is the explosion

            //damage part of the bomb
            ExplosionDamageProjectile.DamageRadius = (float)(radius * 1.5f);
            Projectile.NewProjectile(position.X, position.Y, 0, 0, mod.ProjectileType("ExplosionDamageProjectile"), 3000, 100, projectile.owner, 0.0f, 0);

            for (int x = -radius; x <= radius; x++)
            {
                for (int y = -radius; y <= radius; y++)
                {
                    int xPosition = (int)(x + position.X / 16.0f);
                    int yPosition = (int)(y + position.Y / 16.0f);

                    if (Math.Sqrt(x * x + y * y) <= radius-3 + 0.5)   //this make so the explosion radius is a circle
                    {
                        if (Main.tile[xPosition, yPosition].type == TileID.LihzahrdBrick || Main.tile[xPosition, yPosition].type == TileID.LihzahrdAltar || Main.tile[xPosition, yPosition].type == TileID.LihzahrdFurnace || Main.tile[xPosition, yPosition].type == TileID.Cobalt || Main.tile[xPosition, yPosition].type == TileID.Palladium || Main.tile[xPosition, yPosition].type == TileID.Mythril || Main.tile[xPosition, yPosition].type == TileID.Orichalcum || Main.tile[xPosition, yPosition].type == TileID.Adamantite || Main.tile[xPosition, yPosition].type == TileID.Titanium ||
                            Main.tile[xPosition, yPosition].type == TileID.Chlorophyte || Main.tile[xPosition, yPosition].type == TileID.DefendersForge || Main.tile[xPosition, yPosition].type == TileID.DemonAltar)
                        {

                        }
                        else if (CalamityMod != null && (Main.tile[xPosition, yPosition].type == CalamityMod.TileType("SeaPrism") || Main.tile[xPosition, yPosition].type == CalamityMod.TileType("AerialiteOre") || Main.tile[xPosition, yPosition].type == CalamityMod.TileType("CryonicOre")
                        || Main.tile[xPosition, yPosition].type == CalamityMod.TileType("CharredOre") || Main.tile[xPosition, yPosition].type == CalamityMod.TileType("PerennialOre") || Main.tile[xPosition, yPosition].type == CalamityMod.TileType("ScoriaOre")
                        || Main.tile[xPosition, yPosition].type == CalamityMod.TileType("AstralOre") || Main.tile[xPosition, yPosition].type == CalamityMod.TileType("ExodiumOre") || Main.tile[xPosition, yPosition].type == CalamityMod.TileType("UelibloomOre")
                        || Main.tile[xPosition, yPosition].type == CalamityMod.TileType("AuricOre") || Main.tile[xPosition, yPosition].type == CalamityMod.TileType("AbyssGravel") || Main.tile[xPosition, yPosition].type == CalamityMod.TileType("Voidstone") || Main.tile[xPosition, yPosition].type == CalamityMod.TileType("PlantyMush")
                        || Main.tile[xPosition, yPosition].type == CalamityMod.TileType("Tenebris") || Main.tile[xPosition, yPosition].type == CalamityMod.TileType("ArenaBlock") || Main.tile[xPosition, yPosition].type == CalamityMod.TileType("Cinderplate") || Main.tile[xPosition, yPosition].type == CalamityMod.TileType("ExodiumClusterOre")))
                        {
                            if (Main.tile[xPosition, yPosition].type == TileID.Dirt)
                            {
                                WorldGen.KillTile(xPosition, yPosition, false, false, false);  //this makes the explosion destroy tiles  
                                //if (CanBreakWalls) WorldGen.KillWall(xPosition, yPosition, false);
                            }
                        }
                        else if (ThoriumMod != null && (Main.tile[xPosition, yPosition].type == ThoriumMod.TileType("Aquaite") || Main.tile[xPosition, yPosition].type == ThoriumMod.TileType("LodeStone") || Main.tile[xPosition, yPosition].type == ThoriumMod.TileType("ValadiumChunk") || Main.tile[xPosition, yPosition].type == ThoriumMod.TileType("IllumiteChunk")
                            || Main.tile[xPosition, yPosition].type == ThoriumMod.TileType("PearlStone") || Main.tile[xPosition, yPosition].type == ThoriumMod.TileType("DepthChestPlatform")))
                        {
                            if (Main.tile[xPosition, yPosition].type == TileID.Dirt)
                            {
                                WorldGen.KillTile(xPosition, yPosition, false, false, false);  //this makes the explosion destroy tiles  
                                //if (CanBreakWalls) WorldGen.KillWall(xPosition, yPosition, false);
                            }
                        }

                        else
                        {
                            WorldGen.KillTile(xPosition, yPosition, false, false, true);  //this makes the explosion destroy tiles  
                            //if (CanBreakWalls) WorldGen.KillWall(xPosition, yPosition, false);
                        }
                    }
                    if (Math.Sqrt(x * x + y * y) > radius - 3 + 0.5 && Math.Sqrt(x * x + y * y) < radius + 0.5 && y > 0 && x > -radius+3 && x < radius - 3) //Places Tiles
                    {
                        WorldGen.KillTile(xPosition, yPosition, false, false, true);  //this makes the explosion destroy tiles
                        WorldGen.PlaceTile(xPosition, yPosition, ModContent.TileType<NuclearWasteSurfaceTile>());
                    }
                }
            }

            //for (int x = -radius; x <= radius; x++)
            //{
            //    for (int y = -radius; y <= radius; y++)
            //    {
            //        int xPosition = (int)(x + position.X / 16.0f);
            //        int yPosition = (int)(y + position.Y / 16.0f);

            //        if (Math.Sqrt(x * x + y * y) <= radius + 0.5)   //this make so the explosion radius is a circle
            //        {
            //            if (Main.tile[xPosition, yPosition].type == TileID.LihzahrdBrick || Main.tile[xPosition, yPosition].type == TileID.LihzahrdAltar || Main.tile[xPosition, yPosition].type == TileID.LihzahrdFurnace || Main.tile[xPosition, yPosition].type == TileID.Cobalt || Main.tile[xPosition, yPosition].type == TileID.Palladium || Main.tile[xPosition, yPosition].type == TileID.Mythril || Main.tile[xPosition, yPosition].type == TileID.Orichalcum || Main.tile[xPosition, yPosition].type == TileID.Adamantite || Main.tile[xPosition, yPosition].type == TileID.Titanium ||
            //                Main.tile[xPosition, yPosition].type == TileID.Chlorophyte || Main.tile[xPosition, yPosition].type == TileID.DefendersForge || Main.tile[xPosition, yPosition].type == TileID.DemonAltar)
            //            {

            //            }
            //            else if (CalamityMod != null && (Main.tile[xPosition, yPosition].type == CalamityMod.TileType("SeaPrism") || Main.tile[xPosition, yPosition].type == CalamityMod.TileType("AerialiteOre") || Main.tile[xPosition, yPosition].type == CalamityMod.TileType("CryonicOre")
            //            || Main.tile[xPosition, yPosition].type == CalamityMod.TileType("CharredOre") || Main.tile[xPosition, yPosition].type == CalamityMod.TileType("PerennialOre") || Main.tile[xPosition, yPosition].type == CalamityMod.TileType("ScoriaOre")
            //            || Main.tile[xPosition, yPosition].type == CalamityMod.TileType("AstralOre") || Main.tile[xPosition, yPosition].type == CalamityMod.TileType("ExodiumOre") || Main.tile[xPosition, yPosition].type == CalamityMod.TileType("UelibloomOre")
            //            || Main.tile[xPosition, yPosition].type == CalamityMod.TileType("AuricOre") || Main.tile[xPosition, yPosition].type == CalamityMod.TileType("AbyssGravel") || Main.tile[xPosition, yPosition].type == CalamityMod.TileType("Voidstone") || Main.tile[xPosition, yPosition].type == CalamityMod.TileType("PlantyMush")
            //            || Main.tile[xPosition, yPosition].type == CalamityMod.TileType("Tenebris") || Main.tile[xPosition, yPosition].type == CalamityMod.TileType("ArenaBlock") || Main.tile[xPosition, yPosition].type == CalamityMod.TileType("Cinderplate") || Main.tile[xPosition, yPosition].type == CalamityMod.TileType("ExodiumClusterOre")))
            //            {
            //                if (Main.tile[xPosition, yPosition].type == TileID.Dirt)
            //                {
            //                    WorldGen.KillTile(xPosition, yPosition, false, false, false);  //this makes the explosion destroy tiles  
            //                    if (CanBreakWalls) WorldGen.KillWall(xPosition, yPosition, false);
            //                }
            //            }
            //            else if (ThoriumMod != null && (Main.tile[xPosition, yPosition].type == ThoriumMod.TileType("Aquaite") || Main.tile[xPosition, yPosition].type == ThoriumMod.TileType("LodeStone") || Main.tile[xPosition, yPosition].type == ThoriumMod.TileType("ValadiumChunk") || Main.tile[xPosition, yPosition].type == ThoriumMod.TileType("IllumiteChunk")
            //                || Main.tile[xPosition, yPosition].type == ThoriumMod.TileType("PearlStone") || Main.tile[xPosition, yPosition].type == ThoriumMod.TileType("DepthChestPlatform")))
            //            {
            //                if (Main.tile[xPosition, yPosition].type == TileID.Dirt)
            //                {
            //                    WorldGen.KillTile(xPosition, yPosition, false, false, false);  //this makes the explosion destroy tiles  
            //                    if (CanBreakWalls) WorldGen.KillWall(xPosition, yPosition, false);
            //                }
            //            }

            //            else
            //            {
            //                WorldGen.KillTile(xPosition, yPosition, false, false, true);  //this makes the explosion destroy tiles  
            //                if (CanBreakWalls) WorldGen.KillWall(xPosition, yPosition, false);
            //            }
            //        }
            //        if (Math.Sqrt(x * x + y * y) <= radius + 3 + 0.5 && Math.Sqrt(x * x + y * y) > radius + 0.5 && y > 0) //Places Tiles
            //        {

            //        }
            //    }
            //}

        }


    }
}