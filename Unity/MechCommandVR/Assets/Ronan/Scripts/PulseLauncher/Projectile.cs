using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageOutput
{
    float DamageAmount { get; set; }
}

public class Projectile : MonoBehaviour, IDamageOutput
{
    public float DespawnTime = 5f;
    public float LaunchForce = 5f;
    public float Damage = 0.1f;

    public float DamageAmount { get { return Damage; } set { Damage = value; } }

    private Rigidbody rigidbody = null;
    private float despawnTimer = 0f;
    private bool isLaunched = false;


    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        Destroy(this);
    }

    private void Update()
    {
        if (isLaunched)
        {
            if(despawnTimer >= DespawnTime)
            {
                Destroy(this);
            }
            else
            {
                despawnTimer += (Time.deltaTime);
            }
        }
    }

    public void Launch(PulseLauncher blaster)
    {
        transform.position = blaster.LaunchPoint.position;
        transform.rotation = blaster.transform.rotation;

        rigidbody.AddRelativeForce(Vector3.forward * 10, ForceMode.Impulse);

        isLaunched = true;
    }

}
