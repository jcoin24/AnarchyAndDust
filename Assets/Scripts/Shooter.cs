using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace CompleteProject
{
    public class Shooter : MonoBehaviour
    {
        private GameObject target;
        public Animator anim;
        public NavMeshAgent agent;
        public float rotationSpeed;
        private float aimtime;
        private float firetime;
        private GameObject gunObj;
        private EnemyGun gun;


        // Use this for initialization
        void Start()
        {
            target = GameObject.FindGameObjectWithTag("player");
            anim = GetComponent<Animator>();
            gunObj = transform.Find("Beta:Hips/Beta:Spine/Beta:Spine1/Beta:Spine2/Beta:RightShoulder/Beta:RightArm/Beta:RightForeArm/Beta:RightHand/M40A3 Rifle_prefab").gameObject;
            gun = gunObj.GetComponent<EnemyGun>();
        }

        // Update is called once per frame
        void Update()
        {

            Vector3 direction = (target.transform.position - transform.position).normalized;
            var lookRotation = Quaternion.LookRotation(direction);
            float dist = Mathf.Abs(Vector3.Distance(target.transform.position, transform.position));
            if (aimtime >= 5)
            {
                anim.SetBool("Fire", true);
                gun.fire();
                firetime = 0f;
                aimtime = 0f;
            }
            if (anim.GetBool("Fire"))
            {
                firetime += Time.deltaTime;
            }
            if (firetime > .5)
            {
                Debug.Log("Shot");
                anim.SetBool("Fire", false);
                firetime = 0f;
            }
            if (anim.GetBool("Aim"))
            {

                lookRotation *= Quaternion.Euler(0, 44, 0);
                transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * rotationSpeed);
                aimtime += Time.deltaTime;
            }
            if (dist > 10 && anim.GetBool("Aim") == false)
            {
                anim.SetBool("Move", true);
                agent.Resume();
                agent.SetDestination(target.transform.position);
            }
            if (dist < 10)
            {
                anim.SetBool("Aim", true);
                anim.SetBool("Move", false);
                agent.Stop();
            }
            if (dist > 15 && anim.GetBool("Aim"))
            {
                anim.SetBool("Aim", false);
                anim.SetBool("Move", true);
            }

            if (anim.GetBool("Aim") && ((lookRotation.y - .05f) <= transform.rotation.y && transform.rotation.y <= (lookRotation.y + .05f)))
            {

                anim.SetBool("TurnR", false);
                anim.SetBool("TurnL", false);

            }
            if (anim.GetBool("Aim") && (lookRotation.y + .05f) < transform.rotation.y)
            {

                anim.SetBool("TurnL", true);
            }
            if (anim.GetBool("Aim") && (lookRotation.y - .05f) > transform.rotation.y)
            {

                anim.SetBool("TurnR", true);
            }
        }

    }
}
