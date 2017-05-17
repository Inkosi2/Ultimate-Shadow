using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SombraBehaviour : MonoBehaviour {

    public double EaglosSpeed, distX, distY, moduloDist, uniX, uniY, targetX, targetY; //Detectar al jugador.
    public double jumpAngleRange, time, angle;
    public int fase, HP, SombraSpeed, optimRange;    
    public bool approach, attackInstanciated, acting, spearReady;
    public double angulo; 
    public GameObject player, flecha, flechaPrefab;
    

    // Use this for initialization
    void Start () {
        SombraSpeed = 12;
        HP = 10;
        fase = 1;
        optimRange = 10;
        time = 0;
        acting = false;
       // jumpAngleRange = Math.PI/2+Math.PI/10;  
	}
	
	// Update is called once per frame
	void Update () {
        //Calcular el tiempo:
        time += Time.deltaTime;


        // Vector hacia el jugador.
        distX = player.transform.position.x - transform.position.x;
        distY = player.transform.position.y - transform.position.y;
        
        moduloDist = Math.Sqrt(Math.Pow(distX, 2) + Math.Pow(distY, 2));
        
        uniX = distX / moduloDist;
        uniY = distY / moduloDist;

        if (fase == 3)
        {
            if (moduloDist < optimRange || acting)
            {
                charge();
            }
            else
            {
                chase();
            }
        }
        else if (fase == 2)
        {
            movement();
        }
        else if (fase == 1)
        {
            movement();
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

        if (time >= 0.6)
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
            flecha = (GameObject)Instantiate(flechaPrefab);
            flecha.transform.position = transform.position;
            attackInstanciated = true;
            flecha.transform.position = new Vector2(System.Convert.ToSingle(transform.position.x - uniX / 3), System.Convert.ToSingle(transform.position.y - uniY / 3));

            if (targetY < 0)
            {
                angle = ((2 * Math.PI - Math.Acos(targetX)) * Mathf.Rad2Deg + 180);
            }

            else
            {
                angle = ((Math.Acos(targetX)) * Mathf.Rad2Deg + 180);
            }

            flecha.transform.rotation = Quaternion.Euler(0, 0, System.Convert.ToSingle(angle));
            flecha.GetComponent<Rigidbody2D>().velocity = new Vector2(System.Convert.ToSingle(-targetX * 20), System.Convert.ToSingle(-targetY * 20));           

        }
                  
        attackInstanciated = false; 
    }

    void charge()
    {       
        if (!acting)
        {
            targetX = -uniX;
            targetY = -uniY;            
            acting = true;
            time = 0;
            GetComponent<SpriteRenderer>().color = Color.red;
        }
        GetComponent<Rigidbody2D>().velocity = new Vector2((float)-targetX * 15, (float)-targetY * 15);
        if (time > 1)
        {
            jumpAway();
            GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
        }
        if (time > 2)
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
            acting = false;
        }
        
    }
    void jumpAway()
    {
        targetX = uniX;
        targetY = uniY;
        GetComponent<Rigidbody2D>().velocity = new Vector2((float)targetX * 15, (float)targetY * 15);
    }

    void chase()
    {
        GetComponent<Rigidbody2D>().velocity = new Vector2((float)uniX * SombraSpeed, (float)uniY * SombraSpeed);
    }
}
