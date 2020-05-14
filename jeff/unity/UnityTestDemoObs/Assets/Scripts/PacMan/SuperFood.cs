using UnityEngine;
using System.Collections;

public class SuperFood : Food {


    public override void Hit(GameObject p)
    {
        Player player = p.GetComponentInParent<Player>();
        player.PowerUp();
        base.Hit(p);
    }    

}
