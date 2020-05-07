public class BloodCell : Destructible
{
    public override void Destroy()
    {
        onDestroy = () => level.Destroy(this);
        base.Destroy();
    }
}

