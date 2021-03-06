﻿using KeysToInsanity.Code.Interactive_Objects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace KeysToInsanity.Code.Interface
{
    class HUD : BasicSprite
    {
        private new RenderTarget2D spriteTex; // This hides BasicSprite's Texture2D, which RenderTarget2D inherits
        private GraphicsDevice gd;
        private SpriteContainer hudSprites = new SpriteContainer();

        // This class is kind of weird
        // First of all, all elements are drawn to external texture, why?
        // To ensure that the HUD is
        // a) drawn all at once
        // b) drawn over everything else (we will draw the HUD last in the draw order of the main draw function)
        // and c) not a part of collision detection
        // This also allows us to define custom behavior for drawing whatever sprites are added to
        // the HUD's list of sprites to draw. And adjust the color of the entire HUD itself.
        public HUD(Game game, GraphicsDevice d) : base(new RenderTarget2D(d,
                d.PresentationParameters.BackBufferWidth,
                d.PresentationParameters.BackBufferHeight,
                false,
                d.PresentationParameters.BackBufferFormat,
                DepthFormat.Depth24), false)
        {
            BasicSprite hud_key_frame = new BasicSprite(game, "hud_key_frame", false);
            hud_key_frame.spriteSize = new Point(60, 60);
            hud_key_frame.spritePos = new Vector2(740, 0);
            
            BasicSprite hud_health_frame = new BasicSprite(game, "health_bar_frame", false);
            hud_health_frame.spriteSize = new Point(200, 30);
            hud_health_frame.spritePos = new Vector2(1, 0);
           
            BasicSprite hud_health_color = new BasicSprite(game, "health_bar_color", false);
            hud_health_color.spriteSize = new Point(202, 32);
            hud_health_color.spritePos = new Vector2(0, 0);

            hud_key_frame.addTo(hudSprites);
            hud_health_color.addTo(hudSprites);
            hud_health_frame.addTo(hudSprites);

            gd = d;
        }

        // This draws the HUD to it's texture
        public void drawHUD(SpriteBatch spriteBatch)
        {
            gd.SetRenderTarget(spriteTex);
            gd.DepthStencilState = new DepthStencilState() { DepthBufferEnable = true }; // didn't know you could do this until now
            gd.Clear(Color.White);

            // Draw HUD
            foreach (BasicSprite s in hudSprites)
            {
                spriteBatch.Draw(s.spriteTex, new Rectangle(s.spritePos.ToPoint(), s.spriteSize), Color.White);
            }

            gd.SetRenderTarget(null);
        }

        public override void draw(SpriteBatch spriteBatch)
        {
            drawHUD(spriteBatch);
            base.draw(spriteBatch);
        }

        // player picked up a key
        public void addKey(Key k)
        {
            k.addTo(hudSprites);
        }
    }
}
