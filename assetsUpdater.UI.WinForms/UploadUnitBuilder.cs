using assetsUpdater.Interfaces;
using assetsUpdater.Model.Network;
using assetsUpdater.Tencent.Network;

using NLog;

using System.Collections.Generic;
using System.Threading.Tasks;

namespace assetsUpdater.UI.WinForms
{
    public class UploadUnitBuilder
    {
        public static readonly Logger Logger = LogManager.GetCurrentClassLogger();
        public IAddressBuilder AddressBuilder { get; set; }

        public string VFolderName
        {
            get;
        }

        public UploadUnitBuilder(IAddressBuilder addressBuilder, string vcsName)
        {
            AddressBuilder = addressBuilder;
            //Possible Folder "Folder1\\folder2" instead of "example_folder"
            VFolderName = vcsName.Replace('\\', '/');
            Logger.Debug($"AddressBuidler->\nRootAddress:{addressBuilder.RootDownloadAddress}\nlocalPath:{addressBuilder.LocalRootPath}");
        }

        public Task<TencentUploadUnit> Build(string key)
        {
            var localPath = AddressBuilder.BuildDownloadLocalPath(key);

            key = key.Replace('\\', '/');
            var resourceKey = VFolderName.TrimEnd('/') + '/' + key.TrimStart('/');

            var up = new UploadPackage(resourceKey, localPath);

            var unit = TencentCosHelper.BuildUploadUnit(up);
            Logger.Debug($"localPath:{localPath},resourceKey:{resourceKey},fileKey:{key}");
            return Task.FromResult(unit);
        }

        public async Task<IEnumerable<TencentUploadUnit>> Build(IEnumerable<string> keys)
        {
            List<TencentUploadUnit> units = new List<TencentUploadUnit>()
      ;
            foreach (var key in keys)
            {
                var unit = await Build(key).ConfigureAwait(false);
                units.Add(unit);
            }

            return units;
            //Parallel.ForEach()
        }
    }
}