using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Dice3D : MonoBehaviour
{
    public Camera cam;
    [SerializeField] private Rigidbody _rb;
    [SerializeField] private MeshCollider _meshCollider;
    [SerializeField] private MeshFilter _meshFilter;
    [SerializeField] private float _upImpulse;
    [SerializeField] private float _rotationSpeed;
    [SerializeField] private float _checkTimeStop;
    [SerializeField] private float _cameraDistance;
    
    private float _timeStopped;
    private Quaternion _lastRotation;
    private Vector3 _startPos;
    private Dice _diceType = Dice.D8;
    private bool _beingUsed;

    public Action OnDiceStopped;

    private void Awake()
    {
        _startPos = transform.position;
        cam.transform.SetParent(null);
        cam.gameObject.SetActive(false);
    }

    private void Update()
    {
        cam.transform.position = new Vector3(transform.position.x, transform.position.y + _cameraDistance, transform.position.z);
    }

    public void RollDice()
    {
        SetStartPos(_startPos);
        gameObject.SetActive(true);
        StartCoroutine(Roll());
    }

    public Texture GetRenderTexture()
    {
        return cam.targetTexture;
    }

    public void SetDiceMesh(Mesh mesh)
    {
        _meshCollider.sharedMesh = mesh;
        _meshFilter.mesh = mesh;
    }

    public Dice GetDiceType()
    {
        return _diceType;
    }
    
    public void SetDiceType(Dice dice)
    {
        _diceType = dice;
    }

    IEnumerator Roll()
    {
        cam.gameObject.SetActive(true);
        _timeStopped = 0;
        _rb.AddForce(Vector3.up * _upImpulse, ForceMode.Impulse);
        float x = Random.Range(-1f, 1f);
        float z = Random.Range(-1f, 1f);
        _rb.AddTorque(new Vector3(x,0,z).normalized * _rotationSpeed, ForceMode.Acceleration);

        yield return new WaitForSeconds(0.2f);
        
        while (!StoppedRotating())
        {
            yield return null;
        }
        
        OnDiceStopped?.Invoke();
        
    }

    private bool StoppedRotating()
    {
        if (_timeStopped >= _checkTimeStop)
            return true;

        if (_lastRotation == transform.rotation)
            _timeStopped += Time.deltaTime;
        else
            _timeStopped = 0;

        _lastRotation = transform.rotation;

        return false;
    }

    public void DisableDice()
    {
        cam.gameObject.SetActive(false);
        gameObject.SetActive(false);
        OnDiceStopped = null;
        _beingUsed = false;
    }

    public bool GetBeingUsed()
    {
        return _beingUsed;
    }
    
    public void SetBeingUsed()
    {
        _beingUsed = true;
    }

    public void SetStartPos(Vector3 pos)
    {
        transform.position = pos;
    }
}
