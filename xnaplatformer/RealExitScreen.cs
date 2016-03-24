using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace xnaplatformer
{
    public class RealExitScreen : GameScreen
    {
        public RealExitScreen()
        {
            Environment.Exit(0);
        }
    }
}