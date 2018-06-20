using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Configuration;

namespace SetupCustomAction
{
    [RunInstaller(true)]
    public class EncryptConfigFile : System.Configuration.Install.Installer
    {
        [System.Security.Permissions.SecurityPermission(System.Security.Permissions.SecurityAction.Demand)]
        public override void Commit(System.Collections.IDictionary savedState)
        {
            base.Commit(savedState);
            EncryptConfig();
        }

        [System.Security.Permissions.SecurityPermission(System.Security.Permissions.SecurityAction.Demand)]
        private void EncryptConfig()
        {
            string configPath = this.Context.Parameters["CONFIG_PATH"];

            var fileMap = new ExeConfigurationFileMap();
            fileMap.ExeConfigFilename = configPath;
            var configuration = ConfigurationManager.OpenMappedExeConfiguration(fileMap, ConfigurationUserLevel.None);
            ConfigurationSection section = configuration.GetSection("connectionStrings");
            if (!section.SectionInformation.IsProtected)
            {
                section.SectionInformation.ProtectSection("DataProtectionConfigurationProvider");
                section.SectionInformation.ForceSave = true;
                configuration.Save(ConfigurationSaveMode.Modified);
            }
        }
    }
}
