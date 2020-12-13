using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Entities.Mover {
    public class ArmController : MonoBehaviour
    {

        Mover mover;

        // Start is called before the first frame update
        void Start()
        {
            mover = transform.parent.gameObject.GetComponent<Mover>();
        }

        // Update is called once per frame
        void Update()
        {
            float progress = mover.Progress;
            transform.rotation = Quaternion.Euler(0, 0, -90.0f * progress);
        }
    }
}
