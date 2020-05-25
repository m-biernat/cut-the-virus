using UnityEngine;

public class BloodCell : Destructible
{
    public GameObject brokenStar;

    public override void Destroy()
    {
        level.DestroyAlly();
        
        onDestroy = () => {
            level.bloodCells.Remove(this);
            Instantiate(brokenStar, transform.position, brokenStar.transform.rotation);
            Destroy(gameObject);
        };
        
        base.Destroy();
    }
}

