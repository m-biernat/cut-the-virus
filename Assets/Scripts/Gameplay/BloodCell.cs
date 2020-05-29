using UnityEngine;

public class BloodCell : Destructible
{
    public GameObject brokenStar;

    // Method executes when BloodCell is destroyed
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

