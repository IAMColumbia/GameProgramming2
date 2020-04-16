using UnityEngine;
using System.Collections;
using System;

public class ShotSprite : MonoBehaviour {

    private Vector3 moveTranslation;
    SpriteRenderer spriteRenderer;

    public Vector2 Direction;
    public float Speed;

    public enum ShotState { Start,  Shooting, Collided , Done };
    public ShotState State;

    // Use this for initialization
    void Start () {
        SetupShot();
	}

    private void SetupShot()
    {
        Util.GetComponentIfNull<SpriteRenderer>(this, ref spriteRenderer);
        this.State = ShotState.Start;
    }

    // Update is called once per frame
    void Update () {

        if (this.State == ShotState.Shooting)
        {
            //move the shot
            this.moveTranslation = new Vector3(this.Direction.x, this.Direction.y) * Time.deltaTime * this.Speed;
            this.transform.position = new Vector3(this.transform.position.x + this.moveTranslation.x,
                                                  this.transform.position.y + this.moveTranslation.y);
        }

        //check off screen
        if(Util.IsOnScreen(this.gameObject) == false)
        {
            this.State = ShotState.Done;
        }
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "Player")
        {
            
        }

        if (coll.gameObject.tag == "Ghost")
        {

        }
    }
}
