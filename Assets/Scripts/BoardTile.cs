namespace SAL
{
    using Data;
    using UnityEngine;
    using UnityEngine.UI;

    public class BoardTile : MonoBehaviour
    {
        [SerializeField] private Text serialNo;

        private TileData data;

        public void Init(TileData data)
        {
            this.data = data;

            SetData();
        }

        private void SetData()
        {
            serialNo.text = data.id.ToString();
        }
    }

}