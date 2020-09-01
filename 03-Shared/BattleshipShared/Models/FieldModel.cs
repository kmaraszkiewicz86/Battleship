namespace BattleshipShared.Models
{
    public class FieldModel
    {
        public ShipModel ShipModel { get; set; }

        public bool IsNotEmpty => ShipModel != null;

        public bool WasHit { get; set; }

        public FieldModel(ShipModel shipModel)
        {
            ShipModel = shipModel;
        }
    }
}
