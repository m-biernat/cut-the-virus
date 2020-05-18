public class BloodCell : Destructible
{
    public override void Destroy()
    {
        level.DestroyAlly();
        
        onDestroy = () => {
            level.bloodCells.Remove(this);
            Destroy(gameObject);
        };
        
        base.Destroy();
    }
}

