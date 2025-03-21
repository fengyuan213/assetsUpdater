using assertUpdater.DbModel;

namespace assertUpdater.Operations
{
    public class DifferFileAssertOperation : IOperation
    {
        //Combination of Add & Delete

        public DifferFileAssertOperation(string vcsFolder, RemoteDbFile file)
        {
            _remoteDbFile = file;
            _vcsFolder = vcsFolder;



            var addedOperation = new AddFileAssertOperation(file);
            var deletedOperation = new DeleteFileAssertOperation(vcsFolder, file);
            InnerOperations.Add(deletedOperation);
            InnerOperations.Add(addedOperation);
            //Decorator pattern

            /*
           var stack = new DifferFileAssertOperation(addedOperation);
           stack = new DifferFileAssertOperation(deletedOperation);
           stack.Execute();*/
        }


        public RemoteDbFile _remoteDbFile;
        public string _vcsFolder;

        public double Progress
        {
            get
            {
                double totalPg = 0;
                foreach (var innerOperation in InnerOperations)
                {
                    totalPg += innerOperation.Progress;
                }

                return totalPg / InnerOperations.Count;
            }
            set
            {

            }
        }

        //public IOperation WrappeeOperation { get; set; }
        public List<IOperation> InnerOperations { get; set; } = new List<IOperation>();
        public async Task Execute()
        {
            foreach (var innerOperation in InnerOperations)
            {
                await innerOperation.Execute().ConfigureAwait(false);
            }
        }
    }
}
