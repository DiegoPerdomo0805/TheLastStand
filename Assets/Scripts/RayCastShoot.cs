using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayCastShoot : MonoBehaviour
{
    // Health
    public float maxHealth = 20f;
    public float Health;

    // Foreign scripts and health management and decrease
    private Edge edgeScript;
    private MovePlayer movePlayerScript;
    private float OriginalSpeed;
    private float OriginalSpeed2;
    public float speedReductionFactor = 0.8f;
    public GameObject mainCamera;
    private float originalEdge;

    // Apuntar
    public Vector3 Aim;

    // Variables de arma
    public float shootCooldown = 0.25f;
    private float shootTimer;
    public int Ammo;
    public int Magazines; // Cantidad de cartuchos, sujeto a balanceo
    public int MagSize = 8; // Cartucho para una PPD-34
    public int maxMag = 6; // Máxima cantidad de cartuchos 
    public GameObject bulletPrefab;

    // Variables de granadas y explosivos

    public float expCooldown = 3f;
    private float expTimer;
    public int Grenades;
    public int GCarry = 4; // valor arbitrario, sujeto a balanceo
    public GameObject grenadePrefab;
    public int throwForce = 20;

    // TextMeshes
    public TMPro.TextMeshProUGUI municiones;
    public TMPro.TextMeshProUGUI cartuchos;
    public TMPro.TextMeshProUGUI granadas;


    // Audio
    public AudioClip BangBang;
    public AudioClip Reload;
    public AudioClip EmptyMag;
    public AudioClip ambiente;
    private AudioSource aSource;

    void Start () 
    {
        shootTimer = 0;
        expTimer = 0;
        Ammo = MagSize;
        Grenades = GCarry;
        Magazines = maxMag;
        Health = maxHealth;

        movePlayerScript = GetComponent<MovePlayer>();
        
        if(mainCamera != null)
        {
            edgeScript = mainCamera.GetComponent<Edge>();
            if(edgeScript == null)
            {
                Debug.Log("NO HAY SHADER LA GRAN DIABLA");
            }
        }
        else
        {
            Debug.Log("NO HAY CÁMARA LA GRAN DIABLA");
        }
        //movePlayerScript = GetComponent<MovePlayer>();

        //Para el Respawn
        OriginalSpeed  = movePlayerScript.Speed;
        OriginalSpeed2 = movePlayerScript.Speed2;
        originalEdge   = edgeScript.darken;

        municiones.text = "Balas: " + Ammo;
        cartuchos.text = "Cartuchos: " + Magazines;
        granadas.text = "Granadas: " + Grenades;

        aSource = GetComponent<AudioSource>();
    }

    void TakeDamage(float amount)
    {
        Health -= amount;
        if (Health < 0)
        {
            Health = 0;
        }

        float healthPercentage =  (amount / maxHealth);
        Debug.Log(" - " + healthPercentage);
        edgeScript.darken -= (healthPercentage * 0.5f);
        //movePlayerScript.Speed *= speedReductionFactor;
        //movePlayerScript.Speed2 *= speedReductionFactor;
    }


    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            TakeDamage(maxHealth / 5);
        }
    }

    void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            TakeDamage(maxHealth / 10 * Time.deltaTime); // Damage over time while in contact
        }
    }

    public Transform RespawnPoint;
    public GameObject safeZone;
    public GameObject vPrefab;

    void SpawnVampire()
    {
        GameObject newV = Instantiate(vPrefab, transform.position, Quaternion.identity);
        newV.SetActive(true);
    }

    void Respawn(){
        SpawnVampire();
        Magazines = maxMag;
        Ammo = MagSize;
        Grenades = GCarry;
        Health = maxHealth;

        System.Random random= new System.Random();

        float XSize = safeZone.GetComponent<BoxCollider>().bounds.size.x;
        float ZSize = safeZone.GetComponent<BoxCollider>().bounds.size.z;

        //Debug.Log($" - {XSize}, {ZSize}");

        int r = random.Next(1, 20); //D20 RULES!!!!!
        if (r < 11)
        {
            transform.position = new Vector3(RespawnPoint.position.x + XSize/2 + 1, transform.position.y, RespawnPoint.position.z);
        }
        else
        {
            transform.position = new Vector3(RespawnPoint.position.x, transform.position.y, RespawnPoint.position.z + ZSize/2 + 1);
        }

        //transform.position = new Vector3(RespawnPoint.position.x, transform.position.y, RespawnPoint.position.z);

        movePlayerScript.Speed = OriginalSpeed;
        movePlayerScript.Speed2 = OriginalSpeed2;
        edgeScript.darken = originalEdge;

        municiones.text = "Balas: " + Ammo;
        cartuchos.text = "Cartuchos: " + Magazines;
        granadas.text = "Granadas: " + Grenades;
    }


    void Update () 
    {
        Aim = Input.mousePosition;
        // Cool Down de disparo
        if(shootTimer > 0){
            shootTimer -= Time.deltaTime;
        }
        else{
            shootTimer = 0;
        }

        // Cool Down de granada
        if(expTimer > 0){
            expTimer -= Time.deltaTime;
        }
        else{
            expTimer = 0;
        }



        // Disparar
        if (Input.GetButtonDown("Fire1") && shootTimer == 0){
            if (Ammo > 0)
            {
                aSource.PlayOneShot(BangBang);
                Shoot(Aim);
                shootTimer = shootCooldown;
                Ammo--;
                municiones.text = "Balas: " + Ammo;
            }
            else
            {
                aSource.PlayOneShot(EmptyMag);
            }
        }
        
        // Recargar 
        if(Input.GetKeyDown(KeyCode.R) && Magazines > 0){
            aSource.PlayOneShot(Reload);
            Ammo = MagSize;
            Magazines--;
            municiones.text = "Balas: " + Ammo;
            cartuchos.text = "Cartuchos: " + Magazines;
        }

        // Granadas-
        if (Input.GetButtonDown("Fire2") && Grenades > 0 && expTimer == 0){
            Grenade(Aim);
            expTimer = expCooldown;
            Grenades--;
            granadas.text = "Granadas: " + Grenades;
        }

        if(transform.position.y < -6.0f || Health == 0){
            Respawn();
        }
    }


    void Shoot(Vector3 apunta)
    {
        Ray fire = Camera.main.ScreenPointToRay(apunta);
        RaycastHit hit;

        if(Physics.Raycast(fire, out hit)){
            //Debug.Log("Bang " + hit.transform.name);
            //SeeBullet(fire.origin, hit.point);

            HuntPlayer v = hit.transform.GetComponent<HuntPlayer>();
            if(v != null && hit.transform.tag != "Dead")
            {
                float damage = 10f;
                v.TakeDamage(damage);

                Vector3 knockbackDirection = hit.transform.position - transform.position;
                knockbackDirection.y = 0; // Keep knockback horizontal
                knockbackDirection.Normalize();

                float knockbackForce = (damage / v.maxHealth) * 100; // Adjust the multiplier as needed
                v.GetComponent<Rigidbody>().AddForce(knockbackDirection * knockbackForce, ForceMode.Impulse);
            }

            SeeBullet(transform.position, hit.point);
        }
        else{
            Vector3 end = fire.origin + fire.direction * 20;
            //SeeBullet(fire.origin, end);
            SeeBullet(transform.position, end);
        }
    }

    public Material bala;


    void SeeBullet(Vector3 o, Vector3 f)
    {
        GameObject bulletVisual = Instantiate(bulletPrefab);
        LineRenderer lineRenderer = bulletVisual.GetComponent<LineRenderer>();
        lineRenderer.material = bala;
        lineRenderer.widthMultiplier = 0.05f;

        lineRenderer.SetPosition(0, o);
        lineRenderer.SetPosition(1, f);

        Destroy(bulletVisual, 0.025f);
    }

    /*void Grenade(Vector3 apunta)
    {
        // Instantiate grenade prefab at current position with appropriate rotation
        Vector3 where = transform.position;
        where.y += 1.5f;
        GameObject grenadeInstance = Instantiate(grenadePrefab, where, Quaternion.identity);

        // Calculate direction towards the target
        //float mag = apunta.x * apunta.x
        //
        Vector3 temp = apunta.normalized;
        Vector3 direction = new Vector3(temp.x, 0, temp.y);
        Debug.Log("granada: " + grenadeInstance.transform.position + "  dirección: "+ direction + "  vector: "+ direction * throwForce );

        // Access grenade's Rigidbody component and apply force in the calculated direction
        Rigidbody grenadeRigidbody = grenadeInstance.GetComponent<Rigidbody>();
        grenadeRigidbody.AddForce(direction * throwForce, ForceMode.Impulse);
        Destroy(grenadeInstance, 6f);
    }*/
        void Grenade(Vector3 apunta)
    {
        // Instantiate grenade prefab at current position with appropriate rotation
        Vector3 where = transform.position;
        where.y += 1.5f;
        GameObject grenadeInstance = Instantiate(grenadePrefab, where, Quaternion.identity);
    
        // Convert screen position (mouse position) to world position
        Ray cameraRay = Camera.main.ScreenPointToRay(apunta);
        Plane groundPlane = new Plane(Vector3.up, Vector3.zero);
        float rayLength;
    
        // Check if the ray intersects with the ground plane
        if (groundPlane.Raycast(cameraRay, out rayLength))
        {
            Vector3 pointToHit = cameraRay.GetPoint(rayLength);
        
            // Calculate direction towards the target
            Vector3 direction = (pointToHit - where).normalized;
        
            // Access grenade's Rigidbody component and apply force in the calculated direction
            Rigidbody grenadeRigidbody = grenadeInstance.GetComponent<Rigidbody>();
            grenadeRigidbody.AddForce(direction * throwForce, ForceMode.Impulse);
        
            Debug.Log("Grenade: " + grenadeInstance.transform.position + " Direction: " + direction + " Vector: " + direction * throwForce);
        
            Destroy(grenadeInstance, 6f);
        }
    }

}