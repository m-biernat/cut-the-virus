public class Virus : Destructible
{
    public override void Destroy()
    {
        onDestroy = () => level.Destroy(this);
        base.Destroy();
    }
}
