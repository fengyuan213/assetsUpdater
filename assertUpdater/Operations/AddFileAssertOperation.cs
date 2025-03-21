using assertUpdater.DbModel;

namespace assertUpdater.Operations
{
    public class AddFileAssertOperation : IOperation
    {
        protected Progress<double> PgReporter { get; set; } = new Progress<double>();

        public double Progress
        {
            get => InnerOperations.First().Progress;
            set
            {
                
            }
        }

        public IEnumerable<IOperation> InnerOperations;
      
        //private string _vcsFolder { get; set; }
        private RemoteDbFile _file { get; set; }
        //Download file on demand
        public AddFileAssertOperation(RemoteDbFile file)
        {

            //_vcsFolder = vcsFolder;
            _file = file;

            InnerOperations = new[] { _file.BuildDownloadOperation() };


            //_file.AddressBuilder.LocalRootPath = vcsFolder;
            //_file.AddressBuilder.RemoteRootPath = vcsFolder;
     
        }


        public async Task Execute()
        {
            await InnerOperations.First().Execute().ConfigureAwait(false);

        }
        
    }
}
