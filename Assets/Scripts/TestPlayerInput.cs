using UnityEngine;

public class TestPlayerInput : MonoBehaviour,IPlayerActivity
{
    Vector2 moveInput;
    Vector2 lookInput;
    bool isAction;

    void Update()
    {
        moveInput = new Vector2(
            Input.GetAxisRaw("Horizontal"),
            Input.GetAxisRaw("Vertical")
        );

        lookInput = new Vector2(
            Input.GetAxis("Mouse X"),
            Input.GetAxis("Mouse Y")
        );

        isAction = Input.GetMouseButton(0);
    }

    public Vector2 MoveInput => moveInput;
    public Vector2 LookInput => lookInput;
    public bool IsAction => isAction;
}
