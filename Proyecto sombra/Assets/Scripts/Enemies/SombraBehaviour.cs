using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SombraBehaviour : MonoBehaviour {

    public double EaglosSpeed, distX, distY, moduloDist, uniX, uniY, targetX, targetY; //Detectar al jugador.
    public double jumpAngleRange, time, angle, summonCD;
    public int fase, HP, SombraSpeed, optimRange;    
    public bool approach, attackInstanciated, acting, spearReady, charging;
    public double angulo; 
    public GameObject player, shadowHound, shadowHoundPrefab;
    public GameObject spear, spearPrefab;   
    public Slider healthBar;
       

    // Use this for initialization
    void Start () {
        SombraSpeed = 8;
        HP = 6;
        fase = 3;
        optimRange = 10;
        time = 0;
        summonCD = 0;
        acting = false;
       // jumpAngleRange = Math.PI/2+Math.PI/10;  
	}
	
	// Update is called once per frame
	void Update () {
        //Calcular el tiempo:
        time += Time.deltaTime;
        summonCD += Time.deltaTime;

        // Vector hacia el jugador.
        distX = player.transform.position.x - transform.position.x;
        distY = player.transform.position.y - transform.position.y;
        
        moduloDist = Math.Sqrt(Math.Pow(distX, 2) + Math.Pow(distY, 2));
        
        uniX = distX / moduloDist;
        uniY = distY / moduloDist;

        if (HP <= 0)
        {
            Destroy(this.gameObject);
            SceneManager.LoadScene("Sombra Maestra");
        }
        else if (HP <= 2) fase = 3;
        else if (HP <= 4) fase = 2;
        
        if (fase == 3)
        {
            if (moduloDist < optimRange || acting)
            {
                attack();
            }
            else
            {
                chase();
            }
        }
        else if (fase == 2)
        {
            movement();
            if (summonCD > 3)
            {
                summonHound();                
            }
        }
        else if (fase == 1)
        {
            movement();
        }
        else
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
        }
        
    }

    void movement()
    {
        if (!acting)
        {
           
            if (spearReady)
            {
                GetComponent<SpriteRenderer>().color = Color.red;
                spearThrow();
            }
            else
            {
                GetComponent<SpriteRenderer>().color = Color.green;
            }
            approach = moduloDist > optimRange;
            acting = true;
            time = 0;
            calculateJumpAngleRange();
            double[] uniJump = calculateUniJump();
            double uniJumpX = uniJump[0];
            double uniJumpY = uniJump[1];

            GetComponent<Rigidbody2D>().velocity = new Vector2(System.Convert.ToSingle(uniJumpX), System.Convert.ToSingle(uniJumpY));
        }


        if (time >= 0.5)
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
            GetComponent<SpriteRenderer>().color = Color.white;

        }

        if (time >= 0.8)
        {
            acting = false;
            spearReady = !spearReady;
        }
        
        

    }

    void calculateJumpAngleRange()
    {
        double distToOptimRange = Math.Abs(moduloDist - optimRange);
        jumpAngleRange = Math.PI * Math.Pow(1.1, -distToOptimRange);
    }

    double[] calculateUniJump()
    {
        double uniPlayerAngle = 0;
        if (uniX >= 0 && uniY >= 0)
        {
            uniPlayerAngle = Math.Atan(uniY / uniX);
        }
        else if(uniX >= 0 && uniY <= 0)
        {
            uniPlayerAngle = Math.Atan(uniY / uniX) + 2 * Math.PI;
        }
        else if(uniX <= 0 && uniY >= 0)
        {
            uniPlayerAngle = Math.Atan(uniY / uniX) + Math.PI;
        }
        else if(uniX <= 0 && uniY <= 0)
        {
            uniPlayerAngle = Math.Atan(uniY / uniX) + Math.PI;
        }


        double uniJumpAngle = uniPlayerAngle + UnityEngine.Random.Range(-1f, 1f) * jumpAngleRange / 2;

        double uniJumpX = SombraSpeed * Math.Cos(uniJumpAngle);
        double uniJumpY = SombraSpeed * Math.Sin(uniJumpAngle);

        if (!approach)
        {
            uniJumpX = -uniJumpX;
            uniJumpY = -uniJumpY;
        }

        // debugear
        angulo = uniPlayerAngle * 180 / Math.PI;
        double uniRangeRAngle = uniPlayerAngle - jumpAngleRange / 2;
        double uniRangeLAngle = uniPlayerAngle + jumpAngleRange / 2;
        double uniRangeLX = 2 * Math.Cos(uniRangeLAngle);
        double uniRangeLY = 2 * Math.Sin(uniRangeLAngle);

        double uniRangeRX = 2 * Math.Cos(uniRangeRAngle);
        double uniRangeRY = 2 * Math.Sin(uniRangeRAngle);

        // uniPlayer
        Debug.DrawLine(new Vector3(transform.position.x, transform.position.y), new Vector3(transform.position.x + (float)uniX * 10, transform.position.y + (float)uniY * 10), Color.blue, 1f, false);
        // uniJump
        Debug.DrawLine(new Vector3(transform.position.x, transform.position.y), new Vector3(transform.position.x + (float)uniJumpX / 2, transform.position.y + (float)uniJumpY / 2), Color.black, 1f, false);
        if (approach)
        {
            // uniRangeL
            Debug.DrawLine(new Vector3(transform.position.x, transform.position.y), new Vector3(transform.position.x + (float)uniRangeLX, transform.position.y + (float)uniRangeLY), Color.magenta, 1f, false);
            // uniRangeR
            Debug.DrawLine(new Vector3(transform.position.x, transform.position.y), new Vector3(transform.position.x + (float)uniRangeRX, transform.position.y + (float)uniRangeRY), Color.magenta, 1f, false);
        }
        else
        {
            // uniRangeL
            Debug.DrawLine(new Vector3(transform.position.x, transform.position.y), new Vector3(transform.position.x - (float)uniRangeLX, transform.position.y - (float)uniRangeLY), Color.magenta, 1f, false);
            // uniRangeR
            Debug.DrawLine(new Vector3(transform.position.x, transform.position.y), new Vector3(transform.position.x - (float)uniRangeRX, transform.position.y - (float)uniRangeRY), Color.magenta, 1f, false);
        }
        
        return new double[] { uniJumpX, uniJumpY };
    }

    void spearThrow()
    {
       
           
            targetX = -uniX;
            targetY = -uniY;

            if (!attackInstanciated)
            {
                spear = (GameObject)Instantiate(spearPrefab);
                
                

                attackInstanciated = true;
                spear.transform.position = new Vector2(System.Convert.ToSingle(transform.position.x - uniX / 3), System.Convert.ToSingle(transform.position.y - uniY / 3));
                

                if (targetY < 0)
                {
                    angle = ((2 * Math.PI - Math.Acos(targetX)) * Mathf.Rad2Deg + 180);
                }

                else
                {
                    angle = ((Math.Acos(targetX)) * Mathf.Rad2Deg + 180);
                }

                spear.transform.rotation = Quaternion.Euler(0, 0, System.Convert.ToSingle(angle));
                spear.GetComponent<Rigidbody2D>().velocity = new Vector2(System.Convert.ToSingle(-targetX * 15), System.Convert.ToSingle(-targetY * 15));
            }

            attackInstanciated = false;
        
    }

    void attack
        ()
    {       
        if (!acting)
        {
            targetX = uniX;
            targetY = uniY;            
            acting = true;
            time = 0;            
        }

        if (time < 1)
        {
            GetComponent<SpriteRenderer>().color = Color.red;
            charge();
        }
        
        if (time >= 1 && time <= 2)
        {
            GetComponent<SpriteRenderer>().color = Color.green;
            jumpAway();
        }
        if (time > 2)
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
            GetComponent<SpriteRenderer>().color = Color.white;            
            spearThrow();
            acting = false;
        }       
        
    }
    void charge()
    {
        GetComponent<Rigidbody2D>().velocity = new Vector2((float)targetX * 15, (float)targetY * 15);
        charging = true;
        

    }
    void jumpAway()
    {
        charging = false;
        targetX = -uniX;
        targetY = -uniY;
        GetComponent<Rigidbody2D>().velocity = new Vector2((float)targetX * 15, (float)targetY * 15);
    }

    void chase()
    {
        GetComponent<Rigidbody2D>().velocity = new Vector2((float)uniX * SombraSpeed, (float)uniY * SombraSpeed);
    }

    void summonHound()
    {
        shadowHound = (GameObject)Instantiate(shadowHoundPrefab);
        shadowHound.transform.position = new Vector2(System.Convert.ToSingle(transform.position.x - uniX * 5), System.Convert.ToSingle(transform.position.y - uniY * 5));
        shadowHound.GetComponent<HoundBehaviour>().player = player;
        summonCD = 0;
    }   

    void OnTriggerEnter2D(Collider2D cono)
    {
        if (cono.tag == "Attack" || cono.tag == "Arrow")
        {
            HP--;
            healthBar.value--;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (charging && collision.gameObject.tag == "Jugador")
        {
            player.GetComponent<PlayerBehaviour>().HP--;
            player.GetComponent<PlayerBehaviour>().maxHP--;
        }
    }
}
