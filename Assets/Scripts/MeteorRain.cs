using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MeteorRain : MonoBehaviour {

    public GameObject explosion;
    private float SpinningSpeed;
    Animator animator;

    private void Awake()
    {
        animator = this.GetComponent<Animator>();
        SpinningSpeed = Random.Range(-1f, 1f);
        animator.SetFloat("SpeedMultiplier", SpinningSpeed);        
    }

    //START GRAVITY SCALE
    public float gravityScale = 1.0f;

    // Global Gravity doesn't appear in the inspector. Modify it here in the code
    // (or via scripting) to define a different default gravity for all objects.

    public static float globalGravity = -9.81f;

    Rigidbody m_rb;

    void OnEnable()
    {
        m_rb = GetComponent<Rigidbody>();
        m_rb.useGravity = false;
    }

    void FixedUpdate()
    {
        Vector3 gravity = globalGravity * gravityScale * Vector3.up;
        m_rb.AddForce(gravity, ForceMode.Acceleration);
    }
    //END GRAVITY SCALE

    public void OnTriggerEnter()
    {
        GameObject.Destroy(gameObject);
    }

    public void Destroy()
    {
        Instantiate(explosion, new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, 0), Quaternion.identity);
        Destroy(gameObject);
    }

}
