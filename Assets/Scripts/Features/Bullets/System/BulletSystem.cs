using Features.Bullets.Collision;

namespace Features.Bullets.System
{
    public sealed class BulletSystem 
    {
         private readonly BulletController _bulletController;

         public BulletSystem(BulletController bulletController)
         {
             _bulletController = bulletController;
         }
         
        public void Fire(BulletData data)
        {
            _bulletController.Fire(data);
        }
    }
}