using System;
namespace assertUpdater.Operations
{

    public interface IOperation
    {
        public double Progress { get; set; }
        public Task Execute();
    }
}
