namespace BattleshipShared.Models
{
    public class FieldModel
    {
        public ShipModel ShipModel { get; set; }

        public bool IsEmpty => ShipModel != null;

        public FieldModel()
        {

        }

        public FieldModel(ShipModel shipModel)
        {
            ShipModel = shipModel;
        }
    }
}
