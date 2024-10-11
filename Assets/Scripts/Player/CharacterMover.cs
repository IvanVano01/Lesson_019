using UnityEngine;

public class CharacterMover 
{
    private float _speed;
    private float _speedRotation;
    private float _deadZone = 0.01f;

    private InputHandler  _inputHandler;
    private CharacterController _characterController;
    private GameObject _character;

    public CharacterMover(float speed, float speedRotation, InputHandler inputHandler, CharacterController characterController, GameObject character)
    {
        _speed = speed;
        _speedRotation = speedRotation;
        _inputHandler = inputHandler;
        _characterController = characterController;
        _character = character;
    }    

    public void Update()
    {
        Vector3 direction = _inputHandler.InputDirection;

        _characterController.Move(direction * _speed * Time.deltaTime);

        if(direction.magnitude> _deadZone)
            Rotate(direction);
        
    }

    private void Rotate(Vector3 direction)
    {
        Quaternion lookRotation = Quaternion.LookRotation(direction.normalized);

        _character.transform.rotation = Quaternion.RotateTowards(_character.transform.rotation,lookRotation, _speedRotation * Time.deltaTime);

    }
}
