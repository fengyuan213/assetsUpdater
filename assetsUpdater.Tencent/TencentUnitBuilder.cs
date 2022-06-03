#region Using

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using assetsUpdater.Interfaces;
using assetsUpdater.Model.Network;
using assetsUpdater.Tencent.Network;

#endregion

namespace assetsUpdater.Tencent
{
    public class TencentUnitBuilder
    {
       
        public TencentUnitBuilder(IAddressBuilder addressBuilder, string vcsName)
        {
            AddressBuilder = addressBuilder;
            //Possible Folder "Folder1\\folder2" instead of "example_folder"
            VFolderName = vcsName.Replace('\\', '/');
            Console.WriteLine (
                $"AddressBuidler->\nRootAddress:{addressBuilder.RootDownloadAddress}\nlocalPath:{addressBuilder.LocalRootPath}");
        }

        public IAddressBuilder AddressBuilder { get; set; }

        public string VFolderName { get; }


        private TencentCosHelper _tencentCosHelper = new TencentCosHelper(TencentCosHelper.DefaultCosConfiguration());

        public Task<TencentUploadUnit> Build(string key)
        {
            var localPath = AddressBuilder.BuildDownloadLocalPath(key);

            key = key.Replace('\\', '/');
            var resourceKey = VFolderName.TrimEnd('/') + '/' + key.TrimStart('/');

            var up = new UploadPackage(resourceKey, localPath);

            var unit = _tencentCosHelper.BuildUploadUnit(up);
            Console.WriteLine($"localPath:{localPath},resourceKey:{resourceKey},fileKey:{key}");
            return Task.FromResult(unit);
        }

        public IEnumerable<TencentUploadUnit> Build(IEnumerable<string> keys)
        {
            var units = new List<TencentUploadUnit>();


            Parallel.ForEach(keys, async key =>
            {
                var unit = await Build(key).ConfigureAwait(false);
                units.Add(unit);
            });
            return units;
            //Parallel.ForEach()
        }
    }
}