
using UnityEngine;
using UnityEngine.EventSystems;

public enum BirdState
{
    Waiting,
    BeforeShoot,
    AfterShoot,
    WaitToDie
}

public class Bird : MonoBehaviour
{
    private bool isMouseDown = false;
    private float maxDistance = 2.1f;

    public float flySpeed = 15f;

    protected Rigidbody2D rgd;

    public bool isFlying = true;

    public BirdState state = BirdState.BeforeShoot;

    protected TrailRenderer trail;

    public bool isHaveUsedSkill = false;

    //private Collider2D collider;

    private void Awake()
    {
        trail = GetComponent<TrailRenderer>();
        //collider = GetComponent<Collider2D>();
    }

    void Start()
    {
        rgd = GetComponent<Rigidbody2D>();
        rgd.bodyType = RigidbodyType2D.Static;
    }

    void Update()
    {
        switch(state)
        {
            case BirdState.Waiting:
                WaitControl();
                break;

            case BirdState.BeforeShoot:
                MoveControl();
                break;

            case BirdState.AfterShoot:
                StopControl();
                SkillControl();
                break;

            case BirdState.WaitToDie:

                break;
        }
    }

    private void WaitControl()
    {

    }

    private void OnMouseDown()
    {
        if (state == BirdState.BeforeShoot && EventSystem.current.IsPointerOverGameObject()==false )
        {
            isMouseDown = true;
            Slingshot.Instance.StartDraw(transform);
            AudioManager.Instance.PlayBirdSelect(transform.position);
        }
    }

    private void OnMouseUp()
    {
        if (state == BirdState.BeforeShoot && EventSystem.current.IsPointerOverGameObject() == false)
        {
            isMouseDown = false;
            Slingshot.Instance.EndDraw();
            Fly();
        }
    }

    private void MoveControl()
    {
        if (isMouseDown)
        {
            transform.position = GetMousePosition();
        }
    }

    private Vector3 GetMousePosition()
    {
        Vector3 centerPosition = Slingshot.Instance.getCenterPosition();
        Vector3 mp = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mp.z = 0;
        Vector3 mouseDir = mp -centerPosition;

        float distance = mouseDir.magnitude;

        if (distance > maxDistance)
        {
            mp = centerPosition + mouseDir.normalized * maxDistance;
        }

        return mp;
    }

    private void Fly()
    {
        rgd.bodyType = RigidbodyType2D.Dynamic;

        rgd.velocity = (Slingshot.Instance.getCenterPosition() - transform.position).normalized * flySpeed;

        Invoke(nameof(SetState), 0.25f);

        AudioManager.Instance.PlayBirdFlying(transform.position);
    }

    private void SetState()
    {
        state = BirdState.AfterShoot;
    }

    public void GoStage(Vector3 position)
    {
        state = BirdState.BeforeShoot;
        transform.position = position;
    }

    private void StopControl()
    {
        if (rgd.velocity.magnitude < 0.1f)
        {
            state = BirdState.WaitToDie;
            trail.enabled = false;
            trail.Clear();
            Invoke("LoadNextBird", 1f);
        }
    }

    private void SkillControl()
    {
        if(isHaveUsedSkill)
        {
            return;
        }

        if (isFlying == true && Input.GetMouseButtonDown(0)) 
        {
            FlyingSkill();
        }

        if(Input.GetMouseButtonDown(0))
        {
            FullTimeSkill();
        }
    }

    protected virtual void FlyingSkill()
    {
        isHaveUsedSkill = true;
    }

    protected virtual void FullTimeSkill()
    {
        isHaveUsedSkill = true;
    }

    protected void LoadNextBird()
    {
        GameObject.Instantiate(Resources.Load("Boom1"), transform.position, Quaternion.identity);
        Destroy(gameObject);
        GameManager.Instance.LoadNextBird();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(state==BirdState.AfterShoot)
        {
            isFlying = false;
        }
        if (state == BirdState.AfterShoot && collision.relativeVelocity.magnitude > 6)
        {
            AudioManager.Instance.PlayBirdCollison(transform.position);
        }
    }
}
