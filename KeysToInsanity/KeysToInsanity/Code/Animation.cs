﻿using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;


namespace KeysToInsanity
{
    class Animation
    {
        List<AnimationFrame> frames = new List<AnimationFrame>();
        TimeSpan timeIntoAnimation;

        TimeSpan Duration
        {
            get
            {
                double totalSeconds = 0;
                foreach (var frame in frames)
                {
                    totalSeconds += frame.Duration.TotalSeconds;
                }

                return TimeSpan.FromSeconds(totalSeconds);
            }
        }

        public void AddFrame(Rectangle rectangle, TimeSpan duration)
        {
            AnimationFrame newFrame = new AnimationFrame()
            {
                SourceRectangle = rectangle,
                Duration = duration
            };

            frames.Add(newFrame);
        }

        public void Update(GameTime gameTime)
        {
            double secondsIntoAnimation =
                timeIntoAnimation.TotalSeconds + gameTime.ElapsedGameTime.TotalSeconds;

            double remainder = secondsIntoAnimation % Duration.TotalSeconds;

            timeIntoAnimation = TimeSpan.FromSeconds(remainder);
        }

        public Rectangle CurrentRectangle
        {
            get
            {
                AnimationFrame currentFrame = null;

                //See if we can find the frame
                TimeSpan accumulatedTime = new TimeSpan();
                for(int i = 0; i < frames.Count; i++)
                {
                    if (accumulatedTime + frames[i].Duration >= timeIntoAnimation)
                    {
                        currentFrame = frames[i];
                        break;
                    }
                    else
                    {
                        accumulatedTime += frames[i].Duration;
                    }
                }
                /*If no frame was found, then we ty the last frame,
             in case if timeIntoDuration exceeds Duration*/
                if (currentFrame == null)
                {
                    currentFrame = frames.LastOrDefault();
                }
                if (currentFrame != null)
                {
                    return currentFrame.SourceRectangle;
                }
                else
                {
                    return Rectangle.Empty;
                }
            }
        }
    }
}
