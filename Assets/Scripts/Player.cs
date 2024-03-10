using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 7f;
    [SerializeField] private GameInput gameInput;
    public bool IsWalking {get; private set;}
    private float _rotateSpeed = 10f, _playerRadius = 0.7f, _playerHeight = 2, moveDistance;
    private bool canMove = true;
    private Vector2 inputVector;
    private Vector3 moveDirection, moveDirX, moveDirZ;

    private void FixedUpdate()
    {
        inputVector = gameInput.GetMovementVectorNormalized();
        moveDirection = new Vector3(inputVector.x, 0, inputVector.y);
        moveDirX = new Vector3(moveDirection.x, 0, 0);
        moveDirZ = new Vector3(0, 0, moveDirection.z);
        moveDistance = _moveSpeed * Time.fixedDeltaTime;

        canMove = IsCanMove(moveDirection);

        if(!canMove)
        {
            canMove = IsCanMove(moveDirX);

            if(canMove)
                moveDirection = moveDirX;
            else
            {
                canMove = IsCanMove(moveDirZ);

                if(canMove)
                    moveDirection = moveDirZ;
            }
        }
        
        if (canMove)
        {
            transform.position += moveDirection * moveDistance;
        }

        IsWalking = moveDirection != Vector3.zero;

        transform.forward = Vector3.Slerp(transform.forward, moveDirection, _rotateSpeed * Time.fixedDeltaTime);
    }

    private bool IsCanMove(Vector3 moveDirection)
    {
        bool canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * _playerHeight, _playerRadius, moveDirection, moveDistance);

        return canMove;
    }
}
