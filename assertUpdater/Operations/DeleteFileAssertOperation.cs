using System.IO.Pipes;
using System.Runtime.CompilerServices;
using assertUpdater.DbModel;

namespace assertUpdater.Operations
{
    public class DeleteFileAssertOperation : IOperation
    {
        private string _vcsFolder;
        private DbFile _file;
        public DeleteFileAssertOperation(string vcsFolder, DbFile file)
        {
            _file = file;
            _vcsFolder = vcsFolder;
        }

        public DeleteFileAssertOperation(string path)
        {
            
        }
        public double Progress { get; set; }

        public Task Execute()
        {
            Progress = 0;
            var path = Path.Join(_vcsFolder, _file.RelativePath);
            try
            {
                File.Delete(path);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to delete {path}, ex:{ex}");
                throw;
            }

            Progress = 100;
            return Task.CompletedTask;
        }
    }
}
