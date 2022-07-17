using UnityEngine;
using UnityEngine.Serialization;

namespace GameSystems.Dice
{
    [CreateAssetMenu(fileName = "New Dice Properties", menuName = "Dice System/Dice Property Object", order = 90)]
    public class DiceProperties : ScriptableObject
    {
        public GameObject dieWorldAsset;
        public GameObject dieUiAsset;

        [SerializeField] private DiceProperties lowerValueDie;

        [SerializeField] private int maximum;
        public int Maximum => maximum;

        public int Roll()
        {
            return Random.Range(1, Maximum);
        }

        public DiceProperties Degrade()
        {
            return lowerValueDie;
        }
    }
}