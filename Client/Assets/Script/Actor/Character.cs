using UnityEngine;

public class Character : Actor
{
    public bool IsMy { get; private set; }

    public void Awake()
    {
        stateMachine = new StateMachine(this);
        actionMachine = new ActionMachine(this);

        ChangeState(BodyType.Up, StateType.Idle);
        ChangeState(BodyType.Low, StateType.Idle);

        tag = "Player";

        RotationModel(bodyTransform, transform);
    }

    public void FlagIsMyActor(bool isMy)
    {
        IsMy = isMy;
    }

    private void Update()
    {
        SetCharacterBodyRotaion();

        stateMachine.Update();
    }


    [SerializeField]
    private Transform bodyTransform;
    [SerializeField]
    private Transform characterTransform;

    private float angle;
    private Vector2 mouse;
    private Vector3 leftVector = Vector3.one;
    private Vector3 rightVector = new Vector3(-1, 1, 1); 

    public void RotationModel(Transform bodyTransform, Transform characterTransform)
    {
        this.bodyTransform = bodyTransform;
        this.characterTransform = characterTransform;
    }

    public void SetCharacterBodyRotaion()
    {
        mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        characterTransform.localScale = (mouse.x < characterTransform.transform.position.x) ? leftVector : rightVector;

        float maxRotation = 40;
        float minRotation = -60;
        float x = 0;
        float y = 0;
        bool isRight = characterTransform.localScale.x == -1;
        Vector2 bodyPos = characterTransform.position;

        x = isRight ? (mouse.x - bodyPos.x) : (bodyPos.x - mouse.x);
        y = isRight ? (mouse.y - bodyPos.y) : (bodyPos.y - mouse.y);

        angle = Mathf.Clamp((Mathf.Atan2(y, x) * Mathf.Rad2Deg), minRotation, maxRotation);
        bodyTransform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }
}
