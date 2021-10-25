namespace Clicker
{
    internal sealed class EnemyMoveModel 
    {
        public EnemyMoveTypes GetRandomMoveTypeValue() =>
            (EnemyMoveTypes) typeof(EnemyMoveTypes).GetRandomEnumValue();
    }
}