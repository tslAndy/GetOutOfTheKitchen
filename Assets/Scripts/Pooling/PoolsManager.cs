using Enemies;
using Enemies.Hamburger;


namespace Pooling
{
    public class PoolsManager : Singleton<PoolsManager>
    {
        public readonly PoolMemory<Hamburger> HamburgerPool;

        public PoolsManager()
        {
            HamburgerPool = new PoolMemory<Hamburger>();
        }

    }
}
