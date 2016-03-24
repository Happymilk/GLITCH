using System;

namespace xnaplatformer
{
#if WINDOWS || XBOX
    public class Program
    {
        static void Main(string[] args)
        {
            using (Game1 game = new Game1())
            {
                game.Run();
            }
        }
    }
#endif
}