using assertUpdaterRefactor.DbModel;
using assertUpdaterRefactor.StorageProvider;

namespace assertUpdaterRefactor
{
    public class CachedDataManager : DataManager, ICloneable
    {
        
        
        public CachedDataManager(IStorageProvider storageProvider) : base(storageProvider)
        {
            _cachedData = StorageProvider.RefreshAsync().Result;
            // _cachedData = new Lazy<DbData>(()=>StorageProvider.Refresh().Result);
           
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
        //Regularly flush change to disk
        private DbData _cachedData;
        protected override void SetDbData(DbData data)
        {
            _cachedData = data;
            /* tmpData = data;
             OnDbDataSet.Invoke(this,EventArgs.Empty);*/
            //flush data when changed
            isFlushing = true;
          /*  Task.Run((() => StorageProvider.Flush(data))).ContinueWith((task =>
            {
                isFlushing = false;
            }));*/
            //await StorageProvider.Flush(data).ConfigureAwait(false).;
            ThreadPool.QueueUserWorkItem((state =>
            {
                lock (StorageProvider)
                {
                    StorageProvider.FlushAsync(data).ContinueWith((task => isFlushing = false)).Wait();

                }

            }));
        }

        private bool isFlushing = false;
        
        protected override DbData GetDbData()
        {
            if (isFlushing)
            {
                //Data has been assigned, but not flushed to the database
                return _cachedData;
            }
            if (_cachedData!=DbData.Empty)
            { 
                //refresh cache
                //_cachedData = base.GetDbData();
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
