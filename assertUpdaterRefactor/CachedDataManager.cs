using assertUpdaterRefactor.DbModel;
using assertUpdaterRefactor.StorageProvider;

namespace assertUpdaterRefactor
{
    public class CachedDataManager : DataManager, ICloneable
    {


        public CachedDataManager(IStorageProvider storageProvider) : base(storageProvider)
        {


        }

        public override DbConfig Config
        {
            get => GetDbData().Config;
            set => GetDbData().Config = value;
        }

        public override DbData Data
        {
            get => GetDbData();
            set => SetDbData(value);
        }
        private DbData _cachedData;
        protected override void SetDbData(DbData data)
        {
            _cachedData = data;
        }

        protected override DbData GetDbData()
        {
            if (_cachedData == DbData.Empty)
            {
                _cachedData = base.GetDbData();
            }

            return _cachedData;
        }

        public override object Clone()
        {
            CachedDataManager manager = new((IStorageProvider)StorageProvider.Clone())
            {
                Data = (DbData)Data.Clone(),
                Config = (DbConfig)Config.Clone()
            };
            //manager.StorageProvider = StorageProvider;


            return manager;

        }
    }
}
