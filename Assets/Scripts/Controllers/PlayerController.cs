using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerController : MonoBehaviour
{
    [FormerlySerializedAs("mSpeed")] [SerializeField]
    float _speed = 10.0f;

    private Vector3 _destPos;
    void Start()
    {
        Managers.Input.MouseAction -= OnMouseClicked;
        Managers.Input.MouseAction += OnMouseClicked;
    }

    public enum PlayerState
    {
        Idle,
        Die,
        Move,
    }

    private PlayerState _state = PlayerState.Idle;

    void UpdateDie()
    {
        
    }

    void UpdateMove()
    {
        Vector3 dir = _destPos - transform.position;
        if (dir.magnitude < 0.001f)
        {
            _state = PlayerState.Idle;
        }
        else
        {
            var moveDist = Mathf.Clamp(_speed * Time.deltaTime, 0, dir.magnitude);
            transform.position += dir.normalized * moveDist;
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dir), 20 * Time.deltaTime);
        }
        
        // 애니메이션
        Animator anim = GetComponent<Animator>();
        // 현재 게임 상태에 대한 정보를 넘겨준다
        anim.SetFloat("speed", _speed);
    }

    void UpdateIdle()
    {
        Animator anim = GetComponent<Animator>();
        anim.SetFloat("speed", 0);
    }
    void Update()
    {
        switch (_state)
        {
            case PlayerState.Die:
                UpdateDie();
                break;
            case PlayerState.Idle:
                UpdateIdle();
                break;
            case PlayerState.Move:
                UpdateMove();
                break;
        }

    }
    
    void OnMouseClicked(Define.MouseEvent evt)
    {
        if (_state == PlayerState.Die) return;
        // if (evt != Define.MouseEvent.Click) return;
        
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            
        // Vector3 mousePos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.nearClipPlane));
        // Vector3 dir = mousePos - Camera.main.transform.position;
        // dir = dir.normalized;

        Debug.DrawRay(Camera.main.transform.position, ray.direction * 100, Color.red, 1f);

        LayerMask mask = LayerMask.GetMask("Monster") | LayerMask.GetMask("Wall");
        // int mask = (1 << 8) | (1 << 9);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 100f, mask))
        {
            _destPos = hit.point;
            _state = PlayerState.Move;
        }
    }
    
    // void OnKeyboard()
    // {
    //     if (Input.GetKey(KeyCode.I))
    //     {
    //         transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.forward), 0.1f);
    //         // transform.rotation = Quaternion.LookRotation(Vector3.forward);
    //         // transform.Translate(Vector3.forward * (Time.deltaTime * mSpeed)); // local
    //         transform.position += Vector3.forward * (Time.deltaTime * _speed);
    //     }
    //     if (Input.GetKey(KeyCode.K))
    //     {
    //         transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.back), 0.1f);
    //         // transform.rotation = Quaternion.LookRotation(Vector3.back);
    //         // transform.Translate(Vector3.forward * (Time.deltaTime * mSpeed));
    //         transform.position += Vector3.back * (Time.deltaTime * _speed);
    //     }
    //     if (Input.GetKey(KeyCode.J))
    //     {
    //         transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.left), 0.1f);
    //         // transform.rotation = Quaternion.LookRotation(Vector3.left);
    //         // transform.Translate(Vector3.forward * (Time.deltaTime * mSpeed));
    //         transform.position += Vector3.left * (Time.deltaTime * _speed);
    //     }
    //     if (Input.GetKey(KeyCode.L))
    //     {
    //         transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.right), 0.1f);
    //         // transform.rotation = Quaternion.LookRotation(Vector3.right);
    //         // transform.Translate(Vector3.forward * (Time.deltaTime * mSpeed));
    //         transform.position += Vector3.right * (Time.deltaTime * _speed);
    //     }
    //
    //     _moveToDest = false;
    // }
}
