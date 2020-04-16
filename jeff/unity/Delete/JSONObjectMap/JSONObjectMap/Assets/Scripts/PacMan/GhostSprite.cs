using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GhostSprite : MonoBehaviour {

    public GameObject PacMan;
    private Player PacManPlayer;

    public GhostState State;

    protected Ghost ghost;
    public float Speed;

    private Vector3 moveTranslation;
    SpriteRenderer spriteRenderer;
    public Sprite EvadeTexture, NormalTexture;

    protected Vector3 viewPoint;

    void Awake()
    {
        this.ghost = new Ghost();
    }

    // Use this for initialization
    void Start() {
        this.SetupGhost();
    }


    void OnTriggerEnter2D(Collider2D coll) {
        if (coll.gameObject.tag == "Player")
        {
            if (this.State == GhostState.Dead)
            {
                //Debug.Log(string.Format("{0} triggerEnter with {1} already dead change to Roving", this, coll.ToString()));
                //this.State = GhostState.Roving;

            }
            else
            {
                Debug.Log(string.Format("{0} triggerEnter with {1} change to dead", this, coll.ToString()));
                this.State = GhostState.Dead;
                this.ghost.State = GhostState.Dead;

            }
        }
    }

    void OnDrawGizmos() {
        Gizmos.DrawLine(this.transform.position, this.viewPoint);
    }

    public void SetupGhost()
    {
        Util.GetComponentIfNull<SpriteRenderer>(this, ref spriteRenderer);
        PacManPlayer = PacMan.GetComponentInParent<Player>();

        this.ghost.Attach(PacManPlayer.PacMan);
        this.State = this.ghost.State;

        viewPoint = new Vector3(this.transform.position.x + (this.ghost.Direction.x * 5), this.transform.position.y + (this.ghost.Direction.y * 5));
        this.spriteRenderer = this.GetComponent<SpriteRenderer>();
        //NormalTexture = Resources.Load<Sprite>("Assets//RedGhost");
        //EvadeTexture = Resources.Load<Sprite>("Assets//GhostHit");
    }

    public void DetachFromPacMan()
    {
        this.ghost.Detach(this.PacManPlayer.PacMan);
    }

    // Update is called once per frame
    void Update() {
        this.transform.position = Util.BounceOffWalls(this.transform.position, GetComponent<Renderer>().bounds.size.x - 1, GetComponent<Renderer>().bounds.size.y - 1, ref this.ghost.Direction);
        
        switch (this.State)
        {
            case GhostState.Evading:
                this.ChangeGhostTectureToBlue();
                //Evade if close rove if safe
                //Hard coded safe distance of 2
                if ((this.transform.position - PacManPlayer.transform.position).magnitude < 2)
                {
                    UpdateGhostEvading();
                }
                else
                {
                    UpdateGhostRoving();
                }
                
                break;

            case GhostState.Roving:
                //set color to normal
                this.ChangeGhostTextureToNormal();
                this.UpdateGhostRoving();
                break;

            case GhostState.Chasing:
                //set color to normal
                this.ChangeGhostTextureToNormal();
                this.UpadateGhostChasing();

                break;
        }

        //move the ghost
        this.moveTranslation = new Vector3(this.ghost.Direction.x, this.ghost.Direction.y) * Time.deltaTime * this.Speed;
        this.transform.position = new Vector3(this.transform.position.x + this.moveTranslation.x,
                                              this.transform.position.y + this.moveTranslation.y);

        if (this.State != this.ghost.State)		//only set value if state is changed
        {
            //this.ghost.State = this.State;
            this.State = this.ghost.State;
        }
    }

    protected virtual void ChangeGhostTextureToNormal()
    {
        //Change texture if Chasing
        //this.spriteRenderer.color = Color.white;
        this.spriteRenderer.sprite = NormalTexture;
        
    }

    protected virtual void ChangeGhostTectureToBlue()
    {
        //Change texture if evading
        this.spriteRenderer.sprite = EvadeTexture;
    }

    private void UpdateGhostRoving()
    {
        viewPoint = new Vector3(this.transform.position.x + (this.ghost.Direction.x * 5), this.transform.position.y + (this.ghost.Direction.y * 5));
        RaycastHit2D[] hit = Physics2D.LinecastAll(this.transform.position, this.viewPoint);
        Debug.DrawLine(this.transform.position, this.viewPoint, Color.green);

        for (int i = 0; i < hit.Length; i++)
            if (hit[i].rigidbody != null)
            {
                if (hit[i].rigidbody.tag == "Player")
                {
                    Debug.Log(string.Format("{0} saw {1} changed to chasing", this, hit[i].rigidbody.tag));
                    this.ghost.State = GhostState.Chasing;
                }
            }
    }

    private void UpdateGhostEvading()
    {
        Vector3 heading = PacMan.transform.position - this.transform.position;
        heading = Vector3.Normalize(heading);
        this.ghost.Direction = -heading;
    }

    private void UpadateGhostChasing()
    {
        Vector3 heading = PacMan.transform.position - this.transform.position;
        heading = Vector3.Normalize(heading);
        this.ghost.Direction = heading;
    }


}