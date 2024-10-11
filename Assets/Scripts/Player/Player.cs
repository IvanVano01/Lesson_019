using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class Player : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _speedRotation;

    private InputHandler _inputHandler;
    private CharacterController _characterController;
    private CharacterMover _characterMover;

    private void Awake()
    {
        _characterController = GetComponent<CharacterController>();

        _inputHandler = new InputHandler();
        _characterMover = new CharacterMover(_speed, _speedRotation,_inputHandler,_characterController,this.gameObject);
    }

    private void Update()
    {
        _inputHandler.Update();
        _characterMover.Update();
    }
}
