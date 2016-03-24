using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Media;

namespace xnaplatformer
{
    public class RealExitScreen : GameScreen
    {
        public RealExitScreen()
        {
            for (int i = 0; i < 50000; i++)
            {
                MediaPlayer.Volume -= 0.00002f;
                if (MediaPlayer.Volume == 0f)
                    Environment.Exit(0);
            }         
        }
    }
}