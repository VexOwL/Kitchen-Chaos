using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 7f;
    private float _rotateSpeed = 10f;

    private void Update()
    {
        Vector2 inputVector = new Vector2();
        inputVector = inputVector.normalized;

        if(Input.GetKey(KeyCode.W))
            inputVector.y = +1;

        if(Input.GetKey(KeyCode.S))
            inputVector.y = -1;

        if(Input.GetKey(KeyCode.D))
            inputVector.x = +1;

        if(Input.GetKey(KeyCode.A))
            inputVector.x = -1;

        Vector3 moveDirection = new Vector3(inputVector.x, 0, inputVector.y);
        transform.position += moveDirection * _moveSpeed * Time.deltaTime;

        transform.forward = Vector3.Slerp(transform.forward, moveDirection, _rotateSpeed * Time.deltaTime);
    }
}
