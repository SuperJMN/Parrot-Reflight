using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.UI.Popups;
using Reflight.Core;

namespace Zafiro.Uwp.Core
{
    public class UwpSettingsStore : ISettingsStore
    {
        private readonly ApplicationDataContainer settings;
        private readonly string parentTypeName;
        private readonly Type parentType;

        public UwpSettingsStore(string name, Type type, ApplicationDataContainer settings)
        {
            this.settings = settings;
            parentTypeName = name;
            parentType = type;
        }

        public T Get<T>([CallerMemberName] string propertyName = null)
        {
            if (propertyName == null)
            {
                throw new InvalidOperationException();
            }

            var settingKey = GetSettingKey(propertyName);
            if (settings.Values.TryGetValue(settingKey, out var v))
                return (T) v;
            else
                return GetDefaultValue<T>(propertyName);
        }

        public void Set<T>(T value, [CallerMemberName] string propertyName = null)
        {
            var settingKey = GetSettingKey(propertyName);
            if (settings.Values.ContainsKey(settingKey))
            {
                var currentValue = (T)settings.Values[settingKey];
                if (EqualityComparer<T>.Default.Equals(currentValue, value))
                {
                    return;
                }
            }

            settings.Values[settingKey] = value;
        }

        private string GetSettingKey(string propertyName)
        {
            return parentTypeName + "_" + propertyName;
        }

        private T GetDefaultValue<T>(string name)
        {
            var attr = parentType.GetProperty(name).GetCustomAttribute<DefaultSettingValueAttribute>();
            if (attr != null)
            {
                return (T)attr.Value;
            }

            return default(T);
        }
    }

    public class DialogService : IDialogService
    {
        public Task ShowError(string title, string message)
        {
            var dialog = new MessageDialog(message) { Title = title };
            return dialog.ShowAsync().AsTask();
        }

        public Task ShowMessage(string message)
        {
            var dialog = new MessageDialog(message);
            return dialog.ShowAsync().AsTask();
        }
    }
}