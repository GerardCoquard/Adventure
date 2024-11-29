using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Dice3D : MonoBehaviour
{
    public Camera cam;
    public Rigidbody rb;
    public float upImpulse;
    public float rotationSpeed;
    public float checkTimeStop;
    private float timeStopped;
    private Quaternion lastRotation;
    private Vector3 startPos;

    public Action OnDiceStopped;

    private void Start()
    {
        cam.transform.SetParent(null);
        cam.gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        StartCoroutine(Roll());

        OnDiceStopped += () => Debug.Log("STOPPED!");
    }

    IEnumerator Roll()
    {
        cam.gameObject.SetActive(true);
        rb.AddForce(Vector3.up * upImpulse, ForceMode.Impulse);
        float x = Random.Range(-1f, 1f);
        float z = Random.Range(-1f, 1f);
        rb.AddTorque(new Vector3(x,0,z).normalized * rotationSpeed, ForceMode.Acceleration);
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
    }

    public void SetStartPos(Vector3 pos)
    {
        startPos = pos;
        transform.position = pos;
    }
}
