namespace roxority.Shared.ComponentModel
{
    using roxority.Shared;
    using roxority.Shared.Win32;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Reflection;
    using System.Windows.Forms;

    [ProvideProperty("SettingsMachineScope", typeof(IComponent)), ProvideProperty("SettingsName", typeof(IComponent)), ProvideProperty("SettingsUserScope", typeof(IComponent))]
    internal class SettingManager : Component, IExtenderProvider
    {
        private static readonly Dictionary<IComponent, string> componentNames = new Dictionary<IComponent, string>();
        private readonly Dictionary<IComponent, string[]> machineProviders = new Dictionary<IComponent, string[]>();
        private readonly Dictionary<IComponent, string[]> userProviders = new Dictionary<IComponent, string[]>();

        internal static string GetName(IComponent c)
        {
            string str;
            Control control = c as Control;
            if (componentNames.TryGetValue(c, out str) && !SharedUtil.IsEmpty(str))
            {
                return str;
            }
            if (((control = c as Control) != null) && !SharedUtil.IsEmpty(control.Name))
            {
                return control.Name;
            }
            if (((c != null) && (c.Site != null)) && !SharedUtil.IsEmpty(c.Site.Name))
            {
                return c.Site.Name;
            }
            if (c != null)
            {
                return c.GetType().FullName.Replace('.', '_').Replace('+', '_');
            }
            return string.Empty;
        }

        [DefaultValue((string) null), Localizable(false), ExtenderProvidedProperty]
        internal string[] GetSettingsMachineScope(IComponent provider)
        {
            string[] strArray;
            if (!this.machineProviders.TryGetValue(provider, out strArray))
            {
                return null;
            }
            return strArray;
        }

        [ExtenderProvidedProperty, Localizable(false), DefaultValue((string) null)]
        internal string GetSettingsName(IComponent provider)
        {
            string str;
            if (componentNames.TryGetValue(provider, out str) && !SharedUtil.IsEmpty(str))
            {
                return str;
            }
            return null;
        }

        [Localizable(false), ExtenderProvidedProperty, DefaultValue((string) null)]
        internal string[] GetSettingsUserScope(IComponent provider)
        {
            string[] strArray;
            if (!this.userProviders.TryGetValue(provider, out strArray))
            {
                return null;
            }
            return strArray;
        }

        internal void LoadSettings(string prefix)
        {
            foreach (Dictionary<IComponent, string[]> dictionary in new Dictionary<IComponent, string[]>[] { this.userProviders, this.machineProviders })
            {
                foreach (KeyValuePair<IComponent, string[]> pair in dictionary)
                {
                    foreach (string str in pair.Value)
                    {
                        PropertyInfo info;
                        object obj2;
                        if (((info = pair.Key.GetType().GetProperty(str)) != null) && ((obj2 = RegistryUtil.GetValue((dictionary == this.userProviders) ? Application.UserAppDataRegistry : Application.CommonAppDataRegistry, prefix + "_" + GetName(pair.Key) + "_" + str, info.GetValue(pair.Key, null))) != null))
                        {
                            try
                            {
                                info.SetValue(pair.Key, obj2, null);
                            }
                            catch
                            {
                            }
                        }
                    }
                }
            }
        }

        internal void SaveSettings(string prefix)
        {
            foreach (Dictionary<IComponent, string[]> dictionary in new Dictionary<IComponent, string[]>[] { this.userProviders, this.machineProviders })
            {
                foreach (KeyValuePair<IComponent, string[]> pair in dictionary)
                {
                    foreach (string str in pair.Value)
                    {
                        try
                        {
                            PropertyInfo property = pair.Key.GetType().GetProperty(str);
                            if (property != null)
                            {
                                RegistryUtil.SetValue((dictionary == this.userProviders) ? Application.UserAppDataRegistry : Application.CommonAppDataRegistry, prefix + "_" + GetName(pair.Key) + "_" + str, property.GetValue(pair.Key, null));
                            }
                        }
                        catch
                        {
                        }
                    }
                }
            }
        }

        [Localizable(false), ExtenderProvidedProperty, DefaultValue((string) null)]
        internal void SetSettingsMachineScope(IComponent provider, string[] settings)
        {
            if (SharedUtil.IsEmpty((ICollection) settings))
            {
                this.machineProviders.Remove(provider);
            }
            else
            {
                this.machineProviders[provider] = settings;
            }
        }

        [Localizable(false), DefaultValue((string) null), ExtenderProvidedProperty]
        internal void SetSettingsName(IComponent provider, string name)
        {
            if (SharedUtil.IsEmpty(name))
            {
                componentNames.Remove(provider);
            }
            else
            {
                componentNames[provider] = name;
            }
        }

        [ExtenderProvidedProperty, Localizable(false), DefaultValue((string) null)]
        internal void SetSettingsUserScope(IComponent provider, string[] settings)
        {
            if (SharedUtil.IsEmpty((ICollection) settings))
            {
                this.userProviders.Remove(provider);
            }
            else
            {
                this.userProviders[provider] = settings;
            }
        }

        bool IExtenderProvider.CanExtend(object extendee)
        {
            return ((extendee != null) && (extendee != this));
        }
    }
}

