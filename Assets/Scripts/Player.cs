using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 7f;
    [SerializeField] private GameInput _gameInput;
    [SerializeField] private LayerMask _countersLayerMask;
    private float _rotateSpeed = 10f, _playerRadius = 0.7f, _playerHeight = 2, _interactDistance = 2f, _moveDistance;
    private bool _canMove = true;
    private Vector2 _inputVector;
    private Vector3 _moveDirection, _moveDirX, _moveDirZ;
    //private Vector3 _lookDirection;
    public bool IsWalking { get; private set; }

    private void FixedUpdate()
    {
        HandleMovement();
        HandleInteractions();
    }

    private void HandleInteractions()
    {
        if(Physics.Raycast(transform.position, transform.forward, out RaycastHit raycastHit, _interactDistance, _countersLayerMask))
        {
            if(raycastHit.transform.TryGetComponent(out ClearCounter clearCounter))
                clearCounter.Interact();
        }
    }

    private void HandleMovement()
    {
        _inputVector = _gameInput.GetMovementVectorNormalized();
        _moveDirection = new Vector3(_inputVector.x, 0, _inputVector.y);
        _moveDirX = new Vector3(_moveDirection.x, 0, 0);
        _moveDirZ = new Vector3(0, 0, _moveDirection.z);
        _moveDistance = _moveSpeed * Time.fixedDeltaTime;

        _canMove = IsCanMove(_moveDirection);

        if (!_canMove)
        {
            _canMove = IsCanMove(_moveDirX);

            if (_canMove)
                _moveDirection = _moveDirX;
            else
            {
                _canMove = IsCanMove(_moveDirZ);

                if (_canMove)
                    _moveDirection = _moveDirZ;
            }
        }

        if (_canMove)
        {
            transform.position += _moveDirection * _moveDistance;
        }

        IsWalking = _moveDirection != Vector3.zero;

        transform.forward = Vector3.Slerp(transform.forward, _moveDirection, _rotateSpeed * Time.fixedDeltaTime);
    }

    private bool IsCanMove(Vector3 moveDirection)
    {
        bool canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * _playerHeight, _playerRadius, moveDirection, _moveDistance);

        return canMove;
    }
}
