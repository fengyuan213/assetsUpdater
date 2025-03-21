using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using assertUpdater.AddressBuilder;
using assertUpdater.Network;
using assertUpdater.Operations;

namespace assertUpdater.DbModel
{
    public class RemoteDbFile : DbFile
    {
      

        public RemoteDbFile(IAddressBuilder addressBuilder, string relativePath, string hash, long fileSize) : base(relativePath, hash, fileSize)
        {
            AddressBuilder = addressBuilder;
        }

        public RemoteDbFile(IAddressBuilder addressBuilder,DbFile file ) : base(file)
        {
            AddressBuilder = addressBuilder;

            // Convert Dbfile to Remote file   



            /*
                  if (isBuildUniqueAddress)
                   {


                       Console.WriteLine($"{MethodBase.GetCurrentMethod()},  \"Database are built by unique urls\"");

                       DbData data = storageProvider.RefreshAsync().Result; //todo::add cache
                       foreach (DbFile dataDatabaseFile in data.DatabaseFiles)
                       {
                           dataDatabaseFile.DownloadAddress =
                               config.DownloadAddressBuilder.BuildUri(dataDatabaseFile.RelativePath).ToString();
                       }
                   }
                   Console.WriteLine($"Database Build finished type:{t.FullName}");
             */
        }



        public DownloadOperation BuildDownloadOperation(DownloadConfig config=null)
        {
       

            var localPath = AddressBuilder.BuildDownloadLocalPath(RelativePath);
            var op = new DownloadOperation(Url.ToString(),localPath , config);
            
            return op;
        }








        public IAddressBuilder AddressBuilder { get; set; }

        private Uri _url;
        public Uri Url
        {
            get
            {

                _url = AddressBuilder.BuildUri(RelativePath);


                return _url;
            }
            set
            {
                _url = value;
            }
        }




        

        public DbFile Convert()
        {
            return new DbFile(RelativePath, Hash, FileSize);
        }
    
    }
}
