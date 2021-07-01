/*namespace CharacterSpace
{
    [System.Serializable]
    public class CharacterAttributes
    {
        public Attributes attribute;
        public float baseValue;

        /// <summary>
        /// Amount is a float because some attributes will have decimals especially when percentages are added to stats. (e.g. +20% melee damage)
        /// </summary>
        /// <param name="attribute"></param>
        /// <param name="amount"></param>
        public CharacterAttributes(Attributes attribute, float amount)
        {
            this.attribute = attribute;
            this.baseValue = amount;
        }
    }
}
*/