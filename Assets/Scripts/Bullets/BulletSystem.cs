namespace Bullets
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