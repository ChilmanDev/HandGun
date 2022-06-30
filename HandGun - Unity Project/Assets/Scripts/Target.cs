using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    [SerializeField] float speed = 5;
    [HideInInspector] public bool isActive;

    [SerializeField] HandPose destroyPose;



    [SerializeField] ParticleSystem particleConst;
    [SerializeField] ParticleSystem particleDestroy;

    // Start is called before the first frame update
    void Start()
    {
        isActive = false;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(new Vector3(0f,1f,0f) * speed * Time.deltaTime);

        if(!particleDestroy.gameObject.activeInHierarchy)
            return;
        if(particleDestroy.isStopped)
        {
            Destroy(gameObject);
            GameManager.Instance.TargetDestroyed();
        }
    }

    public void Activate(){
        isActive = true;
    }

    public void handleHit(HandPose pose)
    {
        if(!isActive)
            return;

        if(pose != destroyPose)
        {
            wrongPose();
            return;
        }

        hitTarget();
    }

    void hitTarget()
    {
        particleConst.gameObject.SetActive(false);
        GetComponent<MeshRenderer>().enabled = false;
        particleDestroy.gameObject.SetActive(true);
        StartCoroutine("DelayDestroy", 0.2f);
    }

    IEnumerator DelayDestroy(float sec)
    {
        yield return new WaitForSeconds(sec);

        GameManager.Instance.TargetDestroyed();
        Destroy(gameObject);
    }

    void wrongPose()
    {
        GameManager.Instance.TargetMissed();
    }
}
