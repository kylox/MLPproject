using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace MLPproject
{
    public static class Data
    {
        static public MouseState mouseState { get; private set; }
        static public MouseState prevMouseState { get; private set; }
        static public KeyboardState keyboardState { get; private set; }
        static public KeyboardState prevKeyboardState { get; private set; }
        static public void Update()
        {
            prevMouseState = mouseState;
            mouseState = Mouse.GetState();
            prevKeyboardState = keyboardState;
            keyboardState = Keyboard.GetState();
        }
    }
}
