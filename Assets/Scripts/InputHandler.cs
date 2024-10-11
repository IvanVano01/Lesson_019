using UnityEngine;

public class InputHandler
{
    private const string Horizontal = "Horizontal";
    private const string Vertical = "Vertical";

    private float _horizontalAxisX;
    private float _horizontalAxisZ;

    public Vector3 InputDirection { get; private set; }

    public void Update()
    {
        _horizontalAxisX = Input.GetAxisRaw(Horizontal);
        _horizontalAxisZ = Input.GetAxisRaw(Vertical);

        InputDirection = new Vector3(-_horizontalAxisZ, 0, _horizontalAxisX);
    }
}
