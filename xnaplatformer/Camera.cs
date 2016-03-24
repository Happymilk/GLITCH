﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;

namespace xnaplatformer
{
    public class Camera
    {
        private static Camera instance;
        Vector2 position;
        Matrix viewMatrix;
        
        public Matrix ViewMatrix
        {
            get { return viewMatrix; }
        }

        public static Camera Instance
        {
            get
            {
                if (instance == null)
                    instance = new Camera();
                return instance;
            }
        }

        public void SetLocalPosition(Vector2 focalPosition)
        {
            position = new Vector2(focalPosition.X - ScreenManager.Instance.Dimensions.X / 2,
                focalPosition.Y - ScreenManager.Instance.Dimensions.Y / 2);
            if (position.X < 0)
                position.X = 0;
            if (position.Y < 0)
                position.Y = 0;
        }

        public void Update()
        {
            viewMatrix = Matrix.CreateTranslation(new Vector3(-position, 0));
        }
    }
}
