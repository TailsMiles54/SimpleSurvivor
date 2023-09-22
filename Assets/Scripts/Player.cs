using Sirenix.OdinInspector;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private CharacterController _characterController;
    
    [SerializeField] private int _speed = 1;
    [SerializeField] private int _speed_rotation = 3;

    void Update()
    {
        transform.Rotate(0, Input.GetAxis("Horizontal") * _speed_rotation, 0);
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        
        float curSpeed = _speed * Input.GetAxis("Vertical") * (Input.GetKey(KeyCode.LeftShift) ? 3 : 1);

        _characterController.SimpleMove(forward * curSpeed);
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.gameObject.name);
    }

    [Button("PickUp")]
    private void PickUp()
    {
        GetComponent<Animator>().SetTrigger("PickUpTrigger");
    }
}