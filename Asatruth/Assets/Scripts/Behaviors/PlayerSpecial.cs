using UnityEngine;
using System.Collections;
using System;

public class PlayerSpecial : AbstractBehavior {

    public int specialSelect = 0;

    
    public GameObject projectilePrefab;
    public Vector2 firePosition = Vector2.zero;
    public Color debugColor = Color.yellow;
    public float debugRadius = 3f;
    public GameObject player;
    public PlayerHealth heal;

    public bool shooting;
    public bool healing;
    public bool punching;
    public bool casting;

    private bool isShooting;
    public float RangePwrCap = 0.7f;
    public float RangePwr = 0f;
    public float shootDelay = .5f;
    public float healingDelay = .5f;
    public float punchDelay = .5f;

    private float timeElapsed = 0f;
    public bool attacking = false;
    private float attackTimer = 0;
    private float attackCD = 2f;

    public Collider2D SpecialMelee;

    

    override protected void Awake() {
        player = GameObject.FindGameObjectWithTag("Player");
        heal = player.GetComponent<PlayerHealth>();
        this.inputState = new InputState();
    }


    protected virtual void Shoot(bool value) {

        if (value)
        {
            if (projectilePrefab != null)
            {

                if (isShooting)
                {
                    RangePwr = timeElapsed / RangePwrCap;
                    if (RangePwr > 1)
                    {
                        RangePwr = 1;
                    }
                }

                if (!shooting)
                {
                    shooting = true;
                    isShooting = true;

                }
                //if (canFire && timeElapsed > shootDelay) {                        
                //        CreateProjectile(CalculateFirePosition());
                //        timeElapsed = 0;
                //        shooting = true;
                //        ToggleScripts(false);
                //    }
                //    else {
                //    shooting = false;
                //    ToggleScripts(true);
                //}
            }
        }
        else
        {
            if (timeElapsed > shootDelay)
            {
                CreateProjectile(CalculateFirePosition());
                isShooting = false;
                shooting = false;
            }

           
        }
        //ToggleScripts(!value);

    }
    protected virtual void Heal(bool value) {
        //var canHeal = inputState.GetButtonValue(inputButtons[0]);
        //if (canHeal) {
        if (value)
        {
            healing = true;
        }
        else
        {
            if (timeElapsed > healingDelay)
            {
                heal.curHealth += 20;
                healing = false;
            }
        }

        
        //}
    }
    protected virtual void Punch(bool value) {


        if (value)
        {
            punching = true;
            attackTimer = attackCD;
            SpecialMelee.enabled = true;

        }
        else
        {
            if (timeElapsed > punchDelay)
            {
                SpecialMelee.enabled = false;
                punching = false;
                ToggleScripts(true);
            }
        }
    }
    //protected virtual void Magic(bool value) {

    //}

    void Update () {
        if(shooting || punching || healing)
        {
        timeElapsed += Time.deltaTime;
        }
        else
        {
            timeElapsed = 0;
        }

        
        switch (specialSelect) {
            case 1: //ranged
                var canShoot = Input.GetButtonDown("Fire1");
                //vet inte varför men getbuttonvalue funkar inte riktigt
                //var canShoot = inputState.GetButtonValue(inputButtons[0]);
                if (canShoot && !shooting) {
                     Shoot(true);
                }
                else if (shooting && !canShoot) {
                    Shoot(false);
                }

                break;
                
            case 2: //heal  

                var canHeal = Input.GetButtonDown("Fire1");

                if (canHeal && !healing) {
                    Heal(true);
                    canHeal = false;
                }
                else if (healing && !canHeal) {
                    Heal(false);
                }

                break;
            case 3: //punch

                var canPunch = Input.GetButtonDown("Fire1");
          

                if (canPunch && !punching ) {
                    Punch(true);
                }
                else if (punching && !canPunch) {
                    Punch(false);
                }
                

                break;
            case 4: //spell
                //var canCast = inputState.GetButtonValue(inputButtons[0]);
                //if (canCast && !casting) {
                //    Magic(true);
                //}
                //else if (casting && !canCast) {
                //    Magic(false);
                //}

                break;
            default: 
                break;
                
        }
	}


    Vector2 CalculateFirePosition() {
        var pos = firePosition;
        pos.x *= (float)inputState.direction;
        pos.x += transform.position.x;
        pos.y += transform.position.y;
        return pos;
    }


    public void CreateProjectile(Vector2 pos) {
        var clone = Instantiate(projectilePrefab, pos, Quaternion.identity) as GameObject;
        clone.transform.localScale = transform.localScale;
    }

    void OnDrawGizmos() {
        Gizmos.color = debugColor;

        var pos = firePosition;
        if (inputState != null)
            pos.x *= (float)inputState.direction;
        pos.x += transform.position.x;
        pos.y += transform.position.y;

        Gizmos.DrawWireSphere(pos, debugRadius);
    }
    public Boolean loadingSpecial()
    {
        return punching || healing || shooting || casting;
    }
    
}
