namespace Clicker
{
    internal interface IDestroyable
    {
        float ExplosionRadiusCoefficient { get; }
        float ExplosionForceCoefficient { get; }
        void DestroyEffectsInit();
        void TakeDamageEffectsInit();
    }
}