using UnityEngine;
using Random = System.Random;

namespace Scenes.Scripts
{
    public class RiMController : MonoBehaviour
    {
        public GameObject hole;

        public GameObject passingPlate;
        public GameObject rimStripe;
        private float posXHole1=2;
        private float posXHole2=0;
        private float posXStripe=0;
        private float posXPassingPlate=0;
        // Start is called before the first frame update
        void Start()
        {
            //transform.position = new Vector3(value, transform.position.y, transform.position.z);
            //hole.transform.position =new Vector3(posXHole, 0, 0);
            //passingPlate.transform.position =new Vector3(posXPassingPlate, 0, 0);

      
            Random random = new Random();
            var randPos = random.Next(0, 2);
            if (randPos == 0)
            {
                posXHole1 = -2;
                posXStripe = 2;
            }
            else
            {
                posXHole1 = 2;
                posXStripe = -2;
            }

            float offsetZ=0;
        
            Debug.Log("par="+transform.parent.position.z);
            if (transform.parent.position.z==0)
            {
                offsetZ = -100;
            }
        
            Vector3 posHole1 = new Vector3(posXHole1, transform.parent.position.y, offsetZ+transform.parent.position.z);
     
            Vector3 posHole2 = new Vector3(posXHole2, transform.parent.position.y, offsetZ+transform.parent.position.z);
            Vector3 posStripe = new Vector3(posXStripe, transform.parent.position.y, offsetZ+transform.parent.position.z);
            Vector3 posPlate = new Vector3(posXPassingPlate, (float)(transform.parent.position.y-0.5), offsetZ+transform.parent.position.z+3);
            GameObject holeClone1 = Instantiate(hole, posHole1,hole.transform.rotation);
            holeClone1.transform.SetParent(this.transform);
            Debug.Log("phol1="+holeClone1.transform.position.z);
            GameObject holeClone2 = Instantiate(hole, posHole2,hole.transform.rotation);
            holeClone2.transform.SetParent(this.transform);
            GameObject stripeClone = Instantiate(rimStripe, posStripe,rimStripe.transform.rotation);
            stripeClone.transform.SetParent(this.transform);
            GameObject plateClone = Instantiate(passingPlate, posPlate,passingPlate.transform.rotation);
            plateClone.transform.SetParent(this.transform);
        }
    }
}
