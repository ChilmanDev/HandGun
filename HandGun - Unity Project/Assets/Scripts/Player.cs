using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum HandPose { None, Gun, Spidey, Fist, Paper, Point}

public class Player : MonoBehaviour
{
    [SerializeField] JustRead ardData;
    public GameObject aim;
    public GameObject aimAssist;

    [SerializeField] float sense = 0.0005f;
    [SerializeField] float aimAssistSize = 5f;
    [SerializeField] float maxRange = 30f;
    LineRenderer lineRenderer;
    [SerializeField] float X = 2, Y = 2;

    [SerializeField] HandPose handPose;

    [SerializeField] GameObject gunParticle, fistParticle;

    // Start is called before the first frame update
    bool triggerHold = false;

    Vector3 startPos;

    int bulletCount = 3;
    void Awake() {
        
    }
    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.SetPosition(0, transform.position);
        startPos = aim.transform.position;
        handPose = HandPose.None;
    }

    // Update is called once per frame
    void Update()
    {
        
        Vector3 pos = transform.position + ((aim.transform.position - transform.position).normalized * maxRange);
        //pos = aim.transform.position;
        lineRenderer.SetPosition(1, pos);

        if (Physics.SphereCast(transform.position, aimAssistSize, (aim.transform.position - transform.position).normalized, out RaycastHit hit, maxRange))
        {
            aimAssist.transform.position = hit.point;
        }
        else if (Physics.SphereCast(transform.position, aimAssistSize * 3, (aim.transform.position - transform.position).normalized, out RaycastHit hitCast, maxRange))
        {
            aimAssist.transform.position = hitCast.point;
        }
        else
        {
            aimAssist.transform.position = pos;
        }

        UpdateParticles();
        KeyboardInputs();
        Trigger();
        Aim();
    }

    void KeyboardInputs()
    {

        if (Input.GetKeyDown(KeyCode.E))
            TryShoot();

        if (Input.GetKeyDown(KeyCode.R))
            Reload();

        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        Vector3 newPos = Vector3.zero;// = aim.transform.localPosition + new Vector3(x, y, 0);

        //newPos = new Vector3(Mathf.Clamp(x, -X, X), Mathf.Clamp(y, -Y, Y), 0);

        x*= sense*40;
        y*= sense*40;

        newPos = new Vector3(x, y, 0);

        aim.transform.localPosition = new Vector3(Mathf.Clamp(aim.transform.localPosition.x + newPos.x, -X, X), Mathf.Clamp(aim.transform.localPosition.y + newPos.y, -Y, Y), aim.transform.localPosition.z);
    }

    void Shoot()
    {
        bulletCount--;
        if (Physics.SphereCast(transform.position, aimAssistSize, (aim.transform.position - transform.position).normalized, out RaycastHit raycastHit, maxRange))
        {
            if(raycastHit.collider.gameObject.GetComponent<Target>())
            {                    
                raycastHit.collider.gameObject.GetComponent<Target>().handleHit(handPose);
            }
            else if (Physics.SphereCast(transform.position, aimAssistSize * 2, (aim.transform.position - transform.position).normalized, out RaycastHit raycastHit2, maxRange))
            {
                if(raycastHit2.collider.gameObject.GetComponent<Target>())
                { 
                    raycastHit2.collider.gameObject.GetComponent<Target>().handleHit(handPose);
                }
            }
            else
                GameManager.Instance.TargetMissed();
        }
    }

    void TryShoot()
    {
        if(bulletCount > 0) Shoot();
        else
        {

        }
    }
    

    void Trigger()
    {
        if(!ardData.trigger)
        {
            triggerHold = false;
        }
        else if(triggerHold == false)
        {
            TryShoot();
            triggerHold = true;
        }
    }

    void Aim()
    {
        if(Mathf.Abs(ardData.gyro.x)  > 3)
            aim.transform.position = new Vector3(aim.transform.position.x + ardData.gyro.x * sense, aim.transform.position.y, aim.transform.position.z);

        if(Mathf.Abs(ardData.gyro.z) > 3)
        aim.transform.position = new Vector3(aim.transform.position.x, aim.transform.position.y + ardData.gyro.z * sense, aim.transform.position.z);

        if(ardData.reload) Reload();
    }
    
    //enum HandPose { None, Gun, Spidey, Fist, Paper, Point}
    public void setHandPose(HandPose pose)
    {
        handPose = pose;
    }

    void UpdateParticles()
    {
        gunParticle.SetActive(handPose == HandPose.Gun);

        fistParticle.SetActive(handPose == HandPose.Fist);
    }

    void Reload()
    {
        bulletCount = 3;
        aim.transform.position = startPos;
    }

    public int getBulletCount()
    {
        return bulletCount;
    }
}
