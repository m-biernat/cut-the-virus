public class Virus : Destructible
{
    // Executes when virus is killed
    public override void Destroy()
    {
        onDestroy = () => level.Destroy(this);
        base.Destroy();
    }
}
