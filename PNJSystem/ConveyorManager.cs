using UnityEngine;

namespace ConveyorSystem.Core
{
    public class ConveyorManager : MonoBehaviour, IConveyor
    {

        /*si besoin d'être un singleton
        public static ConveyorManager Instance
        {
            get
            {
                if (Instance == null)
                {
                    GameObject gameObject = new GameObject("ConveyorManager");
                    Instance = gameObject.AddComponent<ConveyorManager>();

                    DontDestroyOnLoad(Instance);
                }

                return Instance;
            }
        }
        private static ConveyorManager instance;
        */


    }
}
