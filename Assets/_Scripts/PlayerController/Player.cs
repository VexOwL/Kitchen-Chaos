using System;
using UnityEngine;

public class Player : MonoBehaviour, IKitchenObjectParent
{
    [SerializeField] private float _moveSpeed = 7f;
    [SerializeField] private GameInput _gameInput;
    [SerializeField] private Transform _kitchenObjectPosition;
    [SerializeField] private LayerMask _countersLayerMask;
    private Counter _selectedCounter;
    private KitchenObject _kitchenObject;
    private float _rotateSpeed = 10f, _playerRadius = 0.7f, _playerHeight = 2, _interactDistance = 2f, _moveDistance;
    private bool _canMove = true;
    private Vector2 _inputVector;
    private Vector3 _moveDirection, _moveDirX, _moveDirZ;
    public bool IsWalking { get; private set; }
    public static Player Instance { get; private set; }
    public event EventHandler<OnSelectedCounterChangedEventArgs> OnSelectedCounterChanged;
    public class OnSelectedCounterChangedEventArgs : EventArgs
    {
        public Counter selectedCounter;
    }

    private void Awake()
    {
        if (Instance != null)
            Debug.Log("There is more than one Player instance");

        Instance = this;
    }

    private void Start()
    {
        _gameInput.OnInteractAction += GameInput_OnInteractAction; //"Подписка на ивент из GameInput"
        _gameInput.OnInteractAlternateAction += GameInput_OnInteractAlternateAction;
    }


    private void FixedUpdate()
    {
        HandleMovement();
    }

    private void Update()
    {
        HandleInteractions();
    }

    private void GameInput_OnInteractAction(object sender, EventArgs eventArgs) //реализует ивент из GameInput (при его вызове происходит это)
    {
        if (_selectedCounter != null)
            _selectedCounter.Interact(this);
    }
    
    private void GameInput_OnInteractAlternateAction(object sender, EventArgs eventArgs)
    {
        if (_selectedCounter != null)
            _selectedCounter.InteractAlternate(this);
    }

    private void HandleInteractions()
    {
        Vector3 originOffSet = new Vector3(0, 0.5f, 0);
        
        if (Physics.Raycast(transform.position + originOffSet, transform.forward, out RaycastHit raycastHit, _interactDistance, _countersLayerMask))
        {
            if (raycastHit.transform.TryGetComponent(out Counter counter))
            {
                if (counter != _selectedCounter)
                {
                    //_selectedCounter = counter;
                    SetSelectedCounter(counter);
                }
            }
            else
                SetSelectedCounter(null);
        }
        else
            SetSelectedCounter(null);
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
            _canMove = _moveDirection.x != 0 && IsCanMove(_moveDirX);

            if (_canMove)
                _moveDirection = _moveDirX;
            else
            {
                _canMove = _moveDirection.z != 0 && IsCanMove(_moveDirZ);

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

    private void SetSelectedCounter(Counter selectedCounter)
    {
        this._selectedCounter = selectedCounter;

        OnSelectedCounterChanged?.Invoke(this, new OnSelectedCounterChangedEventArgs { selectedCounter = _selectedCounter });
    }

    public Transform GetKitchenObjectFollowTransform()
    {
        return _kitchenObjectPosition;
    }

    public void SetKitchenObject(KitchenObject kitchenObject)
    {
        _kitchenObject = kitchenObject;
    }

    public KitchenObject GetKitchenObject()
    {
        return _kitchenObject;
    }

    public void ClearKitchenObject()
    {
        _kitchenObject = null;
    }

    public bool HasKitchenObject()
    {
        return _kitchenObject != null; 
    }
}
