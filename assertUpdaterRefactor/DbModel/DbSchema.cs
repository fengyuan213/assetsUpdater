namespace assertUpdaterRefactor.DbModel
{
    [Serializable]
    public class DbSchema
    {
        public IEnumerable<string> DirList { get; set; } = new List<string>();
        public IEnumerable<string> FileList { get; set; } = new List<string>();
    }
}
