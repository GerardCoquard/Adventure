using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Dice3D : MonoBehaviour
{
    public Camera cam;
    public Rigidbody rb;
    public MeshCollider meshCollider;
    public MeshFilter meshFilter;
    public float upImpulse;
    public float rotationSpeed;
    public float checkTimeStop;
    public float cameraDistance;
    private float timeStopped;
    private Quaternion lastRotation;
    private Vector3 startPos;
    [SerializeField] Dice _diceType;
    private bool beingUsed = false;

    public Action OnDiceStopped;

    private void Start()
    {
        startPos = transform.position;
        cam.transform.SetParent(null);
        cam.gameObject.SetActive(false);
    }

    private void Update()
    {
        cam.transform.position = new Vector3(transform.position.x, transform.position.y + cameraDistance, transform.position.z);
    }

    public void RollDice()
    {
        SetStartPos(startPos);
        gameObject.SetActive(true);
        StartCoroutine(Roll());
    }

    public Texture GetRenderTexture()
    {
        return cam.targetTexture;
    }

    public void SetDiceMesh(Mesh mesh)
    {
        meshCollider.sharedMesh = mesh;
        meshFilter.mesh = mesh;
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
        timeStopped = 0;
        rb.AddForce(Vector3.up * upImpulse, ForceMode.Impulse);
        float x = Random.Range(-1f, 1f);
        float z = Random.Range(-1f, 1f);
        rb.AddTorque(new Vector3(x,0,z).normalized * rotationSpeed, ForceMode.Acceleration);

        yield return new WaitForSeconds(0.2f);
        
        while (!StoppedRotating())
        {
            yield return null;
        }
        
        OnDiceStopped?.Invoke();
        
    }

    private bool StoppedRotating()
    {
        if (timeStopped >= checkTimeStop)
            return true;

        if (lastRotation == transform.rotation)
            timeStopped += Time.deltaTime;
        else
            timeStopped = 0;

        lastRotation = transform.rotation;

        return false;
    }

    public void DisableDice()
    {
        cam.gameObject.SetActive(false);
        gameObject.SetActive(false);
        OnDiceStopped = null;
        beingUsed = false;
    }

    public bool GetBeingUsed()
    {
        return beingUsed;
    }
    
    public void SetBeingUsed()
    {
        beingUsed = true;
    }

    public void SetStartPos(Vector3 pos)
    {
        transform.position = pos;
    }
}
