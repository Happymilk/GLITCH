using System;

namespace xnaplatformer
{
#if WINDOWS || XBOX
    public class Program
    {
        static void Main(string[] args)
        {
            MenuManager mm;
            mm = new MenuManager();
            mm.iWantMusic = true;

            using (Game1 game = new Game1())
            {
                game.Run();
            }
        }
    }
#endif
}