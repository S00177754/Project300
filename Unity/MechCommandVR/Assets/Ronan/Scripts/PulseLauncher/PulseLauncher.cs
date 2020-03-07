using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class PulseLauncher : MonoBehaviour
{
    [Header("SteamVR")]
    public SteamVR_Action_Boolean FireAction = null;

    [Header("Blaster Details")]
    public Transform LaunchPoint;

    [Header("Ammo Clip")]
    public List<GameObject> AmmoList;
    public PulseClip Clip;
    public bool InfiniteAmmo = false;
    public GameObject InfiniteDefaultAmmo;

    private SteamVR_Behaviour_Pose handPose = null;

    private void Awake()
    {
        Clip = new PulseClip();
        handPose = GetComponentInParent<SteamVR_Behaviour_Pose>();

        if (AmmoList != null)
            AmmoList.ForEach(a => Clip.AddToClip(a));
    }

    private void Update()
    {
        if (FireAction.GetStateDown(handPose.inputSource))
            ShootProjectile();
    }

    public void ShootProjectile()
    {
        GameObject projectile;

        if (!InfiniteAmmo)
            projectile = Clip.TakeAmmo();
        else
            projectile = InfiniteDefaultAmmo;

        if (projectile != null)
        {
            projectile = Instantiate(projectile, LaunchPoint.position,LaunchPoint.rotation);
            projectile.GetComponent<Projectile>().Launch(this);
        }
    }
}

public class PulseClip
{
    public Queue<GameObject> Ammo;
    public int ClipCapacity = 50;

    public PulseClip()
    {
        Ammo = new Queue<GameObject>();
    }

    public bool AddToClip(GameObject ammunition)
    {
        Component projectileComponent;

        if (Ammo.Count >= ClipCapacity)
        {
            return false;
        }
        
        if(ammunition.TryGetComponent(typeof(Projectile),out projectileComponent))
        {
            //Add projectile component
            Ammo.Enqueue(ammunition);
            return true;
        }

        return false;
    }

    public GameObject TakeAmmo()
    {
        if(Ammo.Count <= 0)
        {
            return null;
        }
        else
        {
            return Ammo.Dequeue();
        }
    }

    public List<GameObject> EmptyClip()
    {
        List<GameObject> Clip = new List<GameObject>();
        foreach (var item in Ammo)
        {
            Clip.Add(item);
            Ammo.Dequeue();
        }
        return Clip;
    }
    
}
