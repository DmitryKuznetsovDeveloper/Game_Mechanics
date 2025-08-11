using ObjectPool;

namespace Bullets
{
    public interface IBulletFactory
    {
        IBullet Create(BulletData data);
    }

    public sealed class BulletFactory : IBulletFactory
    {
        private readonly IPool<Bullet> _pool;

        public BulletFactory(IPool<Bullet> pool)
        {
            _pool = pool;
        }

        public IBullet Create(BulletData data)
        {
            var bullet = _pool.Get();
            bullet.Initialize(data);
            return bullet;
        }
    }
}
