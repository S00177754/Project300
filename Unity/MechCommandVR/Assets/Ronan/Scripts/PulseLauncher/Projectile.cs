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
        
    }

    private void Update()
    {
        if (isLaunched)
        {
            if(despawnTimer >= DespawnTime)
            {
                Destroy(this.gameObject);
            }
            else
            {
                despawnTimer += (Time.deltaTime * 1);
            }
        }
    }

    public void Launch(PulseLauncher blaster)
    {
        transform.position = blaster.LaunchPoint.position;

        rigidbody.AddRelativeForce(Vector3.forward * LaunchForce, ForceMode.Impulse);

        isLaunched = true;
    }

}
