﻿using System.Diagnostics;
using UnityEngine;

namespace MelonLoader.MelonStartScreen.UI
{
    internal class AnimatedImage
    {
        public readonly int width, height;

        private float frameDelayMS = 90f;
        private Texture2D[] textures;
        private float aspectRatio;

        private Stopwatch stopwatch = new Stopwatch();

        public AnimatedImage(int width, int height, byte[][] framebuffer, float framedelayms = 90f)
        {
            frameDelayMS = framedelayms;
            textures = new Texture2D[framebuffer.Length];
            this.width = width;
            this.height = height;
            aspectRatio = width / (float)height;
            for (int i = 0; i < framebuffer.Length; ++i)
            {
                Texture2D tex = new Texture2D(width, height);
                tex.filterMode = FilterMode.Point;
                ImageConversion.LoadImage(tex, framebuffer[i], false);
                textures[i] = tex;
            }
        }

        public AnimatedImage(int width, int height, Texture2D[] frames, float framedelayms = 90f)
        {
            frameDelayMS = framedelayms;
            textures = frames;
            this.width = width;
            this.height = height;
            aspectRatio = width / (float)height;
        }

        public void Render(int x, int y, int width)
        {
            if (!stopwatch.IsRunning)
                stopwatch.Start();

            int image = (int)((float)(stopwatch.ElapsedMilliseconds / frameDelayMS) % textures.Length);

            Graphics.DrawTexture(new Rect(x, y, width, -(int)(width / aspectRatio)), textures[image]);
        }
    }
}
